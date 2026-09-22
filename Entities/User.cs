using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User
    {
        public User() { }
        public User(int id)
        {
            this.Id = id;
        }

        public String toCliString()
        {
            return Id + " | " + Name + " | " + PasswordHash;
        }
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
    }
}
