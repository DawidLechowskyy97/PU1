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
    public class AddAuthorCommandHandlerNext : IRequestHandler<AddAuthorCommandNext, bool>
    {
        private Database db { get; }

        public AddAuthorCommandHandlerNext(Database db)
        {
            this.db = db;
        }

        public Task<bool> Handle(AddAuthorCommandNext command, CancellationToken token)
        {
            var author = new Author
            {
                FirstName = command.FirstName,
                SecondName = command.SecondName

            };
            author.Books = db.Books.Where(a => command.BooksIDs.Contains(a.Id)).ToList();
            db.Authors.Add(author);
            db.SaveChanges();

            return Task.FromResult(true);
        }

    }
}
