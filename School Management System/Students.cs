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
    public partial class Students : Form
    {

        public Students()
        {
            InitializeComponent();
            DisplayStudents();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=SchoolDB;Integrated Security=True");
        private void DisplayStudents()
        {
            conn.Open();
            String selectQuery = "Select * from StudentTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            studentDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void stdAddBtn_Click(object sender, EventArgs e)
        {

            if (txtStdName.Text == "" || txtStdFees.Text == "" || txtStdAddress.Text == "" || stdGender.SelectedIndex == -1 || stdClass.SelectedIndex == -1)
            {
                MessageBox.Show("Some fields are empty");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into studentTbl(stdName,stdGender,stdDOB,stdClass,stdFees,stdAdd) values (@sName,@sGen,@sDob,@sClass,@sFees,@sAdd)", conn);
                    cmd.Parameters.AddWithValue("@sName", txtStdName.Text);
                    cmd.Parameters.AddWithValue("@sGen", stdGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sDob", stdDate.Value.Date);
                    cmd.Parameters.AddWithValue("@sClass", stdClass.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sFees", txtStdFees.Text);
                    cmd.Parameters.AddWithValue("@sAdd", txtStdAddress.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Added Successfully!");

                    conn.Close();
                    DisplayStudents();
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void reset()
        {
            key = 0;
            txtStdName.Text = "";
            txtStdFees.Text = "";
            txtStdAddress.Text = "";
            stdGender.SelectedIndex = 0;
            stdClass.SelectedIndex = 0;

        }
        int key = 0;
        private void studentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            txtStdName.Text = studentDGV.SelectedRows[0].Cells[1].Value.ToString();
            stdGender.SelectedItem = studentDGV.SelectedRows[0].Cells[2].Value.ToString();
            stdDate.Text = studentDGV.SelectedRows[0].Cells[3].Value.ToString();
            stdClass.SelectedItem = studentDGV.SelectedRows[0].Cells[4].Value.ToString();
            txtStdFees.Text = studentDGV.SelectedRows[0].Cells[5].Value.ToString();
            txtStdAddress.Text = studentDGV.SelectedRows[0].Cells[6].Value.ToString();

            if (txtStdName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(studentDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void stdDeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please select student");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from studentTbl where stdID=@StdKey", conn);
                    cmd.Parameters.AddWithValue("@StdKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Deleted successfully!");
                    conn.Close();
                    DisplayStudents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void stdUpdateBtn_Click(object sender, EventArgs e)
        {
            if (txtStdName.Text == "" || txtStdFees.Text == "" || txtStdAddress.Text == "" || stdGender.SelectedIndex == -1 || stdClass.SelectedIndex == -1)
            {
                MessageBox.Show("Some fields are empty");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update studentTbl set stdName=@sName,stdGender=@sGen,stdDOB=@sDob,stdClass=@sClass,stdFees=@sFees,stdAdd=@sAdd where stdID=@sID", conn);
                    cmd.Parameters.AddWithValue("@sID", key);
                    cmd.Parameters.AddWithValue("@sName", txtStdName.Text);
                    cmd.Parameters.AddWithValue("@sGen", stdGender.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sDob", stdDate.Value.Date);
                    cmd.Parameters.AddWithValue("@sClass", stdClass.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@sFees", txtStdFees.Text);
                    cmd.Parameters.AddWithValue("@sAdd", txtStdAddress.Text);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully!");
                    conn.Close();
                    DisplayStudents();
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

//