# Cybersecurity Awareness Bot - Part 1 & 2

## Description
A cybersecurity awareness chatbot that educates users on online safety practices in South Africa.
Part 1 is a console-based chatbot and Part 2 expands it into a full graphical user interface (GUI) application using Windows Forms.
The bot simulates conversation, recognises cybersecurity keywords, detects user sentiment, and remembers user details to personalise responses.

## Requirements
- .NET 8
- Visual Studio 2026
- Windows OS

## Projects in Solution
- **CyberSecurityAwarenessBot** — Core logic and console application (Part 1)
- **CyberSecurityBot.UI** — Windows Forms GUI application (Part 2)

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

## Classes Overview
### Core Project
- **Program.cs** — Entry point for the console application
- **Chatbot.cs** — Main chatbot logic for console interaction
- **AudioPlayer.cs** — Handles WAV file playback
- **Responses.cs** — Keyword recognition and random responses
- **DisplayHelper.cs** — Shared ASCII logo used by both projects
- **MemoryManager.cs** — Stores and recalls user information
- **SentimentDetector.cs** — Detects user sentiment and responds with empathy

### UI Project
- **MainForm.cs** — Main GUI form with all controls and interaction logic
- **MainForm.Designer.cs** — Form initialisation and component setup

## How to Run

### Part 1 - Console App
1. Clone the repository:
```bash
   git clone https://github.com/T8864/CyberSecurityAwarenessBot.git
```
2. Open the solution in Visual Studio
3. Set **CyberSecurityAwarenessBot** as the startup project
4. Press **F5** to run

### Part 2 - GUI App
1. Clone the repository:
```bash
   git clone https://github.com/T8864/CyberSecurityAwarenessBot.git
```
2. Open the solution in Visual Studio
3. Set **CyberSecurityBot.UI** as the startup project
4. Press **F5** to run

## GitHub Actions CI
This project uses GitHub Actions for Continuous Integration.
Every push is automatically checked for a successful build.

