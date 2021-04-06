using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TourCompany.Models
{
    public class Context:DbContext
    {
        public Context() : base("name=TourCompany")
        { }
        public DbSet<Type> Types { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }
        public DbSet<PayMethod> PayMethods { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
    }
}