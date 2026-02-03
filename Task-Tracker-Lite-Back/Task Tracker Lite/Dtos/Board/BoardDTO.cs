using Task_Tracker_Lite.Dtos.List;

namespace Task_Tracker_Lite.Dtos.Board
{
    public class BoardDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public List<ListDTO> Lists { get; set; } = new();
    }
}
