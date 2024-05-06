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
using System.Windows.Forms.DataVisualization.Charting;

namespace Face_Recognition___Attendance_System
{
    public partial class AdminDash : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        string name;
        public AdminDash(string na)
        {
            InitializeComponent();
            name = na;
            gunaLabel2.Text = name;
            timer1.Start();
            Deptatt();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gunaLabel3.Text = DateTime.Now.ToString("dddd") + ", " + DateTime.Now.ToString("hh:mm:tt");
        }

        private void gunaButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lo = new Login();
            lo.ShowDialog();
            this.Close();
        }
        private void Deptatt()
        {
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;
            chart.AxisX.Title = "Week Of Year";
            chart.AxisX.TitleFont = new Font("Times New Roman", 14, System.Drawing.FontStyle.Bold);
            chart.AxisY.Title = "Percentage (%)";
            chart.AxisY.TitleFont = new Font("Times New Roman", 14, System.Drawing.FontStyle.Bold);
            chart.AxisY.Minimum = 0;
            chart.AxisX.Minimum = 0;
            chart.AxisX.Maximum = 8;
            chart1.Series[0].IsVisibleInLegend = false;

            Axis axisX = chart1.ChartAreas[0].AxisX;
            CultureInfo myCI = new CultureInfo("en-IN");
            Calendar myCal = myCI.Calendar;
            int week = myCal.GetWeekOfYear(DateTime.Now, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek);
            axisX.CustomLabels.Add(0.5, 1.5, "Week" + (week - 3));
            axisX.CustomLabels.Add(2.5, 3.5, "Week" + (week - 2));
            axisX.CustomLabels.Add(4.5, 5.5, "Week" + (week - 1));
            axisX.CustomLabels.Add(6.5, 7.5, "Week" + week);
            int[] weeks = { (week - 3), (week - 2), (week - 1), week };
            {
                chart1.Series.Add("IT Department");
                chart1.Series["IT Department"].ChartType = SeriesChartType.Spline;
                chart1.Series["IT Department"].MarkerStyle = MarkerStyle.Diamond;
                chart1.Series["IT Department"].MarkerSize = 15;
                chart1.Series["IT Department"].IsVisibleInLegend = true;
                chart1.Series["IT Department"].IsValueShownAsLabel = true;
                chart1.Series["IT Department"].Color = Color.FromArgb(3, 38, 70);
                int[] data = new int[4];
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int cnt = 0;
                        try
                        {
                            con.Open();
                            string searchQuery = "SELECT * FROM Attendance where Dept = 'IT Department' And Atweek = '" + weeks[i] + "'";
                            MySqlCommand command = new MySqlCommand(searchQuery, con);
                            MySqlDataReader dr = command.ExecuteReader();
                            while (dr.Read())
                            {
                                cnt++;
                            }
                            dr.Close();
                            int x = EmpCnt("IT Department");
                            if (x != 0)
                            {
                                int t = cnt * 100;
                                float temp = (float)t / x;
                                if(week == weeks[i])
                                {
                                    gunaCircleProgressBar1.Value = Convert.ToInt32(temp);
                                }
                                data[i] = Convert.ToInt32(temp);
                            }
                            else
                            {
                                data[i] = 0;
                                if (week == weeks[i])
                                {
                                    gunaCircleProgressBar1.Value = 0;
                                }
                            }
                            gunaLabel6.Text = x / 5 + " Employees";
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
                chart1.Series["IT Department"].Points.AddXY(1, data[0]);
                chart1.Series["IT Department"].Points.AddXY(3, data[1]);
                chart1.Series["IT Department"].Points.AddXY(5, data[2]);
                chart1.Series["IT Department"].Points.AddXY(7, data[3]);
            }
            {
                chart1.Series.Add("HR Department");
                chart1.Series["HR Department"].ChartType = SeriesChartType.Spline;
                chart1.Series["HR Department"].MarkerStyle = MarkerStyle.Circle;
                chart1.Series["HR Department"].MarkerSize = 15;
                chart1.Series["HR Department"].IsVisibleInLegend = true;
                chart1.Series["HR Department"].IsValueShownAsLabel = true;
                chart1.Series["HR Department"].Color = Color.FromArgb(245, 66, 66);
                int[] data1 = new int[4];
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int cnt = 0;
                        try
                        {
                            con.Open();
                            string searchQuery = "SELECT * FROM Attendance where Dept = 'HR Department' And Atweek = '" + weeks[i] + "'";
                            MySqlCommand command = new MySqlCommand(searchQuery, con);
                            MySqlDataReader dr = command.ExecuteReader();
                            while (dr.Read())
                            {
                                cnt++;
                            }
                            dr.Close();
                            int x = EmpCnt("HR Department");
                            if (x != 0)
                            {
                                int t = cnt * 100;
                                float temp = (float)t / x;
                                if (week == weeks[i])
                                {
                                    gunaCircleProgressBar2.Value = Convert.ToInt32(temp);
                                }
                                data1[i] = Convert.ToInt32(temp);
                            }
                            else
                            {
                                data1[i] = 0;
                                if (week == weeks[i])
                                {
                                    gunaCircleProgressBar2.Value = 0;
                                }
                            }
                            gunaLabel8.Text = x / 5 + " Employees";
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error Occured HR", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
                chart1.Series["HR Department"].Points.AddXY(1, data1[0]);
                chart1.Series["HR Department"].Points.AddXY(3, data1[1]);
                chart1.Series["HR Department"].Points.AddXY(5, data1[2]);
                chart1.Series["HR Department"].Points.AddXY(7, data1[3]);
            }
        }

        private int EmpCnt(string dept)
        {
            int cnt = 0;
            int temp = 0;
            try
            {
                string searchQuery = "SELECT * FROM Employee where Dept = '" + dept + "'";
                MySqlCommand command = new MySqlCommand(searchQuery, con);
                MySqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    cnt++;
                }
                temp = cnt * 5;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error Occured" + er, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            return temp;
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Emp em = new Emp(name);
            em.ShowDialog();
            this.Close();
        }

        private void gunaButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dept de = new Dept(name);
            de.ShowDialog();
            this.Close();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Attendance at = new Attendance(name);
            at.ShowDialog();
            this.Close();
        }

        private void gunaButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminRpt ar = new AdminRpt(name);
            ar.ShowDialog();
            this.Close();
        }
    }
}
