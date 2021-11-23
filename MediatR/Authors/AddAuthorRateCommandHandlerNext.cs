using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRCQRS
{
    public class AddAuthorRateCommandHandlerNext : IRequestHandler<AddAuthorRateCommandNext, bool>
    {
        private Database db { get; }

        public AddAuthorRateCommandHandlerNext(Database db)
        {
            this.db = db;
        }

        public Task<bool> Handle(AddAuthorRateCommandNext command, CancellationToken token)
        {
            var author = db.Authors.Where(x => x.Id == command.id).Single();

            db.AuthorsRate.Add(new AuthorRate
            {
                Type = RateType.AuthorRate,
                Author = author,
                FkAuthor = author.Id,
                Date = DateTime.Now,
                Value = (short)command.rate
            });

            db.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
