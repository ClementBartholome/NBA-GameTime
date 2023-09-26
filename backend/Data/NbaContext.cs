using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class NbaContext : DbContext
    {
        public NbaContext (DbContextOptions<NbaContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Game { get; set; } = default!;
    }
