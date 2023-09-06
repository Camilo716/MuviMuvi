using System.ComponentModel.DataAnnotations;

namespace MuviMuviApi.DTOs;

public class ActorCreationDTO
{
    [Required]
    [StringLength(120)]    
    public string Name { get; set; }
    public DateTime birthdate { get; set; }
}