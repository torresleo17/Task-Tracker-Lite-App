namespace Task_Tracker_Lite.Dtos.List
{
    public class CreateTaskDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
