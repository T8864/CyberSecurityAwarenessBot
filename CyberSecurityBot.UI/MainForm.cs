using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CyberSecurityAwarenessBot.Core;

namespace CyberSecurityBot.UI
{
    public partial class MainForm : Form
    {
        private Label lblLogo;
        private Label lblNamePrompt;
        private TextBox txtName;
        private Button btnStart;
        private Button btnQuiz;
        private Button btnTasks;
        private Panel pnlChatContainer;
        private FlowLayoutPanel flowChat;
        private TextBox txtInput;
        private Button btnSend;
        private Button btnClear;
        private Button btnExit;
        private Button btnActivityLog;
        private Panel pnlHeader;
        private Panel pnlInput;

        private string userName = "";
        private MemoryManager memory = new MemoryManager();
        private SentimentDetector sentimentDetector = new SentimentDetector();

        private static readonly string[] cyberTopics = { "password", "phishing", "scam", "privacy", "malware" };

        // Typing indicator state
        private Panel typingBubble;
        private Label lblTyping;
        private System.Windows.Forms.Timer typingDotsTimer;
        private System.Windows.Forms.Timer typingDelayTimer;
        private int dotCount = 0;
        private string pendingResponse = "";

        public MainForm()
        {
            InitializeComponent();
            SetupForm();
            PlayVoiceGreeting();
        }

        private void PlayVoiceGreeting()
        {
            string audioPath = "assets\\greeting.wav";
            AudioPlayer player = new AudioPlayer(audioPath);
            player.PlayGreeting();
        }

        private void SetupForm()
        {
            // Form settings
            this.Text = "Cybersecurity Awareness Bot";
            this.Size = new Size(800, 620);
            this.BackColor = ColorTranslator.FromHtml("#F0F8FF");
            this.Font = new Font("Segoe UI", 10);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Header panel
            pnlHeader = new Panel();
            pnlHeader.BackColor = ColorTranslator.FromHtml("#1B3A6B");
            pnlHeader.Size = new Size(800, 130);
            pnlHeader.Location = new Point(0, 0);

            // Logo label using DisplayHelper
            lblLogo = new Label();
            lblLogo.Text = DisplayHelper.GetLogo();
            lblLogo.ForeColor = ColorTranslator.FromHtml("#00FFFF");
            lblLogo.Font = new Font("Courier New", 9, FontStyle.Bold);
            lblLogo.Location = new Point(10, 10);
            lblLogo.Size = new Size(780, 115);
            lblLogo.AutoSize = false;

            // Name prompt label
            lblNamePrompt = new Label();
            lblNamePrompt.Text = "Enter your name:";
            lblNamePrompt.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
            lblNamePrompt.Location = new Point(10, 145);
            lblNamePrompt.Size = new Size(120, 25);

            // Name textbox
            txtName = new TextBox();
            txtName.Location = new Point(135, 143);
            txtName.Size = new Size(200, 25);
            txtName.BorderStyle = BorderStyle.FixedSingle;

            // Start button
            btnStart = new Button();
            btnStart.Text = "Start Chat";
            btnStart.BackColor = ColorTranslator.FromHtml("#007BFF");
            btnStart.ForeColor = Color.White;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Location = new Point(345, 141);
            btnStart.Size = new Size(90, 28);
            btnStart.Click += BtnStart_Click;

            // Quiz button
            btnQuiz = new Button();
            btnQuiz.Text = "Quiz";
            btnQuiz.BackColor = ColorTranslator.FromHtml("#FFC107");
            btnQuiz.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
            btnQuiz.FlatStyle = FlatStyle.Flat;
            btnQuiz.Location = new Point(450, 141);
            btnQuiz.Size = new Size(90, 28);
            btnQuiz.Click += BtnQuiz_Click;

            // Tasks button
            btnTasks = new Button();
            btnTasks.Text = "Tasks";
            btnTasks.BackColor = ColorTranslator.FromHtml("#17A2B8");
            btnTasks.ForeColor = Color.White;
            btnTasks.FlatStyle = FlatStyle.Flat;
            btnTasks.Location = new Point(555, 141);
            btnTasks.Size = new Size(90, 28);
            btnTasks.Click += BtnTasks_Click;

            // Chat container (scrollable panel holding the flow layout)
            pnlChatContainer = new Panel();
            pnlChatContainer.Location = new Point(10, 185);
            pnlChatContainer.Size = new Size(760, 300);
            pnlChatContainer.BackColor = ColorTranslator.FromHtml("#F5F5F5");
            pnlChatContainer.BorderStyle = BorderStyle.FixedSingle;
            pnlChatContainer.AutoScroll = true;

            // Flow layout panel - stacks message bubbles vertically
            flowChat = new FlowLayoutPanel();
            flowChat.FlowDirection = FlowDirection.TopDown;
            flowChat.WrapContents = false;
            flowChat.AutoSize = true;
            flowChat.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowChat.Width = pnlChatContainer.Width - 20;
            flowChat.BackColor = ColorTranslator.FromHtml("#F5F5F5");
            flowChat.Padding = new Padding(5);

            pnlChatContainer.Controls.Add(flowChat);

            // Input panel
            pnlInput = new Panel();
            pnlInput.Location = new Point(10, 500);
            pnlInput.Size = new Size(760, 50);

            // Input textbox
            txtInput = new TextBox();
            txtInput.Location = new Point(0, 8);
            txtInput.Size = new Size(370, 30);
            txtInput.BorderStyle = BorderStyle.FixedSingle;
            txtInput.Enabled = false;
            txtInput.KeyDown += TxtInput_KeyDown;

            // Send button
            btnSend = new Button();
            btnSend.Text = "Send";
            btnSend.BackColor = ColorTranslator.FromHtml("#007BFF");
            btnSend.ForeColor = Color.White;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Location = new Point(390, 6);
            btnSend.Size = new Size(70, 30);
            btnSend.Enabled = false;
            btnSend.Click += BtnSend_Click;

            // Clear button
            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.BackColor = ColorTranslator.FromHtml("#28A745");
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Location = new Point(470, 6);
            btnClear.Size = new Size(70, 30);
            btnClear.Click += BtnClear_Click;

            // Exit button
            btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.BackColor = ColorTranslator.FromHtml("#DC3545");
            btnExit.ForeColor = Color.White;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Location = new Point(550, 6);
            btnExit.Size = new Size(70, 30);
            btnExit.Click += BtnExit_Click;

            // Activity Log button
            btnActivityLog = new Button();
            btnActivityLog.Text = "Activity Log";
            btnActivityLog.BackColor = ColorTranslator.FromHtml("#6C757D");
            btnActivityLog.ForeColor = Color.White;
            btnActivityLog.FlatStyle = FlatStyle.Flat;
            btnActivityLog.Location = new Point(630, 6);
            btnActivityLog.Size = new Size(120, 30);
            btnActivityLog.Click += BtnActivityLog_Click;

            // Add controls to panels
            pnlHeader.Controls.Add(lblLogo);
            pnlInput.Controls.Add(txtInput);
            pnlInput.Controls.Add(btnSend);
            pnlInput.Controls.Add(btnClear);
            pnlInput.Controls.Add(btnExit);
            pnlInput.Controls.Add(btnActivityLog);

            // Add everything to form
            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblNamePrompt);
            this.Controls.Add(txtName);
            this.Controls.Add(btnStart);
            this.Controls.Add(btnQuiz);
            this.Controls.Add(btnTasks);
            this.Controls.Add(pnlChatContainer);
            this.Controls.Add(pnlInput);

