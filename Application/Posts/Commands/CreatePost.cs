using Domain.Models;
using MediatR;

namespace Application.Posts.Queries
{
    public class CreatePost : IRequest<Post>
    {
        public string? PostContent { get; set; }
    }
}