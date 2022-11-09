using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorscheMarket.DAL
{
    class ApplicationDbContext:DbContext
    {
        DbSet<Porsche> Porsches { get; set; }
    }
}
