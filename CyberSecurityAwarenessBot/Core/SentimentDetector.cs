using System;

namespace CyberSecurityAwarenessBot.Core
{
    public class SentimentDetector
    {
        // Detect sentiment from user input
        public string DetectSentiment(string input)
        {
            input = input.ToLower().Trim();

            if (input.Contains("worried") || input.Contains("scared") || input.Contains("afraid"))
                return "worried";
            else if (input.Contains("curious") || input.Contains("interested") || input.Contains("want to know"))
                return "curious";
            else if (input.Contains("frustrated") || input.Contains("angry") || input.Contains("annoyed"))
                return "frustrated";
            else if (input.Contains("confused") || input.Contains("dont understand") || input.Contains("not sure"))
                return "confused";
            else if (input.Contains("happy") || input.Contains("great") || input.Contains("good"))
                return "happy";
            else
                return "neutral";
        }

        // Return empathetic response based on sentiment and automatically add a tip
        public string GetSentimentResponse(string sentiment, string input)
        {
            switch (sentiment)
            {
                case "worried":
                    return "Bot: It is completely understandable to feel worried. Cyber threats are real but you can protect yourself. "
                         + GetAutoTip(input);

                case "curious":
                    return "Bot: That is great that you are curious about cybersecurity! Knowledge is your best defence online. "
                         + GetAutoTip(input);

                case "frustrated":
                    return "Bot: I understand your frustration. Cybersecurity can feel overwhelming but I am here to help. "
                         + GetAutoTip(input);

                case "confused":
                    return "Bot: No worries at all! Cybersecurity can be confusing at first. Let me help clear things up. "
                         + GetAutoTip(input);

                case "happy":
                    return "Bot: That is wonderful! Staying positive while learning about cybersecurity is the best approach. "
                         + GetAutoTip(input);

                default:
                    return null;
            }
        }

        // Automatically share a relevant tip based on input
        private string GetAutoTip(string input)
        {
            if (input.Contains("scam"))
                return "Here is a tip: Never send money or personal details to someone you only met online.";
            else if (input.Contains("password"))
                return "Here is a tip: Always use strong unique passwords and never share them with anyone.";
            else if (input.Contains("phishing"))
                return "Here is a tip: Never click links in suspicious emails. Always verify the sender first.";
            else if (input.Contains("privacy"))
                return "Here is a tip: Review your social media privacy settings regularly.";
            else if (input.Contains("malware"))
                return "Here is a tip: Always keep your antivirus software up to date.";
            else
                return "Here is a tip: Always think before you click and never share personal information online.";
        }
    }
}