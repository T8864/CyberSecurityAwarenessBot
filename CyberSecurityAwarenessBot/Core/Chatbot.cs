using System;
using CyberSecurityAwarenessBot.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityAwarenessBot.Core
{
    public class Chatbot
    {
        public void Start()
        {
            // 1️⃣ Play audio greeting
            string audioPath = "assets\\greeting.wav"; // relative path to WAV file
            AudioPlayer player = new AudioPlayer(audioPath);
            player.PlayGreeting();

            // 2️⃣ Display ASCII logo with colors
            DisplayLogo();

            // 3️⃣ Welcome the user (colored)
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the Cybersecurity Awareness Bot!");
            Console.ResetColor();
            Console.WriteLine();

            // 4️⃣ Ask for user name
            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine();

            // 5️⃣ Introduction and instructions (colored)
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Hello {userName}! I'm here to help you stay safe online.");
            Console.WriteLine("You can ask me about passwords, phishing, or safe browsing.");
            Console.WriteLine("Type 'exit' if you want to quit.");
            Console.ResetColor();
            Console.WriteLine();

            // 6️⃣ Chat loop with colored bot responses
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("You: ");
                Console.ResetColor();
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bot: Goodbye! Stay safe online.");
                    Console.ResetColor();
                    break;
                }

                // Bot response in green
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Responses.GetResponse(userInput));
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private void DisplayLogo()
        {
            // Colored ASCII logo
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("   CYBERSECURITY AWARENESS BOT");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
