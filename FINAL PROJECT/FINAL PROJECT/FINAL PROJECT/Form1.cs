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

namespace FINAL_PROJECT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=HITCH\\SQLEXPRESS;Initial Catalog=finalproject;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usernm, userpw;

            usernm = textBox1.Text;
            userpw = textBox2.Text;

            try
            {
                con.Open();

                string querry = "SELECT * FROM userstbl where username = '" + textBox1.Text + "' AND  userpass = '" + textBox2.Text + "'";

                SqlDataAdapter da = new SqlDataAdapter(querry, con);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);




                if (dataTable.Rows.Count == 1)
                {

                    this.Hide();
                    Form3 f3 = new Form3();
                    f3.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Invalid Login Details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();

                    textBox1.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);

            }
            finally
            {
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
