using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Communication
{
    public partial class TrainDrukteForm : Form
    {
        private SerialPort _serialPort;

        public TrainDrukteForm()
        {
            InitializeComponent();
            timer1.Start();

            _serialPort = new SerialPort();
        }

        public void AddText(string str)
        {
            richTextBox1.Text += "\r\n" + str + "\r\n";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    while(_serialPort.BytesToRead > 0)
                    {
                        AddText(_serialPort.ReadChar().ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                AddText(ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //connect
            if (_serialPort != null && !_serialPort.IsOpen && SerialPort.GetPortNames().Length > 0)
            {
                _serialPort.PortName = SerialPort.GetPortNames()[0];
                _serialPort.Open();
                MessageBox.Show("OK");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //send
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Write(textBox1.Text);
                }
            }
            catch(Exception ex)
            {
                AddText(ex.Message);
            }
        }
    }
}
