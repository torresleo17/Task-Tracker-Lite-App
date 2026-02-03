import { useState } from "react";
import * as api from "../api/index";

function Task({ boardId, listId, task, onUpdate, onDelete, onDragStart }) {
  const [isEditing, setIsEditing] = useState(false);
  const [editData, setEditData] = useState({
    title: task.title,
    description: task.description,
    dueDate: task.dueDate ? task.dueDate.split("T")[0] : "",
  });

  const handleSave = async () => {
    try {
      const updated = await api.updateTask(boardId, listId, task.id, {
        title: editData.title,
        description: editData.description || "",
        dueDate: editData.dueDate
          ? new Date(editData.dueDate).toISOString()
          : null,
      });
      onUpdate(updated);
      setIsEditing(false);
    } catch (error) {
      alert("Error actualizando la tarea");
    }
  };

  const handleDelete = async () => {
    if (window.confirm("¿Eliminar esta tarea?")) {
      try {
        await api.deleteTask(boardId, listId, task.id);
        onDelete(task.id);
      } catch (error) {
        alert("Error eliminando la tarea");
      }
    }
  };

  if (isEditing) {
    return (
      <div className="task task--editing">
        <input
          type="text"
          id={`task-title-${task.id}`}
          name={`taskTitle-${task.id}`}
          value={editData.title}
          onChange={(e) => setEditData({ ...editData, title: e.target.value })}
          placeholder="Título"
          className="task__input"
        />
        <textarea
          id={`task-description-${task.id}`}
          name={`taskDescription-${task.id}`}
          value={editData.description}
          onChange={(e) =>
            setEditData({ ...editData, description: e.target.value })
          }
          placeholder="Descripción (opcional)"
          className="task__textarea"
        />
        <input
          type="date"
          id={`task-dueDate-${task.id}`}
          name={`taskDueDate-${task.id}`}
          value={editData.dueDate}
          onChange={(e) =>
            setEditData({ ...editData, dueDate: e.target.value })
          }
          className="task__date"
        />
        <div className="task__actions">
          <button onClick={handleSave} className="btn btn--primary btn--small">
            Guardar
          </button>
          <button
            onClick={() => setIsEditing(false)}
            className="btn btn--secondary btn--small">
            Cancelar
          </button>
        </div>
      </div>
    );
  }

  const dueDate = task.dueDate ? new Date(task.dueDate) : null;
  const isOverdue = dueDate && dueDate < new Date() && !task.isCompleted;

  return (
    <div
      draggable
      onDragStart={onDragStart}
      className={`task ${isOverdue ? "task--overdue" : ""}`}>
      <div className="task__drag-handle">☰</div>
      <div className="task__content">
        <h4 className="task__title">{task.title}</h4>
        {task.description && (
          <p className="task__description">{task.description}</p>
        )}
        {dueDate && (
          <small
            className={`task__date ${isOverdue ? "task__date--overdue" : ""}`}>
            {isOverdue ? "Tarea vencida" : "Vence"}:{" "}
            {dueDate.toLocaleDateString("es-ES")}
          </small>
        )}
      </div>
      <div className="task__actions">
        <button
          onClick={() => setIsEditing(true)}
          className="btn btn--edit"
          title="Editar">
          ✎
        </button>
        <button
          onClick={handleDelete}
          className="btn btn--delete"
          title="Eliminar">
          ✕
        </button>
      </div>
    </div>
  );
}

export default Task;
