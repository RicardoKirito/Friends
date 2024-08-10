using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Friends.Core.Application.Helpers;
using Friends.Core.Application.ViewModels.Friendship;
using WebAppFriends.Midleware;
using Friends.Core.Domain.Entities;
using Friends.Core.Application.ViewModels.Post;
using Friends.Core.Application.ViewModels.Comment;
using System.Xml.Linq;

namespace WebAppFriends.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendshipService _friendship;
        private readonly IUserService _userService;
        private readonly IPostService _postservice;
        private readonly ICommentService _comment;
        private readonly ValidateSession validateSession;
        public FriendController(IFriendshipService friendship, ValidateSession validate, IUserService userService, IPostService post, ICommentService comment)
        {
            _friendship = friendship;
            validateSession = validate;
            _userService = userService;
            _postservice = post;
            _comment = comment;
        }

        public async Task<IActionResult> Index(bool added= false, string username = "")
        {
            if (!validateSession.HasSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            ViewBag.Added = added;
            ViewBag.Name = username;

            UserViewModel user = await _userService.GetWithInclude(HttpContext.Session.Get<UserViewModel>("User").Id);
            List<FriendshipViewModel> friendships =  user.Friendships.Select(s => s = _friendship.GetWithInclude(s.User2)).ToList();
            for(int i=0; i < friendships.Count; i++)
            {
                friendships[i].Id = user.Friendships.ToList()[i].Id;
            }
            var u = friendships.Select(s => s.User.Posts);
            List<PostViewModel> posts = new();
            foreach (var y in u)
            {
                foreach (var j in y)
                {
                    posts.Add(_postservice.GetWithInclude(j.Id));
                }
            }
            ViewBag.Posts = posts.OrderByDescending(x=> x.Id);
            return View(friendships);
        }
        public async Task<IActionResult> SaveFriend()
        {
            if (!validateSession.HasSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveFriend(string Username)
        {

            var user =  _userService.GetAll().Result.FirstOrDefault(x=> x.UserName.Equals(Username));
            if (user != null)
            {
                if(user.Id != HttpContext.Session.Get<UserViewModel>("User").Id)
                {

                    SaveFriendshipViewModel friend = new SaveFriendshipViewModel
                    {
                        User1 = HttpContext.Session.Get<UserViewModel>("User").Id,
                        User2 = user.Id,
                    
                    };
                    SaveFriendshipViewModel friendreverse = new SaveFriendshipViewModel
                    {
                        User2 = HttpContext.Session.Get<UserViewModel>("User").Id,
                        User1 = user.Id,

                    };
                    await _friendship.Add(friend);
                    await _friendship.Add(friendreverse);


                    ViewBag.Valid = $"{user.Name} {user.LastName} añadido exitosamente ";
                    ViewBag.Text = "success";
                    return View("SaveFriend");
                }
                else if(user.Id == HttpContext.Session.Get<UserViewModel>("User").Id)
                {
                    ViewBag.Valid = $"No puedes agregarte a ti mismo como amigo";
                    ViewBag.Text = "danger";
                    return View("SaveFriend");
                }
            }
            ViewBag.Valid = $"{Username} no fue encontrado";
            ViewBag.Text = "danger";
            return View("SaveFriend");
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int PostId, string Content, string vista)
        {

            SaveCommentViewModel vm = new SaveCommentViewModel
            {
                Id = 0,
                PostId = PostId,
                Content = Content,
                UserId = HttpContext.Session.Get<UserViewModel>("User").Id
            };
            await _comment.Add(vm);
            return RedirectToRoute(new { contoller = vista, action = "Index" });
        }

        public async Task<IActionResult> DeleteFriend(int id)
        {
            await _friendship.DeleteById(id);
            return RedirectToRoute(new { controller = "Friend", action = "Index" });
        }
    }
}
