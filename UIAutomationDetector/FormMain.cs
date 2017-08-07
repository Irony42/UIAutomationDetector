using System;
using System.Windows.Automation;
using System.Windows.Forms;
using Point = System.Windows.Point;

namespace UIAutomationDetector
{
    public partial class FormMain : Form
    {
        private bool _isAutoScanning;
        private readonly Timer _timer;

        public FormMain()
        {
            InitializeComponent();

            _timer = new Timer {Interval = 1000};
            _timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            CaptureMousePosition();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptureMousePosition();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _isAutoScanning = !_isAutoScanning;
            button3.Text = _isAutoScanning ? "Stop Auto Scanning" : "Start Auto Scanning";

            if (_isAutoScanning)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var dialogHandle = new DialogHandle())
            {
                var dialogResult = dialogHandle.ShowDialog(this);

                if (dialogResult == DialogResult.OK)
                {
                    var targetHandle = dialogHandle.TargetHandle;
                    if (targetHandle != default(IntPtr))
                    {
                        CaptureHandle(targetHandle);
                    }
                }
            }
        }

        private void CaptureMousePosition()
        {
            try
            {
                var cursorPosition = Cursor.Position;
                var automationElementAtCursorPosition = AutomationElement.FromPoint(new Point(cursorPosition.X, cursorPosition.Y));
                AppendElementInformation(automationElementAtCursorPosition);
            }
            catch (ElementNotAvailableException)
            {
                textBox1.AppendText("Element was not available at this time.");
            }
            catch (Exception ex)
            {
                textBox1.AppendText(string.Format("An error has occured : {0}", ex.Message));
            }
        }

        private void CaptureHandle(IntPtr targetHandle)
        {
            try
            {
                var automationElementFromHandle = AutomationElement.FromHandle(targetHandle);
                AppendElementInformation(automationElementFromHandle);
            }
            catch (ElementNotAvailableException)
            {
                textBox1.AppendText("Element was not available at this time.");
            }
            catch (Exception ex)
            {
                textBox1.AppendText(string.Format("An error has occured : {0}", ex.Message));
            }
        }

        private void AppendElementInformation(AutomationElement ae)
        {
            var aeTextElement = ae.GetElement();
            var aeTextElementToString = aeTextElement != null ? aeTextElement.ToString() : "No information to log.";
            textBox1.AppendText(aeTextElementToString);
        }
    }
}
