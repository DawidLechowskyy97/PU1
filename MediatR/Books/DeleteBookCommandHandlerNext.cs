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
    public class DeleteBookCommandHandlerNext : IRequestHandler<DeleteBookCommandM, bool>
    {
        private readonly Database db;

        public DeleteBookCommandHandlerNext(Database db)
        {
            this.db = db;
        }

        public Task<bool> Handle(DeleteBookCommandM command, CancellationToken token)
        {
            var book = db.Books.Where(x => x.Id == command.id).Single();

            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }

            return Task.FromResult(true);
        }
    }
}
