namespace Task_Tracker_Lite.Dtos.Task
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ListId { get; set; }
    }

}