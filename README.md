# ⚡ Pokémon Backend API – .NET 8

## 📜 Descripción

**Pokémon Backend API** es una API REST desarrollada en **.NET 8**, diseñada bajo los principios de **Clean Architecture**, **Domain-Driven Design (DDD ligero)** y **CQRS** con **MediatR**.

La aplicación permite:
* Autenticación de usuarios mediante **JWT**.
* Consumo de datos reales desde **PokeAPI v2**.
* Gestión de una lista personalizada de Pokémon favoritos, protegida por autorización.

Este proyecto fue desarrollado como prueba técnica, priorizando **buenas prácticas**, **mantenibilidad** y una **arquitectura escalable**.

---

## 🏗️ Arquitectura

La solución implementa **Clean Architecture** (Arquitectura Limpia), separando responsabilidades en capas independientes y desacopladas para lograr un bajo acoplamiento y alta cohesión. 

[Image of Clean Architecture diagram]


### Capas del Sistema

| Capa | Proyecto | Responsabilidades | Dependencias |
| :--- | :--- | :--- | :--- |
| **Domain** | `Marvel.Domain` | Entidades de dominio (`User`, `PokemonFavorite`), Reglas de negocio. | Ninguna externa. |
| **Application** | `Marvel.Application` | DTOs (Request/Response), Servicios de aplicación, Interfaces, **CQRS** (Queries/Handlers), Validaciones con **FluentValidation**. | Domain, Infrastructure (solo interfaces). |
| **Infrastructure** | `Marvel.Infrastructure` | Persistencia con **Entity Framework Core**, Repositorios, Implementación de **JWT**, Consumo de **PokeAPI v2** mediante `HttpClient`. | Domain, Application, Entity Framework Core. |
| **API** | `Marvel.Api` | Controllers REST, Configuración de **Swagger**, Autenticación y autorización **JWT**. | Domain, Application, Infrastructure. |

---

## 📁 Estructura del Proyecto

Marvel.Backend.sln │ ├─ Marvel.Domain │ └─ Entities │   ├─ User.cs │   └─ PokemonFavorite.cs │ ├─ Marvel.Application │ ├─ DTOs │ ├─ Interfaces │ ├─ Services │ ├─ Validators │ └─ Queries (CQRS) │ ├─ Marvel.Infrastructure │ ├─ Persistence │ ├─ Repositories │ └─ Services │   └─ PokeApiService.cs │ └─ Marvel.Api   ├─ Controllers   ├─ Program.cs   └─ appsettings.json

---

## 🔒 Seguridad

* **Autenticación** basada en **JWT Bearer**.
* Tokens firmados con **HMAC SHA256**.
* Endpoints protegidos con el atributo `[Authorize]`.
* **Swagger** configurado para permitir la autenticación JWT.

---

## 🌐 Integración con PokeAPI v2

Se consumen los siguientes *endpoints* reales de la PokeAPI para obtener información de Pokémon:

* `GET /pokemon?offset&limit`
* `GET /pokemon/{id | name}`

> 💡 **Nota:** Los datos externos se **mapean** a DTOs propios del dominio, evitando exponer modelos externos.

---

## 🚦 Endpoints Principales

| Método | Endpoint | Descripción | Autenticación Requerida |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/auth/register` | Registro de nuevo usuario. | No |
| `POST` | `/api/auth/login` | Login y obtención del **JWT Bearer Token**. | No |
| `GET` | `/api/pokemon` | Listado paginado de Pokémon (datos de PokeAPI). | Sí |
| `GET` | `/api/pokemon/{id}` | Detalle de un Pokémon por ID. | Sí |
| `POST` | `/api/favorites` | Agregar un Pokémon a la lista de favoritos. | Sí |
| `DELETE` | `/api/favorites/{pokemonId}` | Eliminar un Pokémon de favoritos por ID. | Sí |
| `GET` | `/api/favorites` | Listar los Pokémon favoritos del usuario actual. | Sí |

---

## ⚙️ Instalación y Ejecución

### Requisitos

* .NET SDK **8.0**
* Visual Studio 2022 o superior (opcional, se puede usar CLI)
* **Git**
* Conexión a Internet (para el consumo de PokeAPI)

### Pasos

1.  **Clonar el repositorio:**
    ```bash
    git clone [https://github.com/fabian90/Marvel.Backend.git](https://github.com/fabian90/Marvel.Backend.git)
    cd Marvel.Backend
    ```

2.  **Restaurar dependencias:**
    ```bash
    dotnet restore
    ```

3.  **Configurar `appsettings.json`:**
    Asegúrate de configurar la sección `Jwt` con valores seguros y únicos:

    ```json
    "Jwt": {
      "Secret": "CLAVE_SUPER_SECRETA_Y_LARGA_DE_AL_MENOS_256_BITS",
      "Issuer": "PokemonApi",
      "Audience": "PokemonApiUsers"
    }
    ```

4.  **Ejecutar la aplicación:**
    ```bash
    dotnet run --project Marvel.Api
    ```

5.  **Acceder a Swagger:**
    Una vez ejecutada, la documentación de la API estará disponible en:
    `https://localhost:{puerto}/swagger`

---

## 💾 Base de Datos

* Se utiliza **Base de Datos InMemory** para un desarrollo rápido y pruebas iniciales.
* La arquitectura está preparada para una fácil migración a **SQL Server**, PostgreSQL u otro motor relacional compatible con Entity Framework Core.

---

## 🛠️ Tecnologías Utilizadas

* **.NET 8**
* **Entity Framework Core**
* **MediatR** (Para la implementación de CQRS)
* **FluentValidation** (Para la validación de DTOs)
* **JWT Bearer Authentication**
* **Swagger / OpenAPI**
* `HttpClient`
* **PokeAPI v2**

---

## ⭐ Objetivo

Este proyecto sirve para demostrar:

* Dominio y aplicación de **.NET moderno (8.0)**.
* Aplicación correcta de **Clean Architecture** y patrones como **CQRS**.
* Implementación segura de **JWT** para autenticación y autorización.
* Integración con **APIs externas** reales.
* Entrega de **código limpio, escalable y profesional**.

