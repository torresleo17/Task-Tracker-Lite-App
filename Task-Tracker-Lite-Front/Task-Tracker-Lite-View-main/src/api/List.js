const BASE_URL = "https://localhost:7095/api";

export const createList = (boardId, listData) =>
  fetch(`${BASE_URL}/boards/${boardId}/lists`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(listData),
  }).then((res) => {
    if (!res.ok) throw new Error("Error creating list");
    return res.json();
  });

export const getListsByBoard = (boardId) =>
  fetch(`${BASE_URL}/boards/${boardId}/lists`).then((res) => {
    if (!res.ok) throw new Error("Error fetching lists");
    return res.json();
  });

export const updateList = (boardId, listId, listData) =>
  fetch(`${BASE_URL}/boards/${boardId}/lists/${listId}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(listData),
  }).then((res) => {
    if (!res.ok) throw new Error("Error updating list");
    return res.json();
  });

export const deleteList = (boardId, listId) =>
  fetch(`${BASE_URL}/boards/${boardId}/lists/${listId}`, {
    method: "DELETE",
  }).then((res) => {
    if (!res.ok) throw new Error("Error deleting list");
  });
