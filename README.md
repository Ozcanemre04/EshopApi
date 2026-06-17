# EshopApi
 REST API for managing products, orders and payments for an e-commerce application.

## Features

- Product management
- Order processing
- Authentication with JWT
- Role-based authorization
- Swagger documentation
- Entity Framework Core
- Stripe payments

## Technologies

- .NET 8
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Swagger/OpenAPI
- Docker
- Stripe

## Architecture

EshopApi
├── Eshop.UnitTest
│   ├── Eshop.Api.Test
│   ├── Eshop.Application.Test
│   └── Eshop.Infrastructure.Test
└── src
    ├── Eshop.Api
    ├── Eshop.Application
    ├── Eshop.Domain
    └── Eshop.Infrastructure

## Prerequisites

- .NET SDK 8.0+
- Docker
- Git

## Environment Variables

- Create a `.env` file in the root of the project:

  ### Database:

  - ConnectionStrings__DefaultConnection=Host=your-Postgres-Host;Port=your-Postgres-Port;    Database=your-Postgres-DB;Username=your-Postgres-Username;Password=your-Postgres-Password
  - POSTGRES_USER=your-Postgres-User
  - POSTGRES_PASSWORD=your-Postgres-Password
  - POSTGRES_DB=your-Postgres-DB

  ### JWT:

  - JWT__Key=a-string-secret-at-least-512-bits-long
  - JWT__Issuer=your-app-url
  - JWT__Audience=your-app-users-url

  ### Api:
  - AspnetCoreUrl=your-Url

  ### Admin:

  - Admin__FirstName=your-admin-Firstname
  - Admin__LastName=your-admin-Lastname
  - Admin__Email=your-admin-Email
  - Admin__UserName=your-admin-Username
  - Admin__Refreshtoken=your-admin-Refresh-token
  - Admin__Password=your-admin-Password
  - Admin__Id=your-admin-Id

  ### Stripe:

  - Stripe__SecretKey=your-Stripe-SecretKey

## Installation

1. Clone repository

   git clone https://github.com/Ozcanemre04/EshopApi.git

2. Navigate to project

   cd eshop

3. Build project in Docker

   docker compose up --build


## API Documentation

After starting the application:

http://localhost:5010/swagger


## Testing

Run tests:

dotnet test