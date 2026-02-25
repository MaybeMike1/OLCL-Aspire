using AspireApp.ApiService.Database;
using AspireApp.ApiService.Endpoints;
using AspireApp.ApiService.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AspireApp.ApiService.Features.Courses;

public static class CreateCourse
{
    public record Request(string Name, string Description, DateTime StartDate, DateTime EndDate);

    public record Response(Guid Id, string Description, string Name, DateTime StartDate, DateTime EndDate, ICollection<Participant> Participants);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.EndDate).GreaterThan(r => r.StartDate);
            RuleFor(r => r.StartDate).LessThan(r => r.EndDate);
        }
    }

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("courses", Handler)
                .WithTags("Courses");
        }
    }

    public static async Task<IResult> Handler([FromBody]Request request, ApplicationDbContext context, IValidator<Request> validator)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var course = new Course
        {
            Description = request.Description,
            EndDate = request.EndDate,
            StartDate = request.StartDate,
            Name = request.Name,
            Participants = [],
        };

        context.Courses.Add(course);

        await context.SaveChangesAsync();

        return Results.Ok(new Response(course.Id, course.Description, course.Name, course.StartDate, course.EndDate, []));
    }
}