using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Core.Entities
{
    public class Author:BaseEntity
    {
        public string FullName { get; set; }
        public int BornYear { get; set; }

        public List<Book> Books { get; set; }
    }
}
