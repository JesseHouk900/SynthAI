namespace Synthesized_AI
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menu_strip = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bot_textBox = new System.Windows.Forms.TextBox();
            this.botToken_textBox = new System.Windows.Forms.TextBox();
            this.guildToken_textBox = new System.Windows.Forms.TextBox();
            this.guild_textBox = new System.Windows.Forms.TextBox();
            this.channelToken_textBox = new System.Windows.Forms.TextBox();
            this.channel_textBox = new System.Windows.Forms.TextBox();
            this.roleID_textBox = new System.Windows.Forms.TextBox();
            this.role_textBox = new System.Windows.Forms.TextBox();
            this.modID_textBox = new System.Windows.Forms.TextBox();
            this.mod_textBox = new System.Windows.Forms.TextBox();
            this.NAIUsername_textBox = new System.Windows.Forms.TextBox();
            this.NAIUser_textbox = new System.Windows.Forms.TextBox();
            this.NAIPassword_textBox = new System.Windows.Forms.TextBox();
            this.NAIPass_textBox = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.OK_button = new System.Windows.Forms.Button();
            this.apply_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.menu_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu_strip
            // 
            this.menu_strip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menu_strip.Location = new System.Drawing.Point(0, 0);
            this.menu_strip.Name = "menu_strip";
            this.menu_strip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menu_strip.Size = new System.Drawing.Size(1210, 24);
            this.menu_strip.TabIndex = 20;
            this.menu_strip.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            this.gameToolStripMenuItem.Click += new System.EventHandler(this.gameToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // bot_textBox
            // 
            this.bot_textBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.bot_textBox.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bot_textBox.ForeColor = System.Drawing.SystemColors.Info;
            this.bot_textBox.Location = new System.Drawing.Point(113, 135);
            this.bot_textBox.Name = "bot_textBox";
            this.bot_textBox.ReadOnly = true;
            this.bot_textBox.Size = new System.Drawing.Size(100, 29);
            this.bot_textBox.TabIndex = 21;
            this.bot_textBox.Text = "Discord Bot";
            this.bot_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // botToken_textBox
            // 
            this.botToken_textBox.BackColor = System.Drawing.Color.Lavender;
            this.botToken_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botToken_textBox.Location = new System.Drawing.Point(89, 190);
            this.botToken_textBox.Name = "botToken_textBox";
            this.botToken_textBox.Size = new System.Drawing.Size(146, 26);
            this.botToken_textBox.TabIndex = 22;
            this.botToken_textBox.WordWrap = false;
            // 
            // guildToken_textBox
            // 
            this.guildToken_textBox.BackColor = System.Drawing.Color.Lavender;
            this.guildToken_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guildToken_textBox.Location = new System.Drawing.Point(289, 190);
            this.guildToken_textBox.Name = "guildToken_textBox";
            this.guildToken_textBox.Size = new System.Drawing.Size(146, 26);
            this.guildToken_textBox.TabIndex = 24;
            this.guildToken_textBox.WordWrap = false;
            // 
            // guild_textBox
            // 
            this.guild_textBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.guild_textBox.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.guild_textBox.ForeColor = System.Drawing.SystemColors.Info;
            this.guild_textBox.Location = new System.Drawing.Point(304, 135);
            this.guild_textBox.Name = "guild_textBox";
            this.guild_textBox.ReadOnly = true;
            this.guild_textBox.Size = new System.Drawing.Size(117, 29);
            this.guild_textBox.TabIndex = 23;
            this.guild_textBox.Text = "Discord Guild";
            this.guild_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // channelToken_textBox
            // 
            this.channelToken_textBox.BackColor = System.Drawing.Color.Lavender;
            this.channelToken_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.channelToken_textBox.Location = new System.Drawing.Point(490, 190);
            this.channelToken_textBox.Name = "channelToken_textBox";
            this.channelToken_textBox.Size = new System.Drawing.Size(146, 26);
            this.channelToken_textBox.TabIndex = 26;
            this.channelToken_textBox.WordWrap = false;
            // 
            // channel_textBox
            // 
            this.channel_textBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.channel_textBox.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.channel_textBox.ForeColor = System.Drawing.SystemColors.Info;
            this.channel_textBox.Location = new System.Drawing.Point(490, 135);
            this.channel_textBox.Name = "channel_textBox";
            this.channel_textBox.ReadOnly = true;
            this.channel_textBox.Size = new System.Drawing.Size(146, 29);
            this.channel_textBox.TabIndex = 25;
            this.channel_textBox.Text = "Discord Channel";
            this.channel_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // roleID_textBox
            // 
            this.roleID_textBox.BackColor = System.Drawing.Color.Lavender;
            this.roleID_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roleID_textBox.Location = new System.Drawing.Point(704, 190);
            this.roleID_textBox.Name = "roleID_textBox";
            this.roleID_textBox.Size = new System.Drawing.Size(146, 26);
            this.roleID_textBox.TabIndex = 28;
            this.roleID_textBox.WordWrap = false;
            // 
            // role_textBox
            // 
            this.role_textBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.role_textBox.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.role_textBox.ForeColor = System.Drawing.SystemColors.Info;
            this.role_textBox.Location = new System.Drawing.Point(728, 135);
            this.role_textBox.Name = "role_textBox";
            this.role_textBox.ReadOnly = true;
            this.role_textBox.Size = new System.Drawing.Size(100, 29);
            this.role_textBox.TabIndex = 27;
            this.role_textBox.Text = "Role ID";
            this.role_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // modID_textBox
            // 
            this.modID_textBox.BackColor = System.Drawing.Color.Lavender;
            this.modID_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modID_textBox.Location = new System.Drawing.Point(89, 416);
            this.modID_textBox.Name = "modID_textBox";
            this.modID_textBox.Size = new System.Drawing.Size(146, 26);
            this.modID_textBox.TabIndex = 30;
            this.modID_textBox.WordWrap = false;
            // 
            // mod_textBox
            // 
            this.mod_textBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.mod_textBox.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.mod_textBox.ForeColor = System.Drawing.SystemColors.Info;
            this.mod_textBox.Location = new System.Drawing.Point(129, 361);
            this.mod_textBox.Name = "mod_textBox";
            this.mod_textBox.ReadOnly = true;
            this.mod_textBox.Size = new System.Drawing.Size(62, 29);
            this.mod_textBox.TabIndex = 29;
            this.mod_textBox.Text = "Mod ID";
            this.mod_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NAIUsername_textBox
            // 
            this.NAIUsername_textBox.BackColor = System.Drawing.Color.Lavender;
            this.NAIUsername_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NAIUsername_textBox.Location = new System.Drawing.Point(289, 416);
            this.NAIUsername_textBox.Name = "NAIUsername_textBox";
            this.NAIUsername_textBox.Size = new System.Drawing.Size(146, 26);
            this.NAIUsername_textBox.TabIndex = 32;
            this.NAIUsername_textBox.WordWrap = false;
            // 
            // NAIUser_textbox
            // 
            this.NAIUser_textbox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.NAIUser_textbox.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.NAIUser_textbox.ForeColor = System.Drawing.SystemColors.Info;
            this.NAIUser_textbox.Location = new System.Drawing.Point(304, 361);
            this.NAIUser_textbox.Name = "NAIUser_textbox";
            this.NAIUser_textbox.ReadOnly = true;
            this.NAIUser_textbox.Size = new System.Drawing.Size(117, 29);
            this.NAIUser_textbox.TabIndex = 31;
            this.NAIUser_textbox.Text = "NAI Username";
            this.NAIUser_textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NAIPassword_textBox
            // 
            this.NAIPassword_textBox.BackColor = System.Drawing.Color.Lavender;
            this.NAIPassword_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NAIPassword_textBox.Location = new System.Drawing.Point(490, 416);
            this.NAIPassword_textBox.Name = "NAIPassword_textBox";
            this.NAIPassword_textBox.PasswordChar = '*';
            this.NAIPassword_textBox.Size = new System.Drawing.Size(146, 26);
            this.NAIPassword_textBox.TabIndex = 34;
            this.NAIPassword_textBox.WordWrap = false;
            // 
            // NAIPass_textBox
            // 
            this.NAIPass_textBox.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.NAIPass_textBox.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.NAIPass_textBox.ForeColor = System.Drawing.SystemColors.Info;
            this.NAIPass_textBox.Location = new System.Drawing.Point(504, 361);
            this.NAIPass_textBox.Name = "NAIPass_textBox";
            this.NAIPass_textBox.ReadOnly = true;
            this.NAIPass_textBox.Size = new System.Drawing.Size(118, 29);
            this.NAIPass_textBox.TabIndex = 33;
            this.NAIPass_textBox.Text = "NAI Password";
            this.NAIPass_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox15
            // 
            this.textBox15.BackColor = System.Drawing.Color.Lavender;
            this.textBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox15.Location = new System.Drawing.Point(704, 416);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(146, 26);
            this.textBox15.TabIndex = 36;
            this.textBox15.WordWrap = false;
            // 
            // textBox16
            // 
            this.textBox16.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox16.Enabled = false;
            this.textBox16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox16.ForeColor = System.Drawing.SystemColors.Info;
            this.textBox16.Location = new System.Drawing.Point(728, 361);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(100, 26);
            this.textBox16.TabIndex = 35;
            this.textBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(876, 728);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(75, 23);
            this.OK_button.TabIndex = 37;
            this.OK_button.Text = "OK";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // apply_button
            // 
            this.apply_button.Location = new System.Drawing.Point(989, 728);
            this.apply_button.Name = "apply_button";
            this.apply_button.Size = new System.Drawing.Size(75, 23);
            this.apply_button.TabIndex = 38;
            this.apply_button.Text = "Apply";
            this.apply_button.UseVisualStyleBackColor = true;
            this.apply_button.Click += new System.EventHandler(this.apply_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(1097, 728);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 39;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1210, 795);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.apply_button);
            this.Controls.Add(this.OK_button);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.NAIPassword_textBox);
            this.Controls.Add(this.NAIPass_textBox);
            this.Controls.Add(this.NAIUsername_textBox);
            this.Controls.Add(this.NAIUser_textbox);
            this.Controls.Add(this.modID_textBox);
            this.Controls.Add(this.mod_textBox);
            this.Controls.Add(this.roleID_textBox);
            this.Controls.Add(this.role_textBox);
            this.Controls.Add(this.channelToken_textBox);
            this.Controls.Add(this.channel_textBox);
            this.Controls.Add(this.guildToken_textBox);
            this.Controls.Add(this.guild_textBox);
            this.Controls.Add(this.botToken_textBox);
            this.Controls.Add(this.bot_textBox);
            this.Controls.Add(this.menu_strip);
            this.Name = "SettingsForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.menu_strip.ResumeLayout(false);
            this.menu_strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu_strip;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.TextBox bot_textBox;
        private System.Windows.Forms.TextBox botToken_textBox;
        private System.Windows.Forms.TextBox guildToken_textBox;
        private System.Windows.Forms.TextBox guild_textBox;
        private System.Windows.Forms.TextBox channelToken_textBox;
        private System.Windows.Forms.TextBox channel_textBox;
        private System.Windows.Forms.TextBox roleID_textBox;
        private System.Windows.Forms.TextBox role_textBox;
        private System.Windows.Forms.TextBox modID_textBox;
        private System.Windows.Forms.TextBox mod_textBox;
        private System.Windows.Forms.TextBox NAIUsername_textBox;
        private System.Windows.Forms.TextBox NAIUser_textbox;
        private System.Windows.Forms.TextBox NAIPassword_textBox;
        private System.Windows.Forms.TextBox NAIPass_textBox;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.Button apply_button;
        private System.Windows.Forms.Button cancel_button;
    }
}