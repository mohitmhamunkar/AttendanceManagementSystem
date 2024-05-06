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
    public partial class ED : Form
    {
        public EmployeeReport edrpt = new EmployeeReport();
        public ED()
        {
            InitializeComponent();
        }

        private void ED_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = edrpt;
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
