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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FINAL_PROJECT
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=HITCH\\SQLEXPRESS;Initial Catalog=finalproject;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void Form7_Load(object sender, EventArgs e)
        {
            try
            {
                // Load products into comboBox1
                SqlCommand com = new SqlCommand("select * from orderstbl", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable table1 = new DataTable();
                da.Fill(table1);

                comboBox1.DataSource = table1;
                comboBox1.DisplayMember = "customer_nm";
                comboBox1.ValueMember = "order_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private int Getorder_id(string customer_nm)
        {
            int customerId = -1; // Default value if not found

            try
            {
                con.Open();

                // Assuming "customerstbl" is the table name and "customer_id" is the column containing primary keys
                SqlCommand command = new SqlCommand($"SELECT orders_id FROM orderstbl WHERE customer_nm = @customer_nm", con);
                command.Parameters.AddWithValue("@customer_nm", customer_nm);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    customerId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving customer ID: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return customerId;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                // Assuming you want to display the product price in textBox2
                textBox2.Text = ((DataRowView)comboBox1.SelectedItem)["total_amount"].ToString();
            }
            else
            {
                textBox2.Text = string.Empty; // Clear textBox2 if no item is selected
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Retrieve selected order_id from the comboBox1
                int orderId = Convert.ToInt32(comboBox1.SelectedValue);

                // Insert into paymenttbl
                SqlCommand com = new SqlCommand("INSERT INTO paymenttbl (order_id, payment_amount, payment_change) VALUES (@order_id, @payment_amount, @payment_change)", con);
                com.Parameters.AddWithValue("@order_id", comboBox1.SelectedValue);
                com.Parameters.AddWithValue("@payment_amount", textBox3.Text);
                com.Parameters.AddWithValue("@payment_change", textBox4.Text);

                com.ExecuteNonQuery();
                MessageBox.Show("Successfully saved.");

                comboBox1.SelectedIndex = -1;
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox3.Text, out double payment) && double.TryParse(textBox2.Text, out double total))
            {
                textBox4.Text = (payment - total).ToString();
            }
            else
            {
                // Handle the case where the input is not a valid integer
                textBox3.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
    }
    }
