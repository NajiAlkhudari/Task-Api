
using System.Text.Json.Serialization;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.DB.Models;

namespace TaskAlbayan.Common
{
    public class CreateUpdateVisitDto
    {
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime CompletionDate { get; set; } = DateTime.Now;
        public List<CreateUpdateVisitTaskDto> TaskDto { get; set; }

    }
}