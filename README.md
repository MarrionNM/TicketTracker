# Ticket Tracker

This repository contains the full-stack implementation of the **Ticket Tracker Platform**, consisting of:

- **Frontend:** Angular 18 (`TaskTraker.Web`)
- **Backend:** .NET 8 Web API (`TaskTracker_BE`)

---

## Prerequisites

Make sure the following tools are installed on your machine:

- [Node.js](https://nodejs.org/) (v18+)
- [Angular CLI](https://angular.io/cli) (v18+)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/) or any IDE
- [Postman](https://www.postman.com/) (optional)

> **If you have any issues setting up the project, feel free to contact me — I can walk you through the entire setup.**

---

# Getting Started

All commands assume you are in the **root project folder (`TaskTracker`)**.

---

# Frontend Setup (Angular)

### 1. Navigate to the frontend project

```bash
cd TaskTraker.Web
```

### 2. Install dependencies

```bash
npm install
```

### 3. Configure the environment file

Open: src/environments/environment.ts

Replace the content with:

```json
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5283/api/'
};
```

### 4. Run the Angular application

```bash
ng serve
```

Your frontend will be available at:

👉 http://localhost:4200

# Backend Setup (.NET 8)

### 1. Navigate to the backend root

```bash
cd TaskTracker_BE/TaskTracker.Api
```

### 2. Clean, restore, and build the solution

```bash
dotnet clean
dotnet restore
dotnet build
```

### 3. Configure application settings

### 4. Run the API

```bash
dotnet run
```

Your backend will be available at:

👉 https://localhost:5283

# Project Structure

```bash
TaskTracker_BE/
│
├── TaskTraker.Web/
│
├── TaskTracker_BE/
│   └── TaskTracker.Api/
|   └── TaskTracker.Tests/
│
├── README.md
└── SOLUTION.md
```
