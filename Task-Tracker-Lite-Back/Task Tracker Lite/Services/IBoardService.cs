using Task_Tracker_Lite.Dtos.Board;

namespace Task_Tracker_Lite.Services
{
    public interface IBoardService
    {
        Task<BoardDTO> AddBoardAsync(CreatedBoardDTO board);
        Task<BoardDTO?> GetByIdAsync(int id);
        Task<IEnumerable<BoardDTO>> GetAllAsync();
        Task<BoardDTO?> UpdateAsync(int id, UpdateBoardDTO board);
        Task<bool> DeleteAsync(int id);
    }
}