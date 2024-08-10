using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Friends.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Este campo es necesariro")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Este campo es necesariro")]
        public string Password { get; set; }

    }
}
