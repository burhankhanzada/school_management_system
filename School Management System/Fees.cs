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
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
            FillStdId();
            DisplayFees();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=localhost;Initial Catalog=SchoolDB;Integrated Security=True");
        private void DisplayFees()
        {
            conn.Open();
            String selectQuery = "Select * from feesTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            feesDGV.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void FillStdId()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select stdID from studentTbl", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("stdID", typeof(int));
            dt.Load(reader);
            feesStdId.ValueMember = "stdID";
            feesStdId.DataSource = dt;
            conn.Close();
        }
        private void GetStdName()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from studentTbl where stdID=@SID", conn);
            cmd.Parameters.AddWithValue("@SID", feesStdId.SelectedValue.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtFees.Text = dr["stdName"].ToString();
            }
            conn.Close();

        }
        private void Reset()
        {
            txtFees.Text = "";
            txtFeesAmnt.Text = "";
            feesStdId.SelectedIndex = -1;
        }
        private void feesAddBtn_Click(object sender, EventArgs e)
        {
            if (txtFees.Text =="" || txtFeesAmnt.Text ==" ") {
                MessageBox.Show("Some fields are missing");
;            }
            else
            {
                string paymentPeriod;
                paymentPeriod = feesDate.Value.Month.ToString() +"/"+ feesDate.Value.Year.ToString();
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select COUNT(*) from feesTbl where stdID = '" + feesStdId.SelectedValue.ToString() + "' and month = '" + paymentPeriod.ToString() + "'", conn);
                DataTable dt = new DataTable();
                 
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("There is now due for this month");
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("insert into feesTbl (stdID,stdName,month,amount) values (@sID,@sName,@sMonth,@sAmount)", conn);
                    cmd.Parameters.AddWithValue("@sID", feesStdId.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@sName", txtFees.Text);
                    cmd.Parameters.AddWithValue("@sMonth", paymentPeriod);
                    cmd.Parameters.AddWithValue("@sAmount", txtFeesAmnt.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fees successfully Paid");

                }
                conn.Close();
                DisplayFees();
                Reset();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void feesStdId_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void feesStdId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetStdName();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainMenu mainObj = new MainMenu();
            mainObj.Show();
            this.Hide();
        }
    }
}
