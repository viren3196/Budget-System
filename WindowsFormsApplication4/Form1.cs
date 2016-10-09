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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
                MessageBox.Show("Enter all fields!");
            else
            {
                if (comboBox1.SelectedIndex.Equals(-1))
                {
                    MessageBox.Show("Insert atleast one item!");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Are you Sure?", "", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        SqlConnection con = new SqlConnection("server=.;initial catalog=budget;integrated security=true");

                        con.Open();

                        string k = comboBox1.SelectedValue.ToString();
                        string SQL = String.Format("select * from budget where iname = '{0}' ", k);
                        SqlDataAdapter ad = new SqlDataAdapter(SQL, con);
                        DataSet ds = new DataSet();
                        ad.Fill(ds, "try");
                        int g = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                        int f = Convert.ToInt32(ds.Tables[0].Rows[0][2]);
                        con.Close();

                        SqlConnection con2 = new SqlConnection("server=.;initial catalog=budget;integrated security=true");
                        con2.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "update budget set [iquant]=@iquant,[itotal]=@itotal where [iname]=@iname";
                        comm.Parameters.AddWithValue("@iname", k);
                        comm.Parameters.AddWithValue("@itotal", Convert.ToInt32(textBox1.Text) + g);
                        comm.Parameters.AddWithValue("@iquant", Convert.ToInt32(textBox2.Text) + f);
                        comm.Connection = con2;
                        comm.ExecuteNonQuery();
                        con2.Close();
                    }
                }
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox3.Text.Equals(""))
                MessageBox.Show("Enter fields!");
            else
            {
                SqlConnection con = new SqlConnection("server=.;initial catalog=budget;integrated security=true");
                con.Open();
                SqlCommand insert = new SqlCommand("insert into budget(iname,iquant,itotal) values (@iname,@iquant,@itotal)", con);
                insert.Parameters.AddWithValue("iname", textBox3.Text);
                insert.Parameters.AddWithValue("iquant", 0);
                insert.Parameters.AddWithValue("itotal", 0);
                insert.ExecuteNonQuery();

                con.Close();
                this.Hide();
                Form1 ob = new Form1();
                ob.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Serif", 12, FontStyle.Regular);
            label2.Font = new Font("Serif", 12, FontStyle.Regular);
            label3.Font = new Font("Serif", 12, FontStyle.Regular);
            label4.Font = new Font("Serif", 12, FontStyle.Regular);
            label5.Font = new Font("Serif", 24, FontStyle.Regular);

            button1.Font = new Font("Serif", 12, FontStyle.Regular);
            button2.Font = new Font("Serif", 12, FontStyle.Regular);
            button3.Font = new Font("Serif", 12, FontStyle.Regular);

            SqlConnection con = new SqlConnection("server=.;initial catalog=budget;integrated security=true");
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select iname from budget order by iname", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.ValueMember = "iname";
            comboBox1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 ob = new Form2("No");
            ob.Show();
        }
    }
}
