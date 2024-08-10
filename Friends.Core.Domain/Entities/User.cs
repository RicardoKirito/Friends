using Friends.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Domain.Entities
{
    public class User : Auditable
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //Nav Prop
        public ICollection<Post> Posts { get; set; }

        public ICollection<Friendship> Friendships { get; set; }
    }
}

