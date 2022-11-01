namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.select_table_eoffice = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.select_table_htlt = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.violetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.fontDialog2 = new System.Windows.Forms.FontDialog();
            this.tab = new System.Windows.Forms.TabControl();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.tab3 = new System.Windows.Forms.TabPage();
            this.buttonauto = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.tab.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tab3.SuspendLayout();
            this.SuspendLayout();
            // 
            // select_table_eoffice
            // 
            this.select_table_eoffice.FormattingEnabled = true;
            this.select_table_eoffice.Location = new System.Drawing.Point(9, 21);
            this.select_table_eoffice.Name = "select_table_eoffice";
            this.select_table_eoffice.Size = new System.Drawing.Size(174, 23);
            this.select_table_eoffice.TabIndex = 0;
            this.select_table_eoffice.SelectedIndexChanged += new System.EventHandler(this.selectcolumn_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(189, 21);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(181, 23);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(189, 77);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(181, 23);
            this.comboBox3.TabIndex = 3;
            // 
            // select_table_htlt
            // 
            this.select_table_htlt.FormattingEnabled = true;
            this.select_table_htlt.Location = new System.Drawing.Point(9, 77);
            this.select_table_htlt.Name = "select_table_htlt";
            this.select_table_htlt.Size = new System.Drawing.Size(174, 23);
            this.select_table_htlt.TabIndex = 2;
            this.select_table_htlt.SelectedIndexChanged += new System.EventHandler(this.select_table_htlt_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(110, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.violetToolStripMenuItem,
            this.yellowToolStripMenuItem});
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.colorToolStripMenuItem.Text = "Color";
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.redToolStripMenuItem.Text = "Red";
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.greenToolStripMenuItem.Text = "Green";
            // 
            // violetToolStripMenuItem
            // 
            this.violetToolStripMenuItem.Name = "violetToolStripMenuItem";
            this.violetToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.violetToolStripMenuItem.Text = "Violet";
            // 
            // yellowToolStripMenuItem
            // 
            this.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem";
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.yellowToolStripMenuItem.Text = "Yellow";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tab1);
            this.tab.Controls.Add(this.tab2);
            this.tab.Controls.Add(this.tab3);
            this.tab.Location = new System.Drawing.Point(0, 3);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(522, 297);
            this.tab.TabIndex = 5;
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.select_table_eoffice);
            this.tab1.Controls.Add(this.comboBox2);
            this.tab1.Controls.Add(this.button1);
            this.tab1.Controls.Add(this.select_table_htlt);
            this.tab1.Controls.Add(this.comboBox3);
            this.tab1.Location = new System.Drawing.Point(4, 24);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(514, 269);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "Chọn trường";
            this.tab1.UseVisualStyleBackColor = true;
            // 
            // tab2
            // 
            this.tab2.Location = new System.Drawing.Point(4, 24);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(514, 269);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "Trường đã chọn";
            this.tab2.UseVisualStyleBackColor = true;
            // 
            // tab3
            // 
            this.tab3.Controls.Add(this.buttonauto);
            this.tab3.Location = new System.Drawing.Point(4, 24);
            this.tab3.Name = "tab3";
            this.tab3.Size = new System.Drawing.Size(514, 269);
            this.tab3.TabIndex = 2;
            this.tab3.Text = "AutoComplete";
            this.tab3.UseVisualStyleBackColor = true;
            // 
            // buttonauto
            // 
            this.buttonauto.Location = new System.Drawing.Point(110, 137);
            this.buttonauto.Name = "buttonauto";
            this.buttonauto.Size = new System.Drawing.Size(75, 23);
            this.buttonauto.TabIndex = 0;
            this.buttonauto.Text = "OK";
            this.buttonauto.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 327);
            this.Controls.Add(this.tab);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tab3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox select_table_eoffice;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private ComboBox select_table_htlt;
        private Button button1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem colorToolStripMenuItem;
        private ToolStripMenuItem redToolStripMenuItem;
        private ToolStripMenuItem blueToolStripMenuItem;
        private ToolStripMenuItem greenToolStripMenuItem;
        private ToolStripMenuItem violetToolStripMenuItem;
        private ToolStripMenuItem yellowToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private FontDialog fontDialog1;
        private FontDialog fontDialog2;
        private TabControl tab;
        private TabPage tab1;
        private TabPage tab2;
        private TabPage tab3;
        private Button buttonauto;
    }
}