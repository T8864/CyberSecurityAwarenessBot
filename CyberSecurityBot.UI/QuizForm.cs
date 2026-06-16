using System;
using System.Drawing;
using System.Windows.Forms;
using CyberSecurityAwarenessBot.Core;

namespace CyberSecurityBot.UI
{
    public partial class QuizForm : Form
    {
        private Label lblQuestionNumber;
        private Label lblQuestion;
        private Panel pnlOptions;
        private Label lblFeedback;
        private Button btnNext;
        private Button btnClose;

        private QuizManager quizManager;

        public QuizForm()
        {
            quizManager = new QuizManager();
            SetupForm();
            quizManager.StartQuiz();
            ShowQuestion();
        }

        private void SetupForm()
        {
            this.Text = "Cybersecurity Quiz";
            this.Size = new Size(550, 480);
            this.BackColor = ColorTranslator.FromHtml("#F0F8FF");
            this.Font = new Font("Segoe UI", 10);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblQuestionNumber = new Label();
            lblQuestionNumber.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblQuestionNumber.ForeColor = ColorTranslator.FromHtml("#6C757D");
            lblQuestionNumber.Location = new Point(15, 15);
            lblQuestionNumber.Size = new Size(500, 25);

            lblQuestion = new Label();
            lblQuestion.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblQuestion.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
            lblQuestion.Location = new Point(15, 45);
            lblQuestion.Size = new Size(500, 70);

            pnlOptions = new Panel();
            pnlOptions.Location = new Point(15, 125);
            pnlOptions.Size = new Size(500, 200);

            lblFeedback = new Label();
            lblFeedback.Font = new Font("Segoe UI", 10);
            lblFeedback.Location = new Point(15, 335);
            lblFeedback.Size = new Size(500, 70);
            lblFeedback.AutoSize = false;

            btnNext = new Button();
            btnNext.Text = "Next Question";
            btnNext.BackColor = ColorTranslator.FromHtml("#007BFF");
            btnNext.ForeColor = Color.White;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Location = new Point(15, 410);
            btnNext.Size = new Size(140, 32);
            btnNext.Visible = false;
            btnNext.Click += BtnNext_Click;

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.BackColor = ColorTranslator.FromHtml("#DC3545");
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Location = new Point(395, 410);
            btnClose.Size = new Size(120, 32);
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblQuestionNumber);
            this.Controls.Add(lblQuestion);
            this.Controls.Add(pnlOptions);
            this.Controls.Add(lblFeedback);
            this.Controls.Add(btnNext);
            this.Controls.Add(btnClose);
        }

        private void ShowQuestion()
        {
            lblFeedback.Text = "";
            btnNext.Visible = false;
            pnlOptions.Controls.Clear();

            if (!quizManager.HasNextQuestion())
            {
                ShowFinalScore();
                return;
            }

            QuizQuestion question = quizManager.GetCurrentQuestion();

            lblQuestionNumber.Text = $"Question {quizManager.GetCurrentQuestionNumber()} of {quizManager.GetTotalQuestions()}";
            lblQuestion.Text = question.QuestionText;

            int y = 0;
            foreach (string option in question.Options)
            {
                Button optionButton = new Button();
                optionButton.Text = option;
                optionButton.Font = new Font("Segoe UI", 10);
                optionButton.BackColor = ColorTranslator.FromHtml("#E4ECF7");
                optionButton.ForeColor = ColorTranslator.FromHtml("#1B3A6B");
                optionButton.FlatStyle = FlatStyle.Flat;
                optionButton.TextAlign = ContentAlignment.MiddleLeft;
                optionButton.Location = new Point(0, y);
                optionButton.Size = new Size(480, 40);
                optionButton.Click += OptionButton_Click;

                pnlOptions.Controls.Add(optionButton);
                y += 48;
            }
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            string selectedAnswer = clicked.Text;

            // Disable all option buttons after selection
            foreach (Control c in pnlOptions.Controls)
            {
                c.Enabled = false;
            }

            string feedback = quizManager.SubmitAnswer(selectedAnswer);

            bool wasCorrect = feedback.StartsWith("Correct");
            lblFeedback.ForeColor = wasCorrect ? ColorTranslator.FromHtml("#28A745") : ColorTranslator.FromHtml("#DC3545");
            lblFeedback.Text = feedback;

            btnNext.Visible = true;
            btnNext.Text = quizManager.HasNextQuestion() ? "Next Question" : "See Results";
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            ShowQuestion();
        }

        private void ShowFinalScore()
        {
            lblQuestionNumber.Text = "Quiz Complete";
            lblQuestion.Text = quizManager.GetFinalMessage();
            lblFeedback.Text = "";
            btnNext.Visible = false;
        }
    }
}
