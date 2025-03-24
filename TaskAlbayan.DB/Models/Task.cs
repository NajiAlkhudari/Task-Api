using System.ComponentModel.DataAnnotations;
using TaskAlbayan.DB;

public class Task
{
  public Guid Id { get; set; }

    [Required]
    [MaxLength(200)] 
    public string TaskName { get; set; }

    [MaxLength(1000)] 
    public string Description { get; set; }

        public string? Notes {get;set;}

    public ICollection<VisitTask> visitTasks { get; set; }

}
