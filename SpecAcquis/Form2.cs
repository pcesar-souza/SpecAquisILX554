using System;
using System.Threading;
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
        const int CHANNEL = 2048;                                                           // default channels number
        const char iniChToken = '$';                                                        // Initial transmission token character
        const char finChToken = '#';                                                        // Final transmission token character
        
        byte[] rxBuffer = new byte[1000000];
        UInt32 dIntTime, Scans;
        int IdxRecBuff = 0, buffCounter = 0;
        public int channel_Init = 0, channel_End = 0;
        //double chart_y;
        //UInt32 chart_x;
        double[] plotSignal = new double[4000];
        //Boolean Start_Graph = false, Make_Graph = false;


        public Form1()
        {
            InitializeComponent();
            timerCOM.Start();
            dIntTime = Convert.ToUInt32(txtBoxITime.Text);
            Scans = Convert.ToUInt32(txtBoxScan.Text);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            txtBoxReceive.Clear();
            if(!chkBoxRun.Checked) chart1.Series["Series1"].Points.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = channel_End - 1;
            IdxRecBuff = 0;
            buffCounter = 0;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = channel_End - 1;
            //chart_x = 0;

            // code for communication with uController... 
            if (btAcquisStartStop.Text == "START Acquisition")
            {
                timerSerialData.Start();
               
                progressBar1.Value = 0;
                
                //btAcquisStartStop.ForeColor = SystemColors.InactiveCaption;
                btAcquisStartStop.ForeColor = SystemColors.Highlight;
                btAcquisStartStop.ForeColor = Color.Red;
                btAcquisStartStop.Text = "STOP Acquisition";
                //btAcquisStartStop.Enabled = false;              // false upto receiving all data
                if (serialPort.IsOpen == true)
                {
                    SerialCleaner();

                    serialPort.Write("B");                                      //---- tells uController to receive data control.
                    serialPort.Write("I" + dIntTime.ToString() + "\n");         //---- sends the detector integration time.
                    serialPort.Write("I" + Scans.ToString() + "\n");            //---- sends scan number.

                    //if(chkBoxRun.Checked) serialPort.Write("Y");                // if checkbox is okay then process 
                    //else serialPort.Write("Z");                                 // continuos run.

                    serialPort.Write("P");                                      //---- tells uController to process controll data.
                    //Start_Graph = true;                                         // Graph only if it's true!
                }
            }
            else if (btAcquisStartStop.Text == "STOP Acquisition")
            {
                timerSerialData.Stop();
                //Start_Graph = false;
                //if (chkBoxRun.Checked) chkBoxRun.Checked = false;
                serialPort.Write("S" + "\n"); //stops acquisition
                //serialcleaner();
                btAcquisStartStop.ForeColor = SystemColors.Highlight;
                //btAcquisStartStop.ForeColor = Color.Blue;
                btAcquisStartStop.Text = "START Acquisition";
            }
        }                                                                       //end of button1_Click

        //--------------------------------------------------------------------------------------------------------------
        private void updateCOMs()
        {
            int i=0;
            bool changedPort; // Port change flag

            //i = 0;
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

        }                                                                       //---------------- End of updateCOMs()


        private void SerialCleaner()                                            // Clean everything on serial buffer
        {
            serialPort.ReadExisting();
            
            serialPort.DiscardOutBuffer();                                  
            serialPort.DiscardInBuffer();
                 
            int readCount = serialPort.BytesToRead;
            byte[] buffer = new byte[readCount];
            serialPort.Read(buffer, 0, readCount);
            return;
        }

        //----------------------------------------------------------------------------------------------------------
        private void btConnect_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen == false)
            {
                try
                {
                    serialPort.PortName     = cmbPortBox1.Items[cmbPortBox1.SelectedIndex].ToString();
                    serialPort.BaudRate     = 2000000;
                    serialPort.Parity       = Parity.None;
                    serialPort.StopBits     = StopBits.One;
                    serialPort.DataBits     = 8;
                    serialPort.Handshake    = Handshake.None;
                    serialPort.RtsEnable    = true;
                    serialPort.Open();
                    SerialCleaner();                                                
                    pictureBox1.Image = SpecAcquis.Properties.Resources.on_img;
                }
                catch
                {
                    return;
                }

                if (serialPort.IsOpen)                                              // Verifies if COM port is opened
                {
                    SerialCleaner();                                                // Clean everything on serial buffer

                    btConnect.Text = "DISCONNECT";                                  // change button text to Disconnect
                    cmbPortBox1.Enabled = false;                                    // unable port changing  
                    serialPort.Write("S");
                    SerialCleaner();
                    serialPort.Write("C");                                          //----- send C to connect -----------
                    string rdserial = serialPort.ReadLine();                        // read data Channel Numbers from microcontroller

                    if (!int.TryParse(rdserial.Substring(1, rdserial.Length - 1), out channel_End))
                    {
                        channel_End = CHANNEL;
                    }

                    txtChanNum.Text = channel_End.ToString();                       // and text box...
                    btAcquisStartStop.Enabled = true;
                    toolStripStatusLabel1.Text = "Port Name: " + serialPort.PortName;
                    toolStripStatusLabel2.Text = "Baud: " + serialPort.BaudRate.ToString();    // Updates strip labels with comm status
                    toolStripStatusLabel3.Text = "Bits: " + serialPort.DataBits.ToString();
                    toolStripStatusLabel4.Text = "Parity: " + serialPort.Parity.ToString();
                    toolStripStatusLabel5.Text = "StopBits: " + serialPort.StopBits.ToString();
                }
            }
            else
            {
                try
                {
                    SerialCleaner();                                                // Clean everything on serial buffer
                    serialPort.Write("S");                                          //------ send S to stop -------------
                    serialPort.Write("D");                                          //------ send D to disconnect -------------
                    serialPort.Close(); 

                    cmbPortBox1.Enabled = true;
                    
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

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        //---------------------------------------------------------------------------------------------------------------
        private void makePlot()
        {
            string s = System.Text.Encoding.UTF8.GetString(rxBuffer, 0, rxBuffer.Length);   // copy from serial buffer to string s  
            string[] tmp1Strings = s.Split('|');                                            // create an array delimited by '|'
            string[] tmp2Strings = new string[CHANNEL];                                     // define the final data string array
            int idxPos1 = Array.IndexOf(tmp1Strings, iniChToken.ToString());                           // finds the occurance of initial transmission data token
            int idxPos2 = Array.IndexOf(tmp1Strings, finChToken.ToString());                           // finds the occurance of final transmission data token

            //txtBoxReceive.AppendText("idx1= "+idxPos1.ToString()+" idx2= "+idxPos2.ToString()+"\n");

            if ((idxPos2 - idxPos1 - 1) == CHANNEL)
            {
                Array.Copy(tmp1Strings, idxPos1 + 1, tmp2Strings, 0, CHANNEL);
                chart1.Series["Series1"].Points.Clear();
            }
            else
            {
                Array.Clear(rxBuffer, 0, rxBuffer.Length);
                return;
            }
            
            if(!chkBoxRun.Checked) btAcquisStartStop.ForeColor = SystemColors.Highlight; 

            for (int i = 0; i < channel_End; i++)
            {
                plotSignal[i] = Double.Parse(tmp2Strings[i]);
                chart1.Series["Series1"].Points.AddXY(i, plotSignal[i]);               
            }

            Array.Clear(rxBuffer, 0, rxBuffer.Length);
            
        }                                                                       // End of makePlot      

        static int searchByte(byte[] haystack,byte needle)
        {
            int success = 0;

            for (int i = 0; i < haystack.Length; i++) if (haystack[i] == needle) success++;           

            return success;
        }

        private void chkBoxRun_CheckedChanged(object sender, EventArgs e)
        {
            if(chkBoxRun.Checked) serialPort.Write("Y");                // if checkbox is okay then process 
            else serialPort.Write("Z");                                 // continuos run.
        }

        //----------------------------------------------------------------------------------------------------------------
        private void timerSerial_Tick(object sender, EventArgs e)               // Read data from serial port, poll every
        {                                                                       // 10ms. If data is there, read a block and write it to array

            int bytestoread = 0;
            int succ = 0;

            timerSerialData.Stop();                                             // once in method...no nested calls

            try
            {
                bytestoread = serialPort.BytesToRead;
            }

            catch (InvalidOperationException ex)
            {
                txtBoxReceive.AppendText("Serial connection lost. Exception type:" + ex.ToString());
                if ((uint)ex.HResult == 0x80131509)
                {
                    timerSerialData.Stop();
                }
            }
                    
            if (serialPort.IsOpen)
            {
                if (bytestoread != 0)
                {
                    byte[] temp = new byte[bytestoread];
                    int i = 0;

                    if (serialPort.IsOpen)
                    {
                        serialPort.Read(temp, 0, bytestoread);                      // copy from serial stream to temp                                                                                    
                                                                                    
                        succ = searchByte(temp, 124);                               // search for array byte delimiter 
                                                                                    // 124 ("|")               
                        if (succ>0)
                        {
                            buffCounter += succ;
                            progressBar1.Value = buffCounter-3;
                            progressBar1.PerformStep();                      
                        }

                        Buffer.BlockCopy(      temp,             0,       rxBuffer,     IdxRecBuff,     bytestoread);
                        //     BlockCopy( Array src, int srcOffset, Array      dst, int  dstOffset, int count)                     

                        IdxRecBuff += bytestoread;                                  // updates array index 
                    }                                                                       // end of the second commport if...
                }

                timerSerialData.Start();

                if (serialPort.BytesToRead == 0 && IdxRecBuff != 0)
                {
                    // Converts rxBuffer array of by to string
                    string s = System.Text.Encoding.UTF8.GetString(rxBuffer, 0, rxBuffer.Length);
                    int iniStrToken = s.IndexOf(iniChToken);                                // look up for the initial token character in transmission
                    int finStrToken = s.IndexOf(finChToken);                                // look up for the final token character in transmission
                    
                    if (iniStrToken < finStrToken) /*/s.IndexOf("E") != -1)*/                                   // if there´s "E" in the buffer, makePlot 
                    {
                        buffCounter = 0;
                        IdxRecBuff = 0;
                        if (!chkBoxRun.Checked)
                        {
                            timerSerialData.Stop();         // not continuos run 
                            btAcquisStartStop.ForeColor = SystemColors.Highlight;
                            btAcquisStartStop.Text = "START Acquisition";
                        }

                        makePlot();                        
                    }                    
                }  
            }                                                                   // End of the first commport "if"  is open
        }                                                                       // End of timerSerial_Tick



        private void txtBoxITime_Validated(object sender, EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorProvider1.SetError(txtBoxITime, "");
        }

        private void progBar()
        {
            int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
            progressBar1.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial",
                (float)10.0, FontStyle.Regular), Brushes.Black, new PointF(progressBar1.Width / 2 - 10, progressBar1.Height / 2 - 7));

        }

    }

}
