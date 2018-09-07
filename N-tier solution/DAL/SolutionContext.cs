using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using N_tier_solution.Models;

namespace N_tier_solution.DAL
{
    public class SolutionContext:DbContext
    {
        public SolutionContext():base("SolutionContext")
        {

        }
        public DbSet<Formulae> Formulae { get; set; }
        public DbSet<Records> Records { get; set; }
    }
}