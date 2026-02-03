using Task_Tracker_Lite.Dtos.List;

namespace Task_Tracker_Lite.Services
{
    public interface IListService
    {
        Task<ListDTO> CreateAsync(int boardId, CreateListDTO dto);
        Task<bool> DeleteAsync(int boardId, int listId);
        Task<IEnumerable<ListDTO>> GetByBoardIdAsync(int boardId);
        Task<ListDTO?> UpdateAsync(int boardId, int listId, UpdateListDTO list);
    }
}