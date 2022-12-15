using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcJav.Models;

namespace MvcJav.Data
{
    public class MvcJavContext : DbContext
    {
        public MvcJavContext (DbContextOptions<MvcJavContext> options)
            : base(options)
        {
        }

        public DbSet<MvcJav.Models.Jav> Jav { get; set; } = default!;
    }
}
