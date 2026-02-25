using AspireApp.ApiService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspireApp.ApiService.Database;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany(x => x.Participants)
            .WithOne(x => x.Course);

        
    }
}