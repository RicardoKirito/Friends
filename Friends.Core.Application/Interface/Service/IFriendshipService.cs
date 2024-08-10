using Friends.Core.Application.ViewModels.Friendship;
using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Interface.Service
{
    public interface IFriendshipService : IGenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>
    {
        FriendshipViewModel GetWithInclude(int id);

    }
}
