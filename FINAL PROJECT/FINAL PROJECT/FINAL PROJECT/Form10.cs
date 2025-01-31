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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=HITCH\\SQLEXPRESS;Initial Catalog=finalproject;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        void viewallorders()
        {
            SqlCommand com = new SqlCommand("select orderstbl.order_id as 'Order No.', customerstbl.customer_fname + customerstbl.customer_lname as 'Customer Name', customerstbl.customer_con as\r\n'Contact No.', customerstbl.customer_address as 'Address', order_product as 'Product Name', order_price as 'Product Price', order_Quantity as 'Quantity', \r\ntotal_amount as 'Total', Order_type as 'Order Type', payment_method as 'Payment Method', payment_amount as 'Payment Amount', Payment_change as 'Payment Change'\r\nfrom orderstbl join customerstbl on orderstbl.customer_id = customerstbl.customer_id join paymenttbl on orderstbl.order_id = paymenttbl.order_id\r\ngroup by orderstbl.order_id, customerstbl.customer_fname + customerstbl.customer_lname, customerstbl.customer_con, customerstbl.customer_address, order_product,\r\norder_price, order_Quantity, total_amount, Order_type, payment_method, payment_amount, Payment_change", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void viewallorders1()
        {
            SqlCommand com = new SqlCommand("\r\nSELECT \r\n    orderstbl.order_id as 'Order No.',\r\n    customerstbl.customer_fname + customerstbl.customer_lname as 'Customer Name',\r\n    customerstbl.customer_con as 'Contact No.',\r\n    customerstbl.customer_address as 'Address',\r\n    orderstbl.order_product as 'Product Name',\r\n    orderstbl.order_price as 'Product Price',\r\n    orderstbl.order_Quantity as 'Quantity',\r\n    orderstbl.total_amount as 'Total',\r\n    orderstbl.Order_type as 'Order Type',\r\n    orderstbl.payment_method as 'Payment Method',\r\n    paymenttbl.payment_id as 'Payment ID',\r\n    paymenttbl.payment_amount as 'Payment Amount',\r\n    paymenttbl.Payment_change as 'Payment Change'\r\nFROM \r\n    orderstbl\r\nINNER JOIN \r\n    customerstbl ON orderstbl.customer_id = customerstbl.customer_id\r\nLEFT JOIN \r\n    paymenttbl ON orderstbl.order_id = paymenttbl.order_id\r\nGROUP BY \r\n    orderstbl.order_id, customerstbl.customer_fname + customerstbl.customer_lname, customerstbl.customer_con,\r\n    customerstbl.customer_address, orderstbl.order_product, orderstbl.order_price, orderstbl.order_Quantity,\r\n    orderstbl.total_amount, orderstbl.Order_type, orderstbl.payment_method, paymenttbl.payment_id,\r\n    paymenttbl.payment_amount, paymenttbl.Payment_change;\r\n", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            viewallorders1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Retrieve the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                // Clear the contents of the RichTextBox
                richTextBox1.Clear();

                // Construct the receipt information
                StringBuilder receiptInfo = new StringBuilder();
                richTextBox1.Clear();


                receiptInfo.AppendLine(" ╔═══════════════════════════════════════════════════════");
                receiptInfo.AppendLine("║                                      🛒 GRAIN & SIP COFFEE 🛍️                                 ║");
                receiptInfo.AppendLine(" ╚═══════════════════════════════════════════════════════");

                // Add your custom column names and their values to the receipt information
                receiptInfo.AppendLine($"╚═══════════════════════════════════════════════════════");
                receiptInfo.AppendLine($"Order No.:              {row.Cells["Order No."].Value}                                                                    ");
                receiptInfo.AppendLine($"Customer Name:           {row.Cells["Customer Name"].Value}                                                                          ");
                receiptInfo.AppendLine($"Contact No.:        {row.Cells["Contact No."].Value}                                                                                  ");
                receiptInfo.AppendLine($"Address:       {row.Cells["Address"].Value}                                                                           ");
                receiptInfo.AppendLine($"Product Name:               {row.Cells["Product Name"].Value}                                                                                        ");
                receiptInfo.AppendLine($"Product Price:             {row.Cells["Product Price"].Value}                                                                      ");
                receiptInfo.AppendLine($"Quantity:        {row.Cells["Quantity"].Value}");
                receiptInfo.AppendLine($"Total:           {row.Cells["Total"].Value}");
                receiptInfo.AppendLine($"Order Type:     {row.Cells["Order Type"].Value}");
                receiptInfo.AppendLine($"Payment Method:   {row.Cells["Payment Method"].Value}");
                receiptInfo.AppendLine($"Payment Amount:  {row.Cells["Payment Amount"].Value}");
                receiptInfo.AppendLine($"Payment Change:  {row.Cells["Payment Change"].Value}");
                receiptInfo.AppendLine($"╚═══════════════════════════════════════════════════════");

                receiptInfo.AppendLine(" ╔═══════════════════════════════════════════════════════");
                receiptInfo.AppendLine("║                                           🛒 THANK YOU. BUY AGAIN! 🛍️                              ║");
                receiptInfo.AppendLine(" ╚═══════════════════════════════════════════════════════");

                // Display the receipt information in the RichTextBox
                richTextBox1.Text = receiptInfo.ToString();
            }
            else
            {
                MessageBox.Show("Please select a row.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, new Font("Microsoft San Serif", 20, FontStyle.Bold), Brushes.Black, new Point(10, 10));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
