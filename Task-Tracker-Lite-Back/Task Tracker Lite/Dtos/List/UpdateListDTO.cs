using System.ComponentModel.DataAnnotations;

namespace Task_Tracker_Lite.Dtos.List
{
    public class UpdateListDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int BoardId { get; set; }
    }
}