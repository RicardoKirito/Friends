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
    public class ReplyRepository: GenericRepository<Reply>, IReplyRepository
    {
        private readonly ApplicationContext context;
        public ReplyRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
