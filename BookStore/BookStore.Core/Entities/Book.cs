using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Core.Entities
{
    public class Book:BaseEntity
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }

        public Author Author { get; set; }
    }
}
