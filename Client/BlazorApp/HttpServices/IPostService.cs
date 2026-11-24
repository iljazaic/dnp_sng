using ApiContracts;

namespace BlazorApp.HttpServices;

public interface IPostService
{
    public Task<PostDto> AddPostAsync(CreatePostDto request);
    public Task<List<PostDto>> GetPostsAsync();
    public Task<PostDto> GetPostAsync(int id);
}
