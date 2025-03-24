using System.ComponentModel.DataAnnotations;

public class Client
{

    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; }

    [Required]
    [MaxLength(500)]
    public string Address { get; set; }


    [Phone]
    [Required]
    [MaxLength(15)]
    public string Phone { get; set; }

    public string? Notes { get; set; }
    public ICollection<Visit> visits { get; set; }
}
