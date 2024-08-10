using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.ViewModels.Post
{
    public class SavePostViewModel
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? ImagePath { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }

    }
}
