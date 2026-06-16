using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot.Core
{
    public class MemoryManager
    {
        // Store user details
        private Dictionary<string, string> memory = new Dictionary<string, string>();

        // Store a list of topics the user has shown interest in
        private List<string> interests = new List<string>();

        // Save a piece of information
        public void Remember(string key, string value)
        {
            if (memory.ContainsKey(key))
                memory[key] = value;
            else
                memory.Add(key, value);
        }

        // Retrieve a piece of information
        public string Recall(string key)
        {
            if (memory.ContainsKey(key))
                return memory[key];
            return null;
        }

        // Check if something is remembered
        public bool Has(string key)
        {
            return memory.ContainsKey(key);
        }

        // Add a topic of interest if not already remembered
        public void AddInterest(string topic)
        {
            topic = topic.ToLower().Trim();
            if (!interests.Contains(topic))
                interests.Add(topic);
        }

        // Get all topics of interest
        public List<string> GetInterests()
        {
            return interests;
        }

        // Check if a topic is already a known interest
        public bool HasInterest(string topic)
        {
            return interests.Contains(topic.ToLower().Trim());
        }
    }
}
