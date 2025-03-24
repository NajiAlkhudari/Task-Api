using System.ComponentModel.DataAnnotations;

namespace TaskAlbayan.DB
{
    public class VisitTask
    {
        public Guid Id { get; set; }

        [Required]
        public Guid VisitId { get; set; }
        public Visit visit { get; set; }

        [Required]
        public Guid TaskId { get; set; }
        public Task task { get; set; }
        public string? Details {get;set;}

        public string? Notes { get; set; }


    }
}