using System.ComponentModel.DataAnnotations;

namespace Task_Tracker_Lite.Dtos.Board
{
    public class UpdateBoardDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
    }
}