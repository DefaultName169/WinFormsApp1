namespace SynceOToHTLT
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tab3 = new System.Windows.Forms.TabPage();
            this.btntab3_Convert = new System.Windows.Forms.Button();
            this.btntab3_Save = new System.Windows.Forms.Button();
            this.Control = new System.Windows.Forms.GroupBox();
            this.panel_right_tab3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbbtab3_eo_table = new System.Windows.Forms.ComboBox();
            this.panel_left_tab3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tab2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbbtab2_htlt_column = new System.Windows.Forms.ComboBox();
            this.cbbtab2_htlt_table = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbbtab2_eo_table = new System.Windows.Forms.ComboBox();
            this.cbbtab2_eo_column = new System.Windows.Forms.ComboBox();
            this.lvtab2 = new System.Windows.Forms.ListView();
            this.btntab2_OK = new System.Windows.Forms.Button();
            this.btntab2_Del = new System.Windows.Forms.Button();
            this.btntab2_Chuyen = new System.Windows.Forms.Button();
            this.tab1 = new System.Windows.Forms.TabPage();
            this.progressBartab1 = new System.Windows.Forms.ProgressBar();
            this.btntab1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.rowchoose = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tab3.SuspendLayout();
            this.Control.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tab2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tab1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tab3
            // 
            this.tab3.Controls.Add(this.btntab3_Convert);
            this.tab3.Controls.Add(this.btntab3_Save);
            this.tab3.Controls.Add(this.Control);
            this.tab3.Location = new System.Drawing.Point(4, 24);
            this.tab3.Margin = new System.Windows.Forms.Padding(0);
            this.tab3.Name = "tab3";
            this.tab3.Size = new System.Drawing.Size(1260, 660);
            this.tab3.TabIndex = 2;
            this.tab3.Text = "Tab3";
            this.tab3.UseVisualStyleBackColor = true;
            // 
            // btntab3_Convert
            // 
            this.btntab3_Convert.Location = new System.Drawing.Point(413, 559);
            this.btntab3_Convert.Name = "btntab3_Convert";
            this.btntab3_Convert.Size = new System.Drawing.Size(96, 45);
            this.btntab3_Convert.TabIndex = 3;
            this.btntab3_Convert.Text = "Chuyển";
            this.btntab3_Convert.UseVisualStyleBackColor = true;
            this.btntab3_Convert.Click += new System.EventHandler(this.btntab3_Convert_Click);
            // 
            // btntab3_Save
            // 
            this.btntab3_Save.Location = new System.Drawing.Point(23, 559);
            this.btntab3_Save.Name = "btntab3_Save";
            this.btntab3_Save.Size = new System.Drawing.Size(96, 45);
            this.btntab3_Save.TabIndex = 1;
            this.btntab3_Save.Text = "Lưu";
            this.btntab3_Save.UseVisualStyleBackColor = true;
            this.btntab3_Save.Click += new System.EventHandler(this.btntab3_Save_Click);
            // 
            // Control
            // 
            this.Control.Controls.Add(this.rowchoose);
            this.Control.Controls.Add(this.panel_right_tab3);
            this.Control.Controls.Add(this.groupBox1);
            this.Control.Controls.Add(this.panel_left_tab3);
            this.Control.Location = new System.Drawing.Point(8, 3);
            this.Control.Name = "Control";
            this.Control.Size = new System.Drawing.Size(1249, 550);
            this.Control.TabIndex = 0;
            this.Control.TabStop = false;
            this.Control.Text = "Control";
            // 
            // panel_right_tab3
            // 
            this.panel_right_tab3.AutoScroll = true;
            this.panel_right_tab3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_right_tab3.Location = new System.Drawing.Point(628, 56);
            this.panel_right_tab3.Name = "panel_right_tab3";
            this.panel_right_tab3.Size = new System.Drawing.Size(615, 480);
            this.panel_right_tab3.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbbtab3_eo_table);
            this.groupBox1.Location = new System.Drawing.Point(9, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 45);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HTLT";
            // 
            // cbbtab3_eo_table
            // 
            this.cbbtab3_eo_table.FormattingEnabled = true;
            this.cbbtab3_eo_table.Location = new System.Drawing.Point(6, 17);
            this.cbbtab3_eo_table.Name = "cbbtab3_eo_table";
            this.cbbtab3_eo_table.Size = new System.Drawing.Size(220, 23);
            this.cbbtab3_eo_table.TabIndex = 0;
            this.cbbtab3_eo_table.SelectedIndexChanged += new System.EventHandler(this.cbbtab3_eo_table_SelectedIndexChanged);
            // 
            // panel_left_tab3
            // 
            this.panel_left_tab3.AutoScroll = true;
            this.panel_left_tab3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_left_tab3.Location = new System.Drawing.Point(9, 56);
            this.panel_left_tab3.Name = "panel_left_tab3";
            this.panel_left_tab3.Size = new System.Drawing.Size(615, 480);
            this.panel_left_tab3.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(76, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 2;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "...",
            "All"});
            this.comboBox1.Location = new System.Drawing.Point(179, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(18, 23);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Số lượng :";
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.groupBox3);
            this.tab2.Controls.Add(this.groupBox2);
            this.tab2.Controls.Add(this.lvtab2);
            this.tab2.Controls.Add(this.btntab2_OK);
            this.tab2.Controls.Add(this.btntab2_Del);
            this.tab2.Controls.Add(this.btntab2_Chuyen);
            this.tab2.Location = new System.Drawing.Point(4, 24);
            this.tab2.Name = "tab2";
            this.tab2.Padding = new System.Windows.Forms.Padding(3);
            this.tab2.Size = new System.Drawing.Size(1260, 660);
            this.tab2.TabIndex = 1;
            this.tab2.Text = "Thiết lập";
            this.tab2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbbtab2_htlt_column);
            this.groupBox3.Controls.Add(this.cbbtab2_htlt_table);
            this.groupBox3.Location = new System.Drawing.Point(8, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(471, 44);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "HTLT";
            // 
            // cbbtab2_htlt_column
            // 
            this.cbbtab2_htlt_column.FormattingEnabled = true;
            this.cbbtab2_htlt_column.Location = new System.Drawing.Point(236, 15);
            this.cbbtab2_htlt_column.Name = "cbbtab2_htlt_column";
            this.cbbtab2_htlt_column.Size = new System.Drawing.Size(220, 23);
            this.cbbtab2_htlt_column.TabIndex = 3;
            // 
            // cbbtab2_htlt_table
            // 
            this.cbbtab2_htlt_table.FormattingEnabled = true;
            this.cbbtab2_htlt_table.Location = new System.Drawing.Point(6, 15);
            this.cbbtab2_htlt_table.Name = "cbbtab2_htlt_table";
            this.cbbtab2_htlt_table.Size = new System.Drawing.Size(220, 23);
            this.cbbtab2_htlt_table.TabIndex = 2;
            this.cbbtab2_htlt_table.SelectedIndexChanged += new System.EventHandler(this.cbbtab2_htlt_table_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbbtab2_eo_table);
            this.groupBox2.Controls.Add(this.cbbtab2_eo_column);
            this.groupBox2.Location = new System.Drawing.Point(6, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(473, 54);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "eOffice";
            // 
            // cbbtab2_eo_table
            // 
            this.cbbtab2_eo_table.FormattingEnabled = true;
            this.cbbtab2_eo_table.Location = new System.Drawing.Point(8, 22);
            this.cbbtab2_eo_table.Name = "cbbtab2_eo_table";
            this.cbbtab2_eo_table.Size = new System.Drawing.Size(220, 23);
            this.cbbtab2_eo_table.TabIndex = 0;
            this.cbbtab2_eo_table.SelectedIndexChanged += new System.EventHandler(this.cbbtab2_eo_table_SelectedIndexChanged);
            // 
            // cbbtab2_eo_column
            // 
            this.cbbtab2_eo_column.FormattingEnabled = true;
            this.cbbtab2_eo_column.Location = new System.Drawing.Point(238, 22);
            this.cbbtab2_eo_column.Name = "cbbtab2_eo_column";
            this.cbbtab2_eo_column.Size = new System.Drawing.Size(220, 23);
            this.cbbtab2_eo_column.TabIndex = 1;
            // 
            // lvtab2
            // 
            this.lvtab2.Location = new System.Drawing.Point(8, 160);
            this.lvtab2.Name = "lvtab2";
            this.lvtab2.Size = new System.Drawing.Size(450, 155);
            this.lvtab2.TabIndex = 9;
            this.lvtab2.UseCompatibleStateImageBehavior = false;
            this.lvtab2.SelectedIndexChanged += new System.EventHandler(this.lvtab2_SelectedIndexChanged);
            // 
            // btntab2_OK
            // 
            this.btntab2_OK.Location = new System.Drawing.Point(528, 192);
            this.btntab2_OK.Name = "btntab2_OK";
            this.btntab2_OK.Size = new System.Drawing.Size(75, 23);
            this.btntab2_OK.TabIndex = 8;
            this.btntab2_OK.Text = "OK";
            this.btntab2_OK.UseVisualStyleBackColor = true;
            this.btntab2_OK.Click += new System.EventHandler(this.btntab2_OK_Click);
            // 
            // btntab2_Del
            // 
            this.btntab2_Del.Enabled = false;
            this.btntab2_Del.Location = new System.Drawing.Point(528, 233);
            this.btntab2_Del.Name = "btntab2_Del";
            this.btntab2_Del.Size = new System.Drawing.Size(75, 23);
            this.btntab2_Del.TabIndex = 6;
            this.btntab2_Del.Text = "Xóa";
            this.btntab2_Del.UseVisualStyleBackColor = true;
            this.btntab2_Del.Click += new System.EventHandler(this.btntab2_Del_Click);
            // 
            // btntab2_Chuyen
            // 
            this.btntab2_Chuyen.Location = new System.Drawing.Point(528, 72);
            this.btntab2_Chuyen.Name = "btntab2_Chuyen";
            this.btntab2_Chuyen.Size = new System.Drawing.Size(75, 23);
            this.btntab2_Chuyen.TabIndex = 5;
            this.btntab2_Chuyen.Text = "Chuyển";
            this.btntab2_Chuyen.UseVisualStyleBackColor = true;
            this.btntab2_Chuyen.Click += new System.EventHandler(this.btntab2_Chuyen_Click);
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.progressBartab1);
            this.tab1.Controls.Add(this.btntab1);
            this.tab1.Location = new System.Drawing.Point(4, 24);
            this.tab1.Name = "tab1";
            this.tab1.Padding = new System.Windows.Forms.Padding(3);
            this.tab1.Size = new System.Drawing.Size(1260, 660);
            this.tab1.TabIndex = 0;
            this.tab1.Text = "Autocomplete";
            this.tab1.UseVisualStyleBackColor = true;
            // 
            // progressBartab1
            // 
            this.progressBartab1.Location = new System.Drawing.Point(8, 157);
            this.progressBartab1.Name = "progressBartab1";
            this.progressBartab1.Size = new System.Drawing.Size(395, 31);
            this.progressBartab1.TabIndex = 1;
            // 
            // btntab1
            // 
            this.btntab1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btntab1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btntab1.Location = new System.Drawing.Point(440, 157);
            this.btntab1.Name = "btntab1";
            this.btntab1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btntab1.Size = new System.Drawing.Size(92, 31);
            this.btntab1.TabIndex = 0;
            this.btntab1.Text = "Chuyển";
            this.btntab1.UseVisualStyleBackColor = false;
            this.btntab1.Click += new System.EventHandler(this.btntab1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab3);
            this.tabControl1.Controls.Add(this.tab1);
            this.tabControl1.Controls.Add(this.tab2);
            this.tabControl1.Location = new System.Drawing.Point(1, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1268, 688);
            this.tabControl1.TabIndex = 2;
            // 
            // rowchoose
            // 
            this.rowchoose.AutoSize = true;
            this.rowchoose.Location = new System.Drawing.Point(628, 31);
            this.rowchoose.Name = "rowchoose";
            this.rowchoose.Size = new System.Drawing.Size(0, 15);
            this.rowchoose.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 697);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tab3.ResumeLayout(false);
            this.Control.ResumeLayout(false);
            this.Control.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tab2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ErrorProvider errorProvider1;
        private TabControl tabControl1;
        private TabPage tab1;
        private ProgressBar progressBartab1;
        private Button btntab1;
        private TabPage tab2;
        private GroupBox groupBox3;
        private ComboBox cbbtab2_htlt_column;
        private ComboBox cbbtab2_htlt_table;
        private GroupBox groupBox2;
        private ComboBox cbbtab2_eo_table;
        private ComboBox cbbtab2_eo_column;
        private ListView lvtab2;
        private Button btntab2_OK;
        private Button btntab2_Del;
        private Button btntab2_Chuyen;
        private TabPage tab3;
        private Button btntab3_Convert;
        private TextBox textBox1;
        private ComboBox comboBox1;
        private Label label1;
        private Button btntab3_Save;
        private GroupBox Control;
        private GroupBox groupBox1;
        private ComboBox cbbtab3_eo_table;
        private Panel panel_left_tab3;
        private Panel panel_right_tab3;
        private Label rowchoose;
    }
}