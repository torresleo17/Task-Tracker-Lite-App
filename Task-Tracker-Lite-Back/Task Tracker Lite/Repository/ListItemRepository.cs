using Microsoft.EntityFrameworkCore;
using Task_Tracker_Lite.Data;
using Task_Tracker_Lite.Domain;

namespace Task_Tracker_Lite.Repository
{
    public class ListItemRepository : IListItemRepository
    {
        private readonly AppDbContext _context;

        public ListItemRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ListItem> CreateAsync(ListItem listItem)
        {
            _context.Lists.Add(listItem);
            await _context.SaveChangesAsync();
            return listItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var listItem = await _context.Lists.FindAsync(id);
            if (listItem == null)
                return false;

            _context.Lists.Remove(listItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ListItem>> GetAllAsync()
        {

            return await _context.Lists.ToListAsync();
        }

        public async Task<ListItem?> GetByIdAsync(int id)
        {

            return await _context.Lists.FindAsync(id);
        }

        public async Task<ListItem?> GetByIdAndBoardIdAsync(int id, int boardId)
        {
            return await _context.Lists.FirstOrDefaultAsync(l => l.Id == id && l.BoardId == boardId);
        }

        public async Task<IEnumerable<ListItem>> GetAllListByBoardIdAsync(int boardId)
        {
            return await _context.Lists.Where(l => l.Board.Id == boardId).ToListAsync();
        }

        public async Task<ListItem?> UpdateAsync(ListItem listItem)
        {
            var existingListItem = await _context.Lists.FindAsync(listItem.Id);
            if (existingListItem == null)
                return null;

            existingListItem.Title = listItem.Title;
            await _context.SaveChangesAsync();
            return existingListItem;
        }
    }
}
