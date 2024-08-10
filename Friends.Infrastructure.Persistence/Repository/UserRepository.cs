using Friends.Core.Application.Helpers;
using Friends.Core.Application.Interface.Repository;
using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using Friends.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Infrastructure.Persistence.Repository
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext context;
        public UserRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<User> AddAsync(User entity)
        {
            entity.Password = PasswordEncryptation.ComputeSha256Has(entity.Password);
            return await base.AddAsync(entity);
        }
        public async Task<User> Login(LoginViewModel login)
        {
            login.Password = PasswordEncryptation.ComputeSha256Has(login.Password);
            User user = context.Set<User>().FirstOrDefault(a => a.Password == login.Password && a.UserName == login.UserName);
            return user;
        }
        public async Task<User> PasswordReset(LoginViewModel log)
        {
            User user = context.Set<User>().FirstOrDefault(a => a.UserName == log.UserName);
            return user;
        }
    }
}
