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
    public partial class AttendShow : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        public AttendShow()
        {
            InitializeComponent();
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {
                string searchQuery = "SELECT EmpID AS 'Employee ID', Empname AS 'Employee Name', Dept AS 'Department', Atdate AS 'Date', Attime AS 'Time', Atmonth AS 'Month', Atweek AS 'Week' FROM Attendance where Atdate = '" + date + "'";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }
    }
}
