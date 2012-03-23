namespace PressePostEtikett
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mnuPressepostTest = new System.Windows.Forms.MenuItem();
            this.mnuPremiumAdress = new System.Windows.Forms.MenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuPremiumAdress0 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Exit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.mnuPressepostTest);
            this.menuItem2.MenuItems.Add(this.mnuPremiumAdress);
            this.menuItem2.MenuItems.Add(this.mnuPremiumAdress0);
            this.menuItem2.Text = "test";
            // 
            // mnuPressepostTest
            // 
            this.mnuPressepostTest.Text = "Pressepost";
            this.mnuPressepostTest.Click += new System.EventHandler(this.mnuPressepostTest_Click);
            // 
            // mnuPremiumAdress
            // 
            this.mnuPremiumAdress.Text = "PremiumAdress";
            this.mnuPremiumAdress.Click += new System.EventHandler(this.mnuPremiumAdress_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Location = new System.Drawing.Point(15, 71);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(207, 182);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 43);
            this.label1.Text = "Pressepost Etikett (52x52) Test. Please scan Datamatrix code.";
            // 
            // mnuPremiumAdress0
            // 
            this.mnuPremiumAdress0.Text = "PremiumAdress00";
            this.mnuPremiumAdress0.Click += new System.EventHandler(this.mnuPremiumAdress0_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuItem mnuPressepostTest;
        private System.Windows.Forms.MenuItem mnuPremiumAdress;
        private System.Windows.Forms.MenuItem mnuPremiumAdress0;
    }
}

