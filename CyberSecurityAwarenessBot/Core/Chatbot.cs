using System;

namespace CyberSecurityAwarenessBot.Core
{
    public class Chatbot
    {
        public void Start()
        {
            // 1. Play audio greeting
            string audioPath = "assets\\greeting.wav";
            AudioPlayer player = new AudioPlayer(audioPath);
            player.PlayGreeting();

            // 2. Display logo from DisplayHelper
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(DisplayHelper.GetLogo());
            Console.ResetColor();
            Console.WriteLine();

            // 3. Welcome the user
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the Cybersecurity Awareness Bot!");
            Console.ResetColor();
            Console.WriteLine();

            // 4. Ask for user name
            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine();

            // 5. Introduction and instructions
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Hello {userName}! I'm here to help you stay safe online.");
            Console.WriteLine("You can ask me about passwords, phishing, or safe browsing.");
            Console.WriteLine("Type 'exit' if you want to quit.");
            Console.ResetColor();
            Console.WriteLine();

            // 6. Chat loop
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

                // Bot response
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Responses.GetResponse(userInput));
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}