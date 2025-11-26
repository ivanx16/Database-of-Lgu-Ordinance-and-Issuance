using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using YourNamespace;
using Database_of_Lgu_Ordinance_and_Issuance;
using Microsoft.VisualBasic.ApplicationServices;



namespace Database_of_Lgu_Ordinance_and_Issuance
{
    public partial class LoginForm : Form
    {
        // Connection string to MySQL database
        private string connectionString = "server=localhost;database=user;user=root;password=Ivandioso16;";

        public LoginForm()
        {
            InitializeComponent();
            this.Load += Form1_Load;

            txtPassword.KeyPress += TxtPassword_KeyPress;

            // Handle Enter key for login in both fields
            txtUsername.KeyDown += TxtFields_KeyDown;
            txtPassword.KeyDown += TxtFields_KeyDown;

            // Connect checkbox event for password visibility
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            // Make sure button1 click event is attached if not in designer
            Login.Click += button1_Click;
        }

        // Form load event
        private void Form1_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }


        // Handle Enter key to trigger login
        private void TxtFields_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent sound/beep
                AttemptLogin(); // Attempt login
            }
        }

        // Login button click event
        private void button1_Click(object sender, EventArgs e)
        {
            AttemptLogin();

        }

        // Perform login attempt
        private void AttemptLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (AuthenticateUser(username, password))
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Open Dashboard
                Dashboard dashboardForm = new Dashboard();
                dashboardForm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Authenticate user against database
        private bool AuthenticateUser(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, username FROM users WHERE username = @username AND password = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                try
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32("id");
                            string retrievedUsername = reader.GetString("username");

                            // ✅ Store in session
                            Session.CurrentUserId = userId;
                            Session.CurrentUsername = retrievedUsername;

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return false;
        }


        // Checkbox toggle password visibility
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !checkBox1.Checked; // Toggle bullet masking
        }

        // Go to registration form
        private void Signup1_Click(object sender, EventArgs e)
        {
            Register_Form registerForm = new Register_Form();
            registerForm.Show();
            this.Hide(); // Hide login form

        }



        // Optional placeholders for empty events
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label2_Click_1(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            forgotForm.Show(); // or use .ShowDialog() if you want it modal
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent two consecutive spaces
            if (e.KeyChar == ' ')
            {
                TextBox txt = sender as TextBox;
                int cursorPos = txt.SelectionStart;

                // If the current position and the previous character is space, block it
                if (cursorPos > 0 && txt.Text[cursorPos - 1] == ' ')
                {
                    e.Handled = true;
                }
            }
        }

    }
}
