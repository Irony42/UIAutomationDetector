using System;
using System.Windows.Forms;

namespace UIAutomationDetector
{
    public partial class DialogHandle : Form
    {
        public DialogHandle()
        {
            InitializeComponent();
        }

        public IntPtr TargetHandle { get; private set; }

        private void DialogHandle_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.None || DialogResult != DialogResult.OK)
                return;
            
            try
            {
                TargetHandle = new IntPtr(Convert.ToInt32(textBox1.Text, 16));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Input was not valid : \n {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                e.Cancel = true;
            }
        }
    }
}
