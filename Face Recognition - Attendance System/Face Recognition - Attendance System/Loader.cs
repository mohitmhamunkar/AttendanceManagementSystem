using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face_Recognition___Attendance_System
{
    public partial class Loader : Form
    {
        public Loader()
        {
            InitializeComponent();
            gunaProgressBar1.Value = 0;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gunaProgressBar1.Increment(1);
            label1.Text = gunaProgressBar1.ProgressPercentText;
            if(gunaProgressBar1.ProgressPercentText == "100%")
            {
                timer1.Stop();
                this.Hide();
                Login lo = new Login();
                lo.ShowDialog();
                this.Close();
            }
        }
    }
}
