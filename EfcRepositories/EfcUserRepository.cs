using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new NotFoundException($"User with ID '{user.Id}' not found");
        }

        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new NotFoundException($"User with ID '{id}' not found");
        }

        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        User? user = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            throw new NotFoundException($"User with ID '{id}' not found");
        }

        return user;
    }

    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }

    public async Task FollowAsync(int followerId, int followingId)
    {
        if (followerId == followingId)
        {
            throw new InvalidOperationException("A user cannot follow themselves");
        }

        if (await ctx.Follows.AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId))
        {
            throw new InvalidOperationException($"User '{followerId}' already follows user '{followingId}'");
        }

        await ctx.Follows.AddAsync(new Follow
        {
            FollowerId = followerId,
            FollowingId = followingId,
            CreatedDate = DateTime.Now
        });
        await ctx.SaveChangesAsync();
    }

    public async Task UnfollowAsync(int followerId, int followingId)
    {
        Follow? follow = await ctx.Follows
            .SingleOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);
        if (follow == null)
        {
            throw new InvalidOperationException($"User '{followerId}' does not follow user '{followingId}'");
        }

        ctx.Follows.Remove(follow);
        await ctx.SaveChangesAsync();
    }

    public IQueryable<User> GetFollowers(int userId)
    {
        IQueryable<int> followerIds = ctx.Follows.Where(f => f.FollowingId == userId).Select(f => f.FollowerId);
        return ctx.Users.Where(u => followerIds.Contains(u.Id));
    }

    public IQueryable<User> GetFollowing(int userId)
    {
        IQueryable<int> followingIds = ctx.Follows.Where(f => f.FollowerId == userId).Select(f => f.FollowingId);
        return ctx.Users.Where(u => followingIds.Contains(u.Id));
    }
}
