using System.ComponentModel.DataAnnotations.Schema;

namespace AspireApp.ApiService.Entities;

public class Course
{
    [Column("Course_PK")]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public ICollection<Participant> Participants { get; set; } = [];
}