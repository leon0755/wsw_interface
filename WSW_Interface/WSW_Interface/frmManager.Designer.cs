namespace WSW_Interface
{
    partial class frmManager
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_save = new System.Windows.Forms.Button();
            this.cb_stopbits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_databits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_parity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_baudrate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_portname = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口设置";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.cb_stopbits);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cb_databits);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cb_parity);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cb_baudrate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cb_portname);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 94);
            this.panel1.TabIndex = 0;
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_save.Location = new System.Drawing.Point(397, 44);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(88, 41);
            this.btn_save.TabIndex = 10;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // cb_stopbits
            // 
            this.cb_stopbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_stopbits.FormattingEnabled = true;
            this.cb_stopbits.Location = new System.Drawing.Point(236, 55);
            this.cb_stopbits.Name = "cb_stopbits";
            this.cb_stopbits.Size = new System.Drawing.Size(91, 20);
            this.cb_stopbits.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位:";
            // 
            // cb_databits
            // 
            this.cb_databits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_databits.FormattingEnabled = true;
            this.cb_databits.Location = new System.Drawing.Point(62, 55);
            this.cb_databits.Name = "cb_databits";
            this.cb_databits.Size = new System.Drawing.Size(91, 20);
            this.cb_databits.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "数据位:";
            // 
            // cb_parity
            // 
            this.cb_parity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_parity.FormattingEnabled = true;
            this.cb_parity.Location = new System.Drawing.Point(394, 17);
            this.cb_parity.Name = "cb_parity";
            this.cb_parity.Size = new System.Drawing.Size(91, 20);
            this.cb_parity.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(341, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "校验位:";
            // 
            // cb_baudrate
            // 
            this.cb_baudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_baudrate.FormattingEnabled = true;
            this.cb_baudrate.Location = new System.Drawing.Point(236, 17);
            this.cb_baudrate.Name = "cb_baudrate";
            this.cb_baudrate.Size = new System.Drawing.Size(91, 20);
            this.cb_baudrate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率:";
            // 
            // cb_portname
            // 
            this.cb_portname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_portname.FormattingEnabled = true;
            this.cb_portname.Location = new System.Drawing.Point(62, 17);
            this.cb_portname.Name = "cb_portname";
            this.cb_portname.Size = new System.Drawing.Size(91, 20);
            this.cb_portname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 237);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通讯管理工具";
            this.Load += new System.EventHandler(this.frmManager_Load);
            this.Activated += new System.EventHandler(this.frmManager_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmManager_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmManager_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_portname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_parity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_baudrate;
        private System.Windows.Forms.ComboBox cb_databits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_stopbits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

