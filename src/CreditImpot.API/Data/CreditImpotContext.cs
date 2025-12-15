
using CreditImpot.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CreditImpot.API.Data
{
    public class CreditImpotContext : DbContext


    {
        public CreditImpotContext(DbContextOptions<CreditImpotContext> options)
        : base(options)
        { }

        public DbSet<DemandeCreditFraisGarde> DemandeCreditFraisGardes { get; set; }

    }
}
