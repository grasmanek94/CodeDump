using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BTSerial2
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;

        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 50;
            timer1.Start();

            _serialPort = new SerialPort();

            RefreshComPorts();
        }

        private string buffer = "";

        public void AddText(string str)
        {
            buffer += str;
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            buffer = rgx.Replace(buffer, "");
            if (buffer.Length > 32)
            {
                buffer = buffer.Remove(0, buffer.Length - 32);
            }

            richTextBox1.Text += str + "\r\n";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();

            if(buffer.IndexOf("Changing BAUD rate to " + textBox2.Text) != -1)
            {
                buffer = "";
                Reconnect();
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

        void Reconnect()
        {
            //connect
            if (_serialPort != null && _serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Close();
                }
                catch (Exception)
                {

                }
            }

            try
            {
                if (_serialPort != null && !_serialPort.IsOpen && comboBox1.Text.Length > 0)
                {
                    _serialPort.BaudRate = int.Parse(textBox2.Text);
                    _serialPort.PortName = comboBox1.Text;
                    _serialPort.Open();
                    MessageBox.Show("OK: " + comboBox1.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " : " + comboBox1.Text);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Reconnect();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //send
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    _serialPort.Write(textBox1.Text.Replace("\\n", "\n").Replace("\\r", "\r").Replace("\\t", "\t"));
                }
            }
            catch (Exception ex)
            {
                AddText(ex.Message);
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    int index = 0;
                    while (_serialPort.BytesToRead > 0)
                    {
                        byte[] arr = { (byte)_serialPort.ReadByte(), 0 };
                        AddText(System.Text.Encoding.ASCII.GetString(arr));
                        if(++index >= 64)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddText(ex.Message);
            }
        }

        private void RefreshComPorts()
        {
            comboBox1.Items.Clear();
            foreach (string COM_Port in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(COM_Port);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //change
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    string toWrite = ":";
                    for(int i = 0; i < 6-textBox3.Text.Length; ++i)
                    {
                        toWrite += "0";
                    }
                    toWrite += textBox3.Text;
                    _serialPort.Write(toWrite);

                    textBox2.Text = textBox3.Text;
                }
            }
            catch (Exception ex)
            {
                AddText(ex.Message);
            }
        }
    }
}
