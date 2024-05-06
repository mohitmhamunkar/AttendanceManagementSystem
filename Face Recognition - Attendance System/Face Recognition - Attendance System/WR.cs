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
    public partial class WR : Form
    {
        public WeeklyReport wrpt = new WeeklyReport();
        public WR()
        {
            InitializeComponent();
        }

        private void WR_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = wrpt;
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
