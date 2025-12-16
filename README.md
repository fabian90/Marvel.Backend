# ⚡ Pokémon Backend API – .NET 8

## 🧭 Tabla de Contenido
1. [📜 Descripción](#-descripción)
2. [🏗️ Arquitectura](#️-arquitectura)
    * [Capas del Sistema](#capas-del-sistema)
3. [📁 Estructura del Proyecto](#-estructura-del-proyecto)
4. [🔒 Seguridad](#-seguridad)
5. [🌐 Integración con PokeAPI v2](#-integración-con-pokeapi-v2)
6. [🚦 Endpoints Principales](#-endpoints-principales)
7. [🖥️ IDE y Manual de Instalación](#️-ide-y-manual-de-instalación)
    * [🔧 IDE Recomendado](#️-ide-recomendado)
    * [📋 Requerimientos del Sistema](#-requerimientos-del-sistema)
    * [🚀 Manual de Instalación y Ejecución](#-manual-de-instalación-y-ejecución)
8. [💾 Base de Datos](#-base-de-datos)
9. [🛠️ Tecnologías Utilizadas](#️-tecnologías-utilizadas)
10. [⭐ Objetivo](#-objetivo)

---

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

## 🖥️ IDE y Manual de Instalación

### 🔧 IDE Recomendado

Para el desarrollo y ejecución del proyecto se recomienda el uso del siguiente entorno:

* **IDE:** Visual Studio 2022 o superior
* **Workloads requeridos:**
    * ✔️ ASP.NET and web development
    * ✔️ .NET desktop development (opcional)
* **Extensiones recomendadas:**
    * Swagger / OpenAPI Support
    * GitHub Extension for Visual Studio (opcional)

> 💡 El proyecto también puede ejecutarse completamente desde la **CLI de .NET**, sin necesidad de un IDE gráfico.

### 📋 Requerimientos del Sistema

Antes de ejecutar la aplicación, asegúrate de contar con:

* **Sistema Operativo:** Windows, Linux o macOS
* **.NET SDK:** versión **8.0** o superior (verificar con `dotnet --version`)
* **Git** instalado
* Navegador web moderno (Chrome, Edge, Firefox)
* Conexión a Internet (para consumo de PokeAPI v2)

### 🚀 Manual de Instalación y Ejecución

#### 1️⃣ Clonar el repositorio

```bash
git clone [https://github.com/fabian90/Marvel.Backend.git](https://github.com/fabian90/Marvel.Backend.git)
cd Marvel.Backend

2️⃣ Restaurar dependencias
Bash

dotnet restore

3️⃣ Configurar variables de aplicación
Editar el archivo appsettings.json ubicado en el proyecto Marvel.Api:

JSON

"Jwt": {
  "Secret": "CLAVE_SUPER_SECRETA_Y_LARGA_DE_AL_MENOS_256_BITS",
  "Issuer": "PokemonApi",
  "Audience": "PokemonApiUsers"
}
⚠️ Advertencia: En entornos productivos, esta configuración debe almacenarse en variables de entorno.

4️⃣ Ejecutar la aplicación
Desde la raíz del proyecto:

Bash

dotnet run --project Marvel.Api
Al iniciar correctamente, la API quedará disponible en:

https://localhost:{puerto}

5️⃣ Acceder a la documentación Swagger
Una vez en ejecución, la documentación interactiva se encuentra en:

https://localhost:{puerto}/swagger

Desde Swagger es posible:

Probar endpoints

Autenticarse con JWT

Validar flujos completos de la API

6️⃣ Consumo desde Frontend (opcional)
El backend está preparado para ser consumido desde aplicaciones frontend modernas como Angular, React o Vue.

Incluye soporte para:

CORS

Autenticación mediante Bearer Token

Preflight automático (OPTIONS 204)

✅ Verificación de Correcta Instalación
La instalación se considera exitosa cuando:

Swagger carga correctamente.

El endpoint /api/auth/register responde.

Se puede obtener un token JWT vía /api/auth/login.

Los endpoints protegidos responden con autorización válida (Código 200).

💾 Base de Datos
Se utiliza Base de Datos InMemory para un desarrollo rápido y pruebas iniciales.

La arquitectura está preparada para una fácil migración a SQL Server, PostgreSQL u otro motor relacional compatible con Entity Framework Core.

🛠️ Tecnologías Utilizadas
.NET 8

Entity Framework Core

MediatR (Para la implementación de CQRS)

FluentValidation (Para la validación de DTOs)

JWT Bearer Authentication

Swagger / OpenAPI

HttpClient

PokeAPI v2

⭐ Objetivo
Este proyecto sirve para demostrar:

Dominio y aplicación de .NET moderno (8.0).

Aplicación correcta de Clean Architecture y patrones como CQRS.

Implementación segura de JWT para autenticación y autorización.

Integración con APIs externas reales.

Entrega de código limpio, escalable y profesional.