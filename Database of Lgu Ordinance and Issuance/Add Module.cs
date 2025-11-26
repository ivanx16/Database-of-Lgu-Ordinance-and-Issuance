using System;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Database_of_Lgu_Ordinance_and_Issuance
{
    public partial class AddModule : Form
    {
        private string connectionString = "server=localhost;database=addmodule;user=root;password=Ivandioso16;";
        private string filePath = "";
        private bool editMode = false;
        private int moduleId = -1;

        public AddModule()
        {
            InitializeComponent();
            this.Load += AddModule_Load;
        }

        // Constructor for editing
        public AddModule(int id, string title, string issuanceNumber, string remarks, string type, string filePathFromDB)
        {
            InitializeComponent();
            this.Load += AddModule_Load;

            editMode = true;
            moduleId = id;

            txtTitle.Text = title;
            txtIssuanceNumber.Text = issuanceNumber;
            txtRemarks.Text = remarks;
            cmbType.SelectedItem = type;
            filePath = filePathFromDB;

            btnAdd.Text = "Update";
            btnUploadFile.Enabled = false;
        }

        private void AddModule_Load(object sender, EventArgs e)
        {
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Items.Clear();

            cmbType.Items.Add("Ordinance");
            cmbType.Items.Add("Executive Order");
            cmbType.Items.Add("Memorandum");
            cmbType.Items.Add("Office Order");
            cmbType.Items.Add("Resolution");

            if (cmbType.Items.Count > 0 && !editMode)
            {
                cmbType.SelectedIndex = 0;
            }
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a File",
                Filter = "PDF Files (*.pdf)|*.pdf|Word Documents (*.docx)|*.docx|All Files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                MessageBox.Show("File selected: " + Path.GetFileName(filePath), "File Uploaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string issuanceNumber = txtIssuanceNumber.Text.Trim();
            string date = dtpDate.Value.ToString("yyyy-MM-dd");
            string remarks = txtRemarks.Text.Trim();
            string type = cmbType.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(issuanceNumber) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Please complete all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlCommand cmd;

                    if (editMode)
                    {
                        cmd = new MySqlCommand("UPDATE modules SET title=@title, issuance_number=@issuance_number, file_path=@file_path, date=@date, remarks=@remarks, type=@type WHERE id=@id", conn);
                        cmd.Parameters.AddWithValue("@id", moduleId);
                    }
                    else
                    {
                        cmd = new MySqlCommand("INSERT INTO modules (title, issuance_number, file_path, date, remarks, type) VALUES (@title, @issuance_number, @file_path, @date, @remarks, @type)", conn);
                    }

                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@issuance_number", issuanceNumber);
                    cmd.Parameters.AddWithValue("@file_path", filePath);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@remarks", remarks);
                    cmd.Parameters.AddWithValue("@type", type);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(editMode ? "Module updated successfully!" : "Module uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Upload_File.Instance?.RefreshTable();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to database: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dtpDate_ValueChanged(object sender, EventArgs e) { }

        
    }
}
