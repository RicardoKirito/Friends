using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Interface.Repository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> Login(LoginViewModel log);
        Task<User> PasswordReset(LoginViewModel log);
    }
}
