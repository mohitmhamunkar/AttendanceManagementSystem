using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Face_Recognition___Attendance_System
{
    public partial class EmpSal : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        string id;
        public EmpSal(string na)
        {
            InitializeComponent();
            id = na;
        }

        private void EmpSal_Load(object sender, EventArgs e)
        {
            int sal = 0;
            try
            {
                con.Open();
                string searchQuery = "SELECT * FROM Employee where EmpID = '" + id + "'";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    sal = Convert.ToInt32(dr["Sal"]);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            int cmon = Monthcnt();
            int cweek = Weekcnt();
            float msal = sal * cmon;
            float wsal = sal * cweek;
            gunaLabel3.Text = msal.ToString();
            gunaLabel10.Text = wsal.ToString();
        }

        private int Monthcnt()
        {
            int cnt = 0;
            string Atmonth = DateTime.Now.ToString("MMMM/yyyy");
            gunaLabel2.Text = Atmonth;
            try
            {
                con.Open();
                string searchQuery = "SELECT * FROM Attendance where EmpID = '" + id + "' And Atmonth = '" + Atmonth + "'";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    cnt++;
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
            return cnt;
        }
        private int Weekcnt()
        {
            int cnt = 0;
            CultureInfo myCI = new CultureInfo("en-IN");
            Calendar myCal = myCI.Calendar;
            int Atweek = myCal.GetWeekOfYear(DateTime.Now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek);
            gunaLabel11.Text = "Week " + Atweek;
            try
            {
                con.Open();
                string searchQuery = "SELECT * FROM Attendance where EmpID = '" + id + "' And Atweek = '" + Atweek + "'";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    cnt++;
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
            return cnt;
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
