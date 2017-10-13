using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrudExamples.WebApi.Models.Database
{
    public class VesselsContext: DbContext
    {
        public VesselsContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {

        }

        public DbSet<VesselEntity> Vessels { get; set; }
    }
}