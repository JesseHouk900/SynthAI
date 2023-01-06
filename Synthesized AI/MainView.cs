using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using JHPersonalCollection;

namespace Synthesized_AI
{
    public partial class MainView : Form
    {
        public Image[] images;
        public string pythonPath;
        public string craiyonBotFileName;
        public string capturedStory;
        public string novelAIBotFileName;
        public string storyResponseText;
        public string storyName;
        public string aiStoryResponseText;
        public string selectedStoryAIAPI;
        public Dictionary<string, string> storyNames;
        public string aiTextColor;
        public string playerTextColor;
        public string promptTextColor;
        public string editTextColor;
        public FileSystemWatcher fileSystemWatcher;
        private Process discordBotProcess;

        public MainView()
        {
            InitializeComponent();
            pythonPath = JHPersonalCollection.Environment.GetPythonPath();
            craiyonBotFileName = "../../synthAIBot.py";
            novelAIBotFileName = "../../synthAIBot.py";
            TestOutputBox.Text = craiyonBotFileName;
            selectedStoryAIAPI = "novelai";
            SetTextColors();
            
            StartDiscordBot();
            
        }
        /// <summary>
        /// Gets the names of the stories from the set story AI API and puts
        /// them in the <c>StoriesListBox</c>. Also assigns those stories and their
        /// keys to the <c>storyNames</c> <c>Dictionary</c>.
        /// </summary>
        public void GetStoryNames()
        {
            string pythonResponse = CallPythonProcess(PrepPythonCommand(
                botFileName: novelAIBotFileName,
                aiFunctionType: "story",
                aiModel: selectedStoryAIAPI,
                action: "getStoryNames"
                ));

            string[] _storyNames =
                JHPersonalCollection.Utils.
                    ProcessPythonListResponce(pythonResponse);
            StoriesListBox.Items.Clear();
            storyNames = new Dictionary<string, string>();

            for (int i = 0; i < _storyNames.Length; i++)
            {
                // Since python response gives story key and story name on
                // different lines, only process every other element
                if (i % 2 == 1)
                {
                    storyNames.Add(_storyNames[i], _storyNames[i - 1]);
                    StoriesListBox.Items.Add(_storyNames[i]);
                }
            }

        }

        /// <summary>
        /// Get the contents of the selected story and populate the 
        /// <c>RichStoryTextBox</c> all pretty and formatted
        /// </summary>
        public void GetStoryContents()
        {
            if (StoriesListBox.SelectedItem != null)
            {

                string pythonResponse = CallPythonProcess(PrepPythonCommand(
                    botFileName: novelAIBotFileName,
                    aiFunctionType: "story",
                    aiModel: selectedStoryAIAPI,
                    action: "getStoryContents",
                    storyID: storyNames[StoriesListBox.SelectedItem.ToString()]
                    ));

                string[] _storyContents =
                    JHPersonalCollection.Utils.
                        ProcessPythonMultiLineListResponce(pythonResponse);

                StoryRichTextBox.Clear();
                string storyTeller = "";
                string prevStoryTeller = "";
                bool setStoryTeller = false;
                string[] respParts;
                Color textColor = Color.White;
                char[] promptDelimiters = { '\n', '\r' };
                for (int i = 0; i < _storyContents.Length; i++)
                {
                    respParts = _storyContents[i].Split('\n');
                    if (respParts.Length == 2)
                    {
                        switch (respParts[1])
                        {
                            case "ai":
                                textColor = MakeColor(aiTextColor);
                                setStoryTeller = true;
                                break;
                            case "user":
                                textColor = MakeColor(playerTextColor);
                                setStoryTeller = true;
                                break;
                            case "edit":
                                textColor = MakeColor(editTextColor);
                                setStoryTeller = true;
                                break;
                            case "prompt":
                                textColor = MakeColor(promptTextColor);
                                setStoryTeller = true;
                                break;
                            default:
                                StoryRichTextBox.SelectionColor = textColor;
                                setStoryTeller = false;
                                break;
                        }
                    }
                    else
                    {
                        StoryRichTextBox.SelectionColor = textColor;
                        setStoryTeller = false;
                    }
                    if (setStoryTeller)
                    {
                        prevStoryTeller = storyTeller;
                        storyTeller = respParts[1];
                        //if (storyTeller == "ai" && prevStoryTeller == "user")
                        //{
                        //    StoryRichTextBox.AppendText("\r\n");
                        //}
                        continue;

                    }
                    if ((storyTeller == "edit" && prevStoryTeller == "user")
                        || (storyTeller == "user" && prevStoryTeller == "edit"))
                    {
                        _storyContents[i] = _storyContents[i].Split(promptDelimiters)[2];
                    }
                    StoryRichTextBox.AppendText(_storyContents[i] /*+ "\r\n\n"*/);

                }
            }
            
        }

