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
    public partial class UserEmp : Form
    {
        string id;
        public UserEmp(string na)
        {
            InitializeComponent();
            id = na;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gunaLabel3.Text = DateTime.Now.ToString("dddd") + ", " + DateTime.Now.ToString("hh:mm:tt");
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EmpReg er = new EmpReg();
            er.TopLevel = false;
            panel2.Controls.Add(er);
            er.Show();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EmpView ev = new EmpView();
            ev.TopLevel = false;
            panel2.Controls.Add(ev);
            ev.Show();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            EmpFace ef = new EmpFace();
            ef.TopLevel = false;
            panel2.Controls.Add(ef);
            ef.Show();
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDash ud = new UserDash(id);
            ud.ShowDialog();
            this.Close();
        }
    }
}
