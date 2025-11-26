using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Database_of_Lgu_Ordinance_and_Issuance
{
    public partial class Upload_File : Form
    {
        public static Upload_File Instance { get; private set; }

        private MySqlConnection conn;
        private string connStr = "server=localhost;database=addmodule;user=root;password=Ivandioso16;";

        public Upload_File()
        {
            InitializeComponent();
            conn = new MySqlConnection(connStr);

            ViewFile.AllowUserToAddRows = false;
            ViewFile.RowHeadersVisible = false;

            Instance = this;
        }

        private void Upload_File_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadFilterTypes();
        }

        public void LoadData(string filter = "", string search = "")
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string query = "SELECT id, title, issuance_number, date, remarks, file_path, type FROM modules WHERE archived = 0 ORDER BY id DESC";

                if (!string.IsNullOrEmpty(filter) && filter != "All")
                    query = "SELECT id, title, issuance_number, date, remarks, file_path, type FROM modules WHERE archived = 0 AND type = @filter ORDER BY id DESC";

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Replace("ORDER BY id DESC", "");
                    query += (query.Contains("WHERE") ? " AND" : " WHERE") +
                             " (title LIKE @search OR issuance_number LIKE @search OR remarks LIKE @search) ORDER BY id DESC";
                }

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(filter) && filter != "All")
                        cmd.Parameters.AddWithValue("@filter", filter);
                    if (!string.IsNullOrEmpty(search))
                        cmd.Parameters.AddWithValue("@search", "%" + search + "%");

                    DataTable dt = new DataTable();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    ViewFile.Columns.Clear();
                    ViewFile.DataSource = dt;

                    ViewFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    FormatDataGridView();
                    AddActionButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void LoadFilterTypes()
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All");
            cmbFilter.Items.Add("Ordinance");

            // Manually ensure 'Executive Order' is included
            if (!cmbFilter.Items.Contains("Executive Order"))
                cmbFilter.Items.Add("Executive Order");

            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string query = "SELECT DISTINCT type FROM modules";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader.GetString("type");
                            if (!cmbFilter.Items.Contains(type))
                            {
                                cmbFilter.Items.Add(type);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading filter types: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

            cmbFilter.SelectedIndex = 0;
        }



        private void FormatDataGridView()
        {
            ViewFile.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            ViewFile.RowTemplate.Height = 35;
            ViewFile.AllowUserToResizeColumns = false;
            ViewFile.AllowUserToResizeRows = false;

            if (ViewFile.Columns.Contains("id"))
                ViewFile.Columns["id"].Visible = false;

            ViewFile.Columns["title"].HeaderText = "Title";
            ViewFile.Columns["title"].Width = 255;

            ViewFile.Columns["issuance_number"].HeaderText = "Issuance Number";
            ViewFile.Columns["issuance_number"].Width = 155;

            ViewFile.Columns["date"].HeaderText = "Date";
            ViewFile.Columns["date"].Width = 150;

            ViewFile.Columns["remarks"].HeaderText = "Remarks";
            ViewFile.Columns["remarks"].Width = 225;

            ViewFile.Columns["file_path"].HeaderText = "File Path";
            ViewFile.Columns["file_path"].DefaultCellStyle.ForeColor = Color.Blue;
            ViewFile.Columns["file_path"].DefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Underline);
            ViewFile.Columns["file_path"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            ViewFile.Columns["file_path"].Width = 270;

            ViewFile.Columns["type"].HeaderText = "Type";
            ViewFile.Columns["type"].Width = 120;
        }

        private void AddActionButtons()
        {
            if (!ViewFile.Columns.Contains("ViewColumn"))
            {
                DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn
                {
                    Name = "ViewColumn",
                    HeaderText = "View",
                    Text = "View",
                    UseColumnTextForButtonValue = true
                };
                ViewFile.Columns.Add(viewButton);
            }

            if (!ViewFile.Columns.Contains("EditColumn"))
            {
                DataGridViewButtonColumn editButton = new DataGridViewButtonColumn
                {
                    Name = "EditColumn",
                    HeaderText = "Edit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true
                };
                ViewFile.Columns.Add(editButton);
            }

            if (!ViewFile.Columns.Contains("ArchiveColumn"))
            {
                DataGridViewButtonColumn archiveButton = new DataGridViewButtonColumn
                {
                    Name = "ArchiveColumn",
                    HeaderText = "Archive",
                    Text = "Archive",
                    UseColumnTextForButtonValue = true
                };
                ViewFile.Columns.Add(archiveButton);
            }
        }

        private void ViewFile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(ViewFile.Rows[e.RowIndex].Cells["id"].Value);

            if (ViewFile.Columns[e.ColumnIndex].Name == "ViewColumn")
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT file_path FROM modules WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        string filePath = cmd.ExecuteScalar()?.ToString();

                        if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                        {
                            System.Diagnostics.Process.Start(filePath);
                        }
                        else
                        {
                            MessageBox.Show("File not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error opening file: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            else if (ViewFile.Columns[e.ColumnIndex].Name == "EditColumn")
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "SELECT title, issuance_number, date, remarks, type, file_path FROM modules WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string title = reader.GetString("title");
                                string issuanceNumber = reader.GetString("issuance_number");
                                string remarks = reader.GetString("remarks");
                                string type = reader.GetString("type");
                                string filePath = reader.GetString("file_path");

                                reader.Close(); // 🔴 Close it here before opening the form

                                AddModule editForm = new AddModule(id, title, issuanceNumber, remarks, type, filePath);
                                editForm.ShowDialog();
                            }
                            else
                            {
                                reader.Close(); // Make sure to close even if not found
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error editing data: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }

            else if (ViewFile.Columns[e.ColumnIndex].Name == "ArchiveColumn")
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = "UPDATE modules SET archived = 1 WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("File archived successfully.");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error archiving file: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(cmbFilter.SelectedItem.ToString(), txtSearch.Text);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(cmbFilter.SelectedItem.ToString(), txtSearch.Text);
        }

        private void Upload1_Click(object sender, EventArgs e)
        {
            AddModule addForm = new AddModule();
            addForm.ShowDialog();
        }

        public void RefreshTable()
        {
            LoadData(cmbFilter.SelectedItem.ToString(), txtSearch.Text);
        }
    }
}
