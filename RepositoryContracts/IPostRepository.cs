using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPostRepository
    {
        Task<Post> AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
        Task<Post> GetSingleAsync(int id);
        IQueryable<Post> GetMany();
    }
}
