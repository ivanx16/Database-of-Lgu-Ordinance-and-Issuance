using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BarangayIssuance
{
    public partial class Archived_Files : Form
    {
        private string connectionString = "server=localhost;database=addmodule;user=root;password=Ivandioso16;";

        public Archived_Files()
        {
            InitializeComponent();
            LoadFilterOptions();
            cmbFilter1.SelectedIndexChanged += FilterOrSearchChanged;
            txtSearch1.TextChanged += FilterOrSearchChanged;

            // Fix: Prevent multiple event subscriptions
            ViewArchivedFiles.CellContentClick -= ViewArchivedFiles_CellContentClick;
            ViewArchivedFiles.CellContentClick += ViewArchivedFiles_CellContentClick;

            LoadArchivedFiles();

            ViewArchivedFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ViewArchivedFiles.MultiSelect = true;
            ViewArchivedFiles.AllowUserToAddRows = false; // Optional, for clean grid
        }


        private void LoadArchivedFiles()
        {
            string query = "SELECT id, title AS 'Title', issuance_number AS 'Issuance Number', date AS 'Date', remarks AS 'Remarks', file_path AS 'File Path', type AS 'Type' FROM modules WHERE archived = 1";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ViewArchivedFiles.Columns.Clear();
                ViewArchivedFiles.DataSource = dt;

                FormatDataGridView();
                AddActionButtonsIfNeeded();
            }
        }

        private void LoadFilterOptions()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT type FROM modules";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                cmbFilter1.Items.Clear();
                cmbFilter1.Items.Add("All");
                cmbFilter1.Items.Add("Ordinance");
                cmbFilter1.Items.Add("Executive Order"); // <-- Added manually

                while (reader.Read())
                {
                    string type = reader.GetString("type");

                    // Avoid adding duplicates
                    if (!cmbFilter1.Items.Contains(type))
                    {
                        cmbFilter1.Items.Add(type);
                    }
                }

                cmbFilter1.SelectedIndex = 0;
            }
        }


        private void FilterOrSearchChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch1.Text.Trim();
            string selectedType = cmbFilter1.SelectedItem?.ToString() ?? "All";

            string query = "SELECT id, title AS 'Title', issuance_number AS 'Issuance Number', date AS 'Date', remarks AS 'Remarks', file_path AS 'File Path', type AS 'Type' FROM modules WHERE archived = 1";

            if (selectedType != "All")
            {
                query += " AND type = @type";
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query += @" AND (
                    title LIKE @search OR
                    issuance_number LIKE @search OR
                    remarks LIKE @search OR
                    date LIKE @search OR
                    file_path LIKE @search OR
                    type LIKE @search
                )";
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (selectedType != "All")
                    cmd.Parameters.AddWithValue("@type", selectedType);

                if (!string.IsNullOrWhiteSpace(searchText))
                    cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                ViewArchivedFiles.Columns.Clear();
                ViewArchivedFiles.DataSource = dt;

                FormatDataGridView();
                AddActionButtonsIfNeeded();
            }
        }

        private void ViewArchivedFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var dgv = ViewArchivedFiles;
            var columnName = dgv.Columns[e.ColumnIndex].Name;

            if (!dgv.Columns.Contains("id") || dgv.Rows[e.RowIndex].Cells["id"].Value == null)
                return;

            string id = dgv.Rows[e.RowIndex].Cells["id"].Value.ToString();
            string filePath = dgv.Rows[e.RowIndex].Cells["File Path"].Value?.ToString();

            if (columnName == "View")
            {
                try
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                catch
                {
                    MessageBox.Show("Unable to open file.");
                }
            }
            else if (columnName == "Restore")
            {
                var confirm = MessageBox.Show("Restore this file?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("UPDATE modules SET archived = 0 WHERE id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    LoadArchivedFiles();
                }
            }
            else if (columnName == "Delete")
            {
                var confirm = MessageBox.Show("Permanently delete this file?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM modules WHERE id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    LoadArchivedFiles();
                }
            }
        }



        private void AddActionButtonsIfNeeded()
        {
            if (!ViewArchivedFiles.Columns.Contains("View"))
            {
                DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn
                {
                    HeaderText = "Action",
                    Name = "View",
                    Text = "View",
                    UseColumnTextForButtonValue = true
                };
                ViewArchivedFiles.Columns.Add(viewButton);
            }

            if (!ViewArchivedFiles.Columns.Contains("Restore"))
            {
                DataGridViewButtonColumn restoreButton = new DataGridViewButtonColumn
                {
                    HeaderText = "",
                    Name = "Restore",
                    Text = "Restore",
                    UseColumnTextForButtonValue = true
                };
                ViewArchivedFiles.Columns.Add(restoreButton);
            }

            if (!ViewArchivedFiles.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
                {
                    HeaderText = "",
                    Name = "Delete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                };
                ViewArchivedFiles.Columns.Add(deleteButton);
            }
        }

        private void FormatDataGridView()
        {
            if (ViewArchivedFiles.Columns.Count == 0) return;

            // Hide row header (first gray column)
            ViewArchivedFiles.RowHeadersVisible = false;

            // Hide 'id' column if exists
            if (ViewArchivedFiles.Columns.Contains("id"))
                ViewArchivedFiles.Columns["id"].Visible = false;

            // Reset sizing so widths are respected
            foreach (DataGridViewColumn col in ViewArchivedFiles.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                col.Resizable = DataGridViewTriState.True;
            }

            // Set fixed widths for each column
            if (ViewArchivedFiles.Columns.Contains("Title"))
                ViewArchivedFiles.Columns["Title"].Width = 225;
            if (ViewArchivedFiles.Columns.Contains("Issuance Number"))
                ViewArchivedFiles.Columns["Issuance Number"].Width = 143;
            if (ViewArchivedFiles.Columns.Contains("Date"))
                ViewArchivedFiles.Columns["Date"].Width = 150;
            if (ViewArchivedFiles.Columns.Contains("Remarks"))
                ViewArchivedFiles.Columns["Remarks"].Width = 190;
            if (ViewArchivedFiles.Columns.Contains("File Path"))
            {
                var col = ViewArchivedFiles.Columns["File Path"];
                col.Width = 235;
                col.DefaultCellStyle.ForeColor = Color.Blue;
                col.DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Underline);
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            if (ViewArchivedFiles.Columns.Contains("Type"))
                ViewArchivedFiles.Columns["Type"].Width = 120;

            // Let button columns autosize
            foreach (DataGridViewColumn col in ViewArchivedFiles.Columns)
            {
                if (col is DataGridViewButtonColumn)
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            ViewArchivedFiles.AllowUserToResizeColumns = true;
            ViewArchivedFiles.AllowUserToOrderColumns = true;
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // Step 1: Check if anything is selected
            if (ViewArchivedFiles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select at least one row to delete.");
                return;
            }

            // Step 2: Ask for confirmation
            DialogResult confirm = MessageBox.Show("Are you sure you want to delete the selected files?",
                                                   "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;

            // Step 3: Loop through selected rows and delete from DB
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                foreach (DataGridViewRow row in ViewArchivedFiles.SelectedRows)
                {
                    string filePath = row.Cells["File Path"].Value.ToString(); // Or use ID if available

                    string query = "DELETE FROM modules WHERE file_path = @filePath";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@filePath", filePath);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Step 4: Refresh the DataGridView
            LoadArchivedFiles();

            MessageBox.Show("Selected files have been deleted successfully.");
        }

  
    }
}