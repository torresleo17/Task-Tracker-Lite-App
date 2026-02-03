# Task Tracker Lite App

Una aplicación de gestión de tareas moderna construida con **C# .NET** en el backend y **React** en el frontend.

## 🎯 Características

- ✅ Crear, editar y eliminar tableros (Boards)
- ✅ Organizar tareas en listas (Lists)
- ✅ Crear y gestionar tareas (Tasks)
- ✅ Interfaz intuitiva y responsiva
- ✅ API RESTful 

## 📋 Requisitos Previos

### Backend
- **.NET 8.0 o superior** 
- **SQLite** 
- **Visual Studio 2022** y **Visual Studio Code**

### Frontend
- **Node.js 16+**
- **npm** (incluido con Node.js)

## 🚀 Instalación y Ejecución

### 1️⃣ Backend (.NET)

#### Paso 1: Navega a la carpeta del backend
```bash
cd Task-Tracker-Lite-Back
cd "Task Tracker Lite"
```

#### Paso 2: Restaura las dependencias
```bash
dotnet restore
```

#### Paso 3: Configura la base de datos

Abre el archivo `appsettings.json` y verifica/actualiza la cadena de conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=TaskTrackerLiteDb;Trusted_Connection=true;Encrypt=false;"
  }
}
```
#### Paso 4: Aplica las migraciones
```bash
dotnet ef database update
```

#### Paso 5: Ejecuta la aplicación
```bash
dotnet run
```

La API estará disponible en: `https://localhost:5000` o `https://localhost:5001`

Puedes probar los endpoints en: `https://localhost:5000/swagger` (si Swagger está configurado)


### 2️⃣ Frontend (React)

#### Paso 1: Navega a la carpeta del frontend
```bash
cd Task-Tracker-Lite-Front/Task-Tracker-Lite-View-main
```

#### Paso 2: Instala las dependencias
```bash
npm install
```

#### Paso 3: Configura la URL de la API

Abre `src/api/api/board.js`,`src/api/api/list.js`,`src/api/api/task.js` (o donde esté configurada la URL base) y asegúrate de que apunte a tu backend:

```javascript
const API_BASE_URL = 'https://localhost:5000/api';
// o
const API_BASE_URL = 'http://localhost:5000/api';
```

#### Paso 4: Ejecuta el servidor de desarrollo
```bash
npm run dev
```

La aplicación estará disponible en: `http://localhost:5173` (o el puerto que indique Vite)

---

## 📦 Estructura del Proyecto

```
Task-Tracker-Lite-App/
├── Task-Tracker-Lite-Back/           # Backend .NET
│   └── Task Tracker Lite/
│       ├── Controllers/               # Controladores API
│       ├── Services/                  # Lógica de negocio
│       ├── Repository/                # Acceso a datos
│       ├── Domain/                    # Modelos de dominio
│       ├── Dtos/                      # Data Transfer Objects
│       ├── Data/                      # DbContext
│       ├── Migrations/                # Migraciones BD
│       └── Program.cs                 # Configuración principal
│
└── Task-Tracker-Lite-Front/           # Frontend React
    └── Task-Tracker-Lite-View-main/
        ├── src/
        │   ├── api/                   # Llamadas a API
        │   ├── components/            # Componentes React
        │   ├── App.jsx                # Componente principal
        │   └── main.jsx               # Entry point
        ├── package.json
        └── vite.config.js
```

---

## 🔌 Endpoints principales de la API

### Boards
- `GET /api/boards` - Obtener todos los tableros
- `GET /api/boards/{id}` - Obtener un tablero específico
- `POST /api/boards` - Crear un nuevo tablero
- `PUT /api/boards/{id}` - Actualizar un tablero
- `DELETE /api/boards/{id}` - Eliminar un tablero

### Lists
- `GET /api/lists` - Obtener todas las listas
- `POST /api/lists` - Crear una nueva lista
- `PUT /api/lists/{id}` - Actualizar una lista
- `DELETE /api/lists/{id}` - Eliminar una lista

### Tasks
- `GET /api/tasks` - Obtener todas las tareas
- `POST /api/tasks` - Crear una nueva tarea
- `PUT /api/tasks/{id}` - Actualizar una tarea
- `DELETE /api/tasks/{id}` - Eliminar una tarea

---

## 🛠️ Tecnologías Utilizadas

### Backend
- **.NET 8** - Framework
- **Entity Framework Core** - ORM
- **SQL Server** - Base de datos
- **C#** - Lenguaje de programación

### Frontend
- **React** - Framework UI
- **Vite** - Build tool
- **JavaScript** - Lenguaje
- **CSS** - Estilos

---

## 🐛 Solución de Problemas

### El backend no inicia
- Verifica que .NET esté instalado: `dotnet --version`
- Comprueba la cadena de conexión en `appsettings.json`
- Asegúrate de que SQLite esté correcto

### El frontend no carga
- Verifica que Node.js está instalado: `node --version`
- Elimina `node_modules` y reinstala: `npm install`
- Comprueba que la URL de la API es correcta en `src/api/*.js`

### Error de CORS
- Si obtienes errores de CORS, asegúrate de que el backend está configurado para permitir requests desde `http://localhost:5173`
- Verifica que la configuración de CORS en `Program.cs` es correcta

---

## 📝 Notas de Desarrollo

- Asegúrate de que el **backend esté corriendo antes de iniciar el frontend**
- Si cambias la estructura de la BD, ejecuta: `dotnet ef migrations add NombreMigracion` y luego `dotnet ef database update`
- Para producción, construye el frontend con: `npm run build`

---

## 👨‍💻 Autor

**Jonathan Leonel Mendoza** - [GitHub](https://github.com/torresleo17)

## 📄 Licencia

Este proyecto está abierto y disponible bajo la licencia MIT.
