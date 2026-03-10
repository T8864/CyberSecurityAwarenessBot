using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBot.Core
{
    public static class Responses
    {
        public static string GetResponse(string input)
        {
            input = input.ToLower().Trim();

            if (input.Contains("password"))
                return "Bot: Always use long and unique passwords. Consider using a password manager.";
            else if (input.Contains("phishing"))
                return "Bot: Phishing is when attackers trick you to share sensitive info. Never click suspicious links!";
            else if (input.Contains("link") || input.Contains("safe browsing"))
                return "Bot: Always check links before clicking. Look for HTTPS and verified sources.";
            else if (input.Contains("how are you"))
                return "Bot: I'm just a bot, but I'm here to help you stay safe online!";
            else if (input.Contains("purpose") || input.Contains("what can you do"))
                return "Bot: My purpose is to teach you about cybersecurity and safe online practices.";
            else
                return "Bot: I didn’t quite understand that. Could you rephrase?";
        }
    }
} 
