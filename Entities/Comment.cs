using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Comment
    {

        public Comment() { }
        public Comment(int id)
        {
            this.Id = id;
        }
        public int Id { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
