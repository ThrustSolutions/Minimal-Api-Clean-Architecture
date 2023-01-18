using Domain.Models;

namespace MinimalApi.Filters
{
    public class PostValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            // this is the second argument of the controller that it checks
            // to validate this argument we use global exception handling 
            var post = context.GetArgument<Post>(0);
            if (string.IsNullOrEmpty(post.Content)) return await Task.FromResult(Results.BadRequest("Post not valid: Content cannot be empty"));

            return await next(context);
        }
    }
}