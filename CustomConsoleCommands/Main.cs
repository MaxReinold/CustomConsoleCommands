using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace CustomConsoleCommands
{
    public partial class Main : Form
    {

        static string fileName = "conlog.log";
        static string blacklistFile = "blacklist.cfg";
        static string sourcePath = "D:\\Games\\Steam\\steamapps\\common\\Counter-Strike Global Offensive\\csgo";
        static string targetPath = Directory.GetCurrentDirectory();
        static string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
        static string destFile = System.IO.Path.Combine(targetPath, fileName);
        static string configPath = "D:\\Games\\Steam\\steamapps\\common\\Counter-Strike Global Offensive\\csgo\\cfg";
        static string configName = "advancedConsoleCommands.cfg";
        static string configFile = System.IO.Path.Combine(configPath, configName);
        static string[] lines;
        static string[] cfgBuffer = new string[100];
        string[] blacklist;
        long length;
        long lastlength;

        static string lastLine;

        Stopwatch stopwatch = new Stopwatch();
        long totalTime = 0;
        int framesCounted = 0;
        public Main()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(294, 62);
            consoleBox.Visible = false;
            refreshRateLbl.Visible = false;
            if(!System.IO.File.Exists(System.IO.Path.Combine(targetPath, blacklistFile)))
            {
                File.Create(System.IO.Path.Combine(targetPath, blacklistFile));
            }
            blacklist = File.ReadAllLines(System.IO.Path.Combine(targetPath, blacklistFile));
            blacklistWords.Lines = blacklist;
            stopwatch.Start();
            refresh.Start();
        }

        private void refresh_Tick(object sender, EventArgs e)
        {
            stopwatch.Stop();
            if(totalTime < 1000)
            {
                totalTime += stopwatch.ElapsedMilliseconds;
                framesCounted++;
            } else
            {
                totalTime = stopwatch.ElapsedMilliseconds;
                framesCounted = 1;
            }
            refreshRateLbl.Text = "Refresh Rate: " + (totalTime / framesCounted);
            if((totalTime / framesCounted) < 10)
            {
                resetLogFileBtn.Show();
            } else
            {
                resetLogFileBtn.Hide();
            }
            try
            {
                System.IO.File.Copy(sourceFile, destFile, true);
            } catch
            {
                Console.WriteLine("Error Copying File");
            }
            length = new System.IO.FileInfo(destFile).Length;
            if(length != lastlength) { 
                lines = System.IO.File.ReadAllLines(destFile);
                lastLine = lines[lines.Length - 1];
                ChatObject chat = new ChatObject(lastLine);
                if (chat.isChat)
                {
                    chat.message = parseChatCommands(chat.message);
                    UpdateOutput(chat.FormatString());
                    
                    cfgBuffer[0] = "say " + chat.message + ";";
                    
                    
                    if (ContainBlacklistWord(cfgBuffer[0], blacklist))
                    {
                        cfgBuffer[0] = "say Blacklisted word found. Not printing to chat.";
                    }
                    UpdateConfig(cfgBuffer);
                } else
                {
                    chat.message = parseConsoleCommands(lastLine);
                }
            }
            

            //Keep at end
            lastlength = length;
            stopwatch.Reset();
            stopwatch.Start();
        }

        static string[] GetRangeOfArray(string[] input, int start, int end)
        {
            if(start < 0 || start >= input.Length || end < 0 || end >= input.Length || end <= start)
            {
                return null;
            }
            string[] output = new string[end - start];
            for(int i = start, a = 0; i < end; i++, a++){
                output[a] = input[i];
            }
            return output;
        }

        void UpdateOutput(string input)
        {
            if(input != consoleBox.Text)
            {
                consoleBox.Text = input;
            }
        }

        public String Translate(String word, String fromLanguage, String toLanguage)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };

            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }

        void UpdateConfig(string[] lines)
        {
            try
            {
                System.IO.File.WriteAllLines(configFile, lines);
            } catch {
                Console.WriteLine("Error Writing File.");
            }
        }

        bool ContainBlacklistWord(string message, string[] blacklist)
        {
            message = message.ToUpper();
            for (int i = 0; i < blacklist.Length; i++)
            {
                if (message.IndexOf(blacklist[i].ToUpper()) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (devOptionsEnabled.Checked)
            {
                this.Size = new System.Drawing.Size(930, 519);
                consoleBox.Visible = true;
                refreshRateLbl.Visible = true;
            } else
            {
                this.Size = new System.Drawing.Size(294, 62);
                consoleBox.Visible = false;
                refreshRateLbl.Visible = false;
            }
        }
        string parseChatCommands(string message)
        {
            string[] commands = message.Split(";");
            if (commands[0].IndexOf("translate") != -1)
            {
                try
                {
                    return Translate(commands[3], commands[1], commands[2]);
                } catch
                {
                    return message;
                }
            }
            else
            {
                return message;
            }
        }
        string parseConsoleCommands(string message)
        {
            if(message.IndexOf("save") != -1)
            {
                string[] settings = parseSettingsFromConsole(GetRangeOfArray(lines, lines.Length - 30, lines.Length - 1));
                if(message.IndexOf("viewmodel") != -1)
                {
                    saveConfig(true, settings, message.Split(" ")[2]);
                }
                else if(message.IndexOf("crosshair") != -1)
                {
                    saveConfig(false, settings, message.Split(" ")[2]);
                }
                return "";
            } else
            {
                return message;
            }
            
        }

        private void saveConfig(bool isViewmodel, string[] lines, string name)
        {
            if (isViewmodel)
            {
                System.IO.File.AppendAllText(configPath + "\\Main\\viewmodel.cfg",
                    "\necho " + name + "\n" +
                    "alias " + name + " \"exec Main/viewmodel/fov_" + name + ".cfg\""
                    );
                System.IO.File.WriteAllLines(configPath + "\\Main\\viewmodel\\fov_" + name + ".cfg", lines);
            } 
            else
            {
                System.IO.File.AppendAllText(configPath + "\\Main\\crosshair.cfg",
                    "\necho " + name + "\n" +
                    "alias " + name + " \"exec Main/xhair/xhair_" + name + ".cfg\""
                    );
                System.IO.File.WriteAllLines(configPath + "\\Main\\xhair\\xhair_" + name + ".cfg", lines);
            }
        }

        private void submitBlacklistBtn_Click(object sender, EventArgs e)
        {
            blacklist = blacklistWords.Lines;
            System.IO.File.WriteAllLines(System.IO.Path.Combine(targetPath, blacklistFile), blacklist);
        }

        private void resetLogFileBtn_Click(object sender, EventArgs e)
        {
            if(!doesProcessExist("Counter-Strike: Global Offensive"))
            {
                File.WriteAllLines(sourceFile, new [] { "\n"});
            }
        }

        private bool doesProcessExist(String name)
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    return (process.MainWindowTitle.Equals(name));
                }
            }
            return false;
        }
        
        //Dev Testing for xhair and vmodel settings
        //Converts output for individule settings into their command form
        private string parseSetting(string input)
        {
            string[] temp = input.Split("\"");
            string output;
            if(temp.Length < 5)
            {
                output = "";
            } else
            {
                output = temp[1] + " " + temp[3];
            }
            return output;
        }
        //Converts many settings into an array of their command forms
        private string[] parseSettings(string[] input)
        {
            string[] temp = new string[input.Length];
            for(int i = 0; i < input.Length; i++)
            {
                temp[i] = parseSetting(input[i]);
            }
            return temp;
        }
        //returns command form of config from display command
        private string[] parseSettingsFromConsole(string[] input)
        {
            int first = input.Length - 1;
            while(first > 0)
            {
                if (input[first].IndexOf("~~~~") != -1) break;
                else
                    first--;
            }
            return parseSettings(GetRangeOfArray(input, first, input.Length - 1));
        }
        //Purely for devtesting
    }

    public class ChatObject
    {
        public bool isChat;
        public string message;
        public string playerName;
        public bool isDead;
        public ChatObject(string input)
        {
            if(input.IndexOf(" : ") != -1)
            {
                isChat = true;
                string[] components = input.Split(" : ");
                message = "";
                if (input.IndexOf("*DEAD*") != -1)
                {
                    isDead = true;
                }
                for (int i = 1; i < components.Length; i++)
                {
                    message += components[i];
                }
                if (isDead)
                {
                    playerName = components[0].Substring(7);
                }
                else
                {
                    playerName = components[0];
                }
                if(playerName.Length > 32)
                {
                    isChat = false;
                }
            } else
            {
                isChat = false;
            }
        }
        public string FormatString()
        {
            if (isChat)
            {
                string deadMessage;
                if (isDead)
                {
                    deadMessage = "Dead";
                } else
                {
                    deadMessage = "Alive";
                }
                return "Player Name: " + playerName + "\nMessage: " + message + "\n" + "Status: " + deadMessage +".";
            } else
            {
                return "Not registered as chat.";
            }
        }
        public string[] ToStringArray()
        {
            string[] output = new string[3];
            if (isChat)
            {
                string deadMessage;
                if (isDead)
                {
                    deadMessage = "Dead";
                }
                else
                {
                    deadMessage = "Alive";
                }
                output[0] = "Player Name: " + playerName;
                output[1] = "Message: " + message;
                output[2] = "Status: " + deadMessage + ".";
                return output;
            }
            else
            {
                return null;
            }
        }
    }

}