        /// <summary>
        /// Take a hex string and turn it into a <c>Color</c>
        /// </summary>
        /// <param name="textColor">a hex <c>string</c></param>
        /// <returns>a <c>Color</c></returns>
        private Color MakeColor(string textColor)
        {
            return Color.FromArgb(1, ColorToInt(textColor, 0),
                                ColorToInt(textColor, 1),
                                ColorToInt(textColor, 2));
        }

        /// <summary>
        /// Take the <c>Text</c> from <c>StoryRichTextBox</c> and assign it to
        /// <c>capturedStory</c>
        /// </summary>
        private void CaptureStoryText()
        {
            capturedStory = StoryRichTextBox.Text;
        }

        /// <summary>
        /// Get story text, send it to AI API, get response, and display it
        /// </summary>
        public void SendStoryText()
        {
            if (StoriesListBox.SelectedItem != null)
            {
                CaptureStoryText();

                storyResponseText = CallPythonProcess(PrepPythonCommand(
                    botFileName: novelAIBotFileName,
                    aiFunctionType: "story",
                    aiModel: selectedStoryAIAPI,
                    action: "sendAndGetStory",
                    storyID: storyNames[StoriesListBox.SelectedItem.ToString()],
                    input: capturedStory
                    ));

                storyResponseText = ProcessResponceText(storyResponseText);
                StoryRichTextBox.SelectionColor = MakeColor(playerTextColor);
                StoryRichTextBox.AppendText(capturedStory);

                StoryRichTextBox.SelectionColor = MakeColor(aiTextColor);
                StoryRichTextBox.AppendText(storyResponseText);
                storyText_richTextBox.Text = "";
            }
        }

        /// <summary>
        /// Format string for python <c>Process</c> command
        /// </summary>
        /// <param name="botFileName">location of bot file</param>
        /// <param name="aiFunctionType"><list type="bullet">
        ///     <item>story</item>
        ///     <item>image</item></list></param>
        /// <param name="aiModel"><list type="bullet">
        ///     <item>novelai</item></list></param>
        /// <param name="action"><list type="bullet">
        ///     <item>sendAndGetStory</item>
        ///     <item>getStory</item>
        ///     <item>getStoryContents</item></list></param>
        /// <param name="storyID">can be left empty</param>
        /// <param name="input">can be left empty</param>
        /// <returns>a <c>string</c> to be used to initiate a python 
        ///     <c>Process</c></returns>
        public string PrepPythonCommand(string botFileName, string aiFunctionType,
            string aiModel, string action = ".", string storyID = ".",
            string input = ".")
        {
            return "\"" + botFileName + "\"" 
                + " " + aiFunctionType
                + " " + aiModel
                + " " + action
                + " " + storyID
                + " " + input;
        }

        /// <summary>
        /// start a <c>synced</c> python <c>Process</c> and collect any output
        /// printed to the console during execution
        /// </summary>
        /// <param name="args">a preprocessed <c>string</c> containing
        ///     file name and any following arguments</param>
        /// <returns>a <c>string</c> containing logged python output</returns>
        public string CallPythonProcess(string args)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = "\"" + pythonPath + "\"",
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            Process process = Process.Start(processInfo);

            string responseText = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            process.Close();

            return responseText;
        }

