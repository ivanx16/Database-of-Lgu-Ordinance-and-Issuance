using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using BarangayIssuance;
using System.IO;

namespace Database_of_Lgu_Ordinance_and_Issuance
{
    public partial class Dashboard : Form
    {
        private Panel panelDashboard;

        public Dashboard()
        {
            InitializeComponent();
            
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblUsername.Text = Session.CurrentUsername;
            this.WindowState = FormWindowState.Maximized;
            this.Resize += Dashboard_Resize;
            lblUsername.Text = Session.CurrentUsername;
            UpdateDashboardCounts();
            UpdateTypeCounts();
            LoadTrendsChart();

            timerDateTime.Start();
            UpdateDateTime(); // show immediately on load

            LoadProfilePicture();

            comboUserProfile.SelectedIndex = 0;

            string imagePath = Path.Combine(Application.StartupPath, "profile.jpg");

            CenterMainPanel();
        }


        private void Dashboard_Resize(object sender, EventArgs e)
        {
            CenterMainPanel();
        }

        private void CenterMainPanel()
        {
            if (panelDashboard != null)
            {
                int x = (this.ClientSize.Width - panelDashboard.Width) / 2;
                int y = (this.ClientSize.Height - panelDashboard.Height) / 2;
                panelDashboard.Location = new Point(Math.Max(0, x), Math.Max(0, y));
            }
        }

     


