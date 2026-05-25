using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot.Core
{
    public class MemoryManager
    {
        // Store user details
        private Dictionary<string, string> memory = new Dictionary<string, string>();

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
    }
}
