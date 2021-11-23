using MediatR;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRCQRS
{
    public class DeleteAuthorCommandHandlerNext : IRequestHandler<DeleteAuthorCommandNext, bool>
    {
        private readonly Database db;

        public DeleteAuthorCommandHandlerNext(Database db)
        {
            this.db = db;
        }

        public Task<bool> Handle(DeleteAuthorCommandNext command, CancellationToken token)
        {
            var author = db.Authors.Include(x => x.Books).Where(x => x.Id == command.id).Single();

            if (author.Books.Any() == false)
            {
                db.Authors.Remove(author);
                db.SaveChanges();
            }

            return Task.FromResult(true);

        }
    }
}
