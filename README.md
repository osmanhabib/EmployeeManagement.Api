# Employee Management API

## Assessment Overview

This project involves the creation of a system to track employee information, including personal details, department, and designation. The system will demonstrate standard practices of CRUD operations, database relationships, architectural patterns, and security practices. The main focus will be on building a secure and maintainable system using modern architectural patterns such as CQRS (Command Query Responsibility Segregation) with JWT authentication and authorization.

## Key Technologies

- **Back-end**: ASP.NET Core Web API
  - **Entity Framework Core (EF Core)** for ORM-based database operations
  - **CQRS** (Command Query Responsibility Segregation) for clean separation of reads and writes
  - **JWT Authentication and Authorization** for securing the API
- **Database**: Microsoft SQL Server (MSSQL) (or any RDBMS)
- **Authentication & Security**: JWT (JSON Web Token) for authentication and authorization
- **Architectural Pattern**: Vertical Slicing with CQRS
- **Logging**: Centralized logging system to capture errors and track the performance of the system
- **API Versioning**: To manage multiple versions of the API
- **Containerization**: Docker (optional) for creating isolated and reproducible environments

## Features

### 1. **Employee Information Management**
   - CRUD operations for employee data:
     - Create, Read, Update, Delete employee personal details
     - Track department and designation details

### 2. **Security**
   - **JWT Authentication**: Protects API endpoints with token-based authentication
   - **Authorization**: Role-based access control for different user roles (Admin, Manager, etc.)

### 3. **CQRS Architecture**
   - **Command side** for creating, updating, and deleting data
   - **Query side** for reading employee data efficiently
   - Vertical slicing to keep concerns separated for better scalability and maintainability

### 4. **Database Relationships**
   - Use of EF Core to manage database models with proper relationships (e.g., one-to-many between employees and departments)

### 5. **Centralized Logging**
   - Implement a logging system (e.g., Serilog) to capture and store logs (e.g., errors, request logs) for monitoring and debugging

### 6. **API Versioning**
   - Version control for API endpoints to maintain backward compatibility while introducing new features.

### 7. **Containerization (Docker)**
   - **Docker** for creating isolated environments and making deployment reproducible and scalable

## Installation

### 1. **Clone the repository**
   - Clone this repository to your local machine:

   ```bash
   git clone https://github.com/yourusername/employee-management-system.git
   cd employee-management-system
