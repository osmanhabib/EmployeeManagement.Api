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
   git clone https://github.com/osmanhabib/EmployeeManagement.Api.git
   cd EmployeeManagement.Api
  ```

## Postman Collection
```

{
	"info": {
		"_postman_id": "2ca86a4b-eb35-47cd-bc87-efe020bef8fb",
		"name": "SELISE-Assesment",
		"schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
		"_exporter_id": "39475976"
	},
	"item": [
		{
			"name": "CreateEmployee",
			"request": {
				"auth": {
					"type": "noauth"
				},
				"method": "POST",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n    \"FirstName\":\"Md.Osman\",\r\n    \"lastname\":\"Habib\",\r\n    \"email\":\"osman@gmail.com\",\r\n    \"password\": \"Asdf@12345\",\r\n    \"DepartmentId\":\"217fff7a-d1cc-4405-a501-8ff90e7755\",\r\n    \"DesignationId\":\"117fff7a-d1cc-4405-a501-8ff90e1021\"\r\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "http://localhost:5107/employee/create",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5107",
					"path": [
						"employee",
						"create"
					]
				}
			},
			"response": []
		},
		{
			"name": "Login",
			"request": {
				"method": "POST",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n    \"email\":\"osman@gmail.com\",\r\n    \"password\" : \"Asdf@12345\"\r\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "http://localhost:5107/auth/login",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5107",
					"path": [
						"auth",
						"login"
					]
				}
			},
			"response": []
		},
		{
			"name": "GetDepartments",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxIiwiZ2l2ZW5fbmFtZSI6Im9zbWFuQGdtYWlsLmNvbSIsIlJvbGUiOiJBZG1pbiIsImV4cCI6MTczNDIxMjc2NiwiaXNzIjoiRW1wbG95ZWVNYW5hZ2VtZW50IiwiYXVkIjoiRW1wbG95ZWVNYW5hZ2VtZW50In0.BW_QRkVaxz3GAIuzP_puT5FD1KNGTbBjfzi7TmPU5ow",
							"type": "string"
						}
					]
				},
				"method": "GET",
				"header": [],
				"url": {
					"raw": "http://localhost:5107/department/getlist",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5107",
					"path": [
						"department",
						"getlist"
					]
				}
			},
			"response": []
		},
		{
			"name": "GetDesignations",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxIiwiZ2l2ZW5fbmFtZSI6Im9zbWFuQGdtYWlsLmNvbSIsIlJvbGUiOiJBZG1pbiIsImV4cCI6MTczNDIxMjc2NiwiaXNzIjoiRW1wbG95ZWVNYW5hZ2VtZW50IiwiYXVkIjoiRW1wbG95ZWVNYW5hZ2VtZW50In0.BW_QRkVaxz3GAIuzP_puT5FD1KNGTbBjfzi7TmPU5ow",
							"type": "string"
						}
					]
				},
				"method": "GET",
				"header": [],
				"url": {
					"raw": "http://localhost:5107/designation/getlist",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5107",
					"path": [
						"designation",
						"getlist"
					]
				}
			},
			"response": []
		},
		{
			"name": "DeleteEmployee",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0YjczOTVmMy1iODM3LTQ1OTgtOTEyMi1lNWVlNGY5MTgxZDciLCJFbWFpbCI6Im9zbWFuQGdtYWlsLmNvbSIsIlJvbGUiOiJ1c2VyIiwiZXhwIjoxNzM0MjE5NTU0LCJpc3MiOiJFbXBsb3llZU1hbmFnZW1lbnQiLCJhdWQiOiJFbXBsb3llZU1hbmFnZW1lbnQifQ.K_gdMesC5sRAao9aAX9FlBv20d3F4sKcH473OWCm39s",
							"type": "string"
						}
					]
				},
				"method": "DELETE",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n    \"EmployeeId\" : \"10d9eea4-9aea-4113-81ff-9627497fc14c\"\r\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "http://localhost:5107/employee/delete",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5107",
					"path": [
						"employee",
						"delete"
					]
				}
			},
			"response": []
		},
		{
			"name": "UpdateEmployee",
			"request": {
				"auth": {
					"type": "bearer",
					"bearer": [
						{
							"key": "token",
							"value": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiI0YjczOTVmMy1iODM3LTQ1OTgtOTEyMi1lNWVlNGY5MTgxZDciLCJFbWFpbCI6Im9zbWFuQGdtYWlsLmNvbSIsIlJvbGUiOiJ1c2VyIiwiZXhwIjoxNzM0MjE5MjU0LCJpc3MiOiJFbXBsb3llZU1hbmFnZW1lbnQiLCJhdWQiOiJFbXBsb3llZU1hbmFnZW1lbnQifQ.9WAyvb-V_uWtpsisPrfDvYURnsSjcvv5CgxnQ4LnluM",
							"type": "string"
						}
					]
				},
				"method": "PUT",
				"header": [],
				"body": {
					"mode": "raw",
					"raw": "{\r\n    \"FirstName\" : \"Osman\"\r\n}",
					"options": {
						"raw": {
							"language": "json"
						}
					}
				},
				"url": {
					"raw": "http://localhost:5107/employee/update",
					"protocol": "http",
					"host": [
						"localhost"
					],
					"port": "5107",
					"path": [
						"employee",
						"update"
					]
				}
			},
			"response": []
		}
	]
}

```

