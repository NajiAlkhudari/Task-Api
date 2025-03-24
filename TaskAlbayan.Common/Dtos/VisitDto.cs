using System.Text.Json.Serialization;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.DB.Models;

namespace TaskAlbayan.Common
{
    public class VisitDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public Guid ClientId { get; set; }

        [JsonIgnore]
        public ClientDto Client { get; set; }

        public DateTime CompletionDate { get; set; }
        public List<VisitTaskDto> visitTasks { get; set; }

    }
}