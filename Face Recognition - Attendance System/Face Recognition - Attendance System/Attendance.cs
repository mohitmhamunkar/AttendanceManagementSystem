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
    public partial class Attendance : Form
    {
        string name;
        int cnt = 0;
        public Attendance(string na)
        {
            InitializeComponent();
            name = na;
            timer1.Start();
        }

        public Attendance(string na, int c)
        {
            InitializeComponent();
            name = na;
            cnt = c;
            timer1.Start();
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            if(cnt != 0)
            {
                this.Hide();
                UserDash ud = new UserDash(name);
                ud.ShowDialog();
                this.Close();
            }
            if(cnt == 0)
            {
                this.Hide();
                AdminDash ad = new AdminDash(name);
                ad.ShowDialog();
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gunaLabel3.Text = DateTime.Now.ToString("dddd") + ", " + DateTime.Now.ToString("hh:mm:tt");
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Attend ad = new Attend();
            ad.TopLevel = false;
            panel2.Controls.Add(ad);
            ad.Show();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            AttendShow ats = new AttendShow();
            ats.TopLevel = false;
            panel2.Controls.Add(ats);
            ats.Show();
        }
    }
}
