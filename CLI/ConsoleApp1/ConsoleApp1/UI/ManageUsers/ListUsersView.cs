using Entities;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.UI.ManageUsers
{
    public class ListUsersView
    {
        private readonly IUserRepository userRepository;

        public ListUsersView(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<String> returnUserListString() {
            String returnString = "";

            IQueryable<User> users = userRepository.GetMany();

            foreach (User user in users)
            {
                returnString += user.toCliString() + "\n";
            }

            return returnString;
        }


    }

}
