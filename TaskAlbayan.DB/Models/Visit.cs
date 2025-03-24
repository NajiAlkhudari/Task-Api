using System.ComponentModel.DataAnnotations;
using TaskAlbayan.DB;
using TaskAlbayan.DB.Models;

public class Visit
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    [Required]
    public Guid ClientId { get; set; }
    public Client Client { get; set; }


    [Required]
    public DateTime Date { get; set; }
    public string? Notes { get; set; }

    public ICollection<VisitTask> visitTasks { get; set; }

}
