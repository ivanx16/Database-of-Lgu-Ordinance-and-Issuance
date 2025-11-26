using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using Database_of_Lgu_Ordinance_and_Issuance;

namespace YourNamespace
{
    public partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btnSendResetLink_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter your email address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generate the reset token and send the reset email
            string token = GenerateResetToken();
            SendResetEmail(email, token);
            MessageBox.Show("A password reset link has been sent to your email.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(); // Replace with your actual login form
            loginForm.Show();
        }

        // --- Token Generation ---
        private string GenerateResetToken()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        // --- Email Sending ---
        private void SendResetEmail(string email, string token)
        {
            try
            {
                // Your application domain (replace with your actual domain)
                string resetLink = $"http://www.yourwebsite.com/resetpassword?token={token}";

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("your_email@gmail.com", "your_email_password"), // Use your Gmail credentials
                    EnableSsl = true,
                };

                var message = new MailMessage
                {
                    From = new MailAddress("your_email@gmail.com"),
                    Subject = "Password Reset Request",
                    Body = $"Click the link below to reset your password:\n{resetLink}",
                    IsBodyHtml = false,  // You can change this to true for HTML emails if you want to customize the email more
                };

                message.To.Add(email);  // Add the recipient email
                smtpClient.Send(message);  // Send the email
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message);
            }
        }
    }
}
