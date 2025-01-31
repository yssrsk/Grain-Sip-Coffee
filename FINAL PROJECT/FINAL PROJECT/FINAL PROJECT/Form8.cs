using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FINAL_PROJECT
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=HITCH\\SQLEXPRESS;Initial Catalog=finalproject;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        void viewallorders()
        {
            SqlCommand com = new SqlCommand("\r\nSELECT \r\n    orderstbl.order_id as 'Order No.',\r\n    customerstbl.customer_fname + customerstbl.customer_lname as 'Customer Name',\r\n    customerstbl.customer_con as 'Contact No.',\r\n    customerstbl.customer_address as 'Address',\r\n    orderstbl.order_product as 'Product Name',\r\n    orderstbl.order_price as 'Product Price',\r\n    orderstbl.order_Quantity as 'Quantity',\r\n    orderstbl.total_amount as 'Total',\r\n    orderstbl.Order_type as 'Order Type',\r\n    orderstbl.payment_method as 'Payment Method',\r\n    paymenttbl.payment_id as 'Payment ID',\r\n    paymenttbl.payment_amount as 'Payment Amount',\r\n    paymenttbl.Payment_change as 'Payment Change'\r\nFROM \r\n    orderstbl\r\nINNER JOIN \r\n    customerstbl ON orderstbl.customer_id = customerstbl.customer_id\r\nLEFT JOIN \r\n    paymenttbl ON orderstbl.order_id = paymenttbl.order_id\r\nGROUP BY \r\n    orderstbl.order_id, customerstbl.customer_fname + customerstbl.customer_lname, customerstbl.customer_con,\r\n    customerstbl.customer_address, orderstbl.order_product, orderstbl.order_price, orderstbl.order_Quantity,\r\n    orderstbl.total_amount, orderstbl.Order_type, orderstbl.payment_method, paymenttbl.payment_id,\r\n    paymenttbl.payment_amount, paymenttbl.Payment_change;\r\n", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void viewallorders1()
        {
            SqlCommand com = new SqlCommand("SELECT * from orderstbl", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                var columnNames = dataGridView1.Columns.Cast<DataGridViewColumn>().Select(column => column.Name);
                textBox2.Text = row.Cells[10].Value?.ToString();
                textBox1.Text = row.Cells[0].Value?.ToString();
                comboBox1.Text = row.Cells[4].Value?.ToString();
                textBox5.Text = row.Cells[6].Value?.ToString();
                textBox4.Text = row.Cells[5].Value?.ToString();
                textBox3.Text = row.Cells[7].Value?.ToString();
                comboBox2.Text = row.Cells[8].Value?.ToString();
                comboBox3.Text = row.Cells[9].Value?.ToString();
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            viewallorders();
            dataGridView1.CellClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
            dataGridView1.Refresh();

            try
            {
                // Load products into comboBox1
                SqlCommand com = new SqlCommand("select * from productstbl", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable table1 = new DataTable();
                da.Fill(table1);

                comboBox1.DataSource = table1;
                comboBox1.DisplayMember = "product_name";
                comboBox1.ValueMember = "product_price";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the order?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                con.Open();
                SqlCommand com1 = new SqlCommand("exec dbo.delete_payment'" + textBox2.Text + "'", con);
                SqlCommand com = new SqlCommand("exec dbo.delete_order'" + textBox1.Text + "'", con);
                com1.ExecuteNonQuery();
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully deleted.");
                textBox1.Text = "";
                viewallorders();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                SqlCommand com = new SqlCommand("dbo.edit_order", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@order_id", textBox1.Text);
                com.Parameters.AddWithValue("@order_product", comboBox1.Text);
                com.Parameters.AddWithValue("@order_quantity", textBox5.Text);
                com.Parameters.AddWithValue("@order_price", textBox4.Text);
                com.Parameters.AddWithValue("@total_amount", textBox3.Text);
                com.Parameters.AddWithValue("@order_type", comboBox2.Text);
                com.Parameters.AddWithValue("@payment_method", comboBox3.Text);

                com.ExecuteNonQuery();

                MessageBox.Show("Successfully updated.");
                viewallorders();

                textBox2.Clear();
                textBox3.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating data: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox5.Text, out double quantity) && double.TryParse(textBox4.Text, out double price))
            {
                textBox3.Text = (quantity * price).ToString();
            }
            else
            {
                // Handle the case where the input is not a valid integer
                textBox3.Clear();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                // Assuming you want to display the product price in textBox2
                textBox5.Clear();
                textBox3.Clear();
                textBox4.Text = ((DataRowView)comboBox1.SelectedItem)["product_price"].ToString();
            }
            else
            {
                textBox4.Text = string.Empty; // Clear textBox2 if no item is selected
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}