            // Typing indicator timers
            typingDotsTimer = new System.Windows.Forms.Timer();
            typingDotsTimer.Interval = 400;
            typingDotsTimer.Tick += TypingDotsTimer_Tick;

            typingDelayTimer = new System.Windows.Forms.Timer();
            typingDelayTimer.Interval = 800;
            typingDelayTimer.Tick += TypingDelayTimer_Tick;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter your name to start.", "Name Required");
                return;
            }

            userName = txtName.Text.Trim();
            memory.Remember("name", userName);

            txtInput.Enabled = true;
            btnSend.Enabled = true;
            btnStart.Enabled = false;
            txtName.Enabled = false;

            AddBotMessage($"Hello {userName}! Welcome to the Cybersecurity Awareness Bot.");
            AddBotMessage("You can ask me about passwords, phishing, scams, privacy and safe browsing.");
            AddBotMessage("Type your question below and press Send or Enter!");
        }

        private void BtnQuiz_Click(object sender, EventArgs e)
        {
            QuizForm quizForm = new QuizForm();
            quizForm.ShowDialog();
        }

        private void BtnTasks_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new TaskForm();
            taskForm.ShowDialog();
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSend_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userInput))
                return;

            AddUserMessage(userInput);
            txtInput.Clear();

            // Check NLP intents first (tasks, reminders, quiz, activity log commands)
            NlpResult nlpResult = NlpProcessor.ProcessInput(userInput);
            if (nlpResult.Intent != NlpIntent.None)
            {
                HandleNlpResult(nlpResult);
                return;
            }

            string lowerInput = userInput.ToLower();

            // Check sentiment first
            string sentiment = sentimentDetector.DetectSentiment(userInput);
            string sentimentResponse = sentimentDetector.GetSentimentResponse(sentiment, userInput);

            if (sentimentResponse != null)
            {
                ShowTypingThenRespond(StripBotPrefix(sentimentResponse));
                return;
            }

            // Detect and remember interest in a topic using broader phrasing
            string[] interestPhrases =
            {
                "interested in", "i like", "i love", "i enjoy", "i'm curious about",
                "im curious about", "curious about", "fan of",
                "want to learn about", "want to know about"
            };

            bool mentionsInterestPhrase = false;
            foreach (string phrase in interestPhrases)
            {
                if (lowerInput.Contains(phrase))
                {
                    mentionsInterestPhrase = true;
                    break;
                }
            }

            if (mentionsInterestPhrase)
            {
                foreach (string topic in cyberTopics)
                {
                    if (lowerInput.Contains(topic))
                    {
                        bool alreadyKnown = memory.HasInterest(topic);
                        memory.AddInterest(topic);

                        string msg;
                        if (!alreadyKnown)
                        {
                            msg = $"Great! I will remember that you are interested in {topic}. It is a crucial part of staying safe online.";
                        }
                        else
                        {
                            msg = $"Noted, {topic} is one of your interests. Here is something related: " + StripBotPrefix(Responses.GetRandomTip(topic));
                        }

                        ShowTypingThenRespond(msg);
                        return;
                    }
                }
            }

            // Get the base response
            string response = StripBotPrefix(Responses.GetResponse(userInput));

            // Reference a remembered interest the user hasn't mentioned in this message
            var interests = memory.GetInterests();
            if (interests.Count > 0)
            {
                for (int i = interests.Count - 1; i >= 0; i--)
                {
                    string topic = interests[i];
                    if (!lowerInput.Contains(topic))
                    {
                        response += $"\n\nAs someone interested in {topic}, remember to always stay alert online!";
                        break;
                    }
                }
            }

            ShowTypingThenRespond(response);
        }

        // Handles an NLP-detected intent: tasks, reminders, quiz, or activity log commands
        private void HandleNlpResult(NlpResult result)
        {
            switch (result.Intent)
            {
                case NlpIntent.AddReminder:
                    {
                        DateTime? reminderDate = result.ReminderDays > 0
                            ? DateTime.Now.AddDays(result.ReminderDays)
                            : (DateTime?)null;

                        TaskDatabase.InitializeDatabase();
                        TaskDatabase.AddTask(result.TaskDescription, result.TaskDescription, reminderDate);

                        string logMessage = reminderDate.HasValue
                            ? $"Reminder set for '{result.TaskDescription}' on {reminderDate.Value:dd MMM yyyy}"
                            : $"Task added: '{result.TaskDescription}' (no reminder set)";
                        ActivityLogger.AddEntry(logMessage);

                        string reply = reminderDate.HasValue
                            ? $"Got it! I'll remind you to {result.TaskDescription} on {reminderDate.Value:dd MMM yyyy}."
                            : $"Got it! I've added a task to {result.TaskDescription}.";

                        ShowTypingThenRespond(reply);
                        break;
                    }

                case NlpIntent.AddTask:
                    {
                        TaskDatabase.InitializeDatabase();
                        TaskDatabase.AddTask(result.TaskDescription, result.TaskDescription, null);
                        ActivityLogger.AddEntry($"Task added: '{result.TaskDescription}' (no reminder set)");

                        ShowTypingThenRespond($"Task added: '{result.TaskDescription}'. Would you like to set a reminder for this task?");
                        break;
                    }

                case NlpIntent.ShowActivityLog:
                    {
                        var entries = ActivityLogger.GetRecent(5);

                        if (entries.Count == 0)
                        {
                            ShowTypingThenRespond("I haven't logged any actions yet.");
                            break;
                        }

                        string logSummary = "Here's a summary of recent actions:\n";
                        int number = 1;
                        foreach (string entry in entries)
                        {
                            logSummary += $"{number}. {entry}\n";
                            number++;
                        }

                        ShowTypingThenRespond(logSummary.Trim());
                        break;
                    }

                case NlpIntent.StartQuiz:
                    {
                        ShowTypingThenRespond("Opening the cybersecurity quiz for you!");
                        QuizForm quizForm = new QuizForm();
                        quizForm.ShowDialog();
                        break;
                    }

                case NlpIntent.ShowTasks:
                    {
                        TaskDatabase.InitializeDatabase();
                        var tasks = TaskDatabase.GetAllTasks();

                        if (tasks.Count == 0)
                        {
                            ShowTypingThenRespond("You don't have any tasks yet. Try saying 'add a task to...' to create one.");
                            break;
                        }

                        string taskSummary = "Here are your tasks:\n";
                        foreach (TaskItem task in tasks)
                        {
                            string status = task.IsCompleted ? "[Done]" : "[Pending]";
                            string reminderText = task.ReminderDate.HasValue
                                ? $" - Reminder: {task.ReminderDate.Value:dd MMM yyyy}"
                                : "";
                            taskSummary += $"{status} {task.Title}{reminderText}\n";
                        }

                        ShowTypingThenRespond(taskSummary.Trim());
                        break;
                    }
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            flowChat.Controls.Clear();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            AddBotMessage("Goodbye! Stay safe online.");
            Application.Exit();
        }

        private void BtnActivityLog_Click(object sender, EventArgs e)
        {
            ActivityLogForm logForm = new ActivityLogForm();
            logForm.ShowDialog();
        }

        // Removes the "Bot: " prefix since the bubble itself now visually represents the sender
        private string StripBotPrefix(string message)
        {
            if (message.StartsWith("Bot: "))
                return message.Substring(5);
            return message;
        }

        // --- Typing indicator logic ---

        private void ShowTypingThenRespond(string response)
        {
            pendingResponse = response;
            dotCount = 0;

            typingBubble = CreateBubble("Bot is typing", false, out lblTyping);
            flowChat.Controls.Add(typingBubble);
            ScrollToBottom();

            typingDotsTimer.Start();
            typingDelayTimer.Start();
        }

        private void TypingDotsTimer_Tick(object sender, EventArgs e)
        {
            dotCount = (dotCount + 1) % 4; // 0,1,2,3 dots
            string dots = new string('.', dotCount);
            lblTyping.Text = "Bot is typing" + dots;

            // Resize bubble to fit new text
            ResizeBubbleToLabel(typingBubble, lblTyping);
        }

        private void TypingDelayTimer_Tick(object sender, EventArgs e)
        {
            typingDelayTimer.Stop();
            typingDotsTimer.Stop();

            flowChat.Controls.Remove(typingBubble);
            typingBubble.Dispose();

            AddBotMessage(pendingResponse);
        }

        // --- Message bubble creation ---

        private void AddUserMessage(string message)
        {
            Label lbl;
            Panel bubble = CreateBubble(message, true, out lbl);
            flowChat.Controls.Add(bubble);
            ScrollToBottom();
        }

        private void AddBotMessage(string message)
        {
            Label lbl;
            Panel bubble = CreateBubble(message, false, out lbl);
            flowChat.Controls.Add(bubble);
            ScrollToBottom();
        }

        // Creates a rounded chat bubble panel containing a label.
        // isUser = true aligns right with green styling, false aligns left with blue styling.
        private Panel CreateBubble(string message, bool isUser, out Label label)
        {
            Label lbl = new Label();
            lbl.Text = message;
            lbl.Font = new Font("Segoe UI", 10);
            lbl.AutoSize = true;
            lbl.MaximumSize = new Size(460, 0);
            lbl.ForeColor = isUser ? Color.White : ColorTranslator.FromHtml("#1B3A6B");
            lbl.BackColor = Color.Transparent;
            lbl.Padding = new Padding(0);

            Panel bubble = new Panel();
            bubble.BackColor = isUser ? ColorTranslator.FromHtml("#28A745") : ColorTranslator.FromHtml("#E4ECF7");
            bubble.Padding = new Padding(12, 8, 12, 8);
            bubble.Margin = new Padding(isUser ? 80 : 5, 5, isUser ? 5 : 80, 5);
            bubble.Controls.Add(lbl);

            // Position label inside bubble padding
            lbl.Location = new Point(bubble.Padding.Left, bubble.Padding.Top);

            // Set bubble size based on label size + padding
            bubble.Size = new Size(
                lbl.PreferredWidth + bubble.Padding.Left + bubble.Padding.Right,
                lbl.PreferredHeight + bubble.Padding.Top + bubble.Padding.Bottom
            );

            // Align bubble within the flow panel
            if (isUser)
                bubble.Anchor = AnchorStyles.Right;
            else
                bubble.Anchor = AnchorStyles.Left;

            // Rounded corners
            bubble.Paint += (s, e) =>
            {
                Panel p = (Panel)s;
                int radius = 14;
                Rectangle rect = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
                using (GraphicsPath path = RoundedRect(rect, radius))
                using (SolidBrush brush = new SolidBrush(p.BackColor))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);
                }
            };

            label = lbl;
            return bubble;
        }

        // Resizes a bubble to fit its label's current preferred size (used for animated typing dots)
        private void ResizeBubbleToLabel(Panel bubble, Label lbl)
        {
            bubble.Size = new Size(
                lbl.PreferredWidth + bubble.Padding.Left + bubble.Padding.Right,
                lbl.PreferredHeight + bubble.Padding.Top + bubble.Padding.Bottom
            );
            bubble.Invalidate();
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void ScrollToBottom()
        {
            flowChat.PerformLayout();
            pnlChatContainer.AutoScrollPosition = new Point(0, flowChat.Height);
        }
    }
}