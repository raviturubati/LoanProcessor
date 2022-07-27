using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoanProcessor.Auth.Model;

namespace LoanProcessor.Auth.Data
{
    public class LoanProcessorAuthContext : DbContext
    {
        public LoanProcessorAuthContext (DbContextOptions<LoanProcessorAuthContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
