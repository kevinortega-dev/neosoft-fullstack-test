# 🧩 Neosoft Admin - Prueba Técnica Full Stack

## 📌 Descripción General

Aplicación web Full Stack desarrollada como solución a prueba técnica, orientada a la gestión administrativa de entidades clave:

- 👤 Usuarios
- 🛡️ Roles
- ⚙️ Variables del sistema

El sistema permite realizar operaciones CRUD completas, integrando frontend y backend mediante API REST.

---

## 🏗️ Arquitectura

El proyecto está dividido en dos aplicaciones independientes:

- **Backend:** API REST en .NET Core
- **Frontend:** SPA en Angular

---

## 🛠️ Tecnologías Utilizadas

### 🔙 Backend
- .NET Core Web API
- Entity Framework Core
- MariaDB
- LINQ

### 🔜 Frontend
- Angular
- TypeScript
- HTML5
- CSS3
- RxJS

---

## ⚙️ Funcionalidades Implementadas

### 🔹 Usuarios
- Crear usuario
- Listar usuarios
- Editar usuario
- Eliminar usuario
- Asociación con roles

### 🔹 Roles
- Crear rol
- Listar roles
- Editar rol
- Eliminar rol

### 🔹 Variables
- Crear variable
- Listar variables
- Editar variable
- Eliminar variable

---

## 🔐 Validaciones

- Campos obligatorios en formularios
- Manejo de errores en frontend y backend
- Confirmación antes de eliminación de registros

---

## 🔄 Comunicación

El frontend consume la API mediante servicios HTTP utilizando Angular (`HttpClient`), manejando estados de carga, éxito y error.

---

## 🗄️ Base de Datos

Se utilizó MariaDB.

### Crear base de datos:
```sql
CREATE DATABASE neosoft;
Configurar conexión:

Editar el archivo appsettings.json del backend:

"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=neosoft;user=root;password=tu_password;"
}

Las tablas se crean automáticamente al ejecutar la API.

🚀 Ejecución del Proyecto
📦 Backend
cd NeosoftApi/NeosoftApi
dotnet run

🔗 API disponible en:
http://localhost:5263

🌐 Frontend
cd neosoft-frontend
npm install
npm start

🔗 Aplicación disponible en:
http://localhost:4200

📁 Estructura del Proyecto
Neosoft/
├── NeosoftApi/        # Backend (.NET Core)
├── neosoft-frontend/  # Frontend (Angular)
└── README.md
💡 Decisiones Técnicas
Separación de capas (frontend/backend)
Uso de Entity Framework para acceso a datos
Arquitectura basada en servicios en Angular
Componentización por entidad (Usuarios, Roles, Variables)
📈 Posibles Mejoras
Implementar autenticación (JWT)
Paginación en tablas
Validaciones más robustas (Forms reactivos)
UI con framework (Angular Material / Bootstrap)
👨‍💻 Autor

Kevin Ortega
Ingeniero en Informática