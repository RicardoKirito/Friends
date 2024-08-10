using Friends.Core.Application.Interface.Repository;
using Friends.Core.Domain.Entities;
using Friends.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Infrastructure.Persistence.Repository
{
    public class PostRepository: GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationContext context;
        public PostRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
