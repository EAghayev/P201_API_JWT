using AutoMapper;
using BookStore.Api.DTOs.AuthorDtos;
using BookStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Profiles
{
    public class AppProfile:Profile
    {
        public AppProfile()
        {
            CreateMap<Author, AuthorGetDto>();
            CreateMap<Author, AuthorListItemDto>()
                .ForMember(dest => dest.BK_Count, m => m.MapFrom(src => src.Books.Count));
        }
    }
}
