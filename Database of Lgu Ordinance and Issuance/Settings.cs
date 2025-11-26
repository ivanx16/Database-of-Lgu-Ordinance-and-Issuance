using System;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Database_of_Lgu_Ordinance_and_Issuance
{
    public partial class Settings : Form
    {
        private string dbPathConfigFile = "db_path_config.txt";
        private string connectionString = "server=localhost;database=user;user=root;password=Ivandioso16;";
        private string profilePicPath = "profile.jpg"; // Profile picture path
        public event Action UsernameUpdated;


        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            connectionString = "server=localhost;database=user;user=root;password=Ivandioso16;";

            LoadDatabasePath();
            LoadProfilePicture();

            txtCurrentPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;
            txtUsername.Text = Session.CurrentUsername;
        }



        private void LoadDatabasePath()
        {
            if (File.Exists(dbPathConfigFile))
            {
                string dbPath = File.ReadAllText(dbPathConfigFile);
                if (File.Exists(dbPath))
                {
                    connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;";
                }
                else
                {
                    MessageBox.Show("Saved database file not found. Please select a new one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    File.Delete(dbPathConfigFile);
                }
            }
        }

        private void LoadProfilePicture()
        {
            string imagePath = Path.Combine(Application.StartupPath, "profile.jpg");

            if (File.Exists(imagePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        picProfileSettings.Image = Image.FromStream(fs);
                    }

                    picProfileSettings.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load profile picture: " + ex.Message);
                }
            }
            else
            {
                picProfileSettings.Image = null;
            }
        }





        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Profile Picture";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = ofd.FileName;
                    string savePath = Path.Combine(Application.StartupPath, "profile.jpg");

                    try
                    {
                        // Dispose current image and release file lock
                        if (picProfileSettings.Image != null)
                        {
                            picProfileSettings.Image.Dispose();
                            picProfileSettings.Image = null;

                            GC.Collect(); // Force garbage collection
                            GC.WaitForPendingFinalizers();
                        }

                        // Retry logic in case the file is still temporarily locked
                        bool copied = false;
                        int retries = 0;
                        while (!copied && retries < 5)
                        {
                            try
                            {
                                File.Copy(selectedPath, savePath, true);
                                copied = true;
                            }
                            catch (IOException)
                            {
                                retries++;
                                System.Threading.Thread.Sleep(100);
                            }
                        }

                        if (!copied)
                        {
                            MessageBox.Show("Failed to copy image after several attempts.");
                            return;
                        }

                        // Reload the new image
                        LoadProfilePicture();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error uploading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string newUsername = txtUsername.Text.Trim();

            bool isUsernameChanged = newUsername != Session.CurrentUsername;
            bool isChangingPassword = !string.IsNullOrEmpty(currentPassword) ||
                                       !string.IsNullOrEmpty(newPassword) ||
                                       !string.IsNullOrEmpty(confirmPassword);

            if (isChangingPassword)
            {
                if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
                {
                    MessageBox.Show("Please fill in all password fields to change your password.");
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New password and confirmation do not match.");
                    return;
                }

                // Verify current password from DB
                string storedPassword = ""; // Fetch from DB based on Session.CurrentUserId

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT password FROM users WHERE id = @userId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                        storedPassword = Convert.ToString(cmd.ExecuteScalar());
                    }

                    if (storedPassword != currentPassword)
                    {
                        MessageBox.Show("Current password is incorrect.");
                        return;
                    }

                    // Update password
                    string updateQuery = "UPDATE users SET password = @newPassword WHERE id = @userId";
                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Password changed successfully.");
            }

            if (isUsernameChanged)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE users SET username = @newUsername WHERE id = @userId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@newUsername", newUsername);
                        cmd.Parameters.AddWithValue("@userId", Session.CurrentUserId);
                        cmd.ExecuteNonQuery();
                    }
                }

                Session.CurrentUsername = newUsername;
                MessageBox.Show("Username updated successfully.");

                // Raise the event for Dashboard label update
                UsernameUpdated?.Invoke();
            }

            if (!isChangingPassword && !isUsernameChanged)
            {
                MessageBox.Show("No changes were made.");


            }
            UpdateEmailAndContact();

        }



        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedDbPath = "";

                if (File.Exists(dbPathConfigFile))
                {
                    selectedDbPath = File.ReadAllText(dbPathConfigFile);
                    if (!File.Exists(selectedDbPath))
                    {
                        MessageBox.Show("Saved database file not found. Please select a new one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        selectedDbPath = "";
                    }
                }

                if (string.IsNullOrEmpty(selectedDbPath))
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = "Select your Access database file";
                        openFileDialog.Filter = "Access Database (*.accdb)|*.accdb";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            selectedDbPath = openFileDialog.FileName;
                            File.WriteAllText(dbPathConfigFile, selectedDbPath);
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={selectedDbPath};Persist Security Info=False;";
                string databaseFolder = Path.GetDirectoryName(selectedDbPath);

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Save Backup As";
                    saveFileDialog.Filter = "ZIP files (*.zip)|*.zip";
                    saveFileDialog.FileName = "LGU_Backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".zip";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string zipFilePath = saveFileDialog.FileName;

                        if (File.Exists(zipFilePath))
                        {
                            File.Delete(zipFilePath);
                        }

                        string[] filesToBackup = Directory.GetFiles(databaseFolder);
                        using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                        {
                            foreach (string file in filesToBackup)
                            {
                                zip.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Fastest);
                            }
                        }

                        MessageBox.Show("Backup created successfully at:\n" + zipFilePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Backup failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPassword.Checked;
            txtCurrentPassword.UseSystemPasswordChar = !show;
            txtNewPassword.UseSystemPasswordChar = !show;
            txtConfirmPassword.UseSystemPasswordChar = !show;
        }

        private void UpdateEmailAndContact()
        {
            string email = txtEmail.Text.Trim();
            string contact = txtContactNumber.Text.Trim();

            // Validate email format (optional but recommended)
            if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            // Validate contact number (optional: digits only)
            if (!string.IsNullOrEmpty(contact) && !Regex.IsMatch(contact, @"^\d+$"))
            {
                MessageBox.Show("Contact number must contain only digits.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE users SET email = @Email, contact_number = @Contact WHERE id = @UserId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@UserId", Session.CurrentUserId);
                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Email and contact updated successfully!");


        }
    }
}
