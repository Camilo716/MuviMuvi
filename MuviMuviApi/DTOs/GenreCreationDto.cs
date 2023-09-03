using System.ComponentModel.DataAnnotations;

public class GenreCreationDTO
{
    [Required]
    [StringLength(40)]
    public string Name { get; set; }
}