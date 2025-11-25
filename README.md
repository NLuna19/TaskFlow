# 🧩 TaskFlow API

**TaskFlow** es una API REST desarrollada con **.NET 8** bajo una arquitectura **Clean Architecture**, que permite gestionar proyectos y tareas al estilo Trello o Jira básico.  
Su objetivo es servir como base sólida para proyectos personales, de portfolio o implementaciones empresariales que requieran buenas prácticas en backend con .NET.

---

## 🚀 Tecnologías principales

| Capa               | Tecnologías                                           |
| ------------------ | ----------------------------------------------------- |
| **WebApi**         | .NET 8 Web API, Swagger, JWT (in progress)            |
| **Application**    | FluentValidation, AutoMapper (in progress), Servicios |
| **Infrastructure** | Entity Framework Core, Repositorios                   |
| **Domain**         | Entidades y enums puros                               |

Además:

- Serilog (pendiente de integración)

- SQLite / SQL Server como base de datos

- Preparado para integrarse a frontends Angular o React

---

## 🧱 Arquitectura (Clean Architecture)

El proyecto sigue los principios de **Clean Architecture** y separación por capas:

```

TaskFlow
│
├── TaskFlow.Domain
│   └── Entidades puras (User, Project, TaskItem, enums)
│
├── TaskFlow.Application
│   ├── Interfaces (Repos & Services)
│   ├── Servicios de negocio independientes de infraestructura
│   ├── Validators (FluentValidation
│   └── DTOs & Mapping (pendiente)
│
├── TaskFlow.Infrastructure
│   ├── AppDbContext (EF Core)
│   ├── Repositorios concretos
│   └── Configs de entidad (pendiente)
│
└── TaskFlow.WebApi
    ├── Controllers
    ├── Inyección de dependencias
    ├── Middlewares (pendiente)
    └── Auth (JWT en progreso)

```
✔ Las capas superiores nunca referencian WebApi ni Infrastructure <br>
✔ Application conoce solo interfaces, no implementaciones <br>
✔ Domain no depende de nadie

---

# 📚 Entidades principales

### **User**

* Id, Username, Email
* PasswordHash (sin contraseña en texto plano)
* Relación con proyectos

### **Project**

* Id, Name, Description
* OwnerId
* Lista de TaskItem

### **TaskItem**

* Id, Title, Description
* ProjectId (FK)
* Status (enum: Todo / InProgress / Done)

---

# ⚙️ Estado actual del proyecto

### ✔ Domain completo

### ✔ Repositorios creados

### ✔ Servicios con validación

### ✔ FluentValidation funcionando en Application

### ✔ WebApi consumiendo servicios en lugar de DbContext

### ✔ Arquitectura limpia sin ciclos de dependencia

### ✔ AutoMapper + DTOs

### 🔄 En progreso

* JWT Authentication
* Logs con Serilog
* Configs de EF Core en carpeta **Configurations**
* Manejo de excepciones global (Middleware)

### 🧩 Pendiente

* Tests unitarios (xUnit)
* Documentación avanzada en Swagger
* Dockerfile + docker-compose

---

## ⚙️ Configuración local

### 1️⃣ Clonar el repositorio

```bash
git clone https://github.com/tuusuario/TaskFlow.git
cd TaskFlow
```

### 2️⃣ Restaurar dependencias

```bash
dotnet restore
```

### 3️⃣ Compilar

```bash
dotnet build
```

### 4️⃣ Ejecutar la API

```bash
dotnet run --project TaskFlow.WebApi
```

La API se ejecutará por defecto en:

```
https://localhost:7051
```

### 5️⃣ Swagger UI

Podés explorar los endpoints en:
👉 [https://localhost:7051/swagger](https://localhost:7051/swagger)

---

# 🧰 Estructura de carpetas (real)

```
TaskFlow
│
├── TaskFlow.Domain
│   ├── Entities/
│   └── Enums/
│
├── TaskFlow.Application
│   ├── Interfaces
│   │     ├──Repositories/
│   │     └──Services/
│   ├── Services/
│   ├── Validators/
│   ├── DTOs/ (pendiente)
│   └── Mappings/ (pendiente)
│
├── TaskFlow.Infrastructure
│   ├── Data/
│   ├── Repositories/
│   └── Configurations/ (pendiente)
│
└── TaskFlow.WebApi
    ├── Controllers/
    └── Program.cs (DI + Swagger)
```

---

# 🔐 Próximos pasos (Roadmap)

* [ ] Implementar JWT completo (Login + Register + Claims)
* [ ] Agregar manejo de excepciones global
* [ ] Logs estructurados con Serilog
* [ ] Tests unitarios
* [ ] Dockerización
* [ ] Mejorar documentación Swagger
* [ ] Crear un Frontend (Angular)

---

## 🧰 Autor

**Nicolás Omar Luna** <br>
💼 Fullstack Developer <br>
📧 [[nluna190898@gmail.com](mailto:nluna190898@gmail.com)] <br>
🌐 [linkedin.com/in/nluna190898](https://linkedin.com/in/nluna190898)

---
