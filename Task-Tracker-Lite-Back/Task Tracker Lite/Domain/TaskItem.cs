using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Tracker_Lite.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }

        public int ListId { get; set; }

        [ForeignKey("ListId")]
        public ListItem ListItem { get; set; }
    }


}
