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
    public partial class Form6 : Form
    {
        private DataTable dataTable = new DataTable();
        public Form6()
        {
            InitializeComponent();
            InitializeDataTable();
            dataGridView1.DataSource = dataTable;
        }

        SqlConnection con = new SqlConnection("Data Source=HITCH\\SQLEXPRESS;Initial Catalog=finalproject;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        string rdb;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Green;
            radioButton2.ForeColor = Color.Red;
            rdb = "Pick-up";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Red;
            radioButton2.ForeColor = Color.Green;
            rdb = "Delivery";
        }

        private void InitializeDataTable()
        {
            // Define the columns for your DataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("Customer ID");
            dataTable.Columns.Add("Customer Name"); // Column2
            dataTable.Columns.Add("Product ID");
            dataTable.Columns.Add("Product"); // Column3
            dataTable.Columns.Add("Quantity"); // Column4
            dataTable.Columns.Add("Price"); // Column5
            dataTable.Columns.Add("Total Amount"); // Column6
            dataTable.Columns.Add("Date"); // Column6
            dataTable.Columns.Add("Order Type"); // Column9
            dataTable.Columns.Add("Payment Method"); // Column9
        }

        private int GetCustomer_Id(string customerName)
        {
            int customerId = -1; // Default value if not found

            try
            {
                con.Open();

                // Assuming "customerstbl" is the table name and "customer_id" is the column containing primary keys
                SqlCommand command = new SqlCommand($"SELECT customer_id FROM customerstbl WHERE customer_fname = @customer_Name", con);
                command.Parameters.AddWithValue("@customer_Name", customerName);

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

        private int GetProduct_Id(string productName)
        {
            int productId = -1; // Default value if not found

            try
            {
                con.Open();

                // Assuming "productstbl" is the table name and "product_id" is the column containing primary keys
                SqlCommand command = new SqlCommand($"SELECT product_id FROM productstbl WHERE product_name = @product_Name", con);
                command.Parameters.AddWithValue("@product_Name", productName);

                var result = command.ExecuteScalar();
                if (result != null)
                {
                    productId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving product ID: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return productId;
        }
        private int GetOrderId()
        {
            int orderId = -1; // Default value if not found

            try
            {
                con.Open();

                // Assuming "orderstbl" is the table name and "order_id" is the column containing primary keys
                // You may need to adjust the column names based on your actual database schema
                SqlCommand command = new SqlCommand($"select order_id from orderstbl", con);
                var result = command.ExecuteScalar();
                if (result != null)
                {
                    orderId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving order ID: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return orderId;
        }

        private void AddRowToDataTable()
        {
            // Create a new row and set its values
            DataRow newRow = dataTable.NewRow();

            // Retrieve customer_id based on selected customer_name
            int customer_Id = GetCustomer_Id(comboBox3.Text);
            newRow["Customer ID"] = customer_Id;
            newRow["Customer Name"] = comboBox3.Text; // Column2
            int product_Id = GetProduct_Id(comboBox1.Text);
            newRow["Product ID"] = product_Id;
            newRow["Product"] = comboBox1.Text; // Column3

            // Try parsing quantity as double
            if (double.TryParse(textBox1.Text, out double quantity))
                newRow["Quantity"] = quantity;
            else
                newRow["Quantity"] = DBNull.Value; // or set to a default value

            // Try parsing price as double
            if (double.TryParse(textBox2.Text, out double price))
                newRow["Price"] = price;
            else
                newRow["Price"] = DBNull.Value; // or set to a default value

            // Try parsing total amount as double
            if (double.TryParse(textBox3.Text, out double totalAmount))
                newRow["Total Amount"] = totalAmount;
            else
                newRow["Total Amount"] = DBNull.Value; // or set to a default value
            newRow["Date"] = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            newRow["Order Type"] = rdb; // Assuming 'Service' is a string column
            newRow["Payment Method"] = comboBox2.Text; // Assuming 'Payment Method' is a string column
            // Add the row to the DataTable
            dataTable.Rows.Add(newRow);
        }




        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the cell clicked is not a header cell and is within the range of rows
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
                // Check if an item is selected in comboBox1
                if (comboBox1.SelectedItem != null)
                {
                // Assuming you want to display the product price in textBox2
                textBox1.Clear();
                textBox2.Text = ((DataRowView)comboBox1.SelectedItem)["product_price"].ToString();
                }
                else
                {
                    textBox2.Text = string.Empty; // Clear textBox2 if no item is selected
                }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
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

                // Load customer names into comboBox3
                SqlCommand com1 = new SqlCommand("select * from customerstbl", con);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataTable table2 = new DataTable();
                da1.Fill(table2);


                comboBox3.DataSource = table2;
                comboBox3.DisplayMember = "customer_fname";
                comboBox3.ValueMember = "customer_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRowToDataTable();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double quantity) && double.TryParse(textBox2.Text, out double price))
            {
                textBox3.Text = (quantity * price).ToString();
            }
            else
            {
                // Handle the case where the input is not a valid integer
                textBox3.Clear();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                try
                {
                    con.Open();

                    // Insert order details
                    SqlCommand insertDetailsCommand = new SqlCommand("INSERT INTO orderstbl (customer_id, customer_nm, product_id, order_product, order_quantity, order_price, total_amount, dot, order_type, payment_method) VALUES (@customer_id, @customer_nm, @product_id, @order_product, @order_quantity, @order_price, @total_amount, @dot, @order_type, @payment_method)", con);
                    
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        insertDetailsCommand.Parameters.Clear();
                        insertDetailsCommand.Parameters.AddWithValue("@customer_id", row.Cells["Customer ID"].Value != null ? row.Cells["Customer ID"].Value.ToString() : "0");
                        insertDetailsCommand.Parameters.AddWithValue("@customer_nm", row.Cells["Customer Name"].Value != null ? row.Cells["Customer Name"].Value.ToString() : string.Empty);
                        insertDetailsCommand.Parameters.AddWithValue("@product_id", row.Cells["Product ID"].Value != null ? row.Cells["Product ID"].Value.ToString() : "0");
                        insertDetailsCommand.Parameters.AddWithValue("@order_product", row.Cells["Product"].Value != null ? row.Cells["Product"].Value.ToString() : string.Empty);
                        insertDetailsCommand.Parameters.AddWithValue("@order_quantity", row.Cells["Quantity"].Value != null ? row.Cells["Quantity"].Value.ToString() : "0");
                        insertDetailsCommand.Parameters.AddWithValue("@order_price", row.Cells["Price"].Value != null ? row.Cells["Price"].Value.ToString() : "0");
                        insertDetailsCommand.Parameters.AddWithValue("@total_amount", row.Cells["Total Amount"].Value != null ? row.Cells["Total Amount"].Value.ToString() : "0");
                        insertDetailsCommand.Parameters.AddWithValue("@dot", row.Cells["Date"].Value != null ? row.Cells["Date"].Value : "0");
                        insertDetailsCommand.Parameters.AddWithValue("@order_type", row.Cells["Order Type"].Value != null ? row.Cells["Order Type"].Value.ToString() : "0");
                        insertDetailsCommand.Parameters.AddWithValue("@payment_method", row.Cells["Payment Method"].Value != null ? row.Cells["Payment Method"].Value.ToString() : "0");
                        insertDetailsCommand.ExecuteNonQuery();
                    }

                    con.Close();
                    MessageBox.Show("Sale Created Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sale Created Successfully");
                }
            }
            else
            {
                MessageBox.Show("Must Add an Item in the List");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7();
            f7.ShowDialog();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Iterate through all selected rows and remove them
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to remove.");
            }
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        private void DeleteSelectedItem()
        {
            
        }
        private void listView1_MouseClick(object sender, ColumnClickEventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            // Clear ComboBoxes
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

            // Clear RadioButton selection
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                // Clear ComboBoxes
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;

                // Clear RadioButton selection
                radioButton1.Checked = false;
                radioButton2.Checked = false;
            }
        }
    }
}
