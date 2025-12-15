Pokémon Backend API – .NET 8
 Descripción

Pokémon Backend API es una API REST desarrollada en .NET 8, diseñada bajo los principios de Clean Architecture, Domain-Driven Design (DDD ligero) y CQRS con MediatR.

La aplicación permite la autenticación de usuarios mediante JWT, el consumo de datos reales desde PokeAPI v2 y la gestión de una lista personalizada de Pokémon favoritos, protegida por autorización.

Este proyecto fue desarrollado como prueba técnica, priorizando buenas prácticas, mantenibilidad y arquitectura escalable.

 Arquitectura

La solución implementa Clean Architecture, separando responsabilidades en capas independientes y desacopladas.

Capas del sistema
🔹 Domain (Marvel.Domain)

Entidades del dominio (User, PokemonFavorite)

Reglas de negocio

Sin dependencias externas

🔹 Application (Marvel.Application)

DTOs (Request / Response)

Servicios de aplicación

Interfaces

CQRS (Queries y Handlers)

Validaciones con FluentValidation

🔹 Infrastructure (Marvel.Infrastructure)

Persistencia con Entity Framework Core

Repositorios

Implementación de JWT

Consumo de PokeAPI v2 mediante HttpClient

🔹 API (Marvel.Api)

Controllers REST

Configuración de Swagger

Autenticación y autorización JWT

📁 Estructura del Proyecto
Marvel.Backend.sln
│
├─ Marvel.Domain
│ └─ Entities
│   ├─ User.cs
│   └─ PokemonFavorite.cs
│
├─ Marvel.Application
│ ├─ DTOs
│ ├─ Interfaces
│ ├─ Services
│ ├─ Validators
│ └─ Queries (CQRS)
│
├─ Marvel.Infrastructure
│ ├─ Persistence
│ ├─ Repositories
│ └─ Services
│   └─ PokeApiService.cs
│
└─ Marvel.Api
  ├─ Controllers
  ├─ Program.cs
  └─ appsettings.json

🔐 Seguridad

Autenticación basada en JWT Bearer

Tokens firmados con HMAC SHA256

Endpoints protegidos con [Authorize]

Swagger configurado para autenticación JWT

🌐 Integración con PokeAPI v2

Se consumen los siguientes endpoints reales:

GET /pokemon?offset&limit

GET /pokemon/{id | name}

Los datos externos se mapean a DTOs propios, evitando exponer modelos externos.

📌 Endpoints Principales
Método	Endpoint	Descripción	Autenticación
POST	/api/auth/register	Registro de usuario	No
POST	/api/auth/login	Login y obtención de JWT	No
GET	/api/pokemon	Listado de Pokémon	Sí
GET	/api/pokemon/{id}	Detalle de Pokémon	Sí
POST	/api/favorites	Agregar Pokémon favorito	Sí
DELETE	/api/favorites/{pokemonId}	Eliminar favorito	Sí
GET	/api/favorites	Listar favoritos	Sí
⚙️ Instalación y Ejecución
Requisitos

.NET SDK 8.0

Visual Studio 2022 o superior

Git

Conexión a Internet (PokeAPI)

Pasos

Clonar el repositorio

git clone <https://github.com/fabian90/Marvel.Backend.git>


Restaurar dependencias

dotnet restore


Configurar appsettings.json

"Jwt": {
  "Secret": "CLAVE_SUPER_SECRETA",
  "Issuer": "PokemonApi",
  "Audience": "PokemonApiUsers"
}


Ejecutar la aplicación

dotnet run --project Marvel.Api


Acceder a Swagger

https://localhost:{puerto}/swagger

🧪 Base de Datos

Base de datos InMemory para desarrollo y pruebas

Arquitectura preparada para SQL Server u otro motor relacional

🧠 Tecnologías Utilizadas

.NET 8

Entity Framework Core

MediatR (CQRS)

FluentValidation

JWT Bearer Authentication

Swagger / OpenAPI

HttpClient

PokeAPI v2

🎯 Objetivo

Este proyecto demuestra:

Dominio de .NET moderno

Aplicación correcta de Clean Architecture

Implementación segura de JWT

Integración con APIs externas reales

Código limpio, escalable y profesional