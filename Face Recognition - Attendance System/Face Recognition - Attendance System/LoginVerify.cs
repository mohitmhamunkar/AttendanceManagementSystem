using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Face_Recognition___Attendance_System
{
    public partial class LoginVerify : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        string na;
        public LoginVerify()
        {
            InitializeComponent();
            this.button1.TabStop = false;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int cnt = 0;
            if (textBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please Enter All User Details", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            try
            {
                con.Open();
                string searchQuery = "SELECT * FROM Admin where Admid = '" + textBox1.Text + "' and Adhaar = '" + textBox3.Text + "'";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    cnt++;
                    na = dr["Admid"].ToString();
                }
                if (cnt == 1)
                {
                    MessageBox.Show("User Verified", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    ChangePass cp = new ChangePass(na);
                    cp.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid User or Password", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
