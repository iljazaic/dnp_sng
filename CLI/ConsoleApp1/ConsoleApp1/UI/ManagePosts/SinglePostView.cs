using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1.UI.ManagePosts
{
    public class SinglePostView
    {
        private readonly IPostRepository postRepository;

        public SinglePostView(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }


    }

}
