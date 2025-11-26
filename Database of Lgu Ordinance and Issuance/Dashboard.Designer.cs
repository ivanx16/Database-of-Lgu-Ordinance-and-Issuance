using System.Windows.Forms;
using System;
using System.Drawing;


namespace Database_of_Lgu_Ordinance_and_Issuance
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonIssuance = new System.Windows.Forms.Button();
            this.Archive = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.panelModules = new System.Windows.Forms.Panel();
            this.labelModulesCount = new System.Windows.Forms.Label();
            this.labelModules = new System.Windows.Forms.Label();
            this.chartTrends = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.comboUserProfile = new System.Windows.Forms.ComboBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.panelTopRight = new System.Windows.Forms.Panel();
            this.pictureBoxDashboardProfile = new System.Windows.Forms.PictureBox();
            this.buttonViewlist = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panelOrdinance = new System.Windows.Forms.Panel();
            this.labelOrdinanceCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelExecutive = new System.Windows.Forms.Panel();
            this.labelExecutiveCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelMemorandum = new System.Windows.Forms.Panel();
            this.labelMemorandumCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panelOfficeOrder = new System.Windows.Forms.Panel();
            this.labelOfficeOrderCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panelResolution = new System.Windows.Forms.Panel();
            this.labelResolutionCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.panelModules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrends)).BeginInit();
            this.panelTopRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDashboardProfile)).BeginInit();
            this.panelOrdinance.SuspendLayout();
            this.panelExecutive.SuspendLayout();
            this.panelMemorandum.SuspendLayout();
            this.panelOfficeOrder.SuspendLayout();
            this.panelResolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(530, 98);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(244, 50);
            this.labelTitle.TabIndex = 9;
            this.labelTitle.Text = "DASHBOARD";
            // 
            // buttonIssuance
            // 
            this.buttonIssuance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonIssuance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonIssuance.FlatAppearance.BorderSize = 0;
            this.buttonIssuance.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonIssuance.Location = new System.Drawing.Point(26, 287);
            this.buttonIssuance.Name = "buttonIssuance";
            this.buttonIssuance.Size = new System.Drawing.Size(184, 60);
            this.buttonIssuance.TabIndex = 8;
            this.buttonIssuance.Text = "📤 Add Issuance";
            this.buttonIssuance.UseVisualStyleBackColor = false;
            this.buttonIssuance.Click += new System.EventHandler(this.buttonUpload_Click_1);
            // 
            // Archive
            // 
            this.Archive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Archive.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Archive.FlatAppearance.BorderSize = 0;
            this.Archive.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Archive.Location = new System.Drawing.Point(26, 454);
            this.Archive.Name = "Archive";
            this.Archive.Size = new System.Drawing.Size(184, 60);
            this.Archive.TabIndex = 7;
            this.Archive.Text = "🗂 Archives";
            this.Archive.UseVisualStyleBackColor = false;
            this.Archive.Click += new System.EventHandler(this.Archive_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonSettings.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSettings.Location = new System.Drawing.Point(26, 536);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(184, 60);
            this.buttonSettings.TabIndex = 6;
            this.buttonSettings.Text = "⚙️ Settings";
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // panelModules
            // 
            this.panelModules.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelModules.BackColor = System.Drawing.Color.White;
            this.panelModules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelModules.Controls.Add(this.labelModulesCount);
            this.panelModules.Controls.Add(this.labelModules);
            this.panelModules.Location = new System.Drawing.Point(273, 170);
            this.panelModules.Name = "panelModules";
            this.panelModules.Padding = new System.Windows.Forms.Padding(5);
            this.panelModules.Size = new System.Drawing.Size(134, 70);
            this.panelModules.TabIndex = 0;
            this.panelModules.Paint += new System.Windows.Forms.PaintEventHandler(this.panelModules_Paint);
            // 
            // labelModulesCount
            // 
            this.labelModulesCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelModulesCount.ForeColor = System.Drawing.Color.Black;
            this.labelModulesCount.Location = new System.Drawing.Point(14, 30);
            this.labelModulesCount.Name = "labelModulesCount";
            this.labelModulesCount.Size = new System.Drawing.Size(117, 30);
            this.labelModulesCount.TabIndex = 2;
            this.labelModulesCount.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelModules
            // 
            this.labelModules.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelModules.ForeColor = System.Drawing.Color.Black;
            this.labelModules.Location = new System.Drawing.Point(10, 10);
            this.labelModules.Name = "labelModules";
            this.labelModules.Size = new System.Drawing.Size(100, 23);
            this.labelModules.TabIndex = 0;
            this.labelModules.Text = "📁 Total Issuance";
            // 
            // chartTrends
            // 
            this.chartTrends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea6.Name = "ChartArea1";
            this.chartTrends.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chartTrends.Legends.Add(legend6);
            this.chartTrends.Location = new System.Drawing.Point(250, 256);
            this.chartTrends.Name = "chartTrends";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chartTrends.Series.Add(series6);
            this.chartTrends.Size = new System.Drawing.Size(901, 400);
            this.chartTrends.TabIndex = 13;
            this.chartTrends.Text = "chart1";
            this.chartTrends.Click += new System.EventHandler(this.chartTrends_Click);
            // 
            // comboUserProfile
            // 
            this.comboUserProfile.Items.AddRange(new object[] {
            "Admin",
            "Log out"});
            this.comboUserProfile.Location = new System.Drawing.Point(459, 15);
            this.comboUserProfile.Name = "comboUserProfile";
            this.comboUserProfile.Size = new System.Drawing.Size(130, 24);
            this.comboUserProfile.TabIndex = 4;
            this.comboUserProfile.TabStop = false;
            this.comboUserProfile.SelectedIndexChanged += new System.EventHandler(this.comboUserProfile_SelectedIndexChanged);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.Location = new System.Drawing.Point(419, 8);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(30, 32);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "⟳";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // panelTopRight
            // 
            this.panelTopRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTopRight.BackColor = System.Drawing.Color.Transparent;
            this.panelTopRight.Controls.Add(this.lblDateTime);
            this.panelTopRight.Controls.Add(this.buttonRefresh);
            this.panelTopRight.Controls.Add(this.comboUserProfile);
            this.panelTopRight.Location = new System.Drawing.Point(612, 12);
            this.panelTopRight.Name = "panelTopRight";
            this.panelTopRight.Size = new System.Drawing.Size(603, 60);
            this.panelTopRight.TabIndex = 14;
            // 
            // pictureBoxDashboardProfile
            // 
            this.pictureBoxDashboardProfile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBoxDashboardProfile.Location = new System.Drawing.Point(72, 138);
            this.pictureBoxDashboardProfile.Name = "pictureBoxDashboardProfile";
            this.pictureBoxDashboardProfile.Size = new System.Drawing.Size(104, 77);
            this.pictureBoxDashboardProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDashboardProfile.TabIndex = 46;
            this.pictureBoxDashboardProfile.TabStop = false;
            // 
            // buttonViewlist
            // 
            this.buttonViewlist.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonViewlist.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonViewlist.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonViewlist.Location = new System.Drawing.Point(26, 371);
            this.buttonViewlist.Name = "buttonViewlist";
            this.buttonViewlist.Size = new System.Drawing.Size(184, 60);
            this.buttonViewlist.TabIndex = 15;
            this.buttonViewlist.Text = "👁️ View List";
            this.buttonViewlist.UseVisualStyleBackColor = false;
            this.buttonViewlist.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(133, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 35);
            this.label1.TabIndex = 39;
            this.label1.Text = "BARANGAY ISSUANCE RECORD SYSTEM";
            // 
            // panelOrdinance
            // 
            this.panelOrdinance.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelOrdinance.BackColor = System.Drawing.Color.White;
            this.panelOrdinance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOrdinance.Controls.Add(this.labelOrdinanceCount);
            this.panelOrdinance.Controls.Add(this.label3);
            this.panelOrdinance.Location = new System.Drawing.Point(428, 170);
            this.panelOrdinance.Name = "panelOrdinance";
            this.panelOrdinance.Padding = new System.Windows.Forms.Padding(5);
            this.panelOrdinance.Size = new System.Drawing.Size(125, 70);
            this.panelOrdinance.TabIndex = 3;
            this.panelOrdinance.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // labelOrdinanceCount
            // 
            this.labelOrdinanceCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelOrdinanceCount.ForeColor = System.Drawing.Color.Black;
            this.labelOrdinanceCount.Location = new System.Drawing.Point(14, 30);
            this.labelOrdinanceCount.Name = "labelOrdinanceCount";
            this.labelOrdinanceCount.Size = new System.Drawing.Size(117, 30);
            this.labelOrdinanceCount.TabIndex = 2;
            this.labelOrdinanceCount.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "📜 Ordinance";
            // 
            // panelExecutive
            // 
            this.panelExecutive.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelExecutive.BackColor = System.Drawing.Color.White;
            this.panelExecutive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExecutive.Controls.Add(this.labelExecutiveCount);
            this.panelExecutive.Controls.Add(this.label5);
            this.panelExecutive.Location = new System.Drawing.Point(582, 170);
            this.panelExecutive.Name = "panelExecutive";
            this.panelExecutive.Padding = new System.Windows.Forms.Padding(5);
            this.panelExecutive.Size = new System.Drawing.Size(125, 70);
            this.panelExecutive.TabIndex = 41;
            // 
            // labelExecutiveCount
            // 
            this.labelExecutiveCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelExecutiveCount.ForeColor = System.Drawing.Color.Black;
            this.labelExecutiveCount.Location = new System.Drawing.Point(8, 30);
            this.labelExecutiveCount.Name = "labelExecutiveCount";
            this.labelExecutiveCount.Size = new System.Drawing.Size(117, 30);
            this.labelExecutiveCount.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "🛠️ E.O";
            // 
            // panelMemorandum
            // 
            this.panelMemorandum.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelMemorandum.BackColor = System.Drawing.Color.White;
            this.panelMemorandum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMemorandum.Controls.Add(this.labelMemorandumCount);
            this.panelMemorandum.Controls.Add(this.label7);
            this.panelMemorandum.Location = new System.Drawing.Point(729, 170);
            this.panelMemorandum.Name = "panelMemorandum";
            this.panelMemorandum.Padding = new System.Windows.Forms.Padding(5);
            this.panelMemorandum.Size = new System.Drawing.Size(125, 70);
            this.panelMemorandum.TabIndex = 42;
            // 
            // labelMemorandumCount
            // 
            this.labelMemorandumCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelMemorandumCount.ForeColor = System.Drawing.Color.Black;
            this.labelMemorandumCount.Location = new System.Drawing.Point(8, 30);
            this.labelMemorandumCount.Name = "labelMemorandumCount";
            this.labelMemorandumCount.Size = new System.Drawing.Size(117, 30);
            this.labelMemorandumCount.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(10, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "📝 Memorandum";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(8, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 30);
            this.label8.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(10, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "📁 Total Modules";
            // 
            // panelOfficeOrder
            // 
            this.panelOfficeOrder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelOfficeOrder.BackColor = System.Drawing.Color.White;
            this.panelOfficeOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOfficeOrder.Controls.Add(this.labelOfficeOrderCount);
            this.panelOfficeOrder.Controls.Add(this.label11);
            this.panelOfficeOrder.Location = new System.Drawing.Point(878, 170);
            this.panelOfficeOrder.Name = "panelOfficeOrder";
            this.panelOfficeOrder.Padding = new System.Windows.Forms.Padding(5);
            this.panelOfficeOrder.Size = new System.Drawing.Size(125, 70);
            this.panelOfficeOrder.TabIndex = 43;
            // 
            // labelOfficeOrderCount
            // 
            this.labelOfficeOrderCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelOfficeOrderCount.ForeColor = System.Drawing.Color.Black;
            this.labelOfficeOrderCount.Location = new System.Drawing.Point(8, 30);
            this.labelOfficeOrderCount.Name = "labelOfficeOrderCount";
            this.labelOfficeOrderCount.Size = new System.Drawing.Size(117, 30);
            this.labelOfficeOrderCount.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(10, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 0;
            this.label11.Text = "🏢 Office Order";
            // 
            // panelResolution
            // 
            this.panelResolution.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelResolution.BackColor = System.Drawing.Color.White;
            this.panelResolution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelResolution.Controls.Add(this.labelResolutionCount);
            this.panelResolution.Controls.Add(this.label13);
            this.panelResolution.Location = new System.Drawing.Point(1031, 170);
            this.panelResolution.Name = "panelResolution";
            this.panelResolution.Padding = new System.Windows.Forms.Padding(5);
            this.panelResolution.Size = new System.Drawing.Size(125, 70);
            this.panelResolution.TabIndex = 44;
            // 
            // labelResolutionCount
            // 
            this.labelResolutionCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelResolutionCount.ForeColor = System.Drawing.Color.Black;
            this.labelResolutionCount.Location = new System.Drawing.Point(8, 30);
            this.labelResolutionCount.Name = "labelResolutionCount";
            this.labelResolutionCount.Size = new System.Drawing.Size(117, 30);
            this.labelResolutionCount.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(10, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 0;
            this.label13.Text = "📄 Resolution";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Database_of_Lgu_Ordinance_and_Issuance.Properties.Resources.Untitled_design_removebg_preview1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(41, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(86, 80);
            this.pictureBox1.TabIndex = 45;
            this.pictureBox1.TabStop = false;
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblUsername.Location = new System.Drawing.Point(80, 218);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(83, 37);
            this.lblUsername.TabIndex = 47;
            this.lblUsername.Text = "label2";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDateTime.BackColor = System.Drawing.Color.Transparent;
            this.lblDateTime.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTime.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDateTime.Location = new System.Drawing.Point(149, 7);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(264, 35);
            this.lblDateTime.TabIndex = 48;
            this.lblDateTime.Text = "Date and time";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerDateTime
            // 
            this.timerDateTime.Enabled = true;
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // Dashboard
            // 
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1227, 661);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pictureBoxDashboardProfile);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelResolution);
            this.Controls.Add(this.panelOfficeOrder);
            this.Controls.Add(this.panelMemorandum);
            this.Controls.Add(this.panelExecutive);
            this.Controls.Add(this.panelOrdinance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonViewlist);
            this.Controls.Add(this.chartTrends);
            this.Controls.Add(this.panelModules);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.Archive);
            this.Controls.Add(this.buttonIssuance);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panelTopRight);
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "B";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.panelModules.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTrends)).EndInit();
            this.panelTopRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDashboardProfile)).EndInit();
            this.panelOrdinance.ResumeLayout(false);
            this.panelExecutive.ResumeLayout(false);
            this.panelMemorandum.ResumeLayout(false);
            this.panelOfficeOrder.ResumeLayout(false);
            this.panelResolution.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonIssuance;
        private System.Windows.Forms.Button Archive;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Panel panelModules;
        private System.Windows.Forms.Label labelModules;
        private System.Windows.Forms.Label labelModulesCount;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrends;
        private ComboBox comboUserProfile;
        private Button buttonRefresh;
        private Panel panelTopRight;
        private Button buttonViewlist;
        private Label label1;
        private Panel panelOrdinance;
        private Label labelOrdinanceCount;
        private Label label3;
        private Panel panelExecutive;
        private Label labelExecutiveCount;
        private Label label5;
        private Panel panelMemorandum;
        private Label labelMemorandumCount;
        private Label label7;
        private Label label8;
        private Label label9;
        private Panel panelOfficeOrder;
        private Label labelOfficeOrderCount;
        private Label label11;
        private Panel panelResolution;
        private Label labelResolutionCount;
        private Label label13;
        private PictureBox pictureBox1;
        private PictureBox pictureBoxDashboardProfile;
        private Label lblUsername;
        private Label lblDateTime;
        private Timer timerDateTime;
    }

}
