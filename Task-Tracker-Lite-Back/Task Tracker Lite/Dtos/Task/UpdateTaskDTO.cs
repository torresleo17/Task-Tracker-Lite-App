namespace Task_Tracker_Lite.Dtos.List
{
    public class UpdateTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int ListId { get; set; }
    }
}