using AutoMapper;
using Friends.Core.Application.Dtos.Email;
using Friends.Core.Application.Helpers;
using Friends.Core.Application.Interface.Repository;
using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Service
{
    public class UserService : GenericService<SaveUserViewModel, UserViewModel, User>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        private readonly IEmailService _emailService;
        public UserService(IMapper mapper, IUserRepository repository, IEmailService emailService) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _emailService = emailService;
        }
        public async Task<UserViewModel> Login(LoginViewModel log)
        {
            var user = await _repository.Login(log);
            return _mapper.Map<UserViewModel>(user);
        }
        public override async Task<SaveUserViewModel> Add(SaveUserViewModel viewModel)
        {
            await _emailService.SendAsync(new EmailRequest
            {
                To = viewModel.Email,
                Subject = "Bienbenido a FriendsApp",
                Body = $"<h1>Bienvenid@ a FriendsApp</h1> <p>Tu registro ha sido exitoso, tu nombre de usuario es " +
    $"<span><strong>{viewModel.UserName}</strong><strong><p>"
            });
            return await base.Add(viewModel);
        }
        public async Task<bool> PasswordReset(LoginViewModel log)
        {
            User user = await _repository.PasswordReset(log);
            if(user != null && user.Id != 0)
            {
                user.Password = PasswordEncryptation.ComputeSha256Has(log.Password);
                await _repository.UpdateAsync(user, user.Id);

                await _emailService.SendAsync(new EmailRequest
                {
                    To = user.Email,
                    Subject = "FriendsApp Cambio de Contraseña",
                    Body = "<h3>Se ha hecho un cambio de contraseña en tu cuenta!!</h3>" +
                    "<p>Este correo de alerta te deja saber que se ha hecho un cambio de contraseña en tu cuenta de FrindsApp. <br/> " +
                    "Si no ha sido tú quien ha hecho este cambio cambio <br/>contactanos!!</p>"
                });

                return true;
            }
            return false;
        }
        public async Task<UserViewModel> GetWithInclude( int id)
        {
            User user = _repository.GetWithOne(new List<string> { "Posts", "Friendships" }).Result.FirstOrDefault(a => a.Id.Equals(id));
            return _mapper.Map<UserViewModel>(user);
        }
        public bool Validate(string content, string type)
        {
            User user;
            switch (type)
            {
                case "email":
                    user = _repository.GetAllAsync().Result.FirstOrDefault(x=> x.Email == content);
                    break;
                case "user":
                    user = _repository.GetAllAsync().Result.FirstOrDefault(x => x.UserName == content);
                    break;
                default:
                    user = null;
                    break;
            }
            if (user != null)
            {
                return true;
            }
            return false;
        }   
    }
}
