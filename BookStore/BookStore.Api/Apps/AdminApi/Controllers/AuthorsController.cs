using AutoMapper;
using BookStore.Api.DTOs.AuthorDtos;
using BookStore.Core.Entities;
using BookStore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create(AuthorPostDto postDto)
        {
            Author author = new Author
            {
                BornYear = postDto.BornYear,
                FullName = postDto.FullName
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

            //return StatusCode(201,new {author.Id});
            return StatusCode(201, new { Id=author.Id});
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Author author = _context.Authors.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (author == null) return NotFound();

            AuthorGetDto authorGetDto = _mapper.Map<AuthorGetDto>(author);

            return Ok(authorGetDto);
        }

        private AuthorGetDto _mapToAuthorGetDto(Author author)
        {
            AuthorGetDto authorGetDto = new AuthorGetDto
            {
                Id = author.Id,
                FullName = author.FullName,
                BornYear = author.BornYear,
                CreatedAt = author.CreatedAt,
                ModifiedAt = author.ModifiedAt
            };

            return authorGetDto;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var authors = _context.Authors.Include(x => x.Books).Where(x => !x.IsDeleted);

            AuthorListDto authorListDto = new AuthorListDto
            {
                Items = new List<AuthorListItemDto>(),
                TotalPage = (int)Math.Ceiling(authors.Count()/4d)
            };

            authors = authors.Skip((page - 1) * 4).Take(4);

            //foreach (var item in authors.ToList())
            //{
            //    AuthorListItemDto authorDto = new AuthorListItemDto
            //    {
            //        Id = item.Id,
            //        FullName = item.FullName,
            //        BornYear = item.BornYear,
            //        BooksCount = item.Books.Count
            //    };

            //    authorListDto.Items.Add(authorDto);
            //}
            authorListDto.Items = _mapper.Map<List<AuthorListItemDto>>(authors.ToList());

            return Ok(authorListDto);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(AuthorPostDto authorDto,int id)
        {
            Author author = _context.Authors.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (author == null) return NotFound();

            author.FullName = authorDto.FullName;
            author.BornYear = authorDto.BornYear;
            author.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return NoContent();
        }

    }
}
