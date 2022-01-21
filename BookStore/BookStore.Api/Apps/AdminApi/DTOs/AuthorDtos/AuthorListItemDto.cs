using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.DTOs.AuthorDtos
{
    public class AuthorListItemDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int BornYear { get; set; }
        public int BK_Count { get; set; }
    }
}
