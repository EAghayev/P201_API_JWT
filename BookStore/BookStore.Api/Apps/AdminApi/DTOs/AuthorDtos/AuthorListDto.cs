using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.DTOs.AuthorDtos
{
    public class AuthorListDto
    {
        public List<AuthorListItemDto> Items { get; set; }
        public int TotalPage { get; set; }
    }
}
