
using Friends.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.ViewModels.Friendship
{ 
    public class FriendshipViewModel
    {
        public int Id { get; set; }
        public int User1 { get; set; }
        public int User2 { get; set; }
        public UserViewModel User { get; set; }
    }
}
