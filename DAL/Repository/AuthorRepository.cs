﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Abstract;
using Bookify.Domain.Model;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class AuthorRepository: IAuthorRepository
    {
        private readonly DataContext _context;
        public AuthorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task AddBookToAuthor(Author author, Book book)
        {
            var auth = await _context.Authors.Include(a=> a.AuthorBook).SingleOrDefaultAsync(a => a.Id == author.Id);
            var res = new AuthorBook()
            {
                AuthorId = author.Id,
                BookId = book.Id,
                Author = author,
                Book = book
            };
            author.AuthorBook.Add(res);
        }

        public async Task<List<Author>> GetAll()
        {
            return await _context.Authors.Take(100).ToListAsync();
        }

        public async Task<List<Book>> GetBooks(Author author)
        {
            var auth = await _context.Authors.Include(a => a.AuthorBook).ThenInclude(b => b.Book).SingleOrDefaultAsync(a => a.Id == author.Id);
            return auth.AuthorBook.Select(a => a.Book).ToList();
        }

        public async Task<Author> GetById(string AuthorId)
        {
            var author = await _context.Authors.SingleOrDefaultAsync(a => a.Id == AuthorId);
            return author;
        }

        public async Task Remove(Author author)
        {
            _context.Authors.Remove(author);
        }

        public async Task Update(Author author)
        {
            _context.Authors.Update(author);
        }

        public async Task<List<Author>> Search(string search)
        {
            return await _context.Authors.Where(b => b.Name.Contains(search) || b.Description.Contains(search)).ToListAsync();
        }
    }
}
