using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Face_Recognition___Attendance_System
{
    public partial class EmpReg : Form
    {
        string imgloc;
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        public EmpReg()
        {
            InitializeComponent();
        }

        private void gunaGradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "jpg Files(*.jpg)|*.jpg| PNG Files(*.png)|*.png| All Files(*.*)|*.*";
                if(fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imgloc = fd.FileName;
                    gunaPictureBox1.ImageLocation = imgloc;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            string Fname = gunaTextBox1.Text;
            string Lname = gunaTextBox2.Text;
            string EmpID = gunaTextBox3.Text;
            string Pass = gunaTextBox4.Text;
            string Address = gunaTextBox6.Text;
            string Email = gunaTextBox10.Text;
            string BG = gunaComboBox3.Text;
            string Adhaar = gunaTextBox8.Text;
            string Dept = gunaComboBox1.Text;
            string Gender = gunaComboBox2.Text;
            string Pic = Fname + " " + EmpID + ".jpg";
            string Phone = gunaTextBox11.Text;
            string EPhone = gunaTextBox9.Text;
            
            if(Fname == "" || Lname == "" || EmpID == "" || Address == "" || gunaTextBox5.Text == "" || Email == "" || BG == "" || Adhaar == "" || gunaTextBox7.Text == "" || Dept == "" || Gender == "" || Phone == "" || EPhone == "")
            {
                MessageBox.Show("Enter All Employee Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int Age = Convert.ToInt32(gunaTextBox5.Text);
            int Sal = Convert.ToInt32(gunaTextBox7.Text);
            if (Age < 18 || Age > 75)
            {
                MessageBox.Show("Employee Not Eligible To Work", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(Adhaar.Length != 12)
            {
                MessageBox.Show("Enter Valid Adhaar Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(Phone.Length != 10 || EPhone.Length != 10)
            {
                MessageBox.Show("Enter Valid Phone Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            try
            {
                con.Open();
                string Query = "insert into Employee (Fname, Lname, EmpID, Pass, Address, Age, Email, BG, Adhaar, Sal, Dept, Gender, Pic, Phone, EPhone) values ('" + Fname + "', '" + Lname + "', '" + EmpID + "', '" + Pass + "', '" + Address + "', " + Age + ", '" + Email + "', '" + BG + "', '" + Adhaar + "', " + Sal + ", '" + Dept + "', '" + Gender + "', '" + Pic + "', '" + Phone + "', '" + EPhone + "')";
                MySqlCommand command = new MySqlCommand(Query, con);
                command.ExecuteNonQuery();
                MessageBox.Show("Employee Added Sucessfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                File.Copy(imgloc, Path.Combine(Application.StartupPath + @"\EmpPic\", Pic), false);
                con.Close();
            }
        }

        private void EmpReg_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string searchQuery = "SELECT Dname FROM Department";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                gunaComboBox1.DataSource = table;
                gunaComboBox1.DisplayMember = "Dname";
            }
            catch(Exception)
            {
                MessageBox.Show("Error Occured", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            try
            {
                con.Open();
                string searchQuery = "SELECT * FROM Employee ORDER BY EID DESC LIMIT 1";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataReader dr = command.ExecuteReader();
                while(dr.Read())
                {
                    string temp = dr["EmpID"].ToString();
                    string num = "";
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (Char.IsDigit(temp[i]))
                            num += temp[i];
                    }
                    int n = Convert.ToInt32(num);
                    n++;
                    gunaTextBox3.Text = "Emp" + n;
                    gunaTextBox4.Text = "Emp" + n;
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
        }
    }
}
