using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.UI.ManageUsers
{
    public class ManageUsersView
    {
        private readonly IUserRepository userRepository;

        public ManageUsersView(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        


    }

}
