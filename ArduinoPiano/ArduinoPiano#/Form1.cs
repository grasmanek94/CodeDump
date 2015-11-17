using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Media;
using WMPLib;

namespace ArduinoPiano
{
    public partial class Form1 : Form
    {
        private SerialPort _serialPort;

        private int _amountOfKeys;

        private TrackBar[] trackBars;
        private WindowsMediaPlayer[] tones;

        private enum States
        {
            PRESSED,
            RELEASED
        };

        public Form1()
        {
            InitializeComponent();

            timer1.Tick += Timer1_Tick;
            timer1.Enabled = true;
            timer1.Interval = 50;
            timer1.Start();

            _amountOfKeys = 7;
            trackBars = new TrackBar[_amountOfKeys];
            tones = new WindowsMediaPlayer[_amountOfKeys];

            var _assembly = System.Reflection.Assembly
                        .GetExecutingAssembly().GetName().CodeBase;

            var _path = System.IO.Path.GetDirectoryName(_assembly);

            for (int i = 0; i < _amountOfKeys; ++i)
            {
                tones[i] = new WindowsMediaPlayer();
                tones[i].URL = _path + "/" + i.ToString() + ".wav";
                tones[i].controls.currentPosition = 0.0;
                tones[i].controls.stop();
            }

            trackBars[0] = trackBar1;
            trackBars[1] = trackBar2;
            trackBars[2] = trackBar3;
            trackBars[3] = trackBar4;
            trackBars[4] = trackBar5;
            trackBars[5] = trackBar6;
            trackBars[6] = trackBar7;

            _serialPort = new SerialPort();

            RefreshComPorts();
        }

        private void OnKeyStateChange(int keyNumber, States newState)
        {
            trackBars[keyNumber].Value = (int)newState;
            if(newState == States.PRESSED)
            {
                tones[keyNumber].controls.currentPosition = 0.0;
                tones[keyNumber].controls.play();
            }
            else
            {
                tones[keyNumber].controls.stop();
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
                    _serialPort.BaudRate = 115200;
                    _serialPort.PortName = comboBox1.Text;
                    _serialPort.Open();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " : " + comboBox1.Text);
                return;
            }
            MessageBox.Show("Cannot connec to: " + comboBox1.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Reconnect();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    while (_serialPort.BytesToRead > 0)
                    {
                        int receivedAction =
                            _serialPort.ReadByte() - 65/*A*/;
                        if(receivedAction < _amountOfKeys)
                        {
                            OnKeyStateChange(receivedAction, States.PRESSED);
                        }
                        else
                        {
                            OnKeyStateChange(receivedAction - _amountOfKeys, States.RELEASED);
                        }
                    }
                }
            }
            catch (Exception)
            {

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
    }
}
