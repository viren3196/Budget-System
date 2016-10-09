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

namespace WindowsFormsApplication4
{
    public partial class Form2 : Form
    {
        string y;
        public Form2(string k)
        {
            InitializeComponent();
            y = k;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Serif", 12, FontStyle.Regular);
            label2.Font = new Font("Serif", 12, FontStyle.Regular);

            button1.Font = new Font("Serif", 12, FontStyle.Regular);
            button2.Font = new Font("Serif", 12, FontStyle.Regular);

            textBox1.Font = new Font("Serif", 12, FontStyle.Regular);
            textBox2.Font = new Font("Serif", 12, FontStyle.Regular);

            SqlConnection conn = new SqlConnection("server=.;initial catalog=budget;integrated security=true");
            conn.Open();
            SqlDataAdapter ad = new SqlDataAdapter("select * from budget order by itotal desc;", conn);
            DataSet ds = new DataSet();
            ad.Fill(ds, "try"); 
        
            dataGridView1.DataSource = ds.Tables[0];           
            conn.Close();

            long sum = 0;

            SqlConnection conn2 = new SqlConnection("server=.;initial catalog=budget;integrated security=true");
            conn2.Open();
            SqlDataAdapter ad2 = new SqlDataAdapter("select * from budget", conn2);
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2, "try");

            for (int i = 0; i < ds2.Tables[0].Rows.Count ; i++)
            {
                sum += Convert.ToInt32(ds2.Tables[0].Rows[i][1].ToString());
            }
            conn2.Close();
            textBox1.Text = sum.ToString();

            SqlConnection conn3 = new SqlConnection("server=.;initial catalog=budget;integrated security=true");
            conn3.Open();
            SqlDataAdapter ad3 = new SqlDataAdapter("select * from date", conn3);
            DataSet ds3 = new DataSet();
            ad3.Fill(ds3, "try");
            textBox2.Text = ds3.Tables[0].Rows[0][0].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (y.Equals("No"))
            {
                this.Hide();
                Form3 ob = new Form3();
                ob.Show();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Sure you want to reset?", "", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    SqlConnection conn = new SqlConnection("server=.;initial catalog=budget;integrated security=true");
                    SqlCommand comm1 = new SqlCommand();
                    comm1.CommandType = CommandType.Text;
                    comm1.CommandText = "update date set [date]=@date";
                    comm1.Parameters.AddWithValue("@date", System.DateTime.Now.ToString());
                    comm1.Connection = conn;
                    conn.Open();
                    comm1.ExecuteNonQuery();
                    conn.Close();

                    textBox2.Text = System.DateTime.Now.ToString();

                    SqlConnection con = new SqlConnection("server=.;initial catalog=budget;integrated security=true");
                    SqlCommand comm = new SqlCommand();
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "delete from budget";
                    comm.Connection = con;
                    con.Open();
                    comm.ExecuteNonQuery();
                    con.Close();

                    this.Hide();
                    Form2 ob = new Form2("No");
                    ob.Show();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ob = new Form1();
            ob.Show();
        }
    }
}
