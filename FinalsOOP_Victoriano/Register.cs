using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinalsOOP_Victoriano
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            Hide();
            Login loginForm = new Login();
            loginForm.Show();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string loginType = txtLoginType.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(loginType))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "server=localhost;database=pos_system;uid=root;pwd=;"; // Update with your MySQL credentials
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Check if Admin user already exists
                    if (loginType == "Admin")
                    {
                        string adminCheckQuery = "SELECT COUNT(*) FROM users WHERE login_type = 'Admin'";
                        MySqlCommand adminCheckCmd = new MySqlCommand(adminCheckQuery, conn);
                        int adminCount = Convert.ToInt32(adminCheckCmd.ExecuteScalar());

                        if (adminCount > 0)
                        {
                            MessageBox.Show("An Admin user is already registered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Insert new user into the database
                    string insertQuery = "INSERT INTO users (username, password, login_type) VALUES (@username, @password, @login_type)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@username", username);
                    insertCmd.Parameters.AddWithValue("@password", password);
                    insertCmd.Parameters.AddWithValue("@login_type", loginType);

                    int result = insertCmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("User registered successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Registration failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void closeAppBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


