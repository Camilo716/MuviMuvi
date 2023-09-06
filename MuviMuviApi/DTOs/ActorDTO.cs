using System.ComponentModel.DataAnnotations;

namespace MuviMuviApi.DTOs;

public class ActorDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]    
    public string Name { get; set; }
    public DateTime birthdate { get; set; }
    public string photoUrl { get; set; }
}