using System;

namespace CyberSecurityAwarenessBot.Core
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem()
        {
            Title = "";
            Description = "";
        }

        public TaskItem(int id, string title, string description, DateTime? reminderDate, bool isCompleted)
        {
            Id = id;
            Title = title;
            Description = description;
            ReminderDate = reminderDate;
            IsCompleted = isCompleted;
        }
    }
}
