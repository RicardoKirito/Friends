using Friends.Core.Application.ViewModels.Post;
using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Interface.Service
{
    public interface IPostService : IGenericService<SavePostViewModel, PostViewModel, Post>
    {

        PostViewModel GetWithInclude(int id);
    }
}
