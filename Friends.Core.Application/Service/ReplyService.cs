using AutoMapper;
using Friends.Core.Application.Interface.Repository;
using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.Reply;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Service
{
    public class ReplyService: GenericService<SaveReplyViewModel, ReplyViewModel, Reply>, IReplyService
    {
        private readonly IReplyRepository _repository;
        private readonly IMapper _mapper;

        public ReplyService(IReplyRepository repository, IMapper mapper): base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
