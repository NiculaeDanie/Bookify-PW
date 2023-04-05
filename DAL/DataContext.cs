using Bookify.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseSqlServer("data source=DESKTOP-GQAIKNI\\SQLEXPRESS;initial catalog=Bookify;trusted_connection=true");
    }
}
