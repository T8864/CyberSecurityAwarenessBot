using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

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
                SoundPlayer player = new SoundPlayer(filePath);
                player.PlaySync(); // Simple synchronous playback
            }
            catch
            {
                Console.WriteLine("Bot: Unable to play audio greeting.");
            }
        }
    }  
}