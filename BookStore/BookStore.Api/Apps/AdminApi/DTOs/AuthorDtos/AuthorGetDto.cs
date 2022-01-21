using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.DTOs.AuthorDtos
{
    public class AuthorGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int BornYear { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
