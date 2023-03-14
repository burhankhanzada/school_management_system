using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Management_System
{
    public partial class Attendence : Form
    {
        public Attendence()
        {
            InitializeComponent();
            DisplayAttendance();
            FillStdId();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=SchoolDB;Integrated Security=True");
        private void attendenceAddBtn_Click(object sender, EventArgs e)
        {

        }
        private void FillStdId() {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select stdID from studentTbl",conn);
            SqlDataReader reader= cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("stdID", typeof(int));
            dt.Load(reader);
            attendanceIdCombo.ValueMember = "stdID";
            attendanceIdCombo.DataSource = dt;
            conn.Close();
        }
        private void GetStdName() {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from studentTbl where stdID=@SID", conn);
            cmd.Parameters.AddWithValue("@SID", attendanceIdCombo.SelectedValue.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                attendenceName.Text = dr["stdName"].ToString();
            }
            conn.Close();

        }
        private void DisplayAttendance()
        {
            conn.Open();
            String selectQuery = "Select * from attendenceTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            attendanceDGV.DataSource = ds.Tables[0];
            conn.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void reset() {
            attendanceStatusCombo.SelectedIndex = -1;
            attendenceName.Text ="";
            attendanceIdCombo.SelectedIndex = -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (attendenceName.Text == "" || attendanceStatusCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into attendenceTbl (AttStdId,AttStdName,AttDate,AttStatus) values (@stdID,@stdName,@AttDate,@Status)", conn);
                    cmd.Parameters.AddWithValue("@stdID", attendanceIdCombo.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@stdName", attendenceName.Text);
                    cmd.Parameters.AddWithValue("@AttDate", attendanceDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Status", attendanceStatusCombo.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendance Taken");

                    conn.Close();
                    DisplayAttendance();
                   reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //finally
                //{               
                //}
            }
        }
        int key = 0;
        private void attendanceIdCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStdName();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void btnDeleteAttendance_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void attendanceDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            attendanceIdCombo.SelectedValue = attendanceDGV.SelectedRows[0].Cells[1].Value.ToString();
            attendenceName.Text = attendanceDGV.SelectedRows[0].Cells[2].Value.ToString();
            attendanceDate.Text = attendanceDGV.SelectedRows[0].Cells[3].Value.ToString();
            attendanceStatusCombo.SelectedItem = attendanceDGV.SelectedRows[0].Cells[4].Value.ToString();

            if (attendenceName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(attendanceDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnUpdateAttendance_Click(object sender, EventArgs e)
        {
            if (attendenceName.Text == "" || attendanceStatusCombo.SelectedIndex == -1)
            {
                MessageBox.Show("Some fields are empty");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update attendenceTbl set AttStdId=@stdID,AttStdName=@stdName,AttDate=@AttenDate,AttStatus=@attenStatus where AttNum=@aNum", conn);

                    cmd.Parameters.AddWithValue("@stdID",attendanceIdCombo.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@stdName", attendenceName.Text);
                    cmd.Parameters.AddWithValue("@attenDate", attendanceDate.Value.Date);
                    cmd.Parameters.AddWithValue("@attenStatus", attendanceStatusCombo.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@aNum",key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Attendace Updated Successfully!");
                    conn.Close();
                    DisplayAttendance();
                    reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //finally
                //{               
                //}
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainMenu mainObj = new MainMenu();
            mainObj.Show();
            this.Hide();
        }
    }
}
