using Friends.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Domain.Entities
{
    public class Reply: Auditable
    {
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }

        //Nav Prop
        public User User { get; set; }
        public Comment Comment { get; set; }
    }
}
