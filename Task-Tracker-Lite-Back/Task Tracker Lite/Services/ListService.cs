using Task_Tracker_Lite.Domain;
using Task_Tracker_Lite.Dtos.List;
using Task_Tracker_Lite.Dtos.Task;
using Task_Tracker_Lite.Repository;

namespace Task_Tracker_Lite.Services
{
    public class ListService : IListService
    {
        #region Fields
        private readonly IListItemRepository _listRepository;
        private readonly IBoardRepository _boardRepository;
        private readonly ITaskItemRepository _taskRepository;
        #endregion

        #region Constructor
        public ListService(
            IListItemRepository listRepository,
            IBoardRepository boardRepository,
            ITaskItemRepository taskRepository)
        {
            _listRepository = listRepository;
            _boardRepository = boardRepository;
            _taskRepository = taskRepository;
        }
        #endregion

        #region Methods

        public async Task<ListDTO> CreateAsync(int boardId, CreateListDTO dto)
        {

            var listEntity = new ListItem
            {
                Title = dto.Title,
                BoardId = boardId
            };

            var createdList = await _listRepository.CreateAsync(listEntity);
            if (createdList is null)
                throw new InvalidOperationException("No se pudo crear la lista.");

            return new ListDTO
            {
                Id = createdList.Id,
                Title = createdList.Title,
                BoardId = createdList.BoardId
            };
        }

        public async Task<ListDTO?> GetByIdAsync(int listId)
        {
            var list = await _listRepository.GetByIdAsync(listId);
            if (list is null)
                return null;

            var tasks = await _taskRepository.GetAllTasksByListIdAsync(listId);

            return new ListDTO
            {
                Id = list.Id,
                Title = list.Title,
                BoardId = list.BoardId,
                Tasks = tasks.Select(t => new TaskDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    ListId = t.ListId
                }).ToList()
            };
        }
        public async Task<IEnumerable<ListDTO>> GetByBoardIdAsync(int boardId)
        {
            var lists = await _listRepository.GetAllListByBoardIdAsync(boardId);

            return lists.Select(l => new ListDTO
            {
                Id = l.Id,
                Title = l.Title,
                BoardId = l.BoardId
            });
        }

        public async Task<ListDTO?> UpdateAsync(int boardId, int listId, UpdateListDTO list)
        {
            var board = await _boardRepository.GetByIdAsync(boardId);
            if (board is null)
                return null;

            var existingList = await _listRepository.GetByIdAndBoardIdAsync(listId, boardId);
            if (existingList is null)
                return null;

            existingList.Title = list.Title;

            var updatedList = await _listRepository.UpdateAsync(existingList);
            if (updatedList is null)
                throw new InvalidOperationException("No se pudo actualizar la lista.");

            return new ListDTO
            {
                Id = updatedList.Id,
                Title = updatedList.Title,
                BoardId = updatedList.BoardId,
                Tasks = new List<TaskDTO>()
            };
        }

        public async Task<bool> DeleteAsync(int boardId, int listId)
        {
            // Validar que el board existe
            var board = await _boardRepository.GetByIdAsync(boardId);
            if (board is null)
                return false;

            var existingList = await _listRepository.GetByIdAndBoardIdAsync(listId, boardId);
            if (existingList is null)
                return false;

            return await _listRepository.DeleteAsync(listId);
        }

        #endregion
    }
}