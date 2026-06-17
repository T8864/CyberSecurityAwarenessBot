# Cybersecurity Awareness Bot

A C# chatbot built across three project phases to educate South African citizens on cybersecurity best practices, evolving from a console application into a full WinForms GUI with a task assistant, quiz, NLP-style command recognition, and an activity log.

## Requirements
- .NET 8 SDK
- Visual Studio 2022 or later, with the **.NET desktop development** workload installed
- Windows OS
- **SQL Server Express LocalDB** (used by the Part 3 Task Assistant feature). This is included automatically with the Visual Studio "Data storage and processing" workload. If missing, install it via the Visual Studio Installer, or check it's available by running `sqllocaldb info` in a terminal.

No manual database setup is required — the app creates the `CyberTasksDB` database and `Tasks` table automatically the first time the Task Assistant is opened.

## Project Structure
CyberSecurityAwarenessBot/        - Core console project (also houses shared logic)

Core/

AudioPlayer.cs                - Plays the WAV voice greeting

Chatbot.cs                    - Original Part 1 console chat loop

DisplayHelper.cs              - ASCII art logo

Responses.cs                  - Keyword recognition, random tips, follow-up handling

SentimentDetector.cs          - Detects user sentiment, returns empathetic responses + tips

MemoryManager.cs              - Stores user name and tracked interests

ActivityLogger.cs             - Records timestamped bot actions

QuizQuestion.cs / QuizManager.cs - Cybersecurity quiz question bank and game logic

TaskItem.cs / TaskDatabase.cs - Task model and database access layer (LocalDB)

NlpProcessor.cs                - Detects task/reminder/quiz/log intents from free text

assets/

greeting.wav                  - Voice greeting audio file
CyberSecurityBot.UI/              - WinForms GUI project

MainForm.cs                     - Main chat window (bubbles, typing indicator, feature buttons)

ActivityLogForm.cs               - Activity log viewer window

QuizForm.cs                     - Quiz game window

TaskForm.cs                     - Task assistant window

## How to Run

1. Clone the repository:
```bash
   git clone https://github.com/T8864/CyberSecurityAwarenessBot.git
```
2. Open `CyberSecurityAwarenessBot.sln` in Visual Studio.
3. Set **CyberSecurityBot.UI** as the startup project (right-click the project in Solution Explorer → "Set as Startup Project").
4. Build the solution (Build → Build Solution).
5. Run the application (F5 or the green Start button).

On first launch, a short voice greeting will play, followed by the chatbot interface where you can enter your name and start chatting.

## Features by Part

### Part 1 — Console Chatbot
- Voice greeting played on startup (WAV file via `System.Media.SoundPlayer`)
- ASCII art logo displayed as a header
- Personalised greeting using the user's name
- Predefined responses for common questions (purpose, capabilities, how are you)
- Input validation with a default fallback response
- Coloured console formatting with section dividers

### Part 2 — GUI, Dynamic Responses, Sentiment, and Memory
- Full WinForms GUI translation of all Part 1 features, including the voice greeting and ASCII logo
- Keyword recognition across five cybersecurity topics: password, phishing, scam, privacy, and malware
- Randomised tip selection per topic to keep responses varied
- Conversational follow-up handling ("tell me more", "another tip") that continues the current topic
- Memory feature that tracks the user's name and all topics of interest mentioned during the conversation, later referenced in responses
- Sentiment detection (worried, curious, frustrated, confused, happy) with empathetic responses paired with a relevant tip
- Graceful handling of unrecognised input with a default fallback message

### Part 3 — Task Assistant, Quiz, NLP Simulation, and Activity Log
- **Task Assistant**: add cybersecurity-related tasks with a title, description, and optional reminder date, stored in a local SQL Server LocalDB database (`CyberTasksDB`). Tasks can be marked complete or deleted, with all changes persisted.
- **Cybersecurity Quiz**: a 12-question mini-game mixing multiple-choice and true/false questions covering phishing, passwords, safe browsing, and social engineering. Each answer gives immediate feedback and an explanation, with a final score and rating message.
- **NLP Simulation**: recognises varied phrasings of common requests using keyword and substring matching, including:
  - "remind me to [task] in [N] days" / "remind me to [task] tomorrow" — creates a task with a reminder
  - "add a task to [task]" — creates a task without a reminder
  - "show my tasks" — lists current tasks in the chat
  - "show activity log" / "what have you done for me" — summarises recent bot actions
  - "start quiz" / "quiz me" — opens the quiz
- **Activity Log**: records key actions (tasks added, reminders set, quiz started/completed) with timestamps, viewable via a dedicated button or through chat commands.

All Part 1 and Part 2 features remain fully functional within the Part 3 GUI.

## Continuous Integration

This repository uses GitHub Actions for CI, configured in `.github/workflows/master.yml`, which builds the solution on every push to verify it compiles successfully.

**CI Status Screenshot:**

> _[Insert a screenshot here of a successful GitHub Actions run (green check mark) before final submission]_

## Video Presentations

- **Part 1 Presentation:** _[Insert unlisted YouTube link here]_
- **Part 2 Presentation:** _[Insert unlisted YouTube link here]_
- **Part 3 / POE Presentation:** _[Insert unlisted YouTube link here]_

## Notes

- The voice greeting requires the `assets/greeting.wav` file to be present relative to the application's working directory; this is copied automatically to the output directory on build.
- The Task Assistant database is created automatically on first use and does not require any manual setup beyond having SQL Server LocalDB available.

 