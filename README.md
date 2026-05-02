# 🧾 Order System API

A production-ready **Order Management System API** built with **ASP.NET Core**, following Clean Architecture and best practices. This project demonstrates how to design scalable, maintainable, and testable backend systems using modern .NET technologies.

---

## 🚀 Features

* ✅ Create, update, and manage orders
* ✅ RESTful API design
* ✅ Layered architecture (Controller, Service, Repository)
* ✅ Entity Framework Core (Code First)
* ✅ Database migrations support
* ✅ Dependency Injection (DI)
* ✅ Centralized configuration (`appsettings.json`)
* ✅ Global exception handling
* ✅ Clean and scalable project structure

---

## 🏗️ Architecture

This project follows **Clean Architecture** principles:

```
OrderSystemSolution
│
├── OrderService.API          → Controllers (Entry point)
├── OrderService.Application  → Business logic, DTOs, Interfaces
├── OrderService.Domain       → Entities, Core models
├── OrderService.Infrastructure → EF Core, DB Context, Repositories
```

---

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core Web API
* **Language:** C#
* **ORM:** Entity Framework Core
* **Database:** SQL Server
* **Tools:** Visual Studio / VS Code, Swagger

---

## ⚙️ Getting Started

### 🔹 Prerequisites

* .NET SDK (6 or later)
* SQL Server
* Git

---

### 🔹 Clone Repository

```bash
git clone https://github.com/your-username/order-system-api.git
cd order-system-api
```

---

### 🔹 Configure Database

Update your connection string in:

```
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=OrderDb;Trusted_Connection=True;"
}
```

---

### 🔹 Apply Migrations

```bash
dotnet ef database update
```

---

### 🔹 Run the Application

```bash
dotnet run
```

API will be available at:

```
https://localhost:5001
```

Swagger UI:

```
https://localhost:5001/swagger
```

---

## 📦 API Endpoints (Sample)

| Method | Endpoint         | Description      |
| ------ | ---------------- | ---------------- |
| GET    | /api/orders      | Get all orders   |
| GET    | /api/orders/{id} | Get order by ID  |
| POST   | /api/orders      | Create new order |
| PUT    | /api/orders/{id} | Update order     |
| DELETE | /api/orders/{id} | Delete order     |

---

## 🧩 Key Concepts Used

* Clean Architecture
* Dependency Injection
* Repository Pattern
* DTO (Data Transfer Objects)
* EF Core Migrations
* REST API Design

---

## 🧪 Testing (Optional)

You can test APIs using:

* Swagger UI
* Postman

---

## 📁 .gitignore Highlights

Make sure the following are ignored:

```
bin/
obj/
*.user
appsettings.Development.json
```

---

## ⚠️ Important Notes

* Migration files are included for database version control
* Do not commit sensitive data (connection strings, secrets)
* Use environment variables for production

---

## 🤝 Contributing

Contributions are welcome! Feel free to fork the repo and submit a pull request.

---

## 📄 License

This project is licensed under the MIT License.

---

## 👨‍💻 Author

Developed by **[Your Name]**

---

## ⭐ Support

If you like this project, give it a ⭐ on GitHub!

---
