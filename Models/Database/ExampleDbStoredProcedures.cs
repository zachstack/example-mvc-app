using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleMvcApp.Models.Database
{
    public partial class ExampleDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {

        }
    }
}
