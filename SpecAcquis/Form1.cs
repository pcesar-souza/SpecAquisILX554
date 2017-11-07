using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;     // required to get port access    


namespace SpecAcquis
{
    public partial class Form1 : Form
    {
        string RxString;
        UInt32 dIntTime, Scans;
        public uint channel_Init = 0, channel_End = 0;
        double  chart_y;
        UInt32 chart_x;
        double[] plotSignal = new double[4000];
        Boolean Start_Graph = false, Make_Graph = false;


        public Form1()
        {
            InitializeComponent();
            timerCOM.Enabled = true;
            dIntTime = Convert.ToUInt32(txtBoxITime.Text);
            Scans = Convert.ToUInt32(txtBoxScan.Text);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxReceive.Clear();
            chart1.Series["Series1"].Points.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = channel_End - 1;
            //progressBar1.Minimum = 0;
            //progressBar1.Maximum = 2047;
            chart_x = 0;

            // code for communication with uController... 
            if (btAcquisStartStop.Text == "START Acquisition")
            {
                progressBar1.Value = 0;
                timerProgress.Start();
                btAcquisStartStop.ForeColor = Color.Red;
                btAcquisStartStop.Text = "STOP Acquisition";
                if (serialPort.IsOpen == true)
                {
                    serialPort.DiscardOutBuffer();
                    serialPort.DiscardInBuffer();
                    serialPort.Write("B");                                      //---- tells uController to receive data control.
                    serialPort.Write("I" + dIntTime.ToString() + "\n");         //---- sends the detector integration time.
                    serialPort.Write("I" + Scans.ToString() + "\n");            //---- sends scan number.
                    serialPort.Write("P");                                      //---- tells uController to process controll data.
                    Start_Graph = true;                                         // Graph only if it's true!
                }
            }
            else if (btAcquisStartStop.Text == "STOP Acquisition")
            {
                Start_Graph = false;
                timerProgress.Stop();
                serialPort.Write("S" + "\n"); //stops acquisition
                btAcquisStartStop.ForeColor = Color.Blue;
                btAcquisStartStop.Text = "START Acquisition";
            }
        }


        private void updateCOMs()
        {
            int i;
            bool changedPort; // Port change flag

            i = 0;
            changedPort = false;
            
            if (cmbPortBox1.Items.Count == SerialPort.GetPortNames().Length)    //if port number has changed...
            {
                foreach (string serialname in SerialPort.GetPortNames())        //iterate through the collection to get serial port names available.
                {
                    if (cmbPortBox1.Items[i++].Equals(serialname) == false)     //tests if the ports changed...
                    {
                        changedPort = true;
                    }
                }
            }
            else
            {
                changedPort = true;
            }


            if (changedPort == false)                                           // if wasn't detected any change
            {
                return;                                                         // don't do anything!
            }

            cmbPortBox1.Items.Clear();                                          //clean up comboBox

            foreach (string serialname in SerialPort.GetPortNames())            //add all COMs available
            {
                cmbPortBox1.Items.Add(serialname);
            }

            cmbPortBox1.SelectedIndex = 0;                                      // Points to the first in list
        }                                                                       // End of updateCOMs()


        private void timer1_Tick(object sender, EventArgs e)
        {
            updateCOMs();                                                       // updates COMS as tick goes...
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == false)
            {
                try
                {
                    serialPort.PortName = cmbPortBox1.Items[cmbPortBox1.SelectedIndex].ToString();
                    serialPort.Open();
                    pictureBox1.Image = SpecAcquis.Properties.Resources.on_img;
                }
                catch
                {
                    return;
                }

                if (serialPort.IsOpen)                                              // Verifies if COM port is opened
                {
                    btConnect.Text = "DISCONNECT";                                  // change button text to Disconnect
                    cmbPortBox1.Enabled = false;                                    // unable port changing  
                    serialPort.Write("C");                                          //----- send C to connect -----------
                    btAcquisStartStop.Enabled = true;
                    toolStripStatusLabel1.Text = serialPort.PortName;
                    toolStripStatusLabel2.Text = serialPort.BaudRate.ToString();    // Updates strip labels with comm status
                    toolStripStatusLabel3.Text = serialPort.DataBits.ToString();
                    toolStripStatusLabel4.Text = serialPort.Parity.ToString();
                    toolStripStatusLabel5.Text = serialPort.StopBits.ToString();
                }
            }
            else
            {
                try
                {
                    serialPort.Write("D");                                          //------ send D to disconnect -------------
                    //StartStop = false;                                            // ready for another cycle 
                    serialPort.Close();
                    cmbPortBox1.Enabled = true;
                    //btProgramLoad.Enabled = false;
                    // btStartStop.Enabled = false;
                    //btStartStop.Text = "START SHUTTER";
                    //btStartStop.ForeColor = Color.Blue;
                    btConnect.Text = "CONNECT";
                    pictureBox1.Image = SpecAcquis.Properties.Resources.off_img;
                    btAcquisStartStop.Enabled = false;
                    toolStripStatusLabel1.Text = "Port Name";
                    toolStripStatusLabel2.Text = "Baud Rate";
                    toolStripStatusLabel3.Text = "Data Bits";
                    toolStripStatusLabel4.Text = "Parity";
                    toolStripStatusLabel5.Text = "Stop Bits";
                }
                catch
                {
                    return;
                }
            }
        }                                                                           // End of btConnect_Click()

