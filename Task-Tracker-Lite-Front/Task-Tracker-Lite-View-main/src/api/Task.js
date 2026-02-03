const BASE_URL = "https://localhost:7095/api";

export const createTask = (boardId, listId, taskData) =>
  fetch(`${BASE_URL}/boards/${boardId}/lists/${listId}/tasks`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(taskData),
  }).then((res) => {
    if (!res.ok) throw new Error("Error creating task");
    return res.json();
  });

export const getTasksByList = (boardId, listId) =>
  fetch(`${BASE_URL}/boards/${boardId}/lists/${listId}/tasks`).then((res) => {
    if (!res.ok) throw new Error("Error fetching tasks");
    return res.json();
  });

export const updateTask = (boardId, listId, taskId, taskData) =>
  fetch(`${BASE_URL}/boards/${boardId}/lists/${listId}/tasks/${taskId}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(taskData),
  }).then((res) => {
    if (!res.ok) throw new Error("Error updating task");
    return res.json();
  });

export const deleteTask = (boardId, listId, taskId) =>
  fetch(`${BASE_URL}/boards/${boardId}/lists/${listId}/tasks/${taskId}`, {
    method: "DELETE",
  }).then((res) => {
    if (!res.ok) throw new Error("Error deleting task");
  });