        private void UpdateDashboardCounts()
        {
            string connectionString = "server=localhost;database=addmodule;uid=root;pwd=Ivandioso16;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmdTotal = new MySqlCommand("SELECT COUNT(*) FROM Modules", conn);
                    int total = Convert.ToInt32(cmdTotal.ExecuteScalar());
                    labelModulesCount.Text = total.ToString();
                    labelModules.Text = "\ud83d\udcc1 Total Issuance";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving counts:\n" + ex.Message);
                }
            }
        }

        private void UpdateTypeCounts()
        {
            string connectionString = "server=localhost;database=addmodule;uid=root;pwd=Ivandioso16;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmdOrdinance = new MySqlCommand("SELECT COUNT(*) FROM Modules WHERE type='Ordinance'", conn);
                    labelOrdinanceCount.Text = cmdOrdinance.ExecuteScalar().ToString();

                    MySqlCommand cmdExecutive = new MySqlCommand("SELECT COUNT(*) FROM Modules WHERE type='Executive Order'", conn);
                    labelExecutiveCount.Text = cmdExecutive.ExecuteScalar().ToString();

                    MySqlCommand cmdMemorandum = new MySqlCommand("SELECT COUNT(*) FROM Modules WHERE type='Memorandum'", conn);
                    labelMemorandumCount.Text = cmdMemorandum.ExecuteScalar().ToString();

                    MySqlCommand cmdOfficeOrder = new MySqlCommand("SELECT COUNT(*) FROM Modules WHERE type='Office Order'", conn);
                    labelOfficeOrderCount.Text = cmdOfficeOrder.ExecuteScalar().ToString();

                    MySqlCommand cmdResolution = new MySqlCommand("SELECT COUNT(*) FROM Modules WHERE type='Resolution'", conn);
                    labelResolutionCount.Text = cmdResolution.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving type counts:\n" + ex.Message);
                }
            }
        }

        private void LoadTrendsChart()
        {
            chartTrends.Series.Clear();
            chartTrends.ChartAreas.Clear();
            chartTrends.Titles.Clear();

            ChartArea chartArea = new ChartArea("MainArea");
            chartTrends.ChartAreas.Add(chartArea);

            Series ordinanceSeries = new Series("Ordinances") { ChartType = SeriesChartType.Column, Color = Color.RoyalBlue };
            Series executiveOrderSeries = new Series("Executive Orders") { ChartType = SeriesChartType.Column, Color = Color.Green };
            Series memorandumSeries = new Series("Memorandums") { ChartType = SeriesChartType.Column, Color = Color.Yellow };
            Series officeOrderSeries = new Series("Office Orders") { ChartType = SeriesChartType.Column, Color = Color.OrangeRed };
            Series resolutionSeries = new Series("Resolutions") { ChartType = SeriesChartType.Column, Color = Color.MediumPurple };

            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            int[] ordinanceCounts = new int[12];
            int[] executiveOrderCounts = new int[12];
            int[] memorandumCounts = new int[12];
            int[] officeOrderCounts = new int[12];
            int[] resolutionCounts = new int[12];

            string connectionString = "server=localhost;database=addmodule;user=root;password=Ivandioso16;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        MONTH(date) AS Month,
                        type,
                        COUNT(*) AS Count
                    FROM modules
                    WHERE type IN ('Ordinance', 'Executive Order', 'Memorandum', 'Office Order', 'Resolution')
                    GROUP BY MONTH(date), type;";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int monthIndex = Convert.ToInt32(reader["Month"]) - 1;
                        string type = reader["type"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);

                        switch (type)
                        {
                            case "Ordinance": ordinanceCounts[monthIndex] = count; break;
                            case "Executive Order": executiveOrderCounts[monthIndex] = count; break;
                            case "Memorandum": memorandumCounts[monthIndex] = count; break;
                            case "Office Order": officeOrderCounts[monthIndex] = count; break;
                            case "Resolution": resolutionCounts[monthIndex] = count; break;
                        }
                    }
                }
            }

            for (int i = 0; i < 12; i++)
            {
                ordinanceSeries.Points.AddXY(months[i], ordinanceCounts[i]);
                executiveOrderSeries.Points.AddXY(months[i], executiveOrderCounts[i]);
                memorandumSeries.Points.AddXY(months[i], memorandumCounts[i]);
                officeOrderSeries.Points.AddXY(months[i], officeOrderCounts[i]);
                resolutionSeries.Points.AddXY(months[i], resolutionCounts[i]);
            }

            chartTrends.Series.Add(ordinanceSeries);
            chartTrends.Series.Add(executiveOrderSeries);
            chartTrends.Series.Add(memorandumSeries);
            chartTrends.Series.Add(officeOrderSeries);
            chartTrends.Series.Add(resolutionSeries);

            chartTrends.Titles.Add("Issuance Upload Analytics");
        }

        private void buttonUpload_Click_1(object sender, EventArgs e)
        {
            try
            {
                Upload_File uploadForm = new Upload_File();
                uploadForm.StartPosition = FormStartPosition.CenterParent;

                if (uploadForm.ShowDialog(this) == DialogResult.OK)
                {
                    UpdateDashboardCounts();
                    UpdateTypeCounts();
                    LoadTrendsChart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open Upload File form:\n" + ex.Message);
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            Settings settingsForm = new Settings();
            settingsForm.StartPosition = FormStartPosition.CenterParent;

            if (settingsForm.ShowDialog(this) == DialogResult.OK)
            {
                UpdateDashboardCounts();
                UpdateTypeCounts();
                LoadTrendsChart();
            }
        }

        private void Archive_Click(object sender, EventArgs e)
        {
            Archived_Files archivedForm = new Archived_Files();
            archivedForm.StartPosition = FormStartPosition.CenterParent;

            if (archivedForm.ShowDialog(this) == DialogResult.OK)
            {
                UpdateDashboardCounts();
                UpdateTypeCounts();
                LoadTrendsChart();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewFile viewForm = new ViewFile();
            viewForm.Show();
        }

        private void comboUserProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboUserProfile.SelectedItem.ToString() == "Log out")
            {
                DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Hide();
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                }
                else
                {
                    comboUserProfile.SelectedIndex = 0;
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            UpdateDashboardCounts();
            UpdateTypeCounts();
            LoadTrendsChart();
        }
        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void UpdateDateTime()
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, MMMM dd yyyy hh:mm:ss tt");
        }

        private void LoadProfilePicture()
        {
            string imagePath = Path.Combine(Application.StartupPath, "profile.jpg");

            if (File.Exists(imagePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    using (MemoryStream ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        ms.Position = 0;
                        pictureBoxDashboardProfile.Image = Image.FromStream(ms);
                    }

                    pictureBoxDashboardProfile.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading profile image: " + ex.Message);
                }
            }
            else
            {
                pictureBoxDashboardProfile.Image = null;
            }
        }



        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void panelModules_Paint(object sender, PaintEventArgs e) { }
        private void panelOrdinances_Paint(object sender, PaintEventArgs e) { }
        private void panelIssuances_Paint(object sender, PaintEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void chartTrends_Click(object sender, EventArgs e) { }

        
    }
}
