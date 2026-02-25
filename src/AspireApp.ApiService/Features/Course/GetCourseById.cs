using AspireApp.ApiService.Database;
using AspireApp.ApiService.Endpoints;
using AspireApp.ApiService.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspireApp.ApiService.Features.Courses;

public static class GetCourseById
{
    public record Request(Guid Id);

    public record Response(string Description, string Name, DateTime StartDate, DateTime EndDate, ICollection<Participant> Participants);

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }


    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("courses/{Id}", Handler)
                .WithTags("Courses");
        }
    }

    public static async Task<IResult> Handler([FromRoute] Guid Id, ApplicationDbContext context, IValidator<Request> validator)
    {
        // var validationResult = await validator.ValidateAsync(request);

        // if (!validationResult.IsValid)
        // {
        //     return Results.BadRequest(validationResult.Errors);
       // } 

        var course = await context.Courses
            .Include(x => x.Participants)
            .FirstOrDefaultAsync(x => x.Id == Id);

        if (course is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(new Response(course.Description, course.Name, course.StartDate, course.EndDate, course.Participants));
    }
}