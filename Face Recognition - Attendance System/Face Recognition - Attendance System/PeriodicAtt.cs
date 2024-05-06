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
    public partial class PeriodicAtt : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        DataSet ds = new DataSet();
        string name, date;
        public PeriodicAtt(string na)
        {
            InitializeComponent();
            name = na;
            gunaDateTimePicker1.Format = DateTimePickerFormat.Custom;
            gunaDateTimePicker1.CustomFormat = "dd-MM-yyyy";
        }

        private void PeriodicAtt_Load(object sender, EventArgs e)
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

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            date = gunaDateTimePicker1.Text;
            int x = DateTime.Compare(gunaDateTimePicker1.Value, DateTime.Now);
            if (x > 0)
            {
                MessageBox.Show("Please Select Valid Date", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string searchQuery;
                if (gunaComboBox1.Text == "All Departments")
                {
                    searchQuery = "SELECT EmpID, Empname, Dept, Atmonth, Attime FROM Attendance where Atdate = '" + date + "'";
                }
                else
                {
                    searchQuery = "SELECT EmpID, Empname, Dept, Atmonth, Attime FROM Attendance where Atdate = '" + date + "' And Dept = '" + gunaComboBox1.Text + "'";
                }
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                ds.Tables.Add(table);
                ds.WriteXmlSchema("PeriodicAttendance.xml");
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void gunaGradientButton3_Click(object sender, EventArgs e)
        {
            PR p = new PR();
            p.prpt.SetDataSource(ds);
            p.prpt.SetParameterValue("Name", name);
            p.prpt.SetParameterValue("Month", date);
            p.Show();
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }
    }
}
