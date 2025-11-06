# 🧩 TaskFlow API

**TaskFlow** es una API REST desarrollada con **.NET 8** bajo una arquitectura **Clean Architecture**, que permite gestionar proyectos y tareas al estilo Trello o Jira básico.  
Su objetivo es servir como base sólida para proyectos personales, de portfolio o implementaciones empresariales que requieran buenas prácticas en backend con .NET.

---

## 🚀 Tecnologías utilizadas

- **.NET 8 Web API**
- **Entity Framework Core** (ORM)
- **SQL Server / SQLite**
- **JWT Authentication**
- **FluentValidation**
- **AutoMapper**
- **Swagger / OpenAPI**
- **Serilog** (logging)
- (Opcional) **Angular / React** como front-end // TODO

---

## 🧱 Arquitectura del proyecto

El proyecto sigue los principios de **Clean Architecture** y separación por capas:

```

TaskFlow
├── TaskFlow.Domain # Entidades y enums base (User, Project, TaskItem)
├── TaskFlow.Application # Casos de uso, DTOs, interfaces, validaciones
├── TaskFlow.Infrastructure # Acceso a datos, EF Core, repositorios concretos
└── TaskFlow.WebApi # Endpoints, autenticación, configuración general

```

Cada capa depende solo de la capa inmediatamente inferior, garantizando independencia y mantenibilidad.

---

## 📚 Entidades principales

- **User** → representa a un usuario registrado (almacena hash de contraseña)
- **Project** → proyecto creado por un usuario
- **TaskItem** → tareas asignadas a proyectos
- **TaskStatus** → enum (`Todo`, `InProgress`, `Done`)

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

## 🔐 Próximos pasos

- Implementar **AppDbContext** con EF Core
- Configurar **JWT Authentication**
- Crear controladores de **Users**, **Projects** y **Tasks**
- Agregar **Swagger y AutoMapper**
- Desplegar con **Docker** o **Azure App Service**

---

## 🧰 Autor

**Nicolás Omar Luna** <br>
💼 Fullstack Developer <br>
📧 [[nluna190898@gmail.com](mailto:nluna190898@gmail.com)] <br>
🌐 [linkedin.com/in/nluna190898](https://linkedin.com/in/nluna190898)

---
