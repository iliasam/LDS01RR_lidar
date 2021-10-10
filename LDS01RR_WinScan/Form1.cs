using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static LidarScanningTest1.LdsCommClass;

namespace LidarScanningTest1
{
    public struct RadarPoint
    {
        public double x;
        public double y;
        public double dist;
        public double angleDeg;
        public bool corr;
        public bool Wrong;
        public bool NotVisible;
    }

    public partial class Form1 : Form
    {
        const int UART_BAUD = 115200;
        const int RADAR_ROTATION_DEG = 180;

        struct ScanPoint
        {
            public int RawValue;

            public double RealAngleDeg;

            public double DistM;//meters

            public bool WrongValue;

            public bool DeadZone;
        }


        SerialWorkerClass SerialWorker;
        DataAnalyseClass DataAnalyseObj = new DataAnalyseClass();
        HistogramForm HistogramFormObj;

        LdsCommClass LdsCommObj = new LdsCommClass();

        public IniParser SettingsHolder;//Used for storing settings

        /// <summary>
        /// Rotation period in ms
        /// </summary>
        int RotationPeriod = 0;
        DateTime PrevScanTime = DateTime.Now;

        int RXPacketCnt = 0;

        int CurentPointsCnt = 360;

        ScanPoint[] ScanPoints = new ScanPoint[1000];
        RadarPoint[] RadarPoints = new RadarPoint[1000];

        /// <summary>
        /// Additional rotation, deg
        /// </summary>
        double CurrAngularCorrection = 0.0;//degrees
        int CurrStartAngle = 50;
        int CurrStopAngle = (360 - 50);

        //***************************************************************

        public Form1()
        {
            InitializeComponent();

            cmbPortList.Items.Clear();

            SerialWorker = new SerialWorkerClass(Application.StartupPath + @"\config.ini");
            SerialWorker.AnswerReceived = SerialPortReceivedHandler;
            SerialWorker.SerialFailSignal = SerialFailSignal_function;

            LdsCommObj.PacketReceived += ParseLdsPacket;
            /*
            MavlinkObj.PacketReceived += new PacketReceivedEventHandler(ProcessRxMavlinkPacket);
            */

            string settingsFilePath = Application.StartupPath + @"\config.ini";
            SettingsHolder = new IniParser(settingsFilePath);

            string serialName = SettingsHolder.GetSetting("SERIAL_SETTINGS", "serial");
            if (cmbPortList.Items.Count == 0)
            {
                cmbPortList.Items.Add(serialName);
                cmbPortList.SelectedItem = cmbPortList.Items[0];
            }

            //Load calibration coefficients
            string angCorrStr = SettingsHolder.GetSetting("LIDAR_SETTINGS", "angular_corr");
            CurrAngularCorrection = Convert.ToDouble(angCorrStr, System.Globalization.CultureInfo.InvariantCulture);
            numAngCorrection.Value = (decimal)CurrAngularCorrection;

            string startAngleStr = SettingsHolder.GetSetting("LIDAR_SETTINGS", "start_angle");
            CurrStartAngle = Convert.ToInt32(startAngleStr, System.Globalization.CultureInfo.InvariantCulture);
            numStartAngle.Value = CurrStartAngle;

            string stopAngleStr = SettingsHolder.GetSetting("LIDAR_SETTINGS", "stop_angle");
            CurrStopAngle = Convert.ToInt32(stopAngleStr, System.Globalization.CultureInfo.InvariantCulture);
            numStopAngle.Value = CurrStopAngle;

            radarPlotComponent1.SetRadarRotation(RADAR_ROTATION_DEG);

            timer1.Enabled = true;
        }

        private void SerialFailSignal_function(bool obj)
        {
        }

        // Callback from "Serial Worker"
        private void SerialPortReceivedHandler(byte[] receivedData)
        {
            LdsCommObj.ParseData(receivedData);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CurrAngularCorrection = (double)numAngCorrection.Value;
            CurrStartAngle = (int)numStartAngle.Value;
            CurrStopAngle = (int)numStopAngle.Value;
        }

        void ParseLdsPacket(List<MeasuredPointT> points)
        {
            RXPacketCnt++;
            Invoke((MethodInvoker)delegate ()
            {
                lblPacketCnt.Text = $"RX Packet CNT: {RXPacketCnt}";
            });

            int pointsCnt = points.Count;
            CurentPointsCnt = pointsCnt;
            double angResolution = 360.0 / pointsCnt;

            for (int i = 0; i < pointsCnt; i++)
            {
                ScanPoints[i].RealAngleDeg = 360 - i * angResolution;
                ScanPoints[i].RawValue = 0;

                ScanPoints[i].WrongValue = (bool)(points[i].DistanceMM < 0);

                if ((ScanPoints[i].RealAngleDeg < CurrStartAngle) ||
                    (ScanPoints[i].RealAngleDeg > CurrStopAngle))
                {
                    ScanPoints[i].DistM = 0.0;
                    ScanPoints[i].DeadZone = true;
                }
                else
                {
                    ScanPoints[i].DistM = (double)points[i].DistanceMM / 1000.0;
                    ScanPoints[i].DeadZone = false;
                }

                if (ScanPoints[i].WrongValue)
                    ScanPoints[i].DistM = 3.0;//fixed
            }

            Invoke((MethodInvoker)delegate ()
            {
                CalculateRadarData(pointsCnt);
                radarPlotComponent1.DrawRadar(RadarPoints, pointsCnt, (int)CurrAngularCorrection);

                RotationPeriod = (int)(DateTime.Now - PrevScanTime).TotalMilliseconds;
                PrevScanTime = DateTime.Now;
                double freq = Math.Round(1.0 / (double)(RotationPeriod / 1000.0), 1);//ms -> sec
                lblScanPeriod.Text = $"Scan Period: {RotationPeriod} ms";
                lblScanFreq.Text = $"Scan Freq: {freq:0.0} Hz";
                lblTotalPoints.Text = $"Total Scan Points: {pointsCnt}";

                AnalysePointerData();
                ScanDataAnalyse();
            });
            
        }



