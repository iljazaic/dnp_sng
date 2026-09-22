using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.UI.ManagePosts
{
    public class CreatePostView
    {
        private readonly IPostRepository postRepository;

        public CreatePostView(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }


    }
}
