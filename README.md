# Marvel.Backend API

## Descripción

Marvel.Backend es una **API REST** desarrollada en **.NET 8**, siguiendo los principios de **Domain-Driven Design (DDD)** y **Clean Architecture**.  

El sistema permite a los usuarios registrarse, autenticarse mediante **JWT** y gestionar su lista personalizada de cómics favoritos, integrándose con un **Marvel Mock Service** para obtener información de cómics simulada.

---

## Arquitectura

La solución está organizada siguiendo **Clean Architecture + DDD**, separando responsabilidades en capas bien definidas:

### Capas y proyectos

1. **Marvel.Domain**  
   - Contiene las **entidades de dominio** y lógica de negocio.  
   - Ejemplo de entidades: `User`, `ComicFavorite`.  
   - Aquí viven las reglas de negocio, invariantes y validaciones propias del dominio.

2. **Marvel.Application**  
   - Contiene **DTOs, servicios de aplicación, interfaces, commands y queries (CQRS)**.  
   - Los servicios coordinan la lógica de negocio usando las entidades del dominio y repositorios.  
   - Aquí también se implementa la validación de requests mediante **FluentValidation**.

3. **Marvel.Infrastructure**  
   - Contiene la **implementación de repositorios**, **persistencia con EF Core** y acceso a servicios externos (como Marvel Mock Service).  
   - Configuración de `ApplicationDbContext` con `DbSet<User>` y `DbSet<ComicFavorite>`.

4. **Marvel.Api**  
   - Contiene los **controllers**, middleware, configuración de Swagger y JWT.  
   - Expone los endpoints REST para autenticación y gestión de favoritos.  
   - Protege los endpoints mediante `[Authorize]` y obtiene `UserId` desde el token JWT.

---

## Estructura de carpetas

Marvel.Backend.sln
│
├─ Marvel.Domain
│ └─ Entities
│ ├─ User.cs
│ └─ ComicFavorite.cs
│
├─ Marvel.Application
│ ├─ DTOs
│ │ ├─ Auth
│ │ │ ├─ RegisterRequestDto.cs
│ │ │ ├─ LoginRequestDto.cs
│ │ │ └─ AuthResponseDto.cs
│ │ └─ Favorites
│ │ ├─ AddFavoriteRequest.cs
│ │ └─ FavoriteResponse.cs
│ ├─ Interfaces
│ │ ├─ IAuthenticationService.cs
│ │ ├─ IFavoriteService.cs
│ │ └─ IJwtProvider.cs
│ ├─ Services
│ │ ├─ AuthenticationService.cs
│ │ └─ FavoriteService.cs
│ ├─ Validators
│ │ └─ AddFavoriteRequestValidator.cs
│ └─ Queries / Commands (CQRS)
│
├─ Marvel.Infrastructure
│ ├─ Persistence
│ │ └─ ApplicationDbContext.cs
│ ├─ Repositories
│ │ ├─ UserRepository.cs
│ │ └─ ComicFavoriteRepository.cs
│ └─ Services
│ └─ MarvelMockService.cs
│
└─ Marvel.Api
├─ Controllers
│ ├─ AuthController.cs
│ └─ FavoritesController.cs
├─ Program.cs
└─ appsettings.json
---

## Versiones y librerías

- **.NET 8**
- **Entity Framework Core** → Persistencia y migraciones
- **FluentValidation** → Validación de DTOs
- **Swashbuckle (Swagger)** → Documentación de API
- **Microsoft.AspNetCore.Authentication.JwtBearer** → JWT y seguridad
- **InMemoryDatabase / SQL Server** → Opcional para desarrollo y producción
- **MediatR** → CQRS (comandos y consultas)

---

## Endpoints principales

| Método | Ruta | Descripción | Autenticación |
|--------|------|------------|---------------|
| POST   | /api/auth/register | Registrar usuario | No |
| POST   | /api/auth/login    | Login y obtener JWT | No |
| POST   | /api/favorites     | Agregar cómic favorito | Sí |
| DELETE | /api/favorites/{comicId} | Eliminar favorito | Sí |
| GET    | /api/favorites     | Listar favoritos | Sí |

---

## Ejecución

1. Clonar repositorio.
2. Configurar `appsettings.json` con la cadena de conexión a SQL Server o usar InMemoryDatabase.
3. Restaurar paquetes NuGet:

```bash
dotnet restore
