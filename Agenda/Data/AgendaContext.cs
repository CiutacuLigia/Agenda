using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Agenda.Models;

namespace Agenda.Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext (DbContextOptions<AgendaContext> options)
            : base(options)
        {
        }

        public DbSet<Agenda.Models.Utilizator> Utilizator { get; set; } = default!;
    }
}
