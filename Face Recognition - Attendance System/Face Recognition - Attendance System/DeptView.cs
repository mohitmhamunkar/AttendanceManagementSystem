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
    public partial class DeptView : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        public DeptView()
        {
            InitializeComponent();
        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string searchQuery = "SELECT DID AS 'Department ID', Dname AS 'Department Name', Dloc AS 'Location', DMan AS 'Department Manager', Con AS 'Contact' FROM Department";
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

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }
    }
}
