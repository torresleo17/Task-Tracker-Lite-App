import { useState, useEffect } from "react";
import * as api from "../api/index";
import List from "./List";

const toArray = (data) =>
  Array.isArray(data) ? data : Array.isArray(data?.items) ? data.items : [];

function Board({ boardId, onBack }) {
  const [board, setBoard] = useState(null);
  const [lists, setLists] = useState([]);
  const [newListTitle, setNewListTitle] = useState("");
  const [loading, setLoading] = useState(true);
  const [isLoadingList, setIsLoadingList] = useState(false);

  useEffect(() => {
    loadBoard();
  }, [boardId]);

  const loadBoard = async () => {
    setLoading(true);
    try {
      const boardData = await api.getBoardById(boardId);
      setBoard(boardData);

      const listsSource = toArray(boardData?.lists);

      const listsWithTasks = await Promise.all(
        listsSource.map(async (list) => {
          let tasksList = toArray(list?.tasks);

          if (tasksList.length === 0) {
            const tasksResult = await api.getTasksByList(boardId, list.id);
            tasksList = toArray(tasksResult);
          }

          return { ...list, tasks: tasksList };
        }),
      );

      setLists(listsWithTasks);
    } finally {
      setLoading(false);
    }
  };

  const handleAddList = async (e) => {
    e.preventDefault();
    if (!newListTitle.trim()) return;

    setIsLoadingList(true);
    try {
      const newList = await api.createList(boardId, { title: newListTitle });
      setLists((prev) => [...prev, { ...newList, tasks: [] }]);
      setNewListTitle("");
    } finally {
      setIsLoadingList(false);
    }
  };

  const handleListDeleted = (listId) => {
    setLists((prev) => prev.filter((l) => l.id !== listId));
  };

  const handleListUpdated = (updatedList) => {
    setLists((prev) =>
      prev.map((l) => (l.id === updatedList.id ? updatedList : l)),
    );
  };

  const handleTaskMoved = async (task, sourceListId, targetListId) => {
    try {
      const updatedTask = await api.updateTask(boardId, sourceListId, task.id, {
        title: task.title,
        description: task.description ?? "",
        dueDate: task.dueDate,
        listId: targetListId,
      });

      setLists((prev) =>
        prev.map((list) => {
          if (list.id === sourceListId) {
            return {
              ...list,
              tasks: list.tasks.filter((t) => t.id !== task.id),
            };
          } else if (list.id === targetListId) {
            return {
              ...list,
              tasks: [...list.tasks, updatedTask],
            };
          }
          return list;
        }),
      );
    } catch (error) {}
  };

  if (loading) return <div className="loading">Cargando tablero...</div>;
  if (!board) return <div className="error">Tablero no encontrado</div>;

  return (
    <div className="board-view">
      <div className="board-view__header">
        <button onClick={onBack} className="btn btn--back">
          ← Volver
        </button>
        <h1 className="board-view__title">{board.title}</h1>
      </div>

      <div className="board-view__lists">
        {lists.map((list) => (
          <List
            key={list.id}
            boardId={boardId}
            list={list}
            onListDeleted={handleListDeleted}
            onListUpdated={handleListUpdated}
            onTaskMoved={handleTaskMoved}
          />
        ))}
      </div>

      <form onSubmit={handleAddList} className="add-list-form">
        <input
          type="text"
          id="new-list-title"
          name="newListTitle"
          value={newListTitle}
          onChange={(e) => setNewListTitle(e.target.value)}
          placeholder="Nueva lista..."
          className="add-list-form__input"
          disabled={isLoadingList}
        />
        <button
          type="submit"
          className="btn btn--primary"
          disabled={isLoadingList}>
          {isLoadingList ? "Creando..." : "Crear Lista"}
        </button>
      </form>
    </div>
  );
}

export default Board;
