using Task_Tracker_Lite.Domain;
using Task_Tracker_Lite.Dtos.Board;
using Task_Tracker_Lite.Dtos.List;
using Task_Tracker_Lite.Dtos.Task;
using Task_Tracker_Lite.Repository;

namespace Task_Tracker_Lite.Services
{
    public class BoardService : IBoardService
    {
        #region Fields
        private readonly IBoardRepository _boardRepository;
        private readonly IListItemRepository _listRepository;
        #endregion

        #region Constructor
        public BoardService(IBoardRepository boardRepository, IListItemRepository listRepository)
        {
            _boardRepository = boardRepository;
            _listRepository = listRepository;
        }
        #endregion

        #region Methods

        public async Task<BoardDTO> AddBoardAsync(CreatedBoardDTO board)
        {
            var newBoard = new Board
            {
                Title = board.Title
            };

            var createdBoard = await _boardRepository.CreateAsync(newBoard);
            if (createdBoard is null)
                throw new InvalidOperationException("No se pudo crear el board.");

            // Crear las listas por defecto
            var defaultTitles = new[] { "ToDo", "Doing", "Done" };
            var createdLists = new List<ListItem>();

            foreach (var title in defaultTitles)
            {
                var list = await _listRepository.CreateAsync(new ListItem
                {
                    Title = title,
                    BoardId = createdBoard.Id
                });

                createdLists.Add(list);
            }

            // Mapear a DTOs
            var listDtos = createdLists.Select(l => new ListDTO
            {
                Id = l.Id,
                Title = l.Title,
                BoardId = l.BoardId,
                Tasks = new List<TaskDTO>()
            }).ToList();

            var boardDto = new BoardDTO
            {
                Id = createdBoard.Id,
                Title = createdBoard.Title,
                Lists = listDtos
            };

            return boardDto;
        }

        public async Task<BoardDTO?> GetByIdAsync(int id)
        {
            var board = await _boardRepository.GetByIdAsync(id);
            if (board is null)
                return null;

            var lists = await _listRepository.GetAllListByBoardIdAsync(id);

            var listDtos = lists.Select(l => new ListDTO
            {
                Id = l.Id,
                Title = l.Title,
                BoardId = l.BoardId,
                Tasks = new List<TaskDTO>()
            }).ToList();

            return new BoardDTO
            {
                Id = board.Id,
                Title = board.Title,
                Lists = listDtos
            };
        }

        public async Task<IEnumerable<BoardDTO>> GetAllAsync()
        {
            var boards = await _boardRepository.GetAllAsync();
            var boardDtos = boards.Select(b => new BoardDTO
            {
                Id = b.Id,
                Title = b.Title,
                Lists = new List<ListDTO>()
            }).ToList();

            return boardDtos;
        }

        public async Task<BoardDTO?> UpdateAsync(int id, UpdateBoardDTO board)
        {
            var existingBoard = await _boardRepository.GetByIdAsync(id);
            if (existingBoard is null)
                return null;

            existingBoard.Title = board.Title;
            var updatedBoard = await _boardRepository.UpdateAsync(existingBoard);

            if (updatedBoard is null)
                throw new InvalidOperationException("No se pudo actualizar el board.");

            return new BoardDTO
            {
                Id = updatedBoard.Id,
                Title = updatedBoard.Title,
                Lists = new List<ListDTO>()
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingBoard = await _boardRepository.GetByIdAsync(id);
            if (existingBoard is null)
                return false;

            return await _boardRepository.DeleteAsync(id);
        }

        #endregion
    }
}