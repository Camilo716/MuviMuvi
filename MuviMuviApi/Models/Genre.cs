using System.ComponentModel.DataAnnotations;

namespace MuviMuviApi.Models;

public class Genre: IId
{
    public int Id { get; set; }

    [Required]
    [StringLength(40)]
    public string Name { get; set; }

}