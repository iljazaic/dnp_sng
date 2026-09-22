using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.UI.ManagePosts
{
        public class ManagePostView
        {
            private readonly IPostRepository postRepository;

            public ManagePostView(IPostRepository postRepository)
            {
                this.postRepository = postRepository;
            }


        }

}
