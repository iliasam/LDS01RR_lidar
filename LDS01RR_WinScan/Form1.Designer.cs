namespace LidarScanningTest1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbPortList = new System.Windows.Forms.ComboBox();
            this.btnOpenClose = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblScanPeriod = new System.Windows.Forms.Label();
            this.lblScanFreq = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numAngCorrection = new System.Windows.Forms.NumericUpDown();
            this.btnSaveCoeff = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpenHistogram = new System.Windows.Forms.Button();
            this.lblMaxMIn = new System.Windows.Forms.Label();
            this.lblAVRValue = new System.Windows.Forms.Label();
            this.nudPointsNumber = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDistValue = new System.Windows.Forms.Label();
            this.lblRawValue = new System.Windows.Forms.Label();
            this.numStartAngle = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numStopAngle = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPacketCnt = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalPoints = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblWrongPointsCnt = new System.Windows.Forms.Label();
            this.radarPlotComponent1 = new LidarScanningTest1.RadarPlotComponent();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAngCorrection)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointsNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStopAngle)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbPortList);
            this.groupBox1.Controls.Add(this.btnOpenClose);
            this.groupBox1.Location = new System.Drawing.Point(7, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(267, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port";
            // 
            // cmbPortList
            // 
            this.cmbPortList.FormattingEnabled = true;
            this.cmbPortList.Location = new System.Drawing.Point(10, 18);
            this.cmbPortList.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPortList.Name = "cmbPortList";
            this.cmbPortList.Size = new System.Drawing.Size(176, 21);
            this.cmbPortList.TabIndex = 2;
            this.cmbPortList.DropDown += new System.EventHandler(this.cmbPortList_DropDown);
            // 
            // btnOpenClose
            // 
            this.btnOpenClose.Location = new System.Drawing.Point(190, 14);
            this.btnOpenClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenClose.Name = "btnOpenClose";
            this.btnOpenClose.Size = new System.Drawing.Size(71, 28);
            this.btnOpenClose.TabIndex = 1;
            this.btnOpenClose.Text = "Open";
            this.btnOpenClose.UseVisualStyleBackColor = true;
            this.btnOpenClose.Click += new System.EventHandler(this.btnOpenClose_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblScanPeriod
            // 
            this.lblScanPeriod.AutoSize = true;
            this.lblScanPeriod.Location = new System.Drawing.Point(278, 10);
            this.lblScanPeriod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScanPeriod.Name = "lblScanPeriod";
            this.lblScanPeriod.Size = new System.Drawing.Size(91, 13);
            this.lblScanPeriod.TabIndex = 2;
            this.lblScanPeriod.Text = "Scan Period: N/A";
            // 
            // lblScanFreq
            // 
            this.lblScanFreq.AutoSize = true;
            this.lblScanFreq.Location = new System.Drawing.Point(277, 34);
            this.lblScanFreq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblScanFreq.Name = "lblScanFreq";
            this.lblScanFreq.Size = new System.Drawing.Size(82, 13);
            this.lblScanFreq.TabIndex = 3;
            this.lblScanFreq.Text = "Scan Freq: N/A";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(546, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Angul. corr.:";
            // 
            // numAngCorrection
            // 
            this.numAngCorrection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numAngCorrection.DecimalPlaces = 1;
            this.numAngCorrection.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numAngCorrection.Location = new System.Drawing.Point(614, 8);
            this.numAngCorrection.Margin = new System.Windows.Forms.Padding(2);
            this.numAngCorrection.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numAngCorrection.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.numAngCorrection.Name = "numAngCorrection";
            this.numAngCorrection.Size = new System.Drawing.Size(51, 20);
            this.numAngCorrection.TabIndex = 10;
            // 
            // btnSaveCoeff
            // 
            this.btnSaveCoeff.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCoeff.Location = new System.Drawing.Point(672, 8);
            this.btnSaveCoeff.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveCoeff.Name = "btnSaveCoeff";
            this.btnSaveCoeff.Size = new System.Drawing.Size(51, 43);
            this.btnSaveCoeff.TabIndex = 11;
            this.btnSaveCoeff.Text = "Save Coeff.";
            this.btnSaveCoeff.UseVisualStyleBackColor = true;
            this.btnSaveCoeff.Click += new System.EventHandler(this.btnSaveCoeff_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnOpenHistogram);
            this.groupBox2.Controls.Add(this.lblMaxMIn);
            this.groupBox2.Controls.Add(this.lblAVRValue);
            this.groupBox2.Controls.Add(this.nudPointsNumber);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblDistValue);
            this.groupBox2.Controls.Add(this.lblRawValue);
            this.groupBox2.Location = new System.Drawing.Point(731, 39);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(154, 187);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Analysis";
            // 
            // btnOpenHistogram
            // 
            this.btnOpenHistogram.Location = new System.Drawing.Point(72, 156);
            this.btnOpenHistogram.Name = "btnOpenHistogram";
            this.btnOpenHistogram.Size = new System.Drawing.Size(75, 23);
            this.btnOpenHistogram.TabIndex = 6;
            this.btnOpenHistogram.Text = "Histogram";
            this.btnOpenHistogram.UseVisualStyleBackColor = true;
            this.btnOpenHistogram.Click += new System.EventHandler(this.btnOpenHistogram_Click);
            // 
            // lblMaxMIn
            // 
            this.lblMaxMIn.AutoSize = true;
            this.lblMaxMIn.Location = new System.Drawing.Point(12, 140);
            this.lblMaxMIn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxMIn.Name = "lblMaxMIn";
            this.lblMaxMIn.Size = new System.Drawing.Size(70, 13);
            this.lblMaxMIn.TabIndex = 5;
            this.lblMaxMIn.Text = "MaxMin: N/A";
            // 
            // lblAVRValue
            // 
            this.lblAVRValue.AutoSize = true;
            this.lblAVRValue.Location = new System.Drawing.Point(12, 119);
            this.lblAVRValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAVRValue.Name = "lblAVRValue";
            this.lblAVRValue.Size = new System.Drawing.Size(73, 13);
            this.lblAVRValue.TabIndex = 4;
            this.lblAVRValue.Text = "Average: N/A";
            // 
            // nudPointsNumber
            // 
            this.nudPointsNumber.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPointsNumber.Location = new System.Drawing.Point(38, 93);
            this.nudPointsNumber.Margin = new System.Windows.Forms.Padding(2);
            this.nudPointsNumber.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudPointsNumber.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPointsNumber.Name = "nudPointsNumber";
            this.nudPointsNumber.Size = new System.Drawing.Size(68, 20);
            this.nudPointsNumber.TabIndex = 3;
            this.nudPointsNumber.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudPointsNumber.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 31);
            this.label4.TabIndex = 2;
            this.label4.Text = "Number of analysed points:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDistValue
            // 
            this.lblDistValue.AutoSize = true;
            this.lblDistValue.Location = new System.Drawing.Point(12, 48);
            this.lblDistValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDistValue.Name = "lblDistValue";
            this.lblDistValue.Size = new System.Drawing.Size(75, 13);
            this.lblDistValue.TabIndex = 1;
            this.lblDistValue.Text = "Distance: N/A";
            // 
            // lblRawValue
            // 
            this.lblRawValue.AutoSize = true;
            this.lblRawValue.Location = new System.Drawing.Point(12, 24);
            this.lblRawValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRawValue.Name = "lblRawValue";
            this.lblRawValue.Size = new System.Drawing.Size(85, 13);
            this.lblRawValue.TabIndex = 0;
            this.lblRawValue.Text = "Raw Value: N/A";
            // 
            // numStartAngle
            // 
            this.numStartAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numStartAngle.Location = new System.Drawing.Point(486, 34);
            this.numStartAngle.Margin = new System.Windows.Forms.Padding(2);
            this.numStartAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numStartAngle.Name = "numStartAngle";
            this.numStartAngle.Size = new System.Drawing.Size(51, 20);
            this.numStartAngle.TabIndex = 14;
            this.numStartAngle.ValueChanged += new System.EventHandler(this.numStartAngle_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Start Angle:";
            // 
            // numStopAngle
            // 
            this.numStopAngle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numStopAngle.Location = new System.Drawing.Point(613, 34);
            this.numStopAngle.Margin = new System.Windows.Forms.Padding(2);
            this.numStopAngle.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.numStopAngle.Name = "numStopAngle";
            this.numStopAngle.Size = new System.Drawing.Size(51, 20);
            this.numStopAngle.TabIndex = 16;
            this.numStopAngle.Value = new decimal(new int[] {
            310,
            0,
            0,
            0});
            this.numStopAngle.ValueChanged += new System.EventHandler(this.numStopAngle_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(550, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Stop Angle:";
            // 
            // lblPacketCnt
            // 
            this.lblPacketCnt.Name = "lblPacketCnt";
            this.lblPacketCnt.Size = new System.Drawing.Size(93, 17);
            this.lblPacketCnt.Text = "Packets count: 0";
            // 
            // lblTotalPoints
            // 
            this.lblTotalPoints.Name = "lblTotalPoints";
            this.lblTotalPoints.Size = new System.Drawing.Size(99, 17);
            this.lblTotalPoints.Text = "Total Scan Points:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPacketCnt,
            this.lblTotalPoints});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(893, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblWrongPointsCnt
            // 
            this.lblWrongPointsCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWrongPointsCnt.AutoSize = true;
            this.lblWrongPointsCnt.Location = new System.Drawing.Point(730, 11);
            this.lblWrongPointsCnt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWrongPointsCnt.Name = "lblWrongPointsCnt";
            this.lblWrongPointsCnt.Size = new System.Drawing.Size(97, 13);
            this.lblWrongPointsCnt.TabIndex = 19;
            this.lblWrongPointsCnt.Text = "Wrong Points: N/A";
            // 
            // radarPlotComponent1
            // 
            this.radarPlotComponent1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radarPlotComponent1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.radarPlotComponent1.Location = new System.Drawing.Point(8, 63);
            this.radarPlotComponent1.Margin = new System.Windows.Forms.Padding(2);
            this.radarPlotComponent1.Name = "radarPlotComponent1";
            this.radarPlotComponent1.Size = new System.Drawing.Size(715, 449);
            this.radarPlotComponent1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 538);
            this.Controls.Add(this.lblWrongPointsCnt);
            this.Controls.Add(this.numStopAngle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numStartAngle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSaveCoeff);
            this.Controls.Add(this.numAngCorrection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radarPlotComponent1);
            this.Controls.Add(this.lblScanFreq);
            this.Controls.Add(this.lblScanPeriod);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "LDS Scanning Test v1.0";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numAngCorrection)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointsNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStartAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStopAngle)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenClose;
        private System.Windows.Forms.ComboBox cmbPortList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblScanPeriod;
        private System.Windows.Forms.Label lblScanFreq;
        private RadarPlotComponent radarPlotComponent1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numAngCorrection;
        private System.Windows.Forms.Button btnSaveCoeff;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblRawValue;
        private System.Windows.Forms.Label lblDistValue;
        private System.Windows.Forms.NumericUpDown nudPointsNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAVRValue;
        private System.Windows.Forms.Label lblMaxMIn;
        private System.Windows.Forms.NumericUpDown numStartAngle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numStopAngle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel lblPacketCnt;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalPoints;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label lblWrongPointsCnt;
        private System.Windows.Forms.Button btnOpenHistogram;
    }
}

