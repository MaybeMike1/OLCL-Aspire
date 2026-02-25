using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspireApp.ApiService.Entities;

public class Participant
{
    [Column("Participant_PK")]
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Email { get; set; } = string.Empty;

    public Course Course { get; set; } = new();
}