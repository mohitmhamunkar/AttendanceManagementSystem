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
using System.Globalization;

namespace Face_Recognition___Attendance_System
{
    public partial class MonthlyAtt : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        DataSet ds = new DataSet();
        string name, mo;
        public MonthlyAtt(string na)
        {
            InitializeComponent();
            name = na;
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void MonthlyAtt_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string searchQuery = "SELECT Dname FROM Department";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                DataRow row = table.NewRow();
                row[0] = "All Departments";
                table.Rows.InsertAt(row, 0);
                gunaComboBox1.DataSource = table;
                gunaComboBox1.DisplayMember = "Dname";
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Load Departments", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void gunaGradientButton3_Click(object sender, EventArgs e)
        {
            MR m = new MR();
            m.mrpt.SetDataSource(ds);
            m.mrpt.SetParameterValue("Name", name);
            m.mrpt.SetParameterValue("Month", mo);
            m.Show();
        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            if (gunaComboBox2.Text == "")
            {
                MessageBox.Show("Please Select Month", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            int month = DateTime.ParseExact(gunaComboBox2.Text, "MMMM", CultureInfo.CurrentCulture).Month;
            if (month > DateTime.Now.Month)
            {
                MessageBox.Show("Please Select Valid Month", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            mo = gunaComboBox2.Text + "-" + DateTime.Now.ToString("yyyy");
            try
            {
                string searchQuery;
                if (gunaComboBox1.Text == "All Departments")
                {
                    searchQuery = "SELECT EmpID, Empname, Dept, Atdate, Attime FROM Attendance where Atmonth = '" + mo + "'";
                }
                else
                {
                    searchQuery = "SELECT EmpID, Empname, Dept, Atdate, Attime FROM Attendance where Atmonth = '" + mo + "' And Dept = '" + gunaComboBox1.Text + "'";
                }
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                ds.Tables.Add(table);
                ds.WriteXmlSchema("MonthlyAttendance.xml");
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
    }
}
