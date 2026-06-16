using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot.Core
{
    public static class ActivityLogger
    {
        // Stores all logged actions for this session
        private static List<string> entries = new List<string>();

        // Add a new entry to the log with a timestamp
        public static void AddEntry(string description)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            entries.Add($"[{timestamp}] {description}");
        }

        // Get the most recent N entries, most recent first
        public static List<string> GetRecent(int count)
        {
            List<string> recent = new List<string>();

            int start = entries.Count - count;
            if (start < 0)
                start = 0;

            for (int i = entries.Count - 1; i >= start; i--)
            {
                recent.Add(entries[i]);
            }

            return recent;
        }

        // Get all entries, most recent first
        public static List<string> GetAll()
        {
            List<string> all = new List<string>();

            for (int i = entries.Count - 1; i >= 0; i--)
            {
                all.Add(entries[i]);
            }

            return all;
        }

        // Total number of entries logged
        public static int Count()
        {
            return entries.Count;
        }
    }
}
