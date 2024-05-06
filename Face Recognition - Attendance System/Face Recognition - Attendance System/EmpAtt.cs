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
using System.Windows.Forms.DataVisualization.Charting;

namespace Face_Recognition___Attendance_System
{
    public partial class EmpAtt : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost; database=faces; user=root; password=mohit007");
        string id;
        public EmpAtt(string na)
        {
            InitializeComponent();
            id = na;
            Att();
        }

        private void Att()
        {
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;
            chart.AxisX.Title = "Week Of Year";
            chart.AxisX.TitleFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
            chart.AxisY.Title = "Percentage (%) in 5 Days";
            chart.AxisY.TitleFont = new Font("Times New Roman", 16, System.Drawing.FontStyle.Bold);
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

            chart1.Series.Add("IT Department");
            chart1.Series["IT Department"].ChartType = SeriesChartType.Spline;
            chart1.Series["IT Department"].MarkerStyle = MarkerStyle.Diamond;
            chart1.Series["IT Department"].MarkerSize = 15;
            chart1.Series["IT Department"].IsVisibleInLegend = true;
            chart1.Series["IT Department"].IsValueShownAsLabel = true;
            chart1.Series["IT Department"].Color = Color.FromArgb(3, 38, 70);
            int[] data = new int[4];
            int[] weeks = { (week - 3), (week - 2), (week - 1), week };
            {
                for(int i = 0; i < 4; i++)
                {
                    int cnt = 0;
                    try
                    {
                        con.Open();
                        string searchQuery = "SELECT * FROM Attendance where EmpID = '" + id + "' And Atweek = '" + weeks[i] + "'";
                        MySqlCommand command = new MySqlCommand(searchQuery, con);
                        MySqlDataReader dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            cnt++;     
                        }
                        float temp = cnt * 20;
                        data[i] = Convert.ToInt32(temp);
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

        private void gunaGradientButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
