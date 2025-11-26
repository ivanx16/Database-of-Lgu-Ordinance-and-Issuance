namespace YourNamespace
{
    partial class ForgotPasswordForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSendResetLink;
        private System.Windows.Forms.LinkLabel linkBackToLogin;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Label labelEmail;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSendResetLink = new System.Windows.Forms.Button();
            this.linkBackToLogin = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(97, 32);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(277, 32);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Forgot your password?";

            // labelSubtitle
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelSubtitle.Location = new System.Drawing.Point(46, 75);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(380, 23);
            this.labelSubtitle.TabIndex = 1;
            this.labelSubtitle.Text = "Enter your email address to reset your password.";

            // labelEmail
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelEmail.Location = new System.Drawing.Point(46, 117);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(116, 23);
            this.labelEmail.TabIndex = 2;
            this.labelEmail.Text = "Email Address";

            // txtEmail
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(46, 144);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(319, 30);
            this.txtEmail.TabIndex = 3;

            // btnSendResetLink
            this.btnSendResetLink.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSendResetLink.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSendResetLink.ForeColor = System.Drawing.Color.White;
            this.btnSendResetLink.Location = new System.Drawing.Point(46, 192);
            this.btnSendResetLink.Name = "btnSendResetLink";
            this.btnSendResetLink.Size = new System.Drawing.Size(320, 37);
            this.btnSendResetLink.TabIndex = 4;
            this.btnSendResetLink.Text = "Send Reset Link";
            this.btnSendResetLink.UseVisualStyleBackColor = false;
            this.btnSendResetLink.Click += new System.EventHandler(this.btnSendResetLink_Click);

            // linkBackToLogin
            this.linkBackToLogin.AutoSize = true;
            this.linkBackToLogin.Location = new System.Drawing.Point(206, 245);
            this.linkBackToLogin.Name = "linkBackToLogin";
            this.linkBackToLogin.Size = new System.Drawing.Size(88, 16);
            this.linkBackToLogin.TabIndex = 5;
            this.linkBackToLogin.TabStop = true;
            this.linkBackToLogin.Text = "Back to Login";
            this.linkBackToLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBackToLogin_LinkClicked);

            // ForgotPasswordForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 299);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.labelSubtitle);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnSendResetLink);
            this.Controls.Add(this.linkBackToLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ForgotPasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forgot Password";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
