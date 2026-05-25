using System;
using System.Drawing;
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
        private RichTextBox rtbChat;
        private TextBox txtInput;
        private Button btnSend;
        private Button btnClear;
        private Button btnExit;
        private Panel pnlHeader;
        private Panel pnlInput;

        private string userName = "";
        private MemoryManager memory = new MemoryManager();

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

            // Chat display
            rtbChat = new RichTextBox();
            rtbChat.Location = new Point(10, 185);
            rtbChat.Size = new Size(760, 300);
            rtbChat.BackColor = ColorTranslator.FromHtml("#F5F5F5");
            rtbChat.BorderStyle = BorderStyle.FixedSingle;
            rtbChat.ReadOnly = true;
            rtbChat.Font = new Font("Segoe UI", 10);
            rtbChat.ScrollBars = RichTextBoxScrollBars.Vertical;

            // Input panel
            pnlInput = new Panel();
            pnlInput.Location = new Point(10, 500);
            pnlInput.Size = new Size(760, 50);

            // Input textbox
            txtInput = new TextBox();
            txtInput.Location = new Point(0, 8);
            txtInput.Size = new Size(480, 30);
            txtInput.BorderStyle = BorderStyle.FixedSingle;
            txtInput.Enabled = false;
            txtInput.KeyDown += TxtInput_KeyDown;

            // Send button
            btnSend = new Button();
            btnSend.Text = "Send";
            btnSend.BackColor = ColorTranslator.FromHtml("#007BFF");
            btnSend.ForeColor = Color.White;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.Location = new Point(490, 6);
            btnSend.Size = new Size(80, 30);
            btnSend.Enabled = false;
            btnSend.Click += BtnSend_Click;

            // Clear button
            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.BackColor = ColorTranslator.FromHtml("#28A745");
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Location = new Point(580, 6);
            btnClear.Size = new Size(80, 30);
            btnClear.Click += BtnClear_Click;

            // Exit button
            btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.BackColor = ColorTranslator.FromHtml("#DC3545");
            btnExit.ForeColor = Color.White;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Location = new Point(670, 6);
            btnExit.Size = new Size(80, 30);
            btnExit.Click += BtnExit_Click;

            // Add controls to panels
            pnlHeader.Controls.Add(lblLogo);
            pnlInput.Controls.Add(txtInput);
            pnlInput.Controls.Add(btnSend);
            pnlInput.Controls.Add(btnClear);
            pnlInput.Controls.Add(btnExit);

            // Add everything to form
            this.Controls.Add(pnlHeader);
            this.Controls.Add(lblNamePrompt);
            this.Controls.Add(txtName);
            this.Controls.Add(btnStart);
            this.Controls.Add(rtbChat);
            this.Controls.Add(pnlInput);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter your name to start.", "Name Required");
                return;
            }

            userName = txtName.Text.Trim();

            // Remember the user's name
            memory.Remember("name", userName);

            txtInput.Enabled = true;
            btnSend.Enabled = true;
            btnStart.Enabled = false;
            txtName.Enabled = false;

            AppendMessage($"Bot: Hello {userName}! Welcome to the Cybersecurity Awareness Bot.", ColorTranslator.FromHtml("#1B3A6B"));
            AppendMessage("Bot: You can ask me about passwords, phishing, scams, privacy and safe browsing.", ColorTranslator.FromHtml("#1B3A6B"));
            AppendMessage("Bot: Type your question below and press Send or Enter!", ColorTranslator.FromHtml("#1B3A6B"));
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

            AppendMessage($"You: {userInput}", ColorTranslator.FromHtml("#28A745"));

            // Remember favourite topic if mentioned
            if (userInput.ToLower().Contains("interested in") || userInput.ToLower().Contains("i like"))
            {
                string[] topics = { "password", "phishing", "scam", "privacy", "malware" };
                foreach (string topic in topics)
                {
                    if (userInput.ToLower().Contains(topic))
                    {
                        memory.Remember("favouriteTopic", topic);
                        AppendMessage($"Bot: Great! I will remember that you are interested in {topic}. It is a crucial part of staying safe online.", ColorTranslator.FromHtml("#1B3A6B"));
                        txtInput.Clear();
                        return;
                    }
                }
            }

            // Recall favourite topic in response
            string response = Responses.GetResponse(userInput);

            if (memory.Has("favouriteTopic"))
            {
                string topic = memory.Recall("favouriteTopic");
                if (!userInput.ToLower().Contains(topic))
                {
                    response += $"\n\nBot: As someone interested in {topic}, remember to always stay alert online!";
                }
            }

            AppendMessage(response, ColorTranslator.FromHtml("#1B3A6B"));
            txtInput.Clear();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            rtbChat.Clear();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            AppendMessage("Bot: Goodbye! Stay safe online.", ColorTranslator.FromHtml("#1B3A6B"));
            Application.Exit();
        }

        private void AppendMessage(string message, Color colour)
        {
            rtbChat.SelectionStart = rtbChat.TextLength;
            rtbChat.SelectionLength = 0;
            rtbChat.SelectionColor = colour;
            rtbChat.AppendText(message + Environment.NewLine + Environment.NewLine);
            rtbChat.ScrollToCaret();
        }
    }
}
