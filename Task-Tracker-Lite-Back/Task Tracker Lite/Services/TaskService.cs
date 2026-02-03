using Task_Tracker_Lite.Domain;
using Task_Tracker_Lite.Dtos.List;
using Task_Tracker_Lite.Dtos.Task;
using Task_Tracker_Lite.Repository;

namespace Task_Tracker_Lite.Services
{
    public class TaskService : ITaskService
    {
        #region Fields
        private readonly ITaskItemRepository _taskRepository;
        private readonly IListItemRepository _listRepository;
        private readonly IBoardRepository _boardRepository;
        #endregion

        #region Constructor
        public TaskService(
            ITaskItemRepository taskRepository,
            IListItemRepository listRepository,
            IBoardRepository boardRepository)
        {
            _taskRepository = taskRepository;
            _listRepository = listRepository;
            _boardRepository = boardRepository;
        }
        #endregion

        #region Methods

        public async Task<TaskDTO> CreateAsync(int listId, CreateTaskDTO task)
        {

            var list = await _listRepository.GetByIdAsync(listId);
            if (list is null)
                throw new InvalidOperationException($"La lista con id {listId} no existe en el board .");

            var taskEntity = new TaskItem
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                CreatedAt = DateTime.UtcNow,
                ListId = listId
            };

            var createdTask = await _taskRepository.CreateAsync(taskEntity);
            if (createdTask is null)
                throw new InvalidOperationException("No se pudo crear la tarea.");

            return new TaskDTO
            {
                Id = createdTask.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                DueDate = createdTask.DueDate,
                ListId = createdTask.ListId
            };
        }
        public async Task<IEnumerable<TaskDTO>> GetByListIdAsync(int listId)
        {
            var list = await _listRepository.GetByIdAsync(listId);
            if (list is null)
                throw new InvalidOperationException($"La lista con id {listId} no existe.");

            var tasks = await _taskRepository.GetAllTasksByListIdAsync(listId);

            return tasks.Select(t => new TaskDTO
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                ListId = t.ListId,
                DueDate = t.DueDate
            });
        }


        public async Task<TaskDTO?> UpdateAsync(int listId, int taskId, UpdateTaskDTO task)
        {
            var existingTask = await _taskRepository.GetByIdAsync(taskId);

            if (existingTask is null || existingTask.ListId != listId)
                return null;

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;

            if (task.DueDate.HasValue)
                existingTask.DueDate = task.DueDate;

            if (!(task.ListId == 0))
                existingTask.ListId = task.ListId;

            var updatedTask = await _taskRepository.UpdateAsync(existingTask);
            if (updatedTask is null)
                throw new InvalidOperationException("No se pudo actualizar la tarea.");

            return new TaskDTO
            {
                Id = updatedTask.Id,
                Title = updatedTask.Title,
                Description = updatedTask.Description,
                DueDate = updatedTask.DueDate,
                ListId = updatedTask.ListId
            };
        }


        public async Task<bool> DeleteAsync(int listId, int taskId)
        {

            var existingTask = await _taskRepository.GetByIdAsync(taskId);


            if (existingTask is null || existingTask.ListId != listId)
                return false;

            return await _taskRepository.DeleteAsync(taskId);
        }

        #endregion
    }
}