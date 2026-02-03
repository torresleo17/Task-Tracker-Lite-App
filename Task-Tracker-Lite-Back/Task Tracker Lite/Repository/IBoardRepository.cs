using Task_Tracker_Lite.Domain;

namespace Task_Tracker_Lite.Repository
{
    public interface IBoardRepository
    {
        Task<Board> CreateAsync(Board board);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Board>> GetAllAsync();
        Task<Board?> GetByIdAsync(int id);
        Task<Board?> UpdateAsync(Board board);
    }
}