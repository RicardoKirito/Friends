using Friends.Core.Application.ViewModels.Comment;
using Friends.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string ImagePath { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }

        //Nav Prop
        public ICollection<CommentViewModel> Comments { get; set; }

        public UserViewModel User { get; set; }
    }
}