        private void timerCOM_Tick(object sender, EventArgs e)
        {
            updateCOMs();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtChanNum.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtChanNum.Text = text;
                channel_End = System.Convert.ToUInt32(text);                // Updates system channels number
            }

        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string tmpStr;

            RxString = serialPort.ReadLine(); //  To("\n");                     // Reads data available in serial

            if (RxString.Substring(0, 1) == "C")                                // The data received in serialPort method
            {                                                                   // is coming from another thread context than the UI thread, so it must invoke a new thread...             
                tmpStr = RxString.Substring(1, RxString.Length - 1);            //chart1.Series["Series1"].
                SetText(tmpStr);                                                                                                           
            }
            if (Start_Graph && (RxString.Substring(0, 1) != "E"))
            {
                //plotSignal[chart_x] = Convert.ToDouble(RxString);
                this.Invoke(new EventHandler(getsRxData));   // Calls another thread to plot the data...  
                //chart_x++;
            }/* else if (Start_Graph && (RxString.Substring(0, 1) == "E"))
            {
                if (chart_x > channel_End)
                {
                    Make_Graph = true;
                    timerPlot.Start();
                    timerProgress.Stop();
                    serialPort.Write("S" + "\n"); //stops acquisition
                    btAcquisStartStop.ForeColor = Color.Blue;
                    btAcquisStartStop.Text = "START Acquisition";
                    chart_x = 0;
                }
                for (int i=0; i<channel_End; i++)
                {
                    chart1.Series["Series1"].Points.AddXY(i, plotSignal[i]);
                }
            }*/


        }

        private void getsRxData(object sender, EventArgs e)
        {
            if (chart_x < channel_End) progressBar1.Value = (int)(chart_x); //*100/channel_End);
            //label2.Text = serialPort.BytesToRead.ToString();
            //chart_y = Convert.ToDouble(RxString);
            plotSignal[chart_x] = Convert.ToDouble(RxString);
            //chart_x++;
            
            if (chart_x < channel_End)
            {
                txtBoxReceive.AppendText(chart_x.ToString() + ' ' + plotSignal[chart_x].ToString() /*chart_y.ToString()*/ + '\n');
                chart1.Series["Series1"].Points.AddY(plotSignal[chart_x] /*chart_y*/);
                //textBox4.AppendText(Convert.ToString(chart_x) + " " + Convert.ToString(chart_y));
                chart_x++;
            }

            if (chart_x > channel_End)
            {
                //Make_Graph = true;
                //timerPlot.Start();
                timerProgress.Stop();
                serialPort.Write("S" + "\n"); //stops acquisition
                btAcquisStartStop.ForeColor = Color.Blue;
                btAcquisStartStop.Text = "START Acquisition";
            }
        }                                                                       // End of getsRxData()

        private void txtBoxMFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                //assigns True in Handled to cancel event
                e.Handled = true;
            }
        }


        private void txtBoxITime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
            {
                //assigns True in Handled to cancel event
                e.Handled = true;
            }
        }

        private void txtBoxITime_Leave(object sender, EventArgs e)
        {
            try
            {
                dIntTime = Convert.ToUInt32(txtBoxITime.Text);
            }
            catch
            {
                MessageBox.Show("Argument Value Must Equal or Below 4,294,967,295!");
                txtBoxITime.Text = "4294967295";
            }
        }


        private void txtBoxScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08) // 08 = backspace
            {
                //assigns True in Handled to cancel event
                e.Handled = true;
            }
        }

        private void txtBoxScan_Leave(object sender, EventArgs e)
        {
            try
            {
                Scans = Convert.ToUInt32(txtBoxScan.Text);
            }
            catch
            {
                MessageBox.Show("Argument Value Must Equal or Below 4,294,967,295!");
                txtBoxScan.Text = "4294967295";
            }
        }

        private void txtBoxITime_Validating(object sender, CancelEventArgs e)
        {

        }

        private void timerPlot_Tick_1(object sender, EventArgs e)
        {
            if (Make_Graph) {
                for (int i = 0; i < channel_End; i++)
                {
                    chart1.Series["Series1"].Points.AddXY(i, plotSignal[i]);
                }
                Make_Graph = false;
            }
            timerPlot.Stop();
        }

        private void txtBoxITime_Validated(object sender, EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorProvider1.SetError(txtBoxITime, "");
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial",
                (float)10.0, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));

        }

    }

}
