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
    public partial class Teachers : Form
    {
        
        public Teachers()
        {
            InitializeComponent();
            DisplayTeachers();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=SchoolDB;Integrated Security=True");
        private void DisplayTeachers()
        {
            conn.Open();
            String selectQuery = "Select * from teacherTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            teacherDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            if (txtTeacherName.Text == "" || txtTeacherPhone.Text == "" || txtTeacherAddress.Text == "" || teacherGen.SelectedIndex == -1 || teacherSubj.SelectedIndex == -1)
            {
                MessageBox.Show("Some fields are empty");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into teacherTbl(tName,tGender,tPhone,tSubject,tAddress,tDOB) values (@tName,@tGen,@tPhone,@tSubject,@tAddress,@tDOB)", conn);
                    cmd.Parameters.AddWithValue("@tName", txtTeacherName.Text);
                    cmd.Parameters.AddWithValue("@tGen", teacherGen.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@tPhone", txtTeacherPhone.Text);
                    cmd.Parameters.AddWithValue("@tSubject", teacherSubj.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@tAddress", txtTeacherAddress.Text);
                    cmd.Parameters.AddWithValue("@tDOB", teacherDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Added Successfully!");

                    conn.Close();
                    DisplayTeachers();
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
        private void reset()
        {
            key = 0;
            txtTeacherName.Text = "";
            txtTeacherPhone.Text = "";
            txtTeacherAddress.Text = "";
            teacherGen.SelectedIndex = 0;
            teacherSubj.SelectedIndex = 0;
            teacherDate.ResetText();

        }
        int key = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void teacherDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTeacherName.Text = teacherDGV.SelectedRows[0].Cells[1].Value.ToString();
            teacherGen.SelectedItem = teacherDGV.SelectedRows[0].Cells[2].Value.ToString();
            txtTeacherPhone.Text = teacherDGV.SelectedRows[0].Cells[3].Value.ToString();
            teacherSubj.SelectedItem = teacherDGV.SelectedRows[0].Cells[4].Value.ToString();
            txtTeacherAddress.Text = teacherDGV.SelectedRows[0].Cells[5].Value.ToString();
            teacherDate.Text = teacherDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (txtTeacherName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(teacherDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDeleteTeacher_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please select teacher.");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from teacherTbl where tId=@tKey", conn);
                    cmd.Parameters.AddWithValue("@tKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Deleted successfully!");
                    conn.Close();
                    DisplayTeachers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnUpdateTeacher_Click(object sender, EventArgs e)
        {
            if (txtTeacherName.Text == "" || txtTeacherPhone.Text == "" || txtTeacherAddress.Text == "" || teacherGen.SelectedIndex == -1 || teacherSubj.SelectedIndex == -1)
            {
                MessageBox.Show("Some fields are empty");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update teacherTbl set tName=@thName,tGender=@thGen,tPhone=@thPh,tSubject=@thSub,tAddress=@thAdd,tDOB=@thDOB where tid=@thID", conn);
                    
                    cmd.Parameters.AddWithValue("@thID", key);
                    cmd.Parameters.AddWithValue("@thName", txtTeacherName.Text);
                    cmd.Parameters.AddWithValue("@thGen", teacherGen.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@thPh", txtTeacherPhone.Text);
                    cmd.Parameters.AddWithValue("@thSub", teacherSubj.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@thAdd", txtTeacherAddress.Text);
                    cmd.Parameters.AddWithValue("@thDOB", teacherDate.Value.Date);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teacher Details Updated Successfully!");
                    conn.Close();
                    DisplayTeachers();
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenu mainObj = new MainMenu();
            mainObj.Show();
            this.Hide();
        }
    }
}
