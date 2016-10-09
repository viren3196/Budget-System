using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 ob = new Form4();
            ob.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string y;
            if (textBox1.Text.Equals("9404932554"))
            {
                MessageBox.Show("Password correct! Press Reset Now");
                y = "Yes";
            }
            else
            {
                MessageBox.Show("Password incorrect!");
                y = "No";
            }
            this.Hide();
            Form2 ob = new Form2(y);
            ob.Show();
        }
    }
}
