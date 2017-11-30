namespace SpecAcquis
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timerCOM = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btAcquisStartStop = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.cmbPortBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtChanNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxScan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxITime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBoxReceive = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerSerialData = new System.Windows.Forms.Timer(this.components);
            this.chkBoxRun = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = 19200;
            this.serialPort.ReadBufferSize = 1048576;
            this.serialPort.ReceivedBytesThreshold = 500000;
            // 
            // chart1
            // 
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.Title = "Channels";
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.Title = "Counts";
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(251, 12);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(593, 335);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // timerCOM
            // 
            this.timerCOM.Interval = 1000;
            this.timerCOM.Tick += new System.EventHandler(this.timerCOM_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btAcquisStartStop
            // 
            this.btAcquisStartStop.Enabled = false;
            this.btAcquisStartStop.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btAcquisStartStop.Location = new System.Drawing.Point(120, 233);
            this.btAcquisStartStop.Name = "btAcquisStartStop";
            this.btAcquisStartStop.Size = new System.Drawing.Size(131, 23);
            this.btAcquisStartStop.TabIndex = 1;
            this.btAcquisStartStop.Text = "START Acquisition";
            this.btAcquisStartStop.UseVisualStyleBackColor = true;
            this.btAcquisStartStop.Click += new System.EventHandler(this.button1_Click);
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(84, 32);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(87, 23);
            this.btConnect.TabIndex = 0;
            this.btConnect.Text = "CONNECT";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // cmbPortBox1
            // 
            this.cmbPortBox1.FormattingEnabled = true;
            this.cmbPortBox1.Location = new System.Drawing.Point(6, 32);
            this.cmbPortBox1.Name = "cmbPortBox1";
            this.cmbPortBox1.Size = new System.Drawing.Size(72, 21);
            this.cmbPortBox1.TabIndex = 19;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::SpecAcquis.Properties.Resources.off_img;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(178, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbPortBox1);
            this.groupBox1.Controls.Add(this.btConnect);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 73);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Port Configuration";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtChanNum);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtBoxScan);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtBoxITime);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(18, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(199, 117);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Detector";
            // 
            // txtChanNum
            // 
            this.txtChanNum.Enabled = false;
            this.txtChanNum.Location = new System.Drawing.Point(134, 91);
            this.txtChanNum.Name = "txtChanNum";
            this.txtChanNum.Size = new System.Drawing.Size(39, 20);
            this.txtChanNum.TabIndex = 38;
            this.txtChanNum.Text = "0";
            this.txtChanNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Channels Number ";
            // 
            // txtBoxScan
            // 
            this.txtBoxScan.Location = new System.Drawing.Point(115, 52);
            this.txtBoxScan.Name = "txtBoxScan";
            this.txtBoxScan.Size = new System.Drawing.Size(58, 20);
            this.txtBoxScan.TabIndex = 36;
            this.txtBoxScan.Text = "1";
            this.txtBoxScan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBoxScan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxScan_KeyPress);
            this.txtBoxScan.Leave += new System.EventHandler(this.txtBoxScan_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Integration Time (us)";
            // 
            // txtBoxITime
            // 
            this.txtBoxITime.Location = new System.Drawing.Point(115, 13);
            this.txtBoxITime.Name = "txtBoxITime";
            this.txtBoxITime.Size = new System.Drawing.Size(58, 20);
            this.txtBoxITime.TabIndex = 34;
            this.txtBoxITime.Text = "100";
            this.txtBoxITime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBoxITime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxITime_KeyPress);
            this.txtBoxITime.Leave += new System.EventHandler(this.txtBoxITime_Leave);
            this.txtBoxITime.Validated += new System.EventHandler(this.txtBoxITime_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Scans to Avarage";
            // 
            // txtBoxReceive
            // 
            this.txtBoxReceive.Location = new System.Drawing.Point(850, 15);
            this.txtBoxReceive.Multiline = true;
            this.txtBoxReceive.Name = "txtBoxReceive";
            this.txtBoxReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxReceive.Size = new System.Drawing.Size(138, 297);
            this.txtBoxReceive.TabIndex = 32;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 350);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1000, 22);
            this.statusStrip1.TabIndex = 34;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(64, 17);
            this.toolStripStatusLabel1.Text = "Port Name";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(60, 17);
            this.toolStripStatusLabel2.Text = "Baud Rate";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel3.Text = "Data Bits";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel4.Text = "Parity";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel5.Text = "Stop Bits";
            // 
            // timerSerialData
            // 
            this.timerSerialData.Interval = 1;
            this.timerSerialData.Tick += new System.EventHandler(this.timerSerial_Tick);
            // 
            // chkBoxRun
            // 
            this.chkBoxRun.AutoSize = true;
            this.chkBoxRun.Location = new System.Drawing.Point(18, 239);
            this.chkBoxRun.Name = "chkBoxRun";
            this.chkBoxRun.Size = new System.Drawing.Size(102, 17);
            this.chkBoxRun.TabIndex = 35;
            this.chkBoxRun.Text = "Continuous Run";
            this.chkBoxRun.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Location = new System.Drawing.Point(18, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 51);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Communication Status";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 21);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(181, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 372);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkBoxRun);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txtBoxReceive);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btAcquisStartStop);
            this.Controls.Add(this.chart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Spectra Acquisition Solution for Sony ILX554 - MEGA";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer timerCOM;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btAcquisStartStop;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.ComboBox cmbPortBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBoxITime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBoxReceive;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.TextBox txtBoxScan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.TextBox txtChanNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timerSerialData;
        private System.Windows.Forms.CheckBox chkBoxRun;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

