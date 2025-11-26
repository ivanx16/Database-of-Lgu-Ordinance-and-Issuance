using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Database_of_Lgu_Ordinance_and_Issuance
{
    public partial class ViewFile : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private string connStr = "server=localhost;uid=root;pwd=Ivandioso16;database=addmodule;";

        public ViewFile()
        {
            InitializeComponent();
            PopulateFilterBox();
            LoadData();
            this.dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void PopulateFilterBox()
        {
            FilterBox.Items.AddRange(new string[] { "All", "Ordinance", "Executive Order", "Memorandum", "Office Order", "Resolution" });
            FilterBox.DropDownStyle = ComboBoxStyle.DropDownList;
            FilterBox.SelectedIndex = 0;
        }

        private void LoadData(string query = "SELECT title, issuance_number, date, remarks, file_path, type FROM modules WHERE archived IS NULL OR archived = 0 ORDER BY id DESC")
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    foreach (DataRow row in table.Rows)
                    {
                        // Trim title spaces
                        if (row["title"] != DBNull.Value)
                        {
                            row["title"] = row["title"].ToString().Trim();
                        }

                        if (row["date"] != DBNull.Value)
                        {
                            if (DateTime.TryParse(row["date"].ToString(), out DateTime dateValue))
                            {
                                row["date"] = dateValue;
                            }
                            else
                            {
                                row["date"] = DBNull.Value;
                            }
                        }
                    }

                    bindingSource.DataSource = table;
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.DataSource = bindingSource;

                    SetColumnHeaders();
                    AddViewButton();

                    dataGridView1.RowTemplate.Height = 30;
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dataGridView1.RowTemplate.Height = 35;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void AddViewButton()
        {
            if (!dataGridView1.Columns.Contains("View"))
            {
                DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn
                {
                    HeaderText = "",
                    Name = "View",
                    Text = "View",
                    UseColumnTextForButtonValue = true,
                    Width = 60
                };
                dataGridView1.Columns.Add(viewButton);
            }
        }

        private void SetColumnHeaders()
        {
            dataGridView1.RowHeadersVisible = false; // hide row headers (removes the first column space)

            if (dataGridView1.Columns.Contains("title"))
            {
                dataGridView1.Columns["title"].HeaderText = "Title";
                dataGridView1.Columns["title"].Width = 270;
            }

            if (dataGridView1.Columns.Contains("issuance_number"))
            {
                dataGridView1.Columns["issuance_number"].HeaderText = "Issuance Number";
                dataGridView1.Columns["issuance_number"].Width = 165;
            }

            if (dataGridView1.Columns.Contains("date"))
            {
                dataGridView1.Columns["date"].HeaderText = "Date";
                dataGridView1.Columns["date"].DefaultCellStyle.Format = "MM/dd/yyyy";
                dataGridView1.Columns["date"].Width = 150;
            }

            if (dataGridView1.Columns.Contains("remarks"))
            {
                dataGridView1.Columns["remarks"].HeaderText = "Remarks";
                dataGridView1.Columns["remarks"].Width = 225;
            }

            if (dataGridView1.Columns.Contains("file_path"))
            {
                dataGridView1.Columns["file_path"].HeaderText = "File Path";
                dataGridView1.Columns["file_path"].DefaultCellStyle.ForeColor = Color.Blue;
                dataGridView1.Columns["file_path"].DefaultCellStyle.Font = new Font("Arial", 8F, FontStyle.Underline);
                dataGridView1.Columns["file_path"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns["file_path"].Width = 320;
            }

            if (dataGridView1.Columns.Contains("type"))
            {
                dataGridView1.Columns["type"].HeaderText = "Type";
                dataGridView1.Columns["type"].Width = 150;
            }

            // Prevent resizing
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "View")
            {
                string filePath = dataGridView1.Rows[e.RowIndex].Cells["file_path"].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(filePath) && File.Exists(filePath))
                {
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
                else
                {
                    MessageBox.Show("File not found or invalid file path:\n" + filePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ViewFileSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = ViewFileSearch.Text.Trim().Replace("'", "''");

            if (bindingSource.DataSource is DataTable table)
            {
                string filter = string.Format(
                    "title LIKE '%{0}%' OR issuance_number LIKE '%{0}%' OR remarks LIKE '%{0}%' OR type LIKE '%{0}%'",
                    searchTerm);
                bindingSource.Filter = filter;
            }
        }

        private void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = FilterBox.SelectedItem?.ToString();
            string query = "SELECT title, issuance_number, date, remarks, file_path, type FROM modules WHERE archived IS NULL OR archived = 0";

            if (!string.IsNullOrEmpty(selectedFilter) && selectedFilter != "All")
            {
                query += $" AND type = '{selectedFilter}'";
            }

            query += " ORDER BY id DESC";
            LoadData(query);
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
    }
}
