using Friends.Core.Application.ViewModels.Friendship;
using Friends.Core.Application.ViewModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Date { get; set; }

        //Nav Prop
        public ICollection<PostViewModel> Posts { get; set; }
        public ICollection<FriendshipViewModel> Friendships { get; set; }
    }
}
