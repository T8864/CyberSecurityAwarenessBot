using System;
using System.Drawing;
using System.Windows.Forms;
using CyberSecurityAwarenessBot.Core;

namespace CyberSecurityBot.UI
{
    public partial class TaskForm : Form
    {
        private Label lblTitle;
        private ListBox lstTasks;

        private Label lblTaskTitle;
        private TextBox txtTaskTitle;

        private Label lblTaskDescription;
        private TextBox txtTaskDescription;

        private CheckBox chkSetReminder;
        private DateTimePicker dtpReminder;

        private Button btnAddTask;
        private Button btnMarkComplete;
        private Button btnDelete;
        private Button btnClose;

        public TaskForm()
        {
            SetupForm();
            TaskDatabase.InitializeDatabase();
            LoadTasks();
        }

        private void SetupForm()
        {
            this.Text = "Task Assistant";
            this.Size = new Size(600, 560);
            this.BackColor = ColorTranslator.FromHtml("#F0F8FF");
            this.Font = new Font("Segoe UI", 10);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label();
            lblTitle.Text = "Cybersecurity Task Assistant";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
            lblTitle.Location = new Point(15, 15);
            lblTitle.Size = new Size(550, 30);

            // Task list
            lstTasks = new ListBox();
            lstTasks.Location = new Point(15, 55);
            lstTasks.Size = new Size(555, 180);
            lstTasks.Font = new Font("Segoe UI", 10);
            lstTasks.BackColor = ColorTranslator.FromHtml("#F5F5F5");
            lstTasks.BorderStyle = BorderStyle.FixedSingle;

            // Title input
            lblTaskTitle = new Label();
            lblTaskTitle.Text = "Task Title:";
            lblTaskTitle.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
            lblTaskTitle.Location = new Point(15, 250);
            lblTaskTitle.Size = new Size(100, 25);

            txtTaskTitle = new TextBox();
            txtTaskTitle.Location = new Point(120, 248);
            txtTaskTitle.Size = new Size(450, 25);
            txtTaskTitle.BorderStyle = BorderStyle.FixedSingle;

            // Description input
            lblTaskDescription = new Label();
            lblTaskDescription.Text = "Description:";
            lblTaskDescription.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
            lblTaskDescription.Location = new Point(15, 290);
            lblTaskDescription.Size = new Size(100, 25);

            txtTaskDescription = new TextBox();
            txtTaskDescription.Location = new Point(120, 288);
            txtTaskDescription.Size = new Size(450, 60);
            txtTaskDescription.Multiline = true;
            txtTaskDescription.BorderStyle = BorderStyle.FixedSingle;

            // Reminder checkbox + date picker
            chkSetReminder = new CheckBox();
            chkSetReminder.Text = "Set Reminder";
            chkSetReminder.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
            chkSetReminder.Location = new Point(15, 365);
            chkSetReminder.Size = new Size(120, 25);
            chkSetReminder.CheckedChanged += ChkSetReminder_CheckedChanged;

            dtpReminder = new DateTimePicker();
            dtpReminder.Location = new Point(140, 363);
            dtpReminder.Size = new Size(200, 25);
            dtpReminder.Format = DateTimePickerFormat.Short;
            dtpReminder.Enabled = false;

            // Add task button
            btnAddTask = new Button();
            btnAddTask.Text = "Add Task";
            btnAddTask.BackColor = ColorTranslator.FromHtml("#007BFF");
            btnAddTask.ForeColor = Color.White;
            btnAddTask.FlatStyle = FlatStyle.Flat;
            btnAddTask.Location = new Point(15, 405);
            btnAddTask.Size = new Size(120, 32);
            btnAddTask.Click += BtnAddTask_Click;

            // Mark complete button
            btnMarkComplete = new Button();
            btnMarkComplete.Text = "Mark Complete";
            btnMarkComplete.BackColor = ColorTranslator.FromHtml("#28A745");
            btnMarkComplete.ForeColor = Color.White;
            btnMarkComplete.FlatStyle = FlatStyle.Flat;
            btnMarkComplete.Location = new Point(145, 405);
            btnMarkComplete.Size = new Size(140, 32);
            btnMarkComplete.Click += BtnMarkComplete_Click;

            // Delete button
            btnDelete = new Button();
            btnDelete.Text = "Delete Task";
            btnDelete.BackColor = ColorTranslator.FromHtml("#DC3545");
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Location = new Point(295, 405);
            btnDelete.Size = new Size(120, 32);
            btnDelete.Click += BtnDelete_Click;

            // Close button
            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.BackColor = ColorTranslator.FromHtml("#6C757D");
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Location = new Point(450, 405);
            btnClose.Size = new Size(120, 32);
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitle);
            this.Controls.Add(lstTasks);
            this.Controls.Add(lblTaskTitle);
            this.Controls.Add(txtTaskTitle);
            this.Controls.Add(lblTaskDescription);
            this.Controls.Add(txtTaskDescription);
            this.Controls.Add(chkSetReminder);
            this.Controls.Add(dtpReminder);
            this.Controls.Add(btnAddTask);
            this.Controls.Add(btnMarkComplete);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClose);
        }

        private void ChkSetReminder_CheckedChanged(object sender, EventArgs e)
        {
            dtpReminder.Enabled = chkSetReminder.Checked;
        }

        // Loads all tasks from the database into the list box
        private void LoadTasks()
        {
            lstTasks.Items.Clear();

            var tasks = TaskDatabase.GetAllTasks();

            if (tasks.Count == 0)
            {
                lstTasks.Items.Add("No tasks yet. Add one below.");
                return;
            }

            foreach (TaskItem task in tasks)
            {
                string status = task.IsCompleted ? "[Done]" : "[Pending]";
                string reminderText = task.ReminderDate.HasValue
                    ? $" - Reminder: {task.ReminderDate.Value:dd MMM yyyy}"
                    : "";

                string displayText = $"{status} #{task.Id}: {task.Title} - {task.Description}{reminderText}";
                lstTasks.Items.Add(displayText);
            }
        }

        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            string title = txtTaskTitle.Text.Trim();
            string description = txtTaskDescription.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a task title.", "Title Required");
                return;
            }

            DateTime? reminderDate = chkSetReminder.Checked ? dtpReminder.Value : (DateTime?)null;

            int newId = TaskDatabase.AddTask(title, description, reminderDate);

            string logMessage = reminderDate.HasValue
                ? $"Task added: '{title}' (Reminder set for {reminderDate.Value:dd MMM yyyy})"
                : $"Task added: '{title}' (no reminder set)";

            ActivityLogger.AddEntry(logMessage);

            // Clear inputs
            txtTaskTitle.Clear();
            txtTaskDescription.Clear();
            chkSetReminder.Checked = false;
            dtpReminder.Value = DateTime.Now;

            LoadTasks();
        }

        private void BtnMarkComplete_Click(object sender, EventArgs e)
        {
            int? selectedId = GetSelectedTaskId();
            if (selectedId == null)
            {
                MessageBox.Show("Please select a task first.", "No Task Selected");
                return;
            }

            TaskDatabase.MarkComplete(selectedId.Value);
            ActivityLogger.AddEntry($"Task #{selectedId.Value} marked as completed");

            LoadTasks();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int? selectedId = GetSelectedTaskId();
            if (selectedId == null)
            {
                MessageBox.Show("Please select a task first.", "No Task Selected");
                return;
            }

            TaskDatabase.DeleteTask(selectedId.Value);
            ActivityLogger.AddEntry($"Task #{selectedId.Value} deleted");

            LoadTasks();
        }

        // Extracts the task Id from the selected list item text (format: "[Status] #Id: ...")
        private int? GetSelectedTaskId()
        {
            if (lstTasks.SelectedItem == null)
                return null;

            string selectedText = lstTasks.SelectedItem.ToString();

            int hashIndex = selectedText.IndexOf('#');
            int colonIndex = selectedText.IndexOf(':');

            if (hashIndex == -1 || colonIndex == -1 || colonIndex <= hashIndex)
                return null;

            string idText = selectedText.Substring(hashIndex + 1, colonIndex - hashIndex - 1);

            if (int.TryParse(idText, out int id))
                return id;

            return null;
        }
    }
}