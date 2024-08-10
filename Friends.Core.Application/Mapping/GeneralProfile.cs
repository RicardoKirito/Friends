using AutoMapper;
using Friends.Core.Application.ViewModels.Comment;
using Friends.Core.Application.ViewModels.Friendship;
using Friends.Core.Application.ViewModels.Post;
using Friends.Core.Application.ViewModels.Reply;
using Friends.Core.Application.ViewModels.User;
using Friends.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UserMap
            CreateMap<User, UserViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore())
                 .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());


            CreateMap<User, SaveUserViewModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(sr => sr.CreatedDate))
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.Picture, opt => opt.Ignore())
                .ForMember(dest => dest.Imagepath, opt => opt.MapFrom(src => src.Picture))
                .ReverseMap()
                .ForMember(dest => dest.Posts, opt => opt.Ignore())
                .ForMember(dest => dest.Friendships, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreadedBy, opt => opt.MapFrom(sr=> sr.Date))
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore())
                 .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            #endregion
            #region PostMap
            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(sr => sr.CreatedDate))
                .ReverseMap()
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore());


            CreateMap<Post, SavePostViewModel>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.Date, opt => opt.MapFrom(sr => sr.CreatedDate))
                .ReverseMap()
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore())
                 .ForMember(dest => dest.CreadedBy, opt => opt.MapFrom(sr => sr.UserId))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(sr => sr.Date));
            #endregion
            #region ReplyMap
            CreateMap<Reply, ReplyViewModel>();
            CreateMap<Reply, SaveReplyViewModel>();
            #endregion
            #region CommentMap
            CreateMap<Comment, CommentViewModel>()
                                .ForMember(dest => dest.Date, opt => opt.MapFrom(sr => sr.CreatedDate))
                .ReverseMap()
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore());

            CreateMap<Comment, SaveCommentViewModel>()
                .ReverseMap()
                .ForMember(d => d.User, opt=> opt.Ignore())
                .ForMember(d => d.Post, opt => opt.Ignore())
                .ForMember(d => d.Replys, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore())
                 .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            #endregion
            #region FriendshipMap
            CreateMap<Friendship, FriendshipViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore())
                 .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
            CreateMap<Friendship, SaveFriendshipViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore())
                 .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());

            #endregion
        }
    }
}
