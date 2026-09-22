using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.UI;
using InMemoryRepositories;
using RepositoryContracts;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting CLI app");

            IUserRepository userRepository = new UserInMemoryRepository();
            ICommentRepository commentRepository = new CommentInMemoryRepository();
            IPostRepository postRepository = new PostInMemoryRepository();

            CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
            await cliApp.StartAsync();

        }
    }
}
