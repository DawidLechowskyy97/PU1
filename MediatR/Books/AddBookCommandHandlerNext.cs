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
    public class AddBookCommandHandlerNext : IRequestHandler<AddBookCommandNext, bool>
    {
        private Database db { get; }

        public AddBookCommandHandlerNext(Database db)
        {
            this.db = db;
        }

        public Task<bool> Handle(AddBookCommandNext command, CancellationToken token)
        {
            var book = new Book
            {
                Title = command.Title,
                ReleaseDate = command.ReleaseDate

            };
            book.Authors = db.Authors.Where(a => command.AuthorsIDs.Contains(a.Id)).ToList();
            db.Books.Add(book);
            db.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
