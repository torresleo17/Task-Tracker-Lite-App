import { useState, useEffect } from "react";
import * as api from "../api/index";
import Task from "./Task";

function List({ boardId, list, onListUpdated, onListDeleted, onTaskMoved }) {
  const [tasks, setTasks] = useState(list.tasks || []);
  const [newTaskTitle, setNewTaskTitle] = useState("");
  const [isRenaming, setIsRenaming] = useState(false);
  const [newTitle, setNewTitle] = useState(list.title);
  const [isLoadingTask, setIsLoadingTask] = useState(false);
  const [isDragOver, setIsDragOver] = useState(false);

  useEffect(() => {
    setTasks(list.tasks || []);
  }, [list.tasks]);

  const handleAddTask = async (e) => {
    e.preventDefault();
    if (!newTaskTitle.trim()) return;

    setIsLoadingTask(true);
    try {
      const newTask = await api.createTask(boardId, list.id, {
        title: newTaskTitle,
        description: "",
        dueDate: null,
      });
      setTasks([...tasks, newTask]);
      setNewTaskTitle("");
    } finally {
      setIsLoadingTask(false);
    }
  };

  const handleRenameList = async () => {
    if (!newTitle.trim()) return;
    try {
      const updated = await api.updateList(boardId, list.id, {
        title: newTitle,
      });
      onListUpdated(updated);
      setIsRenaming(false);
    } catch (error) {
      alert("Error renombrando la lista");
    }
  };

  const handleDeleteList = async () => {
    if (
      window.confirm("¿Eliminar esta lista? Se eliminarán todas sus tareas.")
    ) {
      try {
        await api.deleteList(boardId, list.id);
        onListDeleted(list.id);
      } catch (error) {
        alert("Error eliminando la lista");
      }
    }
  };

  const handleTaskDeleted = (taskId) => {
    setTasks(tasks.filter((t) => t.id !== taskId));
  };

  const handleTaskUpdated = (updatedTask) => {
    setTasks(tasks.map((t) => (t.id === updatedTask.id ? updatedTask : t)));
  };

  const handleDragOver = (e) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragOver(true);
  };

  const handleDragLeave = (e) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragOver(false);
  };

  const handleDrop = (e) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragOver(false);

    try {
      const dragData = JSON.parse(e.dataTransfer.getData("application/json"));
      const { task, sourceListId } = dragData;

      if (sourceListId === list.id) {
        return;
      }

      onTaskMoved(task, sourceListId, list.id);
    } catch (error) {}
  };

  const handleDragStart = (e, task, sourceListId) => {
    const dragData = JSON.stringify({ task, sourceListId });
    e.dataTransfer.effectAllowed = "move";
    e.dataTransfer.setData("application/json", dragData);
  };

  return (
    <div className="list">
      <div className="list__header">
        {isRenaming ? (
          <div className="list__rename">
            <input
              autoFocus
              value={newTitle}
              onChange={(e) => setNewTitle(e.target.value)}
              onKeyUp={(e) => e.key === "Enter" && handleRenameList()}
              className="list__input"
            />
            <button
              onClick={handleRenameList}
              className="btn btn--primary btn--small">
              ✓
            </button>
            <button
              onClick={() => setIsRenaming(false)}
              className="btn btn--secondary btn--small">
              ✕
            </button>
          </div>
        ) : (
          <>
            <h3 className="list__title">{list.title}</h3>
            <span className="list__count">{tasks.length}</span>
            <div className="list__header-actions">
              <button
                onClick={() => setIsRenaming(true)}
                className="btn btn--icon"
                title="Renombrar">
                ✎
              </button>
              <button
                onClick={handleDeleteList}
                className="btn btn--icon btn--danger"
                title="Eliminar">
                🗑
              </button>
            </div>
          </>
        )}
      </div>

      <div
        className={`list__tasks ${isDragOver ? "list__tasks--drag-over" : ""}`}
        onDragOver={handleDragOver}
        onDragLeave={handleDragLeave}
        onDrop={handleDrop}>
        {tasks.length === 0 ? (
          <p className="list__empty">No hay tareas</p>
        ) : (
          tasks.map((task) => (
            <Task
              key={task.id}
              boardId={boardId}
              listId={list.id}
              task={task}
              onUpdate={handleTaskUpdated}
              onDelete={handleTaskDeleted}
              onDragStart={(e) => handleDragStart(e, task, list.id)}
            />
          ))
        )}
      </div>

      <form onSubmit={handleAddTask} className="list__add-task">
        <input
          type="text"
          id={`new-task-title-${list.id}`}
          name={`newTaskTitle-${list.id}`}
          value={newTaskTitle}
          onChange={(e) => setNewTaskTitle(e.target.value)}
          placeholder="Agregar tarea..."
          className="list__input"
          disabled={isLoadingTask}
        />
        <button
          type="submit"
          className="btn btn--primary"
          disabled={isLoadingTask}>
          {isLoadingTask ? "Añadiendo..." : "+"}
        </button>
      </form>
    </div>
  );
}

export default List;
