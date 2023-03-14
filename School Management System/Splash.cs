using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School_Management_System
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        int startPoint = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint += 1;
            loadingProgress.Value = startPoint;
            loadingPer.Text = startPoint + "%";
            if(loadingProgress.Value == 100)
            {
                loadingProgress.Value = 0;
                timer1.Stop();
                Login obj = new Login();
                this.Hide();
                obj.Show();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
