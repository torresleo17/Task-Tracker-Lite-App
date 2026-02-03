using Task_Tracker_Lite.Dtos.Task;

namespace Task_Tracker_Lite.Dtos.List
{
    public class ListDTO
    {

        public int Id { get; set; }
        public required string Title { get; set; }
        public List<TaskDTO> Tasks { get; set; }

        public required int BoardId { get; set; }




    }
}