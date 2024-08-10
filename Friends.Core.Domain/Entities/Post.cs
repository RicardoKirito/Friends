using Friends.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Domain.Entities
{
    public class Post : Auditable
    {

        public int UserId { get; set; }
        public string? Title { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public string Imagepath { get; set; }

        //Nav Prop
        public ICollection<Comment> Comments { get; set; }

        public User User { get; set; }
    }
}
