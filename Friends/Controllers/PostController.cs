using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.Post;
using Microsoft.AspNetCore.Mvc;
using WebAppFriends.Midleware;

namespace WebAppFriends.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ValidateSession _validateSession;

        public PostController(IPostService postService, ValidateSession validateSession)
        {
            _postService = postService;
            _validateSession = validateSession;
        }



    }
}
