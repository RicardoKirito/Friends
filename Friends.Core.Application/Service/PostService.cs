using AutoMapper;
using Friends.Core.Application.Interface.Repository;
using Friends.Core.Application.Interface.Service;
using Friends.Core.Application.ViewModels.Post;
using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Service
{
    public class PostService: GenericService<SavePostViewModel, PostViewModel, Post>, IPostService
    {
        private readonly ICommentService _commentService;
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository repository, IMapper mapper, ICommentService comment): base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _commentService = comment;
        }
        public PostViewModel GetWithInclude(int id)
        {
            Post user = _repository.GetWithOne(new List<string> { "Comments" }).Result.FirstOrDefault(a => a.Id.Equals(id));
            return _mapper.Map<PostViewModel>(user);
        }
        public async Task<Task> DeleteById(int id)
        {
            var post = await _repository.GetWithOne(new List<string> { "Comments" });
            var ok =     post.ToList().FirstOrDefault(a => a.Id.Equals(id));
            ok.Comments.Select(x => _commentService.DeleteById(x.Id)
                ); ;
            return base.DeleteById(id);
        }
    }
}
