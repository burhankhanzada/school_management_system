using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace School_Management_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=SchoolDB;Integrated Security=True");
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
        private void countStudent() {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from  studentTbl",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            stdLbl.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void countTeachers()
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from  teacherTbl", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            tchLbl.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void countEvents()
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from  eventsTbl", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            eveLbl.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void sumFees()
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select SUM(amount) from  feesTbl", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            feesLbl.Text = dt.Rows[0][0].ToString();
            conn.Close();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            countStudent();
            countTeachers();
            countEvents();
            sumFees();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void eveLbl_Click(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainMenu mainObj = new MainMenu();
            mainObj.Show();
            this.Hide();
        }
    }
}
