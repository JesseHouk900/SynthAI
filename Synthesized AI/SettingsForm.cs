using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Macs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synthesized_AI
{
    public partial class SettingsForm : Form
    {
        public Form parent;
        public string botToken;
        public string guildToken;
        public string channelToken;
        public string roleID;
        public string modID;
        public string naiUsername;
        public string naiPassword;

        public SettingsForm()
        {
            InitializeComponent();
        }
        public SettingsForm(Form _parent)
        {
            InitializeComponent();
            parent = _parent;
        }

        public void LoadTokensAndIDs()
        {
            string[] lines = GetContentsOfFile(".env");
            foreach (string line in lines)
            {
                if (line.Contains("="))
                {
                    string[] kvp = line.Split('=');
                    DetermineTarget(kvp[0]).Text = kvp[1];
                    SaveEnvValues(kvp);
                }
            }
        }

        public TextBox DetermineTarget(string target)
        {
            switch (target)
            {
                case "DISCORD_BOT":
                    return botToken_textBox;
                    break;
                case "DISCORD_GUILD":
                    return guildToken_textBox;
                    break;
                case "DISCORD_CHANNEL":
                    return channelToken_textBox;
                    break;
                case "ROLE_ID":
                    return roleID_textBox;
                    break;
                case "MOD_ID":
                    return modID_textBox;
                    break;
                case "NAI_PASSWORD":
                    return NAIPassword_textBox;
                    break;
                case "NAI_USERNAME":
                    return NAIUsername_textBox;
                    break;
                default: return null;
            }
        }

        public string[] GetContentsOfFile(string fileName)
        {
            return System.IO.File.ReadAllLines(
               JHPersonalCollection.Environment.GetCurrentDirectory("/", false) + fileName);
        }

        public void SaveEnvValues(string[] kvp)
        {
            switch (kvp[0])
            {
                case "DISCORD_BOT":
                    botToken = kvp[1];
                    break;
                case "DISCORD_GUILD":
                    guildToken = kvp[1];
                    break;
                case "DISCORD_CHANNEL":
                    channelToken = kvp[1];
                    break;
                case "ROLE_ID":
                    roleID = kvp[1];
                    break;
                case "MOD_ID":
                    modID = kvp[1];
                    break;
                case "NAI_PASSWORD":
                    naiPassword = kvp[1];
                    break;
                case "NAI_USERNAME":
                    naiUsername = kvp[1];
                    break;
            }

        }

        public void SaveSettings()
        {
            UpdateEnvVariables();

            string[,] envVariables = {
                { "DISCORD_BOT", botToken },
                { "DISCORD_GUILD", guildToken },
                { "DISCORD_CHANNEL", channelToken },
                { "MOD_ID", modID },
                { "ROLE_ID", roleID },
                { "NAI_USERNAME", naiUsername },
                { "NAI_PASSWORD", naiPassword } };

            string[] lines = GetContentsOfFile(".env");
            string text = "";

            foreach (string line in lines)
            {
                if (!line.Contains("="))
                {
                    text += line + "\r\n";
                }
                else
                {
                    text += envVariables[0, 0] + "=" + envVariables[0, 1] + "\r\n";
                    envVariables = JHPersonalCollection.DataManip.
                        RemoveFirstRowFrom2DArray<string>(envVariables);
                }
            }

            System.IO.File.WriteAllText("../../.env", text);
            
        }

        public void BackToMainMenu()
        {
            this.Hide();
            parent.SetDesktopLocation(this.DesktopLocation.X, this.DesktopLocation.Y);
            parent.Show();
        }

        public void UpdateEnvVariables()
        {
            botToken = botToken_textBox.Text;
            guildToken = guildToken_textBox.Text;
            channelToken = channelToken_textBox.Text;
            roleID = roleID_textBox.Text;
            modID = modID_textBox.Text;
            naiUsername = NAIUsername_textBox.Text;
            naiPassword = NAIPassword_textBox.Text;

        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            parent.Close();
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackToMainMenu();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadTokensAndIDs();
        }

        private void apply_button_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            SaveSettings();
            BackToMainMenu();
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            BackToMainMenu();
        }
    }
}
