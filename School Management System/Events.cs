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
    public partial class Events : Form
    {
        public Events()
        {
            InitializeComponent();
            DisplayEvents();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=SchoolDB;Integrated Security=True");
        private void DisplayEvents()
        {
            conn.Open();
            String selectQuery = "Select * from eventsTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            eventsDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void reset() {
            txteDesc.Text = "";
            txteDuration.Text = "";
        }
        private void eAddBtn_Click(object sender, EventArgs e)
        {
            if (txteDesc.Text == "" || txteDuration.Text == "" )
            {
                MessageBox.Show("Some fields are empty");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("insert into eventsTbl(eDesc,eDate,eDuration) values (@eveDesc,@eveDate,@eveDura)", conn);
                    cmd.Parameters.AddWithValue("@eveDesc", txteDesc.Text);
                    cmd.Parameters.AddWithValue("@eveDate", eDate.Value.Date);
                    cmd.Parameters.AddWithValue("@eveDura", txteDuration.Text);
                   
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Event Added Successfully!");

                    conn.Close();
                    DisplayEvents();
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

        private void button4_Click(object sender, EventArgs e)
        {
            MainMenu mainObj = new MainMenu();
            mainObj.Show();
            this.Hide();
        }
        int key =0;
        private void eDateBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Please select Event");
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("delete from eventsTbl where eId=@eveID", conn);
                    cmd.Parameters.AddWithValue("@eveID", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Event Deleted successfully!");
                    conn.Close();
                    DisplayEvents();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }


        private void eUpdateBtn_Click(object sender, EventArgs e)
        {
            if (txteDesc.Text == "" || txteDuration.Text == "" )
            {
                MessageBox.Show("Some fields are empty");
            }

            else
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("update eventsTbl set eDesc=@eveDesc,eDate=@eveDate,eDuration=@eveDuration where eId=@eveID", conn);
                    cmd.Parameters.AddWithValue("@eveID", key);
                    cmd.Parameters.AddWithValue("@eveDesc", txteDesc.Text);
                    cmd.Parameters.AddWithValue("@eveDate", eDate.Value.Date);
                    cmd.Parameters.AddWithValue("@eveDuration", txteDuration.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Event Updated Successfully!");
                    conn.Close();
                    DisplayEvents();
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

        private void eventsDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txteDesc.Text = eventsDGV.SelectedRows[0].Cells[1].Value.ToString();
            eDate.Text = eventsDGV.SelectedRows[0].Cells[2].Value.ToString();
            txteDuration.Text = eventsDGV.SelectedRows[0].Cells[3].Value.ToString();

            if (txteDesc.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(eventsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
