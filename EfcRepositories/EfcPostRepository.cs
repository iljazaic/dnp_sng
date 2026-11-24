using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Post> AddAsync(Post post)
    {
        EntityEntry<Post> entityEntry = await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new NotFoundException($"Post with ID '{post.Id}' not found");
        }

        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new NotFoundException($"Post with ID '{id}' not found");
        }

        ctx.Posts.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? post = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (post == null)
        {
            throw new NotFoundException($"Post with ID '{id}' not found");
        }

        return post;
    }

    public IQueryable<Post> GetMany()
    {
        return ctx.Posts.AsQueryable();
    }

    public async Task LikeAsync(int postId, int userId)
    {
        if (await ctx.PostLikes.AnyAsync(pl => pl.PostId == postId && pl.UserId == userId))
        {
            throw new InvalidOperationException($"User '{userId}' already liked post '{postId}'");
        }

        await ctx.PostLikes.AddAsync(new PostLike
        {
            PostId = postId,
            UserId = userId,
            CreatedDate = DateTime.Now
        });
        await ctx.SaveChangesAsync();
    }

    public async Task UnlikeAsync(int postId, int userId)
    {
        PostLike? like = await ctx.PostLikes.SingleOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);
        if (like == null)
        {
            throw new InvalidOperationException($"User '{userId}' has not liked post '{postId}'");
        }

        ctx.PostLikes.Remove(like);
        await ctx.SaveChangesAsync();
    }

    public async Task<int> GetLikeCountAsync(int postId)
    {
        return await ctx.PostLikes.CountAsync(pl => pl.PostId == postId);
    }

    public async Task AddTagAsync(int postId, int tagId)
    {
        if (!(await ctx.Tags.AnyAsync(t => t.Id == tagId)))
        {
            throw new NotFoundException($"Tag with ID '{tagId}' not found");
        }

        if (await ctx.PostTags.AnyAsync(pt => pt.PostId == postId && pt.TagId == tagId))
        {
            throw new InvalidOperationException($"Post '{postId}' already has tag '{tagId}'");
        }

        await ctx.PostTags.AddAsync(new PostTag { PostId = postId, TagId = tagId });
        await ctx.SaveChangesAsync();
    }

    public async Task RemoveTagAsync(int postId, int tagId)
    {
        PostTag? postTag = await ctx.PostTags.SingleOrDefaultAsync(pt => pt.PostId == postId && pt.TagId == tagId);
        if (postTag == null)
        {
            throw new InvalidOperationException($"Post '{postId}' does not have tag '{tagId}'");
        }

        ctx.PostTags.Remove(postTag);
        await ctx.SaveChangesAsync();
    }

    public IQueryable<Tag> GetTagsForPost(int postId)
    {
        IQueryable<int> tagIds = ctx.PostTags.Where(pt => pt.PostId == postId).Select(pt => pt.TagId);
        return ctx.Tags.Where(t => tagIds.Contains(t.Id));
    }
}
