using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot.Core
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;
        private int currentIndex;
        private int score;

        public QuizManager()
        {
            questions = BuildQuestionBank();
            currentIndex = 0;
            score = 0;
        }

        // Starts a new quiz attempt - resets progress and score
        public void StartQuiz()
        {
            currentIndex = 0;
            score = 0;
            ActivityLogger.AddEntry($"Quiz started - {questions.Count} questions");
        }

        // Returns true if there are more questions left
        public bool HasNextQuestion()
        {
            return currentIndex < questions.Count;
        }

        // Returns the current question without advancing
        public QuizQuestion GetCurrentQuestion()
        {
            if (!HasNextQuestion())
                return null;

            return questions[currentIndex];
        }

        // Returns the 1-based number of the current question (e.g. 1 for the first question)
        public int GetCurrentQuestionNumber()
        {
            return currentIndex + 1;
        }

        // Checks the answer for the current question, updates score, and advances to the next question.
        // Returns a feedback message including whether the answer was correct and the explanation.
        public string SubmitAnswer(string userAnswer)
        {
            if (!HasNextQuestion())
                return "The quiz has already finished.";

            QuizQuestion question = questions[currentIndex];
            bool isCorrect = string.Equals(userAnswer.Trim(), question.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase);

            string feedback;
            if (isCorrect)
            {
                score++;
                feedback = $"Correct! {question.Explanation}";
            }
            else
            {
                feedback = $"Incorrect. The correct answer was \"{question.CorrectAnswer}\". {question.Explanation}";
            }

            currentIndex++;

            if (!HasNextQuestion())
            {
                ActivityLogger.AddEntry($"Quiz completed - scored {score}/{questions.Count}");
            }

            return feedback;
        }

        // Returns the current score
        public int GetScore()
        {
            return score;
        }

        // Returns total number of questions
        public int GetTotalQuestions()
        {
            return questions.Count;
        }

        // Returns a final summary message based on the score
        public string GetFinalMessage()
        {
            double percentage = (double)score / questions.Count * 100;

            string ratingMessage;
            if (percentage >= 80)
                ratingMessage = "Great job! You're a cybersecurity pro!";
            else if (percentage >= 50)
                ratingMessage = "Good effort! A bit more learning and you'll be a pro.";
            else
                ratingMessage = "Keep learning to stay safe online!";

            return $"Quiz complete! You scored {score}/{questions.Count}. {ratingMessage}";
        }

        // Builds the bank of cybersecurity quiz questions
        private List<QuizQuestion> BuildQuestionBank()
        {
            return new List<QuizQuestion>
            {
                new QuizQuestion(
                    "What should you do if you receive an email asking for your password?",
                    new List<string> { "Reply with your password", "Delete the email", "Report the email as phishing", "Ignore it" },
                    "Report the email as phishing",
                    "Reporting phishing emails helps prevent scams and protects others too."
                ),
                new QuizQuestion(
                    "True or False: It is safe to use the same password for multiple accounts.",
                    new List<string> { "True", "False" },
                    "False",
                    "Reusing passwords means if one account is breached, all your accounts are at risk."
                ),
                new QuizQuestion(
                    "Which of these is the strongest password?",
                    new List<string> { "password123", "JohnSmith1990", "Tr$8!qLpZ2#m", "qwerty" },
                    "Tr$8!qLpZ2#m",
                    "Strong passwords mix uppercase, lowercase, numbers and symbols, and avoid personal details."
                ),
                new QuizQuestion(
                    "True or False: A padlock icon in the browser address bar guarantees a website is completely safe.",
                    new List<string> { "True", "False" },
                    "False",
                    "The padlock only means the connection is encrypted (HTTPS). The site itself could still be malicious."
                ),
                new QuizQuestion(
                    "What is 'social engineering' in cybersecurity?",
                    new List<string> { "A type of antivirus software", "Manipulating people into revealing confidential information", "A method of encrypting data", "A type of firewall" },
                    "Manipulating people into revealing confidential information",
                    "Social engineering relies on tricking people rather than breaking technical systems."
                ),
                new QuizQuestion(
                    "True or False: You should enable two-factor authentication (2FA) wherever possible.",
                    new List<string> { "True", "False" },
                    "True",
                    "2FA adds an extra layer of protection, even if your password is stolen."
                ),
                new QuizQuestion(
                    "What should you do before connecting to a public WiFi network?",
                    new List<string> { "Disable your antivirus", "Use a VPN if possible", "Share your location publicly", "Turn off your firewall" },
                    "Use a VPN if possible",
                    "A VPN encrypts your traffic, making it harder for attackers on public networks to intercept your data."
                ),
                new QuizQuestion(
                    "True or False: Pop-up messages saying 'You've won a prize, click here!' are usually trustworthy.",
                    new List<string> { "True", "False" },
                    "False",
                    "These are classic scam tactics designed to trick users into clicking malicious links."
                ),
                new QuizQuestion(
                    "What is the safest way to verify a suspicious request from your 'bank'?",
                    new List<string> { "Reply to the email directly", "Click the link in the message", "Call the bank using the number on their official website", "Forward it to a friend for advice" },
                    "Call the bank using the number on their official website",
                    "Always verify through an official, independent channel rather than the contact details in the suspicious message itself."
                ),
                new QuizQuestion(
                    "True or False: Keeping your software and apps updated helps protect against malware.",
                    new List<string> { "True", "False" },
                    "True",
                    "Updates often patch security vulnerabilities that malware can exploit."
                ),
                new QuizQuestion(
                    "Which of these is a sign of a phishing email?",
                    new List<string> { "Personalised greeting with your full name", "Urgent demands and threats if you don't act immediately", "Email from a known colleague about a scheduled meeting", "A company newsletter you subscribed to" },
                    "Urgent demands and threats if you don't act immediately",
                    "Urgency and threats are common pressure tactics used by attackers to bypass careful thinking."
                ),
                new QuizQuestion(
                    "True or False: Sharing your location on social media in real-time is always safe.",
                    new List<string> { "True", "False" },
                    "False",
                    "Real-time location sharing can reveal when you're away from home or your daily patterns to strangers."
                )
            };
        }
    }
}
