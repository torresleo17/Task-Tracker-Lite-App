# Task Tracker Lite (Frontend)

Aplicación web para gestionar tableros, listas y tareas. Construida con React y Vite.

## Requisitos

- Node.js 18+
- Backend corriendo (por defecto en https://localhost:7095)

## Instalación

```bash
npm install
```

## Ejecutar en desarrollo

```bash
npm run dev
```

## Estructura principal

- [src/App.jsx](src/App.jsx): pantalla principal y listado de tableros.
- [src/components/Board.jsx](src/components/Board.jsx): vista del tablero y listas.
- [src/components/List.jsx](src/components/List.jsx): gestión de tareas por lista.
- [src/components/Task.jsx](src/components/Task.jsx): edición y eliminación de tareas.
- [src/api/Board.js](src/api/Board.js): llamadas a API de tableros.
- [src/api/List.js](src/api/List.js): llamadas a API de listas.
- [src/api/Task.js](src/api/Task.js): llamadas a API de tareas.
- [src/App.css](src/App.css): estilos globales.

## Endpoints esperados (backend)

Boards

- GET /api/boards
- GET /api/boards/{id}
- POST /api/boards
- PUT /api/boards/{id}
- DELETE /api/boards/{id}

Lists

- GET /api/boards/{boardId}/lists
- POST /api/boards/{boardId}/lists
- PUT /api/boards/{boardId}/lists/{listId}
- DELETE /api/boards/{boardId}/lists/{listId}

Tasks

- GET /api/boards/{boardId}/lists/{listId}/tasks
- POST /api/boards/{boardId}/lists/{listId}/tasks
- PUT /api/boards/{boardId}/lists/{listId}/tasks/{taskId}
- DELETE /api/boards/{boardId}/lists/{listId}/tasks/{taskId}

## Notas

- Si el frontend no encuentra el backend, revisa la URL base en los archivos de [src/api](src/api).
- Para mover una tarea, el backend debe aceptar `listId` en el body y persistir el cambio.
