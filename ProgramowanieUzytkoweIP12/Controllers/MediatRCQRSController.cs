using MediatR;
using MediatRCQRS;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediatRCQRSController : ControllerBase
    {
        private readonly IMediator mediator;

        public MediatRCQRSController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/mediatR/authors")]
        public Task<List<AuthorDTO>> GetAuthors([FromQuery] GetAuthorsQueryNext query)
        {
            return mediator.Send(query);
        }

        [HttpGet("/mediatR/author")]
        public Task<AuthorDTO> GetAuthor([FromQuery] GetAuthorQueryNext query)
        {
            return mediator.Send(query);
        }

        [HttpPost("/mediatR/author/add")]
        public Task<bool> AddAuthor([FromBody] AddAuthorCommandNext command)
        {
            return mediator.Send(command);
        }

        [HttpDelete("/mediatR/author/delete/{id}")]
        public Task<bool> DeleteAuthor(int id)
        {
            return mediator.Send(new DeleteAuthorCommandNext(id));
        }

        [HttpPost("/mediatR/authors/rate/add")]
        public Task<bool> AddAuthorRate([FromBody] int id, int rate)
        {
            return mediator.Send(new AddAuthorRateCommandNext(id, rate));
        }

        [HttpGet("/mediatR/books")]
        public Task<List<BookDTO>> GetBooks([FromQuery] GetBooksQueryNext query)
        {
            return mediator.Send(query);
        }

        [HttpGet("/mediatR/book")]
        public Task<BookDTO> GetBook([FromQuery] GetBookQueryNext query)
        {
            return mediator.Send(query);
        }

        [HttpPost("/mediatR/book/add")]
        public Task<bool> AddBook([FromBody] AddBookCommandNext command)
        {
            return mediator.Send(command);
        }

        [HttpDelete("mediatR/book/delete/{id}")]
        public Task<bool> DeleteBook(int id)
        {
            return mediator.Send(new DeleteBookCommandM(id));
        }

        [HttpPost("/mediatR/book/rate/add")]
        public Task<bool> AddBookRate([FromBody] int id, int rate)
        {
            return mediator.Send(new AddBookRateCommandNext(id, rate));
        }
    }
}
