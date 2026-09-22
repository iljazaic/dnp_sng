using Entities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.UI
{
    public class CliApp
    {

        public CliApp() { }
        private IUserRepository userRepository {get;set;}
        private ICommentRepository commentRepository {get;set;}
        private IPostRepository postRepository {get;set;}

        public CliApp( IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
        {
            this.userRepository = userRepository;
            this.commentRepository = commentRepository;
            this.postRepository = postRepository;
        }


        public async Task AddUserAsync() {

            User user = new User(1);
            User created = await userRepository.AddAsync(user);

        }

        public async Task StartAsync()
        {
        }
    }
}
