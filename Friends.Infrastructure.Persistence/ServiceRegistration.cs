using Friends.Core.Application.Interface.Repository;
using Friends.Infrastructure.Persistence.Context;
using Friends.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("InMemoryDB"))
            {
                services.AddDbContext<ApplicationContext>(op => op.UseInMemoryDatabase("AplicationDB"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(option =>
               option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
               m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #region Repository

                services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddTransient<IUserRepository, UserRepository>();
                services.AddTransient<IPostRepository, PostRepository>();
                services.AddTransient<ICommentRepository, CommentRepository>();
                services.AddTransient<IReplyRepository, ReplyRepository>();
                services.AddTransient<IFriendshipRepository, FriendshipRepository>();

            #endregion
        }
    }
}
