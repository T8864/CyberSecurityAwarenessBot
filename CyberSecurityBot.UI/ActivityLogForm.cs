using System;
using System.Drawing;
using System.Windows.Forms;
using CyberSecurityAwarenessBot.Core;

namespace CyberSecurityBot.UI
{
    public partial class ActivityLogForm : Form
    {
        private Label lblTitle;
        private ListBox lstEntries;
        private Button btnClose;

        public ActivityLogForm()
        {
            SetupForm();
            LoadEntries();
        }

        private void SetupForm()
        {
            this.Text = "Activity Log";
            this.Size = new Size(500, 450);
            this.BackColor = ColorTranslator.FromHtml("#F0F8FF");
            this.Font = new Font("Segoe UI", 10);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label();
            lblTitle.Text = "Recent Activity";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
            lblTitle.Location = new Point(15, 15);
            lblTitle.Size = new Size(460, 30);

            lstEntries = new ListBox();
            lstEntries.Location = new Point(15, 55);
            lstEntries.Size = new Size(460, 300);
            lstEntries.Font = new Font("Segoe UI", 10);
            lstEntries.BackColor = ColorTranslator.FromHtml("#F5F5F5");
            lstEntries.BorderStyle = BorderStyle.FixedSingle;

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.BackColor = ColorTranslator.FromHtml("#DC3545");
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Location = new Point(355, 365);
            btnClose.Size = new Size(120, 32);
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitle);
            this.Controls.Add(lstEntries);
            this.Controls.Add(btnClose);
        }

        private void LoadEntries()
        {
            lstEntries.Items.Clear();

            var entries = ActivityLogger.GetRecent(10);

            if (entries.Count == 0)
            {
                lstEntries.Items.Add("No activity recorded yet.");
                return;
            }

            foreach (string entry in entries)
            {
                lstEntries.Items.Add(entry);
            }
        }
    }
}
