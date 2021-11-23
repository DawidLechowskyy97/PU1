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
    public class AddBookRateCommandHandlerNext : IRequestHandler<AddBookRateCommandNext, bool>
    {
        private Database db { get; }

        public AddBookRateCommandHandlerNext(Database db)
        {
            this.db = db;
        }

        public Task<bool> Handle(AddBookRateCommandNext command, CancellationToken token)
        {
            var book = db.Books.Where(x => x.Id == command.id).Single();

            db.BooksRate.Add(new BookRate
            {
                Type = RateType.BookRate,
                Book = book,
                FkBook = book.Id,
                Date = DateTime.Now,
                Value = (short)command.rate
            });

            db.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
