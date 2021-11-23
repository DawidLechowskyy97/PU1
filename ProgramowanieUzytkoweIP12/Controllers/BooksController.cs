using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//kontroler dla całego repository pattern

namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksRepository repo;
        private readonly AuthorRepository arepo;

        public BooksController(BooksRepository repo, AuthorRepository arepo)
        {
            this.repo = repo;
            this.arepo = arepo;
        }

        [HttpGet("/books")]
        public IEnumerable<BookDTO> GetAllBooks([FromQuery] PaginationDTO pagination)
        {
            return this.repo.GetBooks(pagination);
        }

        [HttpGet("/book")]
        public BookDTO GetBook(int id)
        {
            return this.repo.GetBook(id);
        }

        [HttpPost]
        public BookDTO Post([FromBody] BookRequestDTO bookRequestDTO)
        {
            return this.repo.PostBook(bookRequestDTO);
        }

        [HttpPost("/book/rate/add")]
        public bool AddRateToBook(int id, int rate)
        {
            return this.repo.AddRateToBook(id, rate);
        }

        [HttpPut]
        public BookDTO Put(int index, [FromBody] BookRequestDTO bookDTO)
        {
            return null;
        }

        [HttpDelete("{index}")]
        public bool Delete([FromRoute] int index)
        {
            return this.repo.RemoveBook(index);
        }
        
        [HttpGet("/authors")]
        public IEnumerable<AuthorDTO> GetAuthors([FromQuery] PaginationDTO pagination)
        {
            return this.arepo.GetAuthors(pagination);
        }

        [HttpPost("/authors/add")]
        public bool CreateNewAuthor(AuthorDTO author)
        {
            return this.arepo.CreateNewAuthor(author);
        }

        [HttpPost("/authors/rate/add")]
        public bool AddRateToAuthors(int index, int rate)
        {
            return this.arepo.AddRateToAuthor(index, rate);
        }

        [HttpDelete("/authors/delete/{index}")]
        public bool DeleteAuthor([FromRoute] int index)
        {
            return this.arepo.DeleteAuthor(index);
        }
    }
}
