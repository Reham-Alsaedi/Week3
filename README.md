-- Document Verification System --

- Overview

Our company provides secure digital solutions for businesses and government entities in Saudi Arabia. 
This document verification system allows users to upload official documents and verify them through a unique digital code.

- Tech Stack

Backend: ASP.NET Core with Entity Framework Core & Dapper

Frontend: Angular with TypeScript

Database: SQL Server

- Features

Secure document upload

Digital code-based verification

Efficient data retrieval using EF Core & Dapper

User authentication & authorization

- Setup Instructions

Prerequisites

- Backend:

.NET 8 SDK or later

SQL Server

Entity Framework Core CLI

- Frontend:

Node.js (Latest LTS Version)

Angular CLI

- Backend Setup

Clone the repository:

git clone https://github.com/your-repo/document-verification.git
cd document-verification/Backend

Configure the database connection in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DocumentVerificationDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}

Apply database migrations:

dotnet ef database update

Run the backend API:

dotnet run

- Frontend Setup

Navigate to the frontend directory:

cd ../Frontend

Install dependencies:

npm install

Start the Angular application:

ng serve

The application should now be accessible at http://localhost:4200/.

API Documentation

Base URL

http://localhost:5000/api

Endpoints

1. Upload Document

Endpoint: POST /documents/upload


2. Verify Document

Endpoint: GET /documents/verify/{verificationCode}


3. Fetch User Documents

Endpoint: GET /documents/user/{userId}



Contribution

Fork the repository.

Create a new branch: git checkout -b feature-name

Commit changes: git commit -m 'Added feature-name'

Push to branch: git push origin feature-name

Create a Pull Request.
