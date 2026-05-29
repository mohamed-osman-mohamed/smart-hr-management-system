# 🚀 Smart HR Management System (.NET 8 | Layered Architecture | Enterprise-Ready)

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?style=for-the-badge\&logo=dotnet\&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge\&logo=microsoft-sql-server\&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-563D7C?style=for-the-badge\&logo=bootstrap\&logoColor=white)

---

# 📌 Overview

Smart HR Management System is an enterprise-level HR and administration management solution built using ASP.NET Core MVC following Layered Architecture principles.

The system provides secure authentication, dynamic role-based authorization, employee management, department management, and administrative control features.

---

# 🏗️ Architecture

The project follows Layered Architecture principles for scalability, maintainability, and clean separation of concerns.

```bash
Solution Structure

├── PL_Solution
│   ├── Controllers
│   ├── ViewModels
│   ├── Views
│   ├── Utilities
│   └── wwwroot
│
├── BLL_Solution
│   ├── DTOs
│   ├── Services
│   ├── Profiles
│   ├── Factories
│   └── Service Abstractions
│
├── DAL_Solution
│   ├── Data
│   ├── Models
│   ├── Repositories
│   └── Persistence
```

---

# ✨ Features

## 🔐 Authentication & Authorization

* Login & Registration System
* ASP.NET Core Identity
* Secure Authentication Flow
* Role-Based Authorization
* Dynamic Permissions Management
* Password Hashing
* Remember Me Functionality
* Email Confirmation
* Forgot Password Recovery via Email

## 👨‍💼 HR Management

* Employees Management
* Departments Management
* Users Management
* Roles Management
* Administrative Dashboard

## ⚡ Advanced Features

* AJAX Live Search
* Repository Pattern
* AutoMapper Integration
* Dependency Injection
* Layered Architecture
* Clean Separation of Concerns

---

# 🧰 Tech Stack

* ASP.NET Core MVC
* ASP.NET Core Identity
* Entity Framework Core
* SQL Server
* AutoMapper
* Bootstrap
* AJAX
* jQuery
* LINQ
* Dependency Injection

---

# ⚙️ Installation & Setup

## 1️⃣ Clone Repository

```bash
git clone https://github.com/mohamed-osman-mohamed/smart-hr-management-system.git
```

---

## 2️⃣ Configure Database

Update the connection string inside:

```bash
appsettings.json
```

---

## 3️⃣ Apply Migrations

```bash
dotnet ef database update
```

---

## 4️⃣ Run The Project

```bash
dotnet run
```

---

# 📸 Project Preview

## 🖥️ Home Page

![Home](screenshots/home-page.jpg)

---

## 🔐 Login Page

![Login](screenshots/login-page.jpg)

---

## 📝 Registration Page

![Register](screenshots/registration-page.jpg)

---

## 👨‍💼 Employees Management

![Employees](screenshots/employees-page.jpg)

---

## ➕ Add New Employee

![Add Employee](screenshots/add-new-employee-page.jpg)

---

## ✏️ Edit Employee

![Edit Employee](screenshots/edit-employee-page.jpg)

---

## 🗑️ Delete Employee

![Delete Employee](screenshots/delete-employee-page.jpg)

---

## 📄 Employee Details

![Employee Details](screenshots/employee-details-page.jpg)

---

## 🏢 Departments Management

![Departments](screenshots/department-page.jpg)

---

## ➕ Create Department

![Create Department](screenshots/Create-department-page.jpg)

---

## ✏️ Edit Department

![Edit Department](screenshots/edit-department -page.jpg)

---

## 📄 Department Details

![Department Details](screenshots/department-details-page.jpg)

---

## 👥 Users Management

![Users](screenshots/users-page.jpg)

---

## ✏️ Edit User

![Edit User](screenshots/edit-user-page.jpg)

---

## 📄 User Details

![User Details](screenshots/user-datails-page.jpg)

---

## 🛡️ Roles Management

![Roles](screenshots/roles-page.jpg)

---

## ➕ Create Role

![Create Role](screenshots/create-role-page.jpg)

---

## 🔍 AJAX Live Search

![Search](screenshots/life-searchbar.jpg)

---

## 🔑 Forgot Password

![Forgot Password](screenshots/forget-password-page.jpg)

---

## 🖥️ Admin Dashboard

![Dashboard](screenshots/admin-home-page.jpg)

---

# 🚀 Future Improvements

* Audit Logging
* Export Reports
* Real-Time Notifications
* RESTful API Integration
* Advanced Analytics Dashboard

---

# 👨‍💻 Author

## Mohamed Osman

ASP.NET Core Backend Developer

* Passionate about Backend Engineering
* Interested in Enterprise Architecture
* Focused on Clean Code & Scalable Systems

---

# ⭐ Support

If you like this project, don't forget to give it a ⭐ on GitHub.
