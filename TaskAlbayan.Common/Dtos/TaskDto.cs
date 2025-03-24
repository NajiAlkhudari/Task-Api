using System.ComponentModel.DataAnnotations;
using TaskAlbayan.Common;

public class TaskDto
{
  public Guid Id { get; set; }

 
    public string TaskName { get; set; }
 
    public string Description { get; set; }

        public string? Notes {get;set;}


}
