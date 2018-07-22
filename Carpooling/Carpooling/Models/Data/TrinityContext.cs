using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carpooling.Models.Data
{
    public class TrinityContext : DbContext
    {
        public TrinityContext()
        {
        }

        public TrinityContext(DbContextOptions<TrinityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drives> Drives { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<PTD> PTD { get; set; }
    }
}

