using System.Text.Json.Serialization;

namespace TaskAlbayan.Common.Dtos
{
    public class VisitTaskDto
    {
        public Guid Id { get; set; }

        public Guid VisitId { get; set; }
        
        [JsonIgnore]
        public VisitDto visit { get; set; }

        public Guid TaskId { get; set; }


        public TaskDto task { get; set; }


        public string? Notes { get; set; }

    }
}