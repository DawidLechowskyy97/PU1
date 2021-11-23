using Model;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RepositoryPattern
{
    public class BooksRepository
    {
        private readonly Database db;

        public BooksRepository(Database db)
        {
            this.db = db;
            //db.Books.Add(new Book
            //{
            //    Title = "Jazda z gosciem",
            //    Authors = new List<Author> {
            //        new Author { FirstName = "Pawelek", SecondName = "Tuman"},
            //        new Author { FirstName = "Maciej", SecondName = "Kubik"}
            //    },
            //    ReleaseDate = new System.DateTime(2021, 10, 19),
            //    Rates = new List<BookRate>{
            //        new BookRate { Date = new System.DateTime(2021, 10, 19), Value = 5},
            //        new BookRate { Date = new System.DateTime(2021, 10, 20), Value = 4},
            //        new BookRate { Date = new System.DateTime(2021, 10, 21), Value = 4},
            //    }
            //});
            //db.SaveChanges();
        }
        public List<BookDTO> GetBooks(PaginationDTO pagination)
        {
            //var pag = new PaginationDTO(3, 5);
            return db.Books
                .Include(b => b.Rates)
                .Include(b => b.Authors)
                .Skip(pagination.Page * pagination.Count)
                .Take(pagination.Count)
                .ToList()
                .Select(b => new BookDTO
            {
                ID = b.Id,
                ReleaseDate = b.ReleaseDate,
                AvarageRate = b.Rates.Count > 0 ? b.Rates.Average(r => r.Value) : 0,
                RatesCount = b.Rates.Count,
                Title = b.Title,
                Authors = b.Authors.Select(a => new BookAuthorDTO
                {
                    FirstName = a.FirstName,
                    SecondName = a.SecondName,
                    Id = a.Id
                }).ToList()
            }).ToList();
        }

        public BookDTO GetBook(int id)
        {
            return db.Books.Include(b => b.Rates).Include(b => b.Authors).Select(b => new BookDTO
            {
                ID = b.Id,
                ReleaseDate = b.ReleaseDate,
                AvarageRate = b.Rates.Count > 0 ? b.Rates.Average(r => r.Value) : 0,
                RatesCount = b.Rates.Count,
                Title = b.Title,
                Authors = b.Authors.Select(a => new BookAuthorDTO
                {
                    FirstName = a.FirstName,
                    SecondName = a.SecondName,
                    Id = a.Id
                }).ToList()
            }).Where(b => b.ID == id).Single(); 
        }

        public BookDTO PostBook(BookRequestDTO bookDTO)
        {
            Book book = new Book
            {
                Title = bookDTO.Title,
                ReleaseDate = bookDTO.ReleaseDate,
            };

            book.Authors = db.Authors.Where(a => bookDTO.AuthorsIDs.Contains(a.Id)).ToList();
            db.Books.Add(book);
            db.SaveChanges();

            return new BookDTO
            {
                ID = book.Id,
                Authors = book.Authors.Select(a => new BookAuthorDTO
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    SecondName = a.SecondName
                }).ToList(),
                AvarageRate = 0,
                RatesCount = 0,
                ReleaseDate = book.ReleaseDate,
                Title = book.Title
            };
        }

        public bool RemoveBook(int index)
        {
            var book = db.Books.Where(x => x.Id == index).Single();
            if(book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool AddRateToBook(int index, int rate)
        {
            var book = db.Books.Where(x => x.Id == index).Single();
            if (book != null)
            {
                db.BooksRate.Add(new BookRate
                {
                    Type = RateType.BookRate,
                    Book = book,
                    FkBook = book.Id,
                    Date = DateTime.Now,
                    Value = (short)rate
                });
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
