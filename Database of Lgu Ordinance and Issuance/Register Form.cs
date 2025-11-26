using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Database_of_Lgu_Ordinance_and_Issuance
{
    public partial class Register_Form : Form
    {
        private string connectionString = "server=localhost;database=user;user=root;password=Ivandioso16;";

        public Register_Form()
        {
            InitializeComponent();

        }

        private void Register_Form_Load(object sender, EventArgs e)
        {
            txtPassword1.PasswordChar = '•';  // Mask password input with bullet
            ConfirmPassword.PasswordChar = '•';  // Mask confirm password input with bullet
        }

        // Checkbox to show/hide password
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword1.PasswordChar = checkBox1.Checked ? '\0' : '•';
            ConfirmPassword.PasswordChar = checkBox1.Checked ? '\0' : '•'; // Toggle confirm password visibility
        }

        // Button for Back to Login
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        // Button for Register
        private void Signup1_Click(object sender, EventArgs e)
        {
            string username = txtUsername1.Text.Trim(); // Trim to avoid trailing spaces
            string password = txtPassword1.Text.Trim();
            string confirmPassword = ConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if password and confirm password match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO users (username, password) VALUES (@username, @password)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password); // ✅ Password saved as plain text

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // ✅ After successful registration, open login form and close registration form
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Close(); // Close Register form
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Empty events (Optional to remove if unused)
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}
