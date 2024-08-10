using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Friends.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage ="Se necesita un formato de nuemero de telefono")]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
 //       [Remote(action: "Validation", controller:"User", AdditionalFields ="email")]
        public string Email { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }
        [DataType(DataType.Text)]
        [Remote("Validation", "UserController", ErrorMessage ="Usuario ya existe")]
        public string UserName { get; set; }
        [MaxLength(16, ErrorMessage ="Contraseña muy larga")]
        [MinLength(7, ErrorMessage ="Contraseña muy corta")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare(nameof(Password),ErrorMessage ="Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
        public string? Imagepath { get; set; }
        public DateTime Date { get; set; }

    }
}
