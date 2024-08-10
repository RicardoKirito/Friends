using AutoMapper;
using Friends.Core.Application.Interface.Repository;
using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.Comment;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Service
{
    public class CommentService: GenericService<SaveCommentViewModel, CommentViewModel, Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IMapper mapper): base(repository, mapper)
        {
            _commentRepository = repository;
            _mapper = mapper;
        }
    }
}
