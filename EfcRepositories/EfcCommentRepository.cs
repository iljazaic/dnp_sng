using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcCommentRepository : ICommentRepository
{
    private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        EntityEntry<Comment> entityEntry = await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Comment comment)
    {
        if (!(await ctx.Comments.AnyAsync(c => c.Id == comment.Id)))
        {
            throw new NotFoundException($"Comment with ID '{comment.Id}' not found");
        }

        ctx.Comments.Update(comment);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (existing == null)
        {
            throw new NotFoundException($"Comment with ID '{id}' not found");
        }

        ctx.Comments.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        Comment? comment = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id);
        if (comment == null)
        {
            throw new NotFoundException($"Comment with ID '{id}' not found");
        }

        return comment;
    }

    public IQueryable<Comment> GetMany()
    {
        return ctx.Comments.AsQueryable();
    }

    public async Task LikeAsync(int commentId, int userId)
    {
        if (await ctx.CommentLikes.AnyAsync(cl => cl.CommentId == commentId && cl.UserId == userId))
        {
            throw new InvalidOperationException($"User '{userId}' already liked comment '{commentId}'");
        }

        await ctx.CommentLikes.AddAsync(new CommentLike
        {
            CommentId = commentId,
            UserId = userId,
            CreatedDate = DateTime.Now
        });
        await ctx.SaveChangesAsync();
    }

    public async Task UnlikeAsync(int commentId, int userId)
    {
        CommentLike? like = await ctx.CommentLikes
            .SingleOrDefaultAsync(cl => cl.CommentId == commentId && cl.UserId == userId);
        if (like == null)
        {
            throw new InvalidOperationException($"User '{userId}' has not liked comment '{commentId}'");
        }

        ctx.CommentLikes.Remove(like);
        await ctx.SaveChangesAsync();
    }

    public async Task<int> GetLikeCountAsync(int commentId)
    {
        return await ctx.CommentLikes.CountAsync(cl => cl.CommentId == commentId);
    }

    public IQueryable<Comment> GetReplies(int commentId)
    {
        return ctx.Comments.Where(c => c.ParentCommentId == commentId);
    }
}
