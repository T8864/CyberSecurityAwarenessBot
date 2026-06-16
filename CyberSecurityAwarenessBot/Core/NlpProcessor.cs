using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot.Core
{
    public enum NlpIntent
    {
        None,
        AddReminder,
        AddTask,
        ShowActivityLog,
        StartQuiz,
        ShowTasks
    }

    public class NlpResult
    {
        public NlpIntent Intent { get; set; }
        public string TaskDescription { get; set; }
        public int ReminderDays { get; set; }

        public NlpResult()
        {
            Intent = NlpIntent.None;
            TaskDescription = "";
            ReminderDays = 0;
        }
    }

    public static class NlpProcessor
    {
        public static NlpResult ProcessInput(string input)
        {
            string lowerInput = input.ToLower().Trim();

            // --- Activity log intent ---
            string[] logPhrases =
            {
                "show activity log", "show my activity log", "activity log",
                "what have you done for me", "what have you done", "show recent activity"
            };
            foreach (string phrase in logPhrases)
            {
                if (lowerInput.Contains(phrase))
                {
                    return new NlpResult { Intent = NlpIntent.ShowActivityLog };
                }
            }

            // --- Quiz intent ---
            string[] quizPhrases =
            {
                "start the quiz", "start quiz", "quiz me", "test my knowledge",
                "take the quiz", "play the quiz", "i want to take the quiz"
            };
            foreach (string phrase in quizPhrases)
            {
                if (lowerInput.Contains(phrase))
                {
                    return new NlpResult { Intent = NlpIntent.StartQuiz };
                }
            }

            // --- Show tasks intent ---
            string[] showTasksPhrases =
            {
                "show my tasks", "show tasks", "what tasks do i have",
                "view my tasks", "view tasks", "list my tasks", "list tasks"
            };
            foreach (string phrase in showTasksPhrases)
            {
                if (lowerInput.Contains(phrase))
                {
                    return new NlpResult { Intent = NlpIntent.ShowTasks };
                }
            }

            // --- Reminder intent: "remind me to X in N days" / "remind me to X tomorrow" ---
            if (lowerInput.Contains("remind me"))
            {
                string taskDescription = ExtractReminderTask(lowerInput, input);
                int days = ExtractReminderDays(lowerInput);

                if (!string.IsNullOrWhiteSpace(taskDescription))
                {
                    return new NlpResult
                    {
                        Intent = NlpIntent.AddReminder,
                        TaskDescription = taskDescription,
                        ReminderDays = days
                    };
                }
            }

            // --- Add task intent: "add a task to X" / "add task - X" / "add a task X" ---
            string[] addTaskPhrases = { "add a task", "add task" };
            foreach (string phrase in addTaskPhrases)
            {
                if (lowerInput.Contains(phrase))
                {
                    string taskDescription = ExtractAddTaskDescription(lowerInput, input, phrase);

                    if (!string.IsNullOrWhiteSpace(taskDescription))
                    {
                        return new NlpResult
                        {
                            Intent = NlpIntent.AddTask,
                            TaskDescription = taskDescription
                        };
                    }
                }
            }

            return new NlpResult { Intent = NlpIntent.None };
        }

        // Extracts the task description from a "remind me to X..." phrase, removing trailing time references
        private static string ExtractReminderTask(string lowerInput, string originalInput)
        {
            int startIndex = lowerInput.IndexOf("remind me to");
            int skipLength = "remind me to".Length;

            if (startIndex == -1)
            {
                startIndex = lowerInput.IndexOf("remind me");
                skipLength = "remind me".Length;
            }

            if (startIndex == -1)
                return "";

            string remainder = originalInput.Substring(startIndex + skipLength).Trim();

            // Remove trailing time phrases like "tomorrow", "in 3 days", "in a week"
            remainder = RemoveTrailingTimePhrase(remainder);

            return remainder.Trim(' ', '-', '.', ',');
        }

        // Extracts the task description from an "add a task to X" / "add task - X" phrase
        private static string ExtractAddTaskDescription(string lowerInput, string originalInput, string matchedPhrase)
        {
            int startIndex = lowerInput.IndexOf(matchedPhrase);
            int skipLength = matchedPhrase.Length;

            string remainder = originalInput.Substring(startIndex + skipLength).Trim();

            // Remove a leading "to" if present, e.g. "add a task to enable 2FA" -> "enable 2FA"
            if (remainder.ToLower().StartsWith("to "))
                remainder = remainder.Substring(3);

            return remainder.Trim(' ', '-', '.', ',');
        }

        // Removes trailing time references such as "tomorrow", "in 3 days", "in a week" from a task description
        private static string RemoveTrailingTimePhrase(string text)
        {
            string lower = text.ToLower();

            int index = lower.IndexOf(" tomorrow");
            if (index != -1)
                return text.Substring(0, index);

            index = lower.IndexOf(" in ");
            if (index != -1)
                return text.Substring(0, index);

            return text;
        }

        // Extracts the number of days from phrases like "in 3 days", "tomorrow" (=1), "in a week" (=7)
        private static int ExtractReminderDays(string lowerInput)
        {
            if (lowerInput.Contains("tomorrow"))
                return 1;

            if (lowerInput.Contains("in a week") || lowerInput.Contains("next week"))
                return 7;

            int inIndex = lowerInput.IndexOf("in ");
            if (inIndex != -1)
            {
                string afterIn = lowerInput.Substring(inIndex + 3);
                string[] words = afterIn.Split(' ', '.', ',');

                foreach (string word in words)
                {
                    if (int.TryParse(word, out int days))
                    {
                        return days;
                    }
                }
            }

            return 0; // no reminder date specified
        }
    }
}