        /// <summary>
        /// Compose <c>text</c> from python <c>Process</c> output. Necessary as
        /// python response is broken into lines with standard breaks. This
        /// should fix that and make it look as expected.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>a <c>string</c></returns>
        public string ProcessResponceText(string text)
        {
            char[] delimiters = { '\r', '\n' };
            string[] response = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string processedText = 
                text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)[0];
            for (int i = 1; i < response.Length; i++)
            {
                processedText += "\r\n" + response[i];
            }
            return processedText;
        }

        /// <summary>
        /// Set the color variables that will be used when formatting text
        /// </summary>
        public void SetTextColors()
        {
            // color palette courtsey of https://coolors.co/palettes/trending
            SetTextColor(ref aiTextColor, "ffbe0b");
            SetTextColor(ref playerTextColor, "3A86FF");
            SetTextColor(ref promptTextColor, "beefe0");
            SetTextColor(ref editTextColor, "8338ec");

        }

        /// <summary>
        /// Put <paramref name="colorString"/> in the correct format with
        /// given <paramref name="colorValue"/>
        /// </summary>
        /// <param name="colorString">hex string used by reference</param>
        /// <param name="colorValue">hex value string</param>
        public void SetTextColor(ref string colorString, string colorValue)
        {
            colorString = "#" + colorValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color">hex <c>string</c></param>
        /// <param name="colorIndex">color channel index
        ///     <para>can be 0, 1, 2, indicates r, g, b color respectively
        ///     </para></param>
        /// <returns><c>int</c> value of color channel chosen</returns>
        public int ColorToInt(string color, int colorIndex)
        {
            return HexToInt("" + color[2 * colorIndex + 1] + color[2 * (colorIndex + 1)]);
        }

        /// <summary>
        /// Take a hex code and turn it into its <c>int</c> representation
        /// </summary>
        /// <param name="hex">hex value</param>
        /// <returns><c>int</c> of given hex value</returns>
        public int HexToInt(string hex)
        {
            return Int32.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
        
        /// <summary>
        /// Run an async task for the discord bot
        /// </summary>
        public void StartDiscordBot()
        {
            Task.Run(() => { ProcessDiscordBot(); });
        }

        /// <summary>
        /// Start the discord bot and update images when responce is given from
        /// the bot.
        /// </summary>
        public void ProcessDiscordBot()
        {
            try
            {

                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "\"" + pythonPath + "\"",
                    Arguments = "..\\..\\discordBot.py",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                discordBotProcess = Process.Start(processInfo);

                while (!discordBotProcess.HasExited)
                {
                    string responseText;
                    responseText = discordBotProcess.StandardError.ReadLine();
                    if (responseText != null && !responseText.Contains('['))
                    {
                        // after we've done all the processing, 
                        this.Invoke(new Action(() => {
                            // load the control with the appropriate data
                            OnNewImage(responseText);
                        }));
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Update story image based on the name of the file updated or created
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e">Path of image when created or updated</param>
        private void OnNewImage(string fileName)
        {
            // images are generated with a number based on how many can be 
            // generated at a time. limited by acceptable discord latency
            // currently 2. i.e. files will be named imageType0 or 1.jpg
            char[] numDelimiters = 
                { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            string imageType = fileName.Split(numDelimiters)[0];
            switch (imageType)
            {
                case "landscape":
                    this.Invoke(new MethodInvoker(delegate {
                        landscapePictureBox.ImageLocation = fileName;
                    }));
                    break;
                case "character":
                    this.Invoke(new MethodInvoker(delegate {
                        characterPictureBox.ImageLocation = fileName;
                    }));
                    break;
                case "scene":
                    this.Invoke(new MethodInvoker(delegate {
                        scenePictureBox.ImageLocation = fileName;
                    })); 
                    break;
            }

        }

        /// <summary>
        /// On app initialization, get story names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_Load(object sender, EventArgs e)
        {
            
            if (storyName == null)
            {
                // populate story list
                GetStoryNames(); 
            }
        }


        /// <summary>
        /// When a new story is selected from the <c>listBox</c>, change the 
        /// story contents displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StoriesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStoryContents();
        }

        private void StoryInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        /// <summary>
        /// Send the story user typed, receive and display response and user story
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void send_button_Click(object sender, EventArgs e)
        {
            SendStoryText();
        }

        /// <summary>
        /// Kill active processes on MainView_Form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            discordBotProcess.Kill();
        }
    }
}
