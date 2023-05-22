﻿using Bookify.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserBook
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