        //Simple Data Analyse;
        void ScanDataAnalyse()
        {
            int visiblePointsCnt = 0;
            int badPointsCnt = 0;

            for (int i = 0; i < CurentPointsCnt; i++)
            {
                if (ScanPoints[i].DeadZone == false)
                {
                    visiblePointsCnt++;

                    if (ScanPoints[i].WrongValue)
                        badPointsCnt++;
                }
            }

            double BadPercent = (double)badPointsCnt / visiblePointsCnt * 100;
            lblWrongPointsCnt.Text = $"Wrong Points: {BadPercent:0.0} %";
        }

        // Simple statistic analyse of point at given direction
        void AnalysePointerData()
        {
            int pointer_angle = radarPlotComponent1.GetPointerAngle();//deg

            int pos = CurentPointsCnt * pointer_angle / 360;

            int rawValue = ScanPoints[pos].RawValue;
            double dist = ScanPoints[pos].DistM;

            DataAnalyseObj.AddDataPoint(dist);

            if (HistogramFormObj != null)
                HistogramFormObj.AddNewDisatnceValue(dist);

            lblRawValue.Text = "Raw Value: " + rawValue.ToString();
            lblDistValue.Text = "Distance: " + dist.ToString("0.00") + " m";

            lblAVRValue.Text = "Average: " + DataAnalyseObj.average.ToString("0.00") + " m";

            lblMaxMIn.Text = "MaxMin: " + DataAnalyseObj.min_max.ToString("0.00") + " m";
        }

        void CalculateRadarData(int pointsCnt)
        {
            int i;
            double angle_rad = 0;
            double dist;

            // Radar rotation
            double ang5 = 90 + RADAR_ROTATION_DEG;

            for (i = 0; i < pointsCnt; i++)
            {
                dist = ScanPoints[i].DistM;
                double pointAngleDeg = ScanPoints[i].RealAngleDeg + CurrAngularCorrection + ang5;
                angle_rad = pointAngleDeg / 180.0 * (Math.PI);

                RadarPoints[i].angleDeg = pointAngleDeg;
                RadarPoints[i].dist = dist;
                RadarPoints[i].x = (Math.Cos(angle_rad) * dist);
                RadarPoints[i].y = (Math.Sin(angle_rad) * dist);
                if (RadarPoints[i].dist < 0.03)
                    RadarPoints[i].NotVisible = true;
                else
                    RadarPoints[i].NotVisible = false;

                if ((ScanPoints[i].DeadZone == false) && ScanPoints[i].WrongValue)
                    RadarPoints[i].Wrong = true;
                else
                    RadarPoints[i].Wrong = false;
            }
        }


        private void btnOpenClose_Click(object sender, EventArgs e)
        {
            // If we have opened port - just close it and make necessary things
            if (SerialWorker.ConnectionState == true)
            {
                SerialWorker.ClosePort();

                btnOpenClose.Text = "Open";
                // Exit
                return;
            }

            // Try to open selected COM port
            string port_name = SerialWorker.GetSerialName(cmbPortList.Text);
            int result = SerialWorker.OpenSerialPort(port_name, UART_BAUD);
            if (result == 1)
            {
                btnOpenClose.Text = "Close";
            }
        }

        void UpdatePortList()
        {
            cmbPortList.Items.Clear();
            List<String> portNames = SerialWorker.GetSerialPortInfo();
            foreach (string s in portNames)
            {
                cmbPortList.Items.Add(s);
            }

            if (portNames.Count == 0)
            {
                MessageBox.Show("No serial ports in system!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cmbPortList.SelectedItem = cmbPortList.Items[0];//нашелся хотя бы один порт
            }
        }

        // GUI *********************************************************

        void OpenHistogramForm()
        {
            if (HistogramFormObj == null)
                HistogramFormObj = new HistogramForm();

            if (HistogramFormObj.IsDisposed)
                HistogramFormObj = new HistogramForm();

            HistogramFormObj.Show();
        }

        private void btnSaveCoeff_Click(object sender, EventArgs e)
        {
            string ang_corr_str = CurrAngularCorrection.ToString(System.Globalization.CultureInfo.InvariantCulture);
            SettingsHolder.AddSetting("LIDAR_SETTINGS", "angular_corr", ang_corr_str);

            string start_angle_str = CurrStartAngle.ToString(System.Globalization.CultureInfo.InvariantCulture);
            SettingsHolder.AddSetting("LIDAR_SETTINGS", "start_angle", start_angle_str);

            string stop_angle_str = CurrStopAngle.ToString(System.Globalization.CultureInfo.InvariantCulture);
            SettingsHolder.AddSetting("LIDAR_SETTINGS", "stop_angle", stop_angle_str);

            SettingsHolder.SaveSettings();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DataAnalyseObj.SetLength((int)nudPointsNumber.Value);
        }

        private void cmbPortList_DropDown(object sender, EventArgs e)
        {
            UpdatePortList();
        }

        private void numStartAngle_ValueChanged(object sender, EventArgs e)
        {
            radarPlotComponent1.UpdateStartStopLines(CurrStartAngle, CurrStopAngle);
        }

        private void numStopAngle_ValueChanged(object sender, EventArgs e)
        {
            radarPlotComponent1.UpdateStartStopLines(CurrStartAngle, CurrStopAngle);
        }

        private void btnOpenHistogram_Click(object sender, EventArgs e)
        {
            OpenHistogramForm();
        }
    }
}
