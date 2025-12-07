📦 Multi-Warehouse Inventory Management System

A Multi-Warehouse Inventory Management System integrated with an Ecommerce Platform, built using ASP.NET Core Web API.
This system provides full control over orders, stocks, inventory auditing, and truck logistics, secured with JWT Authentication and an advanced role & permission system.

🚀 Key Features

✅ Full integration with Ecommerce platform

✅ Order creation, confirmation, and quantity synchronization

✅ Stock management with approval workflow

✅ Two-stage inventory auditing system

✅ Inventory discrepancy tracking with reasons

✅ Truck connection, loading, and unloading management

✅ Custom role-based authorization system

✅ Dual JWT Authentication (Internal & Ecommerce)

✅ Clean layered architecture:

Presentation Layer

Business Layer

Data Layer

🔐 Authentication System

The project uses JWT Bearer Authentication with two separate schemes:

1️⃣ Internal System Authentication

Used by:

Warehouse employees

Inventory controllers

Truck managers

Admin operations

2️⃣ Ecommerce Authentication

Used only by the Ecommerce platform for:

Creating orders

Confirming orders

Syncing order quantities



Solution
│
├── Presentation_Layer (Controllers, Authorization)
├── Business_Layer (Business Logic)
├── Data_Layer (Database Access)
├── DTOs (Data Transfer Objects)
├── Enums (Permissions, States)
└── Options (JWT & Ecommerce Configuration)

🛡️ Security Features

✅ JWT Token Validation

✅ Issuer & Audience Validation

✅ Signing Key Verification

✅ Role-Based Access Control

✅ Secure separation between Ecommerce & Internal users


Technologies Used:

ASP.NET Core Web API

JWT Authentication

Dependency Injection

Clean Architecture Pattern

RESTful API Design
