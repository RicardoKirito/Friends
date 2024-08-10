using Friends.Core.Application.ViewModels.Comment;
using Friends.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.ViewModels.Reply
{
    public class ReplyViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }

        //Nav Prop
        public UserViewModel User { get; set; }
        public CommentViewModel Comment { get; set; }
    }
}
