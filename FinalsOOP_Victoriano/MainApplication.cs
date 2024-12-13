using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalsOOP_Victoriano
{
    public partial class MainApplication : Form
    {
        public class OrderItem
        {
            public int ID { get; set; }
            public string Item { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
        }
        private List<OrderItem> orderItems = new List<OrderItem>();

        public MainApplication()
        {
            InitializeComponent();

            // orderDataGridView.ColumnCount = 4;
            // orderDataGridView.Columns[0].Name = "ID";
            // orderDataGridView.Columns[1].Name = "Item";
            // orderDataGridView.Columns[2].Name = "Quantity";
            // orderDataGridView.Columns[3].Name = "Price";
        }

        private void AddToCart(int itemID, string itemName, double itemPrice)
        {
            // Check if the item already exists in the order
            var existingItem = orderItems.FirstOrDefault(item => item.ID == itemID);

            if (existingItem != null)
            {
                // If the item exists, increment its quantity
                existingItem.Quantity++;

                // Update the corresponding label for quantity display
                Control qtyControl = Controls.Find($"itemID{itemID}Qty", true).FirstOrDefault();
                if (qtyControl is Label)
                {
                    Label qtyLabel = qtyControl as Label;
                    qtyLabel.Text = existingItem.Quantity.ToString();
                }
            }
            else
            {
                // If the item does not exist, add it to the order list
                orderItems.Add(new OrderItem { ID = itemID, Item = itemName, Quantity = 1, Price = itemPrice });

                // Update the corresponding label for quantity display
                Control qtyControl = Controls.Find($"itemID{itemID}Qty", true).FirstOrDefault();
                if (qtyControl is Label)
                {
                    Label qtyLabel = qtyControl as Label;
                    qtyLabel.Text = "1";
                }
            }

            // Recalculate the total price
            double totalPrice = orderItems.Sum(item => item.Quantity * item.Price);

            // Display the total price
            totalPriceLabel.Text = $"Total: {totalPrice.ToString("C")}";

            // Refresh the DataGridView to reflect the updated order
            RefreshOrderGrid();

            // Update the quantity labels
            UpdateQuantityLabels();
        }

        private void UpdateQuantityLabels()
        {
            // Iterate through orderItems and update quantity labels
            foreach (var item in orderItems)
            {
                Control qtyLabelControl = Controls.Find($"itemID{item.ID}Label", true).FirstOrDefault();
                if (qtyLabelControl is Label)
                {
                    Label qtyLabel = qtyLabelControl as Label;
                    qtyLabel.Text = item.Quantity.ToString();
                }
            }
        }

        private void RefreshOrderGrid()
        {
            // Clear the existing rows
            orderDataGridView.Rows.Clear();

            // Add each order item to the DataGridView
            foreach (var item in orderItems)
            {
                orderDataGridView.Rows.Add(item.ID, item.Item, item.Quantity, item.Price * item.Quantity);
            }
        }

        private void closeAppBtn_Click_1(object sender, EventArgs e)
        {
            // Exit application instead of having a control box
            Application.Exit();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (orderDataGridView.SelectedRows.Count > 0)
            {
                // Get the index of the selected row
                int rowIndex = orderDataGridView.SelectedRows[0].Index;

                // Get the ID of the item to be deleted from the order
                int itemID = Convert.ToInt32(orderDataGridView.Rows[rowIndex].Cells["ID"].Value);

                // Find and remove the corresponding item from the orderItems list
                var itemToRemove = orderItems.FirstOrDefault(item => item.ID == itemID);
                if (itemToRemove != null)
                {
                    orderItems.Remove(itemToRemove);
                }

                // Remove the selected row from the DataGridView
                orderDataGridView.Rows.RemoveAt(rowIndex);

                // Update the quantity label
                Control qtyLabelControl = Controls.Find($"itemID{itemID}Label", true).FirstOrDefault();
                if (qtyLabelControl is Label)
                {
                    Label qtyLabel = qtyLabelControl as Label;
                    qtyLabel.Text = "0";
                }
            }
        }

        private void payBtn_Click(object sender, EventArgs e)
        {
            // Calculate the total price of all items in the order
            double totalPrice = orderItems.Sum(item => item.Quantity * item.Price);

            // Display the total price
            totalPriceLabel.Text = $"Total: {totalPrice.ToString("C")}";

            // Get the amount paid by the customer from the cashInput textbox
            double cashPaid;
            if (!double.TryParse(cashInput.Text, out cashPaid))
            {
                // Display an error message if the input is not a valid number
                MessageBox.Show("Please enter a valid amount for cash paid.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the amount paid is less than the total price
            if (cashPaid < totalPrice)
            {
                // Display an error message indicating insufficient payment
                MessageBox.Show("Insufficient payment. Please enter an amount equal to or greater than the total price.", "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calculate the balance (change) to be returned to the customer
            double balance = cashPaid - totalPrice;

            // Display the balance
            balanceLabel.Text = $"Balance: {balance.ToString("C")}";
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            // Generate receipt text
            string receiptText = GenerateReceipt();

            // Display receipt text in receiptPrintPanel
            receiptTextBox.Text = receiptText;

            // Print receipt
            PrintReceipt();
        }


        private string GenerateReceipt()
        {
            StringBuilder receiptBuilder = new StringBuilder();

            // Add shop name and contact information
            receiptBuilder.AppendLine("AM Appliances");
            receiptBuilder.AppendLine("+63 9123456789");
            receiptBuilder.AppendLine();

            // Add table header
            receiptBuilder.AppendLine("ID - ITEM - QTY - PRICE");
            receiptBuilder.AppendLine("-------------------------");

            // Add order details
            foreach (var item in orderItems)
            {
                receiptBuilder.AppendLine($"{item.ID} - {item.Item} - {item.Quantity} - {item.Price.ToString("C")}");
            }

            // Add subtotal
            double totalPrice = orderItems.Sum(item => item.Quantity * item.Price);
            receiptBuilder.AppendLine($"Subtotal: {totalPrice.ToString("C")}");

            // Add payment details
            receiptBuilder.AppendLine($"Cash: {cashInput.Text}");
            double cashPaid;
            double.TryParse(cashInput.Text, out cashPaid);
            double balance = cashPaid - totalPrice;
            receiptBuilder.AppendLine($"Balance: {balance.ToString("C")}");

            // Add thank you message
            receiptBuilder.AppendLine("-------------------------");
            receiptBuilder.AppendLine("Thank you!");
            receiptBuilder.AppendLine("-------------------------");

            return receiptBuilder.ToString();
        }
        private void PrintReceipt()
        {
            // Print contents of receiptPrintPanel
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDialog.Document = new PrintDocument();
                printDialog.Document.PrintPage += (sender, e) =>
                {
                    e.Graphics.DrawString(receiptPrintPanel.Text, receiptPrintPanel.Font, Brushes.Black, 10, 10);
                };
                printDialog.Document.Print();
            }
        }

        // Add to cart functions
        private void itemID1_Click(object sender, EventArgs e)
        {
            AddToCart(1, "Microwave", 80);
        }

        private void itemID2_Click(object sender, EventArgs e)
        {
            AddToCart(2, "Refrigerator", 120);
        }

        private void itemID3_Click(object sender, EventArgs e)
        {
            AddToCart(3, "Electric Fan", 110);
        }

        private void itemID4_Click(object sender, EventArgs e)
        {
            AddToCart(4, "Television", 50);
        }

        private void itemID5_Click(object sender, EventArgs e)
        {
            AddToCart(5, "Electric Kettle", 60);
        }

        private void itemID6_Click(object sender, EventArgs e)
        {
            AddToCart(6, "Stand Mixer", 70);
        }

        private void itemID7_Click(object sender, EventArgs e)
        {
            AddToCart(7, "Blender", 65);
        }

        private void itemID8_Click(object sender, EventArgs e)
        {
            AddToCart(8, "Air Conditioner", 35);
        }

        private void itemID9_Click(object sender, EventArgs e)
        {
            AddToCart(9, "Iron", 75);
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            // Clear the user session
            UserSession.Clear();

            // Show the login form
            Login loginForm = new Login();
            loginForm.Show();

            // Close the main application form
            this.Close();
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            // Clear the order items list
            orderItems.Clear();

            // Clear the DataGridView
            orderDataGridView.Rows.Clear();

            // Reset all quantity labels
            for (int i = 1; i <= 9; i++)
            {
                Control qtyControl = Controls.Find($"itemID{i}Label", true).FirstOrDefault();
                if (qtyControl is Label)
                {
                    Label qtyLabel = qtyControl as Label;
                    qtyLabel.Text = "0";
                }
            }

            // Reset total price and balance labels
            totalPriceLabel.Text = "Total: None";
            balanceLabel.Text = "Balance: None";

            // Clear the cash input textbox
            cashInput.Text = string.Empty;

            // Clear the receipt text
            receiptTextBox.Text = string.Empty;
        }
    }
}
