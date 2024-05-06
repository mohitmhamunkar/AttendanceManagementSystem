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
    public partial class Dept : Form
    {
        string name;
        public Dept(string na)
        {
            InitializeComponent();
            name = na;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gunaLabel3.Text = DateTime.Now.ToString("dddd") + ", " + DateTime.Now.ToString("hh:mm:tt");
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            DeptReg dr = new DeptReg();
            dr.TopLevel = false;
            panel2.Controls.Add(dr);
            dr.Show();
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDash ad = new AdminDash(name);
            ad.ShowDialog();
            this.Close();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            DeptView dv = new DeptView();
            dv.TopLevel = false;
            panel2.Controls.Add(dv);
            dv.Show();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            DeptUpdate du = new DeptUpdate();
            du.TopLevel = false;
            panel2.Controls.Add(du);
            du.Show();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            DeptDel de = new DeptDel();
            de.TopLevel = false;
            panel2.Controls.Add(de);
            de.Show();
        }
    }
}
