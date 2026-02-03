using Microsoft.EntityFrameworkCore;
using Task_Tracker_Lite.Data;
using Task_Tracker_Lite.Domain;

namespace Task_Tracker_Lite.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private readonly AppDbContext _context;

        public BoardRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Board> CreateAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var board = await _context.Boards.FindAsync(id);
            if (board == null)
                return false;

            _context.Boards.Remove(board);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Board>> GetAllAsync()
        {

            return await _context.Boards.ToListAsync();
        }

        public async Task<Board?> GetByIdAsync(int id)
        {

            return await _context.Boards.FindAsync(id);
        }

        public async Task<Board?> UpdateAsync(Board board)
        {

            var existingTask = await _context.Boards.FindAsync(board.Id);
            if (existingTask == null)
                return null;

            existingTask.Title = board.Title;
            await _context.SaveChangesAsync();
            return existingTask;
        }
    }
}
