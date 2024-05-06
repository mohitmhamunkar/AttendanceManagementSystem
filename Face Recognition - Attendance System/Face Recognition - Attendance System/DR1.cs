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
    public partial class DR1 : Form
    {
        public DepartmentReport drrpt = new DepartmentReport();
        public DR1()
        {
            InitializeComponent();
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DR1_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = drrpt;
        }
    }
}
