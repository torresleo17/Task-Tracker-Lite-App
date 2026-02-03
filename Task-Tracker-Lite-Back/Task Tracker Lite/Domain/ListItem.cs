

using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Tracker_Lite.Domain
{
    public class ListItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int BoardId { get; set; }

        [ForeignKey("BoardId")]
        public Board Board { get; set; }

        public ICollection<TaskItem> TaskItem { get; set; }
    }


}
