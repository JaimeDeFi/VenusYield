
namespace VenusYield
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.tbBSCAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tblTelegramBotAPI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nBorrowOver = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nBorrowUnder = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bGetID = new System.Windows.Forms.Button();
            this.tbTelegramChatID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nRefreshMins = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lGithub = new System.Windows.Forms.LinkLabel();
            this.lVenusCommunity = new System.Windows.Forms.LinkLabel();
            this.lJ1MtonicTwitter = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nBorrowOver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBorrowUnder)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nRefreshMins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbBSCAddress
            // 
            this.tbBSCAddress.Font = new System.Drawing.Font("Open Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBSCAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.tbBSCAddress.Location = new System.Drawing.Point(171, 21);
            this.tbBSCAddress.Name = "tbBSCAddress";
            this.tbBSCAddress.Size = new System.Drawing.Size(430, 29);
            this.tbBSCAddress.TabIndex = 0;
            this.tbBSCAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "BSC Public Address:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 22);
            this.label5.TabIndex = 3;
            this.label5.Text = "Telegram Bot:";
            // 
            // tblTelegramBotAPI
            // 
            this.tblTelegramBotAPI.Font = new System.Drawing.Font("Open Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblTelegramBotAPI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.tblTelegramBotAPI.Location = new System.Drawing.Point(171, 56);
            this.tblTelegramBotAPI.Name = "tblTelegramBotAPI";
            this.tblTelegramBotAPI.Size = new System.Drawing.Size(430, 29);
            this.tblTelegramBotAPI.TabIndex = 2;
            this.tblTelegramBotAPI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 22);
            this.label2.TabIndex = 5;
            this.label2.Text = "Under";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nBorrowOver);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nBorrowUnder);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 91);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Borrow Reporting";
            // 
            // nBorrowOver
            // 
            this.nBorrowOver.Font = new System.Drawing.Font("Open Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nBorrowOver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.nBorrowOver.Location = new System.Drawing.Point(113, 53);
            this.nBorrowOver.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nBorrowOver.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nBorrowOver.Name = "nBorrowOver";
            this.nBorrowOver.Size = new System.Drawing.Size(54, 29);
            this.nBorrowOver.TabIndex = 11;
            this.nBorrowOver.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label3.Location = new System.Drawing.Point(116, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 22);
            this.label3.TabIndex = 8;
            this.label3.Text = "Over";
            // 
            // nBorrowUnder
            // 
            this.nBorrowUnder.Font = new System.Drawing.Font("Open Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nBorrowUnder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.nBorrowUnder.Location = new System.Drawing.Point(8, 53);
            this.nBorrowUnder.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nBorrowUnder.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nBorrowUnder.Name = "nBorrowUnder";
            this.nBorrowUnder.Size = new System.Drawing.Size(54, 29);
            this.nBorrowUnder.TabIndex = 12;
            this.nBorrowUnder.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label4.Location = new System.Drawing.Point(76, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 22);
            this.label4.TabIndex = 8;
            this.label4.Text = "%";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bGetID);
            this.groupBox2.Controls.Add(this.tbTelegramChatID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tblTelegramBotAPI);
            this.groupBox2.Controls.Add(this.tbBSCAddress);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(617, 133);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // bGetID
            // 
            this.bGetID.Location = new System.Drawing.Point(297, 91);
            this.bGetID.Name = "bGetID";
            this.bGetID.Size = new System.Drawing.Size(87, 29);
            this.bGetID.TabIndex = 6;
            this.bGetID.Text = "Get ID";
            this.bGetID.UseVisualStyleBackColor = true;
            this.bGetID.Click += new System.EventHandler(this.BGetID_Click);
            // 
            // tbTelegramChatID
            // 
            this.tbTelegramChatID.Font = new System.Drawing.Font("Open Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTelegramChatID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.tbTelegramChatID.Location = new System.Drawing.Point(171, 91);
            this.tbTelegramChatID.Name = "tbTelegramChatID";
            this.tbTelegramChatID.Size = new System.Drawing.Size(120, 29);
            this.tbTelegramChatID.TabIndex = 4;
            this.tbTelegramChatID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label8.Location = new System.Drawing.Point(6, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 22);
            this.label8.TabIndex = 5;
            this.label8.Text = "Telegram ChatID:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nRefreshMins);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.groupBox3.Location = new System.Drawing.Point(12, 248);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(175, 91);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Refresh Rate";
            // 
            // nRefreshMins
            // 
            this.nRefreshMins.Font = new System.Drawing.Font("Open Sans", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nRefreshMins.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.nRefreshMins.Location = new System.Drawing.Point(59, 50);
            this.nRefreshMins.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nRefreshMins.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nRefreshMins.Name = "nRefreshMins";
            this.nRefreshMins.Size = new System.Drawing.Size(54, 29);
            this.nRefreshMins.TabIndex = 10;
            this.nRefreshMins.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Open Sans Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label7.Location = new System.Drawing.Point(50, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 22);
            this.label7.TabIndex = 5;
            this.label7.Text = "Minutes";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(193, 151);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 188);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Open Sans", 7F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.label6.Location = new System.Drawing.Point(373, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(268, 133);
            this.label6.TabIndex = 13;
            this.label6.Text = "If this App helps you to sleep better,\r\nand could avoid a tough 10% liquidation,\r" +
    "\nplease, consider a donation.\r\n\r\nThank you so much! :-)\r\n\r\n<-- QRcode for my BEP" +
    "20 address\r\n";
            // 
            // lGithub
            // 
            this.lGithub.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(190)))), ((int)(((byte)(86)))));
            this.lGithub.AutoSize = true;
            this.lGithub.Font = new System.Drawing.Font("Open Sans Light", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lGithub.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lGithub.Location = new System.Drawing.Point(373, 151);
            this.lGithub.Name = "lGithub";
            this.lGithub.Size = new System.Drawing.Size(62, 22);
            this.lGithub.TabIndex = 29;
            this.lGithub.TabStop = true;
            this.lGithub.Text = "GitHub";
            this.lGithub.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lGithub.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LGithub_LinkClicked);
            // 
            // lVenusCommunity
            // 
            this.lVenusCommunity.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(190)))), ((int)(((byte)(86)))));
            this.lVenusCommunity.AutoSize = true;
            this.lVenusCommunity.Font = new System.Drawing.Font("Open Sans Light", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lVenusCommunity.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lVenusCommunity.Location = new System.Drawing.Point(482, 151);
            this.lVenusCommunity.Name = "lVenusCommunity";
            this.lVenusCommunity.Size = new System.Drawing.Size(147, 22);
            this.lVenusCommunity.TabIndex = 30;
            this.lVenusCommunity.TabStop = true;
            this.lVenusCommunity.Text = "Vennus Community";
            this.lVenusCommunity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lVenusCommunity.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lVenusCommunity.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LVenusCommunity_LinkClicked);
            // 
            // lJ1MtonicTwitter
            // 
            this.lJ1MtonicTwitter.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(190)))), ((int)(((byte)(86)))));
            this.lJ1MtonicTwitter.AutoSize = true;
            this.lJ1MtonicTwitter.Font = new System.Drawing.Font("Open Sans Light", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lJ1MtonicTwitter.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lJ1MtonicTwitter.Location = new System.Drawing.Point(522, 317);
            this.lJ1MtonicTwitter.Name = "lJ1MtonicTwitter";
            this.lJ1MtonicTwitter.Size = new System.Drawing.Size(107, 22);
            this.lJ1MtonicTwitter.TabIndex = 31;
            this.lJ1MtonicTwitter.TabStop = true;
            this.lJ1MtonicTwitter.Text = "by @J1Mtonic";
            this.lJ1MtonicTwitter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lJ1MtonicTwitter.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.lJ1MtonicTwitter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LJ1MtonicTwitter_LinkClicked);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 349);
            this.Controls.Add(this.lJ1MtonicTwitter);
            this.Controls.Add(this.lVenusCommunity);
            this.Controls.Add(this.lGithub);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VenusYield Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nBorrowOver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBorrowUnder)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nRefreshMins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBSCAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tblTelegramBotAPI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown nRefreshMins;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nBorrowOver;
        private System.Windows.Forms.NumericUpDown nBorrowUnder;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel lGithub;
        private System.Windows.Forms.LinkLabel lVenusCommunity;
        private System.Windows.Forms.TextBox tbTelegramChatID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel lJ1MtonicTwitter;
        private System.Windows.Forms.Button bGetID;
    }
}