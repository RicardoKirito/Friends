using Friends.Core.Application.Helpers;
using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.Friendship;
using Friends.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAppFriends.Midleware;

namespace Friends.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateSession _validateSession;
        private readonly IEmailService _emailService;
        public UserController(IUserService userService, ValidateSession validate, IEmailService email)
        {
            _userService = userService;
            _validateSession = validate;
            _emailService = email;
        }

        #region Login
        public IActionResult Login()
        {
            if (_validateSession.HasSession())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel log, bool reset = false)
        {
            if (ModelState.IsValid)
            {
                
                UserViewModel user = await _userService.Login(log);
                if (_validateSession.HasSession())
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                if (user != null)
                {
                    HttpContext.Session.Set<UserViewModel>("User", user);

                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                ViewBag.User = "Contraseña o usuario inconrrecto";
                return View("Login", log);
            }
            ViewBag.Reset = reset;
            return View("Login", log);
        }
        public async Task<IActionResult> ResetPassword()
        {
            if (_validateSession.HasSession())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            ViewBag.Reset = "";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(LoginViewModel log)
        {

            if (ModelState.IsValid)
            {

                if( await _userService.PasswordReset(log))
                {
                    ViewBag.Reset = "Cantraseña Ha sido cambiada con exito";
                    ViewBag.Text = "success";
                    return View("ResetPassword",log);
                }
                ViewBag.Text = "danger";
                ViewBag.Reset = "Este usuario no se ha encontrado";
                return View("ResetPassword", log);
            }
            return View("ResetPassword", log);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("User");
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }
        #endregion


        #region SigUp
        public IActionResult SigUp()
        {
            if (_validateSession.HasSession())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View("SigUp", new SaveUserViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SigUp(SaveUserViewModel save)
        {
            if (!ModelState.IsValid)
            {
                return View("SigUp", save);
            }
           SaveUserViewModel user =  await _userService.Add(save);
            if(user != null && user.Id != 0)
            {
                user.Imagepath = UploadFile(save.Picture, user.Id);
                await _userService.Update(user, user.Id);
            }
            
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }
        #endregion

        #region Validations
        [AcceptVerbs("Get", "Post")]
        public JsonResult Validation(string content, string type="user")
        {
            if(_userService.Validate(content, type))
            {
                return Json(true);
            }
            return Json(false);
        }
        [AcceptVerbs("POST", "POST")]
        public IActionResult Exist(string content, string type)
        {
            if (!_userService.Validate(content, type))
            {
                return Json(false);
            }
            return Json(true);
        }
    

        #endregion

        public string UploadFile(IFormFile file, int id, bool IsEditMode = false, string ImageUrl = "")
        {
            if (IsEditMode && file == null)
            {
                return ImageUrl;
            }
            string basePath = $"/Images/{id}/profilepicture";
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

    }
}
