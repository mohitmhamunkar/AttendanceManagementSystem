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
using System.IO;

namespace Face_Recognition___Attendance_System
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void test_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
            string searchQuery = "SELECT * FROM Admin";
            MySqlCommand command = new MySqlCommand(searchQuery, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            try
            {
                string Labelsinf = File.ReadAllText(Application.StartupPath + "/EmpFaces/Faces.txt");
                string[] Labels = Labelsinf.Split(',');
                int NumLabels = Convert.ToInt32(Labels[0]);
                int Count = NumLabels;
                Count--;
                string s = Count.ToString() + ",";
                
                for (int i = 1; i < NumLabels + 1; i++)
                {
                    if(Labels[i] != "Emp102")
                    {
                        s = s + Labels[i] + ",";
                    }
                    
                    //trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/EmpFaces/face" + i + ".bmp"));
                    //labels.Add(Labels[i]);
                }
                label1.Text = s;
                File.WriteAllText(Application.StartupPath + "/EmpFaces/Faces.txt", s);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nothing in The Files" + ex);
            }

        }
    }
}
