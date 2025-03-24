namespace TaskAlbayan.Common.Dtos
{
    public class CreateUpdateVisitTaskDto

    {


        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public string? Notes { get; set; }

    }
}