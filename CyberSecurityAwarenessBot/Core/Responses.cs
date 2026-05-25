using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot.Core
{
    public static class Responses
    {
        // Last topic tracker for follow up questions
        private static string lastTopic = "";

        // Random response lists for varied answers
        private static List<string> phishingTips = new List<string>
        {
            "Bot: Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
            "Bot: Always check the sender's email address carefully. Phishing emails often use addresses that look almost right but have small differences.",
            "Bot: Never click links in suspicious emails. Go directly to the website by typing the address in your browser.",
            "Bot: If an email creates urgency like 'Act now or lose your account', it is likely a phishing attempt."
        };

        private static List<string> passwordTips = new List<string>
        {
            "Bot: Always use long and unique passwords. Consider using a password manager to keep track of them.",
            "Bot: Never use personal details like your name or birthday in your password. Hackers can easily guess these.",
            "Bot: Use a mix of uppercase, lowercase, numbers and symbols to make your password stronger.",
            "Bot: Never reuse the same password across multiple accounts. If one account is hacked, others stay safe."
        };

        private static List<string> scamTips = new List<string>
        {
            "Bot: If something sounds too good to be true online, it probably is a scam.",
            "Bot: Never send money to someone you have only met online. This is a common scam tactic.",
            "Bot: Be careful of fake job offers that ask for your personal or banking details upfront.",
            "Bot: Scammers often pretend to be from banks or government. Always verify by calling official numbers."
        };

        private static List<string> privacyTips = new List<string>
        {
            "Bot: Review your social media privacy settings regularly to control who sees your information.",
            "Bot: Avoid sharing personal details like your address or phone number publicly online.",
            "Bot: Use a VPN when connecting to public WiFi to protect your privacy.",
            "Bot: Be careful what apps you give permission to access your camera, location or contacts."
        };

        private static List<string> malwareTips = new List<string>
        {
            "Bot: Always keep your antivirus software up to date to protect against malware.",
            "Bot: Never download software from untrusted websites. Stick to official sources.",
            "Bot: Malware can hide in email attachments. Never open attachments from unknown senders.",
            "Bot: Regularly scan your device for malware especially after visiting new websites."
        };

        // Random number generator
        private static Random random = new Random();

        public static string GetResponse(string input)
        {
            input = input.ToLower().Trim();

            // Handle follow up questions
            if (input.Contains("tell me more") || input.Contains("explain more") ||
                input.Contains("more details") || input.Contains("give me another tip") ||
                input.Contains("another tip") || input.Contains("more"))
            {
                return GetFollowUp();
            }

            // Keyword recognition
            if (input.Contains("password"))
            {
                lastTopic = "password";
                return passwordTips[random.Next(passwordTips.Count)];
            }
            else if (input.Contains("phishing"))
            {
                lastTopic = "phishing";
                return phishingTips[random.Next(phishingTips.Count)];
            }
            else if (input.Contains("scam"))
            {
                lastTopic = "scam";
                return scamTips[random.Next(scamTips.Count)];
            }
            else if (input.Contains("privacy"))
            {
                lastTopic = "privacy";
                return privacyTips[random.Next(privacyTips.Count)];
            }
            else if (input.Contains("malware"))
            {
                lastTopic = "malware";
                return malwareTips[random.Next(malwareTips.Count)];
            }
            else if (input.Contains("link") || input.Contains("safe browsing"))
            {
                lastTopic = "browsing";
                return "Bot: Always check links before clicking. Look for HTTPS and verified sources.";
            }
            else if (input.Contains("how are you"))
            {
                return "Bot: I am just a bot but I am here and ready to help you stay safe online!";
            }
            else if (input.Contains("purpose") || input.Contains("what can you do"))
            {
                return "Bot: My purpose is to teach you about cybersecurity and safe online practices. Ask me about passwords, phishing, scams, privacy or malware!";
            }
            else
            {
                lastTopic = "";
                return "Bot: I did not quite understand that. Could you rephrase? You can ask me about passwords, phishing, scams, privacy or malware.";
            }
        }

        // Returns another tip on the last topic discussed
        private static string GetFollowUp()
        {
            switch (lastTopic)
            {
                case "password":
                    return passwordTips[random.Next(passwordTips.Count)];
                case "phishing":
                    return phishingTips[random.Next(phishingTips.Count)];
                case "scam":
                    return scamTips[random.Next(scamTips.Count)];
                case "privacy":
                    return privacyTips[random.Next(privacyTips.Count)];
                case "malware":
                    return malwareTips[random.Next(malwareTips.Count)];
                case "browsing":
                    return "Bot: Always use HTTPS websites and avoid clicking on pop up ads or unknown links.";
                default:
                    return "Bot: Could you tell me which topic you want more information on? I can help with passwords, phishing, scams, privacy or malware.";
            }
        }
    }
}
