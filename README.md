# Cybersecurity Awareness Bot - Part 1, 2 & 3

## Description
A cybersecurity awareness chatbot that educates users on online safety practices in South Africa.
Part 1 is a console-based chatbot, Part 2 expands it into a full graphical user interface (GUI) application using Windows Forms, and Part 3 adds advanced features including a task assistant with database storage, a cybersecurity quiz, NLP-style command recognition, and an activity log.
The bot simulates conversation, recognises cybersecurity keywords, detects user sentiment, remembers user details to personalise responses, helps users manage cybersecurity tasks, tests their knowledge, and logs its own actions.

## Requirements
- .NET 8
- Visual Studio 2026
- Windows OS
- **SQL Server Express LocalDB** (required for the Part 3 Task Assistant feature). This is included automatically with the Visual Studio "Data storage and processing" workload. If missing, install it via the Visual Studio Installer, or check it's available by running `sqllocaldb info` in a terminal. No manual database setup is required — the app creates the `CyberTasksDB` database and `Tasks` table automatically the first time the Task Assistant is opened.

## Projects in Solution
- **CyberSecurityAwarenessBot** — Core logic and console application (Part 1)
- **CyberSecurityBot.UI** — Windows Forms GUI application (Parts 2 & 3)

## Part 1 Features
- Plays a voice greeting at startup (WAV format)
- Displays an ASCII logo
- Accepts user input and responds to questions on:
  - Passwords
  - Phishing
  - Safe browsing
- Input validation with default responses
- Enhanced console UI with colours and borders

## Part 2 Features
- Full Windows Forms GUI with cybersecurity themed design
- Voice greeting plays automatically on launch
- ASCII logo displayed in the header
- Keyword recognition for cybersecurity topics:
  - Passwords
  - Phishing
  - Scams
  - Privacy
  - Malware
- Random responses from predefined lists to keep conversation varied
- Conversation flow with follow up questions like "tell me more"
- Memory and recall system to remember user name and favourite topic
- Sentiment detection that responds with empathy and automatically shares a tip
- Error handling for empty inputs and unknown queries
- Exit button with goodbye message

## Part 3 Features
- **Task Assistant** — add cybersecurity-related tasks with a title, description, and optional reminder date, stored in a local SQL Server LocalDB database (`CyberTasksDB`). Tasks can be marked complete or deleted via the GUI, with all changes persisted to the database.
- **Cybersecurity Quiz** — a 12-question mini-game mixing multiple-choice and true/false questions covering phishing, passwords, safe browsing, and social engineering. Each answer gives immediate feedback and an explanation, with a final score and rating message at the end.
- **NLP Simulation** — recognises varied phrasings of common requests using keyword and substring matching, including:
  - "remind me to [task] in [N] days" / "remind me to [task] tomorrow" — creates a task with a reminder
  - "add a task to [task]" — creates a task without a reminder
  - "show my tasks" — lists current tasks directly in the chat
  - "show activity log" / "what have you done for me" — summarises recent bot actions
  - "start quiz" / "quiz me" — opens the quiz
- **Activity Log** — records key actions (tasks added, reminders set, quiz started/completed) with timestamps, viewable via a dedicated "Activity Log" button or through chat commands.
- Chat bubble interface (user messages right-aligned, bot messages left-aligned) with an animated "Bot is typing..." indicator before each response
- Memory expanded to track multiple topics of interest rather than a single favourite topic
- All Part 1 and Part 2 features remain fully functional within the Part 3 GUI

## Classes Overview

### Core Project
- **Program.cs** — Entry point for the console application
- **Chatbot.cs** — Main chatbot logic for console interaction
- **AudioPlayer.cs** — Handles WAV file playback
- **Responses.cs** — Keyword recognition and random responses
- **DisplayHelper.cs** — Shared ASCII logo used by both projects
- **MemoryManager.cs** — Stores and recalls user information and topics of interest
- **SentimentDetector.cs** — Detects user sentiment and responds with empathy
- **ActivityLogger.cs** — Records timestamped bot actions for later review
- **QuizQuestion.cs** — Model representing a single quiz question, its options, and explanation
- **QuizManager.cs** — Manages the quiz question bank, scoring, and progress
- **TaskItem.cs** — Model representing a single cybersecurity task
- **TaskDatabase.cs** — Handles all database operations (create, read, update, delete) for tasks using SQL Server LocalDB
- **NlpProcessor.cs** — Detects task, reminder, quiz, and activity log intents from free-form user text

### UI Project
- **MainForm.cs** — Main GUI form with all controls, chat bubble rendering, typing indicator, and interaction logic
- **MainForm.Designer.cs** — Form initialisation and component setup
- **ActivityLogForm.cs** — Window displaying recent logged actions
- **QuizForm.cs** — Window displaying the cybersecurity quiz, one question at a time, with scoring
- **TaskForm.cs** — Window for adding, viewing, completing, and deleting cybersecurity tasks

## How to Run

### Part 1 - Console App
1. Clone the repository:
```bash
   git clone https://github.com/T8864/CyberSecurityAwarenessBot.git
```
2. Open the solution in Visual Studio
3. Set **CyberSecurityAwarenessBot** as the startup project
4. Press **F5** to run

### Part 2 & 3 - GUI App
1. Clone the repository:
```bash
   git clone https://github.com/T8864/CyberSecurityAwarenessBot.git
```
2. Open the solution in Visual Studio
3. Set **CyberSecurityBot.UI** as the startup project
4. Press **F5** to run

On first launch, a short voice greeting will play, followed by the chatbot interface where you can enter your name and start chatting. Use the **Quiz**, **Tasks**, and **Activity Log** buttons to access the Part 3 features, or trigger them directly through chat commands such as "quiz me", "show my tasks", or "remind me to update my password in 3 days".

## GitHub Actions CI
This project uses GitHub Actions for Continuous Integration.
Every push is automatically checked for a successful build.



