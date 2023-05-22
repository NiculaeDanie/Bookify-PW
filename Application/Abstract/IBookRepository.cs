﻿using Bookify.Domain.Model;
using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById(string id);
        Task Add(Book book);
        Task<List<Book>> GetBookByGenre(Genre genre);
        Task Remove(Book book);
        Task Update(Book book);
        Task IncrementViewCount(Book book);
        Task<Book> AddGenreToBook(Genre genre, Book book);
        Task<List<Book>> GetBookByGenre(Genre genre, List<Book> history);
        Task<List<Book>> Search(string search);
        Task<List<Book>> GetBookByAuthor(Author author);
        Task<List<Book>> GetHistory(string userid);
        Task<List<Book>> GetFullHistory();
        Task<List<Book>> GetFavorites(string userid);
    }
}
