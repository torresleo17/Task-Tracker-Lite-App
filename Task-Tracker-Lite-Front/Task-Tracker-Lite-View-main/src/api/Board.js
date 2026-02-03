const BASE_URL = "https://localhost:7095/api";

export const getAllBoards = () =>
  fetch(`${BASE_URL}/boards`).then((res) => {
    if (!res.ok) throw new Error("Error fetching boards");
    return res.json();
  });

export const getBoardById = (id) =>
  fetch(`${BASE_URL}/boards/${id}`).then((res) => {
    if (!res.ok) throw new Error("Board not found");
    return res.json();
  });

export const createBoard = (boardData) =>
  fetch(`${BASE_URL}/boards`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(boardData),
  }).then((res) => {
    if (!res.ok) throw new Error("Error creating board");
    return res.json();
  });

export const updateBoard = (id, boardData) =>
  fetch(`${BASE_URL}/boards/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(boardData),
  }).then((res) => {
    if (!res.ok) throw new Error("Error updating board");
    return res.json();
  });

export const deleteBoard = (id) =>
  fetch(`${BASE_URL}/boards/${id}`, {
    method: "DELETE",
  }).then((res) => {
    if (!res.ok) throw new Error("Error deleting board");
  });
