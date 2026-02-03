using Task_Tracker_Lite.Domain;

namespace Task_Tracker_Lite.Repository
{
    public interface IListItemRepository
    {
        Task<ListItem> CreateAsync(ListItem listItem);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ListItem>> GetAllAsync();
        Task<IEnumerable<ListItem>> GetAllListByBoardIdAsync(int boardId);
        Task<ListItem?> GetByIdAndBoardIdAsync(int id, int boardId);
        Task<ListItem?> GetByIdAsync(int id);
        Task<ListItem?> UpdateAsync(ListItem listItem);
    }
}