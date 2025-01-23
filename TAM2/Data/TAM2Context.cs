using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TAM2.Models;

namespace TAM2.Data
{
    public class TAM2Context : DbContext
    {
        public TAM2Context (DbContextOptions<TAM2Context> options)
            : base(options)
        {
        }

        public DbSet<TAM2.Models.Attendance> Attendance { get; set; } = default!;
        public DbSet<TAM2.Models.User> User { get; set; } = default!;
        public DbSet<TAM2.Models.TimeOff> TimeOff { get; set; } = default!;
        public DbSet<TAM2.Models.PaidTimeOff> PaidTimeOff { get; set; } = default!;
        public DbSet<TAM2.Models.NonBusinessDay> NonBusinessDay { get; set; } = default!;
    }
}
