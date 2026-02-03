

namespace Task_Tracker_Lite.Domain
{
    public class Board
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public ICollection<ListItem> Lists { get; set; } = new List<ListItem>();
    }


}
