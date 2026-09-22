using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.UI.ManageUsers
{
    public class CreateUserView
    {
        private readonly IUserRepository userRepository;

        public CreateUserView(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


    }

}
