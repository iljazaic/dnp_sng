using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.UI.ManagePosts
{

    namespace ConsoleApp1.UI.ManagePosts
    {
        public class ListPostsView
        {
            private readonly IPostRepository postRepository;

            public ListPostsView(IPostRepository postRepository)
            {
                this.postRepository = postRepository;
            }


        }
    }
}
