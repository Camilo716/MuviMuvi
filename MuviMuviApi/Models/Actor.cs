using System.ComponentModel.DataAnnotations;

namespace MuviMuviApi.Models;

public class Actor
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]    
    public string Name { get; set; }
    public DateTime birthdate { get; set; }
    public string? photoUrl { get; set; }
}