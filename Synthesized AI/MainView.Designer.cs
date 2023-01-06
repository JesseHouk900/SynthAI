namespace Synthesized_AI
{
    partial class MainView
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
            this.landscapePictureBox = new System.Windows.Forms.PictureBox();
            this.TestOutputBox = new System.Windows.Forms.TextBox();
            this.StoryInputTextBox = new System.Windows.Forms.TextBox();
            this.StoriesListBox = new System.Windows.Forms.ListBox();
            this.StoryRichTextBox = new System.Windows.Forms.RichTextBox();
            this.scenePictureBox = new System.Windows.Forms.PictureBox();
            this.characterPictureBox = new System.Windows.Forms.PictureBox();
            this.scene_textBox = new System.Windows.Forms.TextBox();
            this.sceneText_pictureBox = new System.Windows.Forms.PictureBox();
            this.character_textBox = new System.Windows.Forms.TextBox();
            this.characterText_pictureBox = new System.Windows.Forms.PictureBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.landscapeText_pictureBox = new System.Windows.Forms.PictureBox();
            this.send_button = new System.Windows.Forms.Button();
            this.storyText_richTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.landscapePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scenePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sceneText_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterText_pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.landscapeText_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // landscapePictureBox
            // 
            this.landscapePictureBox.Location = new System.Drawing.Point(22, 83);
            this.landscapePictureBox.Name = "landscapePictureBox";
            this.landscapePictureBox.Size = new System.Drawing.Size(256, 256);
            this.landscapePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.landscapePictureBox.TabIndex = 2;
            this.landscapePictureBox.TabStop = false;
            // 
            // TestOutputBox
            // 
            this.TestOutputBox.BackColor = System.Drawing.SystemColors.Control;
            this.TestOutputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TestOutputBox.Enabled = false;
            this.TestOutputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestOutputBox.Location = new System.Drawing.Point(538, 355);
            this.TestOutputBox.Multiline = true;
            this.TestOutputBox.Name = "TestOutputBox";
            this.TestOutputBox.Size = new System.Drawing.Size(366, 287);
            this.TestOutputBox.TabIndex = 4;
            this.TestOutputBox.Visible = false;
            // 
            // StoryInputTextBox
            // 
            this.StoryInputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoryInputTextBox.Location = new System.Drawing.Point(284, 653);
            this.StoryInputTextBox.Name = "StoryInputTextBox";
            this.StoryInputTextBox.Size = new System.Drawing.Size(695, 34);
            this.StoryInputTextBox.TabIndex = 6;
            this.StoryInputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StoryInputTextBox_KeyDown);
            // 
            // StoriesListBox
            // 
            this.StoriesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoriesListBox.FormattingEnabled = true;
            this.StoriesListBox.ItemHeight = 25;
            this.StoriesListBox.Items.AddRange(new object[] {
            "Story 1",
            "Story 2"});
            this.StoriesListBox.Location = new System.Drawing.Point(352, 418);
            this.StoriesListBox.Name = "StoriesListBox";
            this.StoriesListBox.Size = new System.Drawing.Size(190, 254);
            this.StoriesListBox.TabIndex = 7;
            this.StoriesListBox.SelectedIndexChanged += new System.EventHandler(this.StoriesListBox_SelectedIndexChanged);
            // 
            // StoryRichTextBox
            // 
            this.StoryRichTextBox.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.StoryRichTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoryRichTextBox.Location = new System.Drawing.Point(606, 63);
            this.StoryRichTextBox.Name = "StoryRichTextBox";
            this.StoryRichTextBox.Size = new System.Drawing.Size(809, 512);
            this.StoryRichTextBox.TabIndex = 8;
            this.StoryRichTextBox.Text = "";
            // 
            // scenePictureBox
            // 
            this.scenePictureBox.Location = new System.Drawing.Point(320, 83);
            this.scenePictureBox.Name = "scenePictureBox";
            this.scenePictureBox.Size = new System.Drawing.Size(256, 256);
            this.scenePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.scenePictureBox.TabIndex = 9;
            this.scenePictureBox.TabStop = false;
            // 
            // characterPictureBox
            // 
            this.characterPictureBox.Location = new System.Drawing.Point(22, 416);
            this.characterPictureBox.Name = "characterPictureBox";
            this.characterPictureBox.Size = new System.Drawing.Size(256, 256);
            this.characterPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.characterPictureBox.TabIndex = 10;
            this.characterPictureBox.TabStop = false;
            // 
            // scene_textBox
            // 
            this.scene_textBox.BackColor = System.Drawing.Color.SlateBlue;
            this.scene_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scene_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scene_textBox.Location = new System.Drawing.Point(364, 34);
            this.scene_textBox.Name = "scene_textBox";
            this.scene_textBox.Size = new System.Drawing.Size(169, 31);
            this.scene_textBox.TabIndex = 12;
            this.scene_textBox.Text = "Scene";
            this.scene_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sceneText_pictureBox
            // 
            this.sceneText_pictureBox.BackColor = System.Drawing.Color.SlateBlue;
            this.sceneText_pictureBox.Location = new System.Drawing.Point(340, 24);
            this.sceneText_pictureBox.Name = "sceneText_pictureBox";
            this.sceneText_pictureBox.Size = new System.Drawing.Size(216, 50);
            this.sceneText_pictureBox.TabIndex = 11;
            this.sceneText_pictureBox.TabStop = false;
            // 
            // character_textBox
            // 
            this.character_textBox.BackColor = System.Drawing.Color.SlateBlue;
            this.character_textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.character_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.character_textBox.Location = new System.Drawing.Point(64, 370);
            this.character_textBox.Name = "character_textBox";
            this.character_textBox.Size = new System.Drawing.Size(169, 31);
            this.character_textBox.TabIndex = 14;
            this.character_textBox.Text = "Character";
            this.character_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // characterText_pictureBox
            // 
            this.characterText_pictureBox.BackColor = System.Drawing.Color.SlateBlue;
            this.characterText_pictureBox.Location = new System.Drawing.Point(40, 360);
            this.characterText_pictureBox.Name = "characterText_pictureBox";
            this.characterText_pictureBox.Size = new System.Drawing.Size(216, 50);
            this.characterText_pictureBox.TabIndex = 13;
            this.characterText_pictureBox.TabStop = false;
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.SlateBlue;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox.Location = new System.Drawing.Point(66, 37);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(169, 31);
            this.textBox.TabIndex = 16;
            this.textBox.Text = "Landscape";
            this.textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // landscapeText_pictureBox
            // 
            this.landscapeText_pictureBox.BackColor = System.Drawing.Color.SlateBlue;
            this.landscapeText_pictureBox.Location = new System.Drawing.Point(42, 27);
            this.landscapeText_pictureBox.Name = "landscapeText_pictureBox";
            this.landscapeText_pictureBox.Size = new System.Drawing.Size(216, 50);
            this.landscapeText_pictureBox.TabIndex = 15;
            this.landscapeText_pictureBox.TabStop = false;
            // 
            // send_button
            // 
            this.send_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.send_button.Location = new System.Drawing.Point(1320, 594);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(95, 33);
            this.send_button.TabIndex = 17;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // storyText_richTextBox
            // 
            this.storyText_richTextBox.Location = new System.Drawing.Point(606, 581);
            this.storyText_richTextBox.Name = "storyText_richTextBox";
            this.storyText_richTextBox.Size = new System.Drawing.Size(695, 91);
            this.storyText_richTextBox.TabIndex = 18;
            this.storyText_richTextBox.Text = "";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(1459, 699);
            this.Controls.Add(this.storyText_richTextBox);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.landscapeText_pictureBox);
            this.Controls.Add(this.character_textBox);
            this.Controls.Add(this.characterText_pictureBox);
            this.Controls.Add(this.scene_textBox);
            this.Controls.Add(this.sceneText_pictureBox);
            this.Controls.Add(this.characterPictureBox);
            this.Controls.Add(this.scenePictureBox);
            this.Controls.Add(this.StoryRichTextBox);
            this.Controls.Add(this.StoriesListBox);
            this.Controls.Add(this.StoryInputTextBox);
            this.Controls.Add(this.TestOutputBox);
            this.Controls.Add(this.landscapePictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Synthesized AI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainView_FormClosing);
            this.Load += new System.EventHandler(this.MainView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.landscapePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scenePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sceneText_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.characterText_pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.landscapeText_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox landscapePictureBox;
        private System.Windows.Forms.TextBox TestOutputBox;
        private System.Windows.Forms.TextBox StoryInputTextBox;
        private System.Windows.Forms.ListBox StoriesListBox;
        private System.Windows.Forms.RichTextBox StoryRichTextBox;
        private System.Windows.Forms.PictureBox scenePictureBox;
        private System.Windows.Forms.PictureBox characterPictureBox;
        private System.Windows.Forms.TextBox scene_textBox;
        private System.Windows.Forms.PictureBox sceneText_pictureBox;
        private System.Windows.Forms.TextBox character_textBox;
        private System.Windows.Forms.PictureBox characterText_pictureBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.PictureBox landscapeText_pictureBox;
        private System.Windows.Forms.Button send_button;
        private System.Windows.Forms.RichTextBox storyText_richTextBox;
    }
}

