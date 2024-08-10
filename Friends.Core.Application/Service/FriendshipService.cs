using AutoMapper;
using Friends.Core.Application.Interface.Repository;
using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.Friendship;
using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Service
{
    public class FriendshipService: GenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>, IFriendshipService
    {
        private readonly IFriendshipRepository _repository;
        private readonly IMapper _mapper;
        public FriendshipService(IFriendshipRepository repository, IMapper mapper): base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public FriendshipViewModel GetWithInclude(int id)
        {
            var list = _repository.GetWithOne(new List<string> { "User" }).Result;
            if (list.Count != 0)
            {
                Friendship friend = list.FirstOrDefault(d => d.User1 == id);
                return _mapper.Map<FriendshipViewModel>(friend);
            }
                return null;

        }

    }
}
