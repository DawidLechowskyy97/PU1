using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRCQRS.Authors
{
    public class GetAuthorQueryHandlerNext : IRequestHandler<GetAuthorQueryNext, AuthorDTO>
    {
        private readonly Database db;
        public GetAuthorQueryHandlerNext(Database db)
        {
            this.db = db;
        }

        public Task<AuthorDTO> Handle(GetAuthorQueryNext query, CancellationToken token)
        {
            return Task.FromResult(db.Authors
            .Include(b => b.Rates)
            .Include(b => b.Books)
            .ToList().Select(b => new AuthorDTO
            {
                Id = b.Id,
                FirstName = b.FirstName,
                SecondName = b.SecondName,
                AvarageRate = (b.Rates.Count() > 0 ? b.Rates.Average(r => r.Value) : 0),
                RatesCount = (b.Rates.Count() > 0 ? b.Rates.Count() : 0),
                Books = b.Books.Select(a => new AuthorBooksDTO
                {
                    Title = a.Title,
                    Id = a.Id
                }).ToList()
            }).Where(b => b.Id == query.id).Single());
        }
    }
}
