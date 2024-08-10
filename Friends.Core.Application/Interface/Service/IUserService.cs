using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Interface.Service
{
    public interface IUserService: IGenericService<SaveUserViewModel, UserViewModel, User>
    {

        Task<UserViewModel> Login(LoginViewModel log);
        Task<bool> PasswordReset(LoginViewModel log);
        Task<UserViewModel> GetWithInclude(int id);
        bool Validate(string content, string type);
    }
}
