# Neosoft Admin - Prueba Técnica Full Stack

## Descripción

Aplicación web Full Stack desarrollada como solución a prueba técnica, orientada a la gestión administrativa de:

- Usuarios
- Roles
- Variables del sistema

El sistema permite realizar operaciones CRUD completas (Crear, Leer, Editar, Eliminar), integrando frontend y backend mediante una API REST.

---

## Tecnologías Utilizadas

### Backend
- .NET 9 Web API
- Entity Framework Core
- Pomelo.EntityFrameworkCore.MySql
- MariaDB

### Frontend
- Angular 18
- TypeScript
- RxJS
- HttpClient

---

## Estructura del Proyecto
```
Neosoft.Prueba_Kevin/
├── NeosoftApi/
│   └── NeosoftApi/        ← proyecto backend (entrar a esta carpeta para ejecutar)
├── neosoft-frontend/      ← proyecto frontend
└── README.md
```

> **Nota:** La carpeta del backend tiene dos niveles con el mismo nombre (`NeosoftApi/NeosoftApi`). Esto es normal, hay que entrar a la carpeta interior para ejecutar el proyecto.

---

## Requisitos Previos

- .NET 9 SDK
- Node.js y npm
- Angular CLI 18
- MariaDB instalado y corriendo en puerto 3306

---

## Paso 1 - Configurar la Base de Datos

Conectarse a MariaDB y ejecutar el siguiente script completo:
```sql
CREATE DATABASE neosoft_db CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;

USE neosoft_db;

CREATE TABLE Roles (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL,
    Email VARCHAR(200) NOT NULL UNIQUE,
    RolId INT NOT NULL,
    FOREIGN KEY (RolId) REFERENCES Roles(Id)
);

CREATE TABLE Variables (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL,
    Valor VARCHAR(500) NOT NULL,
    Tipo ENUM('texto', 'numérico', 'booleano') NOT NULL
);

INSERT INTO Roles (Nombre) VALUES ('Administrador'), ('Usuario'), ('Supervisor');
```

---

## Paso 2 - Configurar la Conexión

Abrir el archivo `NeosoftApi/NeosoftApi/appsettings.json` y ajustar la contraseña si es necesario:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=neosoft_db;User=root;Password=Admin1234;"
  }
}
```

---

## Paso 3 - Ejecutar el Backend

Abrir una terminal y ejecutar:
```bash
cd NeosoftApi/NeosoftApi
dotnet run
```

La API quedará disponible en: `http://localhost:5263`

### Endpoints disponibles

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | /api/roles | Listar roles |
| POST | /api/roles | Crear rol |
| PUT | /api/roles/{id} | Editar rol |
| DELETE | /api/roles/{id} | Eliminar rol |
| GET | /api/usuarios | Listar usuarios |
| POST | /api/usuarios | Crear usuario |
| PUT | /api/usuarios/{id} | Editar usuario |
| DELETE | /api/usuarios/{id} | Eliminar usuario |
| GET | /api/variables | Listar variables |
| POST | /api/variables | Crear variable |
| PUT | /api/variables/{id} | Editar variable |
| DELETE | /api/variables/{id} | Eliminar variable |

---

## Paso 4 - Ejecutar el Frontend

Abrir otra terminal y ejecutar:
```bash
cd neosoft-frontend
npm install
npx ng serve
```

La aplicación quedará disponible en: `http://localhost:4200`

---

## Funcionalidades

### Usuarios
- Listar, crear, editar y eliminar usuarios
- El campo Rol se carga dinámicamente desde la API
- Validación de email y campos obligatorios
- Confirmación antes de eliminar

### Roles
- Listar, crear, editar y eliminar roles
- Validación de campos obligatorios
- No permite eliminar un rol que tenga usuarios asignados
- Confirmación antes de eliminar

### Variables
- Listar, crear, editar y eliminar variables
- El tipo se selecciona desde un dropdown (texto, numérico, booleano)
- Validación de campos obligatorios
- Confirmación antes de eliminar

---

## Autor

Kevin Ortega  
Ingeniero en Informática