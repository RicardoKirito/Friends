using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddServiceLayer( this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services

            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IReplyService, ReplyService>();
            services.AddTransient<IFriendshipService, FriendshipService>();
            #endregion
        }
    }
}
