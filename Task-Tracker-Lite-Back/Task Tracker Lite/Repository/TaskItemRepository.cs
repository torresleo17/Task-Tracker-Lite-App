using Microsoft.EntityFrameworkCore;
using Task_Tracker_Lite.Data;
using Task_Tracker_Lite.Domain;

namespace Task_Tracker_Lite.Repository
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext _context;

        public TaskItemRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<TaskItem> CreateAsync(TaskItem taskItem)
        {
            _context.Tasks.Add(taskItem);
            await _context.SaveChangesAsync();
            return taskItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem == null)
                return false;

            _context.Tasks.Remove(taskItem);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {

            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {

            return await _context.Tasks.FindAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksByListIdAsync(int listId)
        {
            return await _context.Tasks
                .Where(t => t.ListItem.Id == listId)
                .ToListAsync();
        }

        public async Task<TaskItem?> UpdateAsync(TaskItem taskItem)
        {
            var existingTask = await _context.Tasks.FindAsync(taskItem.Id);
            if (existingTask == null)
                return null;

            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.DueDate = taskItem.DueDate;   
            existingTask.ListId = taskItem.ListId;

            _context.Tasks.Update(existingTask);
            await _context.SaveChangesAsync();
            return existingTask;
        }

        public async Task<bool> DeleteAllByListIdAsync(int listId)
        {
            await _context.Tasks.Where(t => t.ListItem.Id == listId).ExecuteDeleteAsync();
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
