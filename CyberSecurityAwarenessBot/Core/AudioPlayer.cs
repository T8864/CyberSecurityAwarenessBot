
using System;
using System.Media;
using System.IO; 

namespace CyberSecurityAwarenessBot.Core
{
    public class AudioPlayer
    {
        private string filePath;

        public AudioPlayer(string path)
        {
            filePath = path;
        }

        public void PlayGreeting()
        {
            try
            {
                // Check if the file exists first
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Bot: Audio file not found at {filePath}");
                    return;
                }

                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.Load();      // Preload the WAV file
                    player.PlaySync();  // Play synchronously
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bot: Unable to play audio greeting.");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}