import { useState, useEffect } from "react";
import * as Api from "./api/index";
import Board from "./components/Board";

import "./App.css";

const toArray = (data) =>
  Array.isArray(data) ? data : Array.isArray(data?.items) ? data.items : [];

function App() {
  const [boards, setBoards] = useState([]);
  const [selectedBoardId, setSelectedBoardId] = useState(null);
  const [newBoardTitle, setNewBoardTitle] = useState("");
  const [loading, setLoading] = useState(true);
  const [isLoadingBoard, setIsLoadingBoard] = useState(false);

  useEffect(() => {
    loadBoards();
  }, []);

  const loadBoards = async () => {
    try {
      const data = await Api.getAllBoards();
      setBoards(toArray(data));
    } catch (error) {
      alert("Error cargando los tableros");
    } finally {
      setLoading(false);
    }
  };

  const handleCreateBoard = async (e) => {
    e.preventDefault();
    if (!newBoardTitle.trim()) return;

    setIsLoadingBoard(true);
    try {
      const newBoard = await Api.createBoard({ title: newBoardTitle });
      setBoards([...boards, newBoard]);
      setSelectedBoardId(newBoard.id);
      setNewBoardTitle("");
    } finally {
      setIsLoadingBoard(false);
    }
  };

  const handleDeleteBoard = async (boardId) => {
    if (
      window.confirm(
        "¿Eliminar este tablero? Se eliminarán todas sus listas y tareas.",
      )
    ) {
      try {
        await Api.deleteBoard(boardId);
        const newBoards = boards.filter((b) => b.id !== boardId);
        setBoards(newBoards);
        if (selectedBoardId === boardId) {
          setSelectedBoardId(newBoards.length > 0 ? newBoards[0].id : null);
        }
      } catch (error) {
        alert("Error eliminando el tablero");
      }
    }
  };

  if (loading) return <div className="loading">Cargando...</div>;

  if (selectedBoardId) {
    return (
      <Board
        boardId={selectedBoardId}
        onBack={() => setSelectedBoardId(null)}
      />
    );
  }

  return (
    <div className="app">
      <aside className="sidebar">
        <div className="sidebar__header">
          <h2 className="sidebar__title">📋 Tableros</h2>
        </div>

        <form onSubmit={handleCreateBoard} className="sidebar__create-form">
          <input
            type="text"
            id="new-board-title"
            name="newBoardTitle"
            value={newBoardTitle}
            onChange={(e) => setNewBoardTitle(e.target.value)}
            placeholder="Nuevo tablero..."
            className="sidebar__input"
            disabled={isLoadingBoard}
          />
          <button
            type="submit"
            className="btn btn--primary"
            disabled={isLoadingBoard}>
            {isLoadingBoard ? "..." : "Crear"}
          </button>
        </form>

        <div className="sidebar__boards">
          {boards.length === 0 ? (
            <p className="sidebar__empty">No hay tableros. ¡Crea uno!</p>
          ) : (
            boards.map((board) => (
              <div key={board.id} className="sidebar__board-item">
                <button
                  onClick={() => setSelectedBoardId(board.id)}
                  className="sidebar__board-button">
                  {board.title}
                </button>
                <button
                  onClick={() => handleDeleteBoard(board.id)}
                  className="btn btn--icon btn--danger btn--small"
                  title="Eliminar">
                  🗑
                </button>
              </div>
            ))
          )}
        </div>
      </aside>

      <main className="main-content">
        <div className="empty-state">
          <h1>👋 Bienvenido a Task Tracker</h1>
          <p>Selecciona un tablero o crea uno nuevo para empezar</p>
        </div>
      </main>
    </div>
  );
}

export default App;
