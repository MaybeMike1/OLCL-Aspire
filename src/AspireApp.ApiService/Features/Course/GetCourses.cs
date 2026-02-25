using AspireApp.ApiService.Database;
using AspireApp.ApiService.Endpoints;
using FluentValidation;

namespace AspireApp.ApiService.Features.Courses;

public static class GetCourses
{
    public record Request();

    public record Response();

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {

        }
    }

    // public sealed class Endpoint : IEndpoint
    // {
    //     public void MapEndpoint(IEndpointRouteBuilder app)
    //     {
            
    //     }
    // }

    public static async Task<IResult> Handler(Request request, ApplicationDbContext context, IValidator validator)
    {
        return Results.Ok();
    }
}