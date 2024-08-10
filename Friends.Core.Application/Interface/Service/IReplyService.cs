using Friends.Core.Application.ViewModels.Reply;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Interface.Service
{
    public interface IReplyService : IGenericService<SaveReplyViewModel, ReplyViewModel, Reply>
    {


    }
}
