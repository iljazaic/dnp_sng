using ApiContracts;

namespace BlazorApp.HttpServices;

public interface ICommentService
{
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request);
}
