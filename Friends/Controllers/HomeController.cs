using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.User;
using Friends.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Friends.Core.Application.Helpers;
using WebAppFriends.Midleware;
using Friends.Core.Application.Service;
using Friends.Core.Application.ViewModels.Post;
using Friends.Core.Application.ViewModels.Comment;
using Friends.Core.Domain.Entities;
using Microsoft.Extensions.Hosting;

namespace Friends.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ICommentService _comment;
        private readonly ValidateSession _validateSession;

        public HomeController(ILogger<HomeController> logger, ValidateSession validateSession, IUserService user, IPostService postService, ICommentService comment)
        {
            _logger = logger;
            _userService = user;
            _validateSession = validateSession;
            _postService = postService;
            _comment = comment;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateSession.HasSession())
            {
                return RedirectToRoute(new { controller = "User", action = "Login" });
            }
            UserViewModel user = await _userService.GetWithInclude(HttpContext.Session.Get<UserViewModel>("User").Id);
            ViewBag.User = user.Posts.OrderByDescending(x => x.Id).Select(
                x => x =  _postService.GetWithInclude(x.Id)
                );
            return View();
            
        }


        [HttpPost]
        public async Task<IActionResult> CreatePost(SavePostViewModel postvm, int options)
        {
            UserViewModel user = await _userService.GetWithInclude(HttpContext.Session.Get<UserViewModel>("User").Id);
            ViewBag.User = user.Posts;
            postvm.Type = options;
            if (!ModelState.IsValid)
            {
                return View("Index", postvm);
            }
             SavePostViewModel post = await  _postService.Add(postvm);
            if (post != null && postvm.File !=null) 
            {
                
                post.ImagePath = UploadFile(postvm.File, post.UserId, post.Id);

            }
            else
            {
                 post.ImagePath = ".";

            }
                await _postService.Update(post, post.Id);
              return RedirectToRoute(new { contoller = "Home", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost(SavePostViewModel post)
        {
            if (post.File != null)
            {
                string basePath = $"/Images/{post.UserId}/PostImages/{post.Id}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
                if (Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                    {
                        folder.Delete(true);
                    }
                    Directory.Delete(path);
                }
                post.ImagePath = UploadFile(post.File, post.UserId, post.Id);

            }
            await _postService.Update(post, post.Id);
            return RedirectToRoute(new { contoller = "Home", action = "Index" });
        }
        public async Task<IActionResult> DeletePost(int id)
        {
            SavePostViewModel post = await _postService.GetById(id);
            if(post.ImagePath != null)
            {

                string basePath = $"/Images/{post.UserId}/PostImages/{id}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
                if (Directory.Exists(path))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    foreach (FileInfo file in directoryInfo.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                    {
                        folder.Delete(true);
                    }
                    Directory.Delete(path);
                }
            }
            await _postService.DeleteById(id);
            return RedirectToRoute(new { contoller = "Home", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> CreateComment(int PostId,string Content, string vista)
        {

            SaveCommentViewModel vm = new SaveCommentViewModel{
                Id= 0,
                PostId= PostId,
                Content = Content,
                UserId= HttpContext.Session.Get<UserViewModel>("User").Id
            };
            await _comment.Add(vm);
            return RedirectToRoute(new { contoller = vista, action = "Index" });
        }

        public string UploadFile(IFormFile file, int iduser, int postid, bool IsEditMode = false, string ImageUrl = "")
        {
            if (IsEditMode && file == null)
            {
                return ImageUrl;
            }
            string basePath = $"/Images/{iduser}/PostImages/{postid}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            //Create Folder if not exist

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //Get file path
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new FileInfo(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, filename);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return $"{basePath}/{filename}";
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}