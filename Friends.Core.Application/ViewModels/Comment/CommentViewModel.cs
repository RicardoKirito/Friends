using Friends.Core.Application.ViewModels.Post;
using Friends.Core.Application.ViewModels.Reply;
using Friends.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }

        //Nav Prop
        public ICollection<ReplyViewModel> Replys { get; set; }
        public UserViewModel User { get; set; }
        public PostViewModel Post { get; set; }
    }
}
