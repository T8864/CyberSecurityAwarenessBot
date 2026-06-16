using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace CyberSecurityAwarenessBot.Core
{
    public static class TaskDatabase
    {
        // Connects to the LocalDB server itself (no specific database) - used to create the database if missing
        private const string ServerConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Integrated Security=true;TrustServerCertificate=true;";

        // Connects to the CyberTasksDB database - used for all task operations
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=CyberTasksDB;Integrated Security=true;TrustServerCertificate=true;";

        // Creates the database (if missing) and the Tasks table (if missing)
        public static void InitializeDatabase()
        {
            // Step 1: Create the database if it doesn't exist
            using (SqlConnection serverConnection = new SqlConnection(ServerConnectionString))
            {
                serverConnection.Open();

                string createDbQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CyberTasksDB')
                    CREATE DATABASE CyberTasksDB";

                using (SqlCommand command = new SqlCommand(createDbQuery, serverConnection))
                {
                    command.ExecuteNonQuery();
                }
            }

            // Step 2: Create the Tasks table if it doesn't exist
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string createTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Tasks' AND xtype='U')
                    CREATE TABLE Tasks (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Title NVARCHAR(200) NOT NULL,
                        Description NVARCHAR(1000) NULL,
                        ReminderDate DATETIME NULL,
                        IsCompleted BIT NOT NULL DEFAULT 0
                    )";

                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Adds a new task and returns the generated Id
        public static int AddTask(string title, string description, DateTime? reminderDate)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string insertQuery = @"
                    INSERT INTO Tasks (Title, Description, ReminderDate, IsCompleted)
                    VALUES (@Title, @Description, @ReminderDate, 0);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Description", description ?? "");
                    command.Parameters.AddWithValue("@ReminderDate", reminderDate.HasValue ? (object)reminderDate.Value : DBNull.Value);

                    object result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

        // Returns all tasks, ordered by Id
        public static List<TaskItem> GetAllTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Id, Title, Description, ReminderDate, IsCompleted FROM Tasks ORDER BY Id";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string title = reader.GetString(1);
                        string description = reader.IsDBNull(2) ? "" : reader.GetString(2);
                        DateTime? reminderDate = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);
                        bool isCompleted = reader.GetBoolean(4);

                        tasks.Add(new TaskItem(id, title, description, reminderDate, isCompleted));
                    }
                }
            }

            return tasks;
        }

        // Marks a task as completed
        public static void MarkComplete(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Tasks SET IsCompleted = 1 WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Deletes a task
        public static void DeleteTask(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Tasks WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Updates a task's reminder date
        public static void SetReminder(int id, DateTime reminderDate)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Tasks SET ReminderDate = @ReminderDate WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@ReminderDate", reminderDate);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
