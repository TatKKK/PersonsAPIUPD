using Microsoft.EntityFrameworkCore;
using PersonsDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<PersonRelationship> PersonRelationships { get; set; }
        public DbSet<City> Cities { get; set; }  
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonRelationship>()
                 .HasKey(pr => new { pr.PersonId, pr.RelatedPersonId });

            modelBuilder.Entity<PersonRelationship>()
           .HasOne(rp => rp.Person)
           .WithMany(p => p.PersonRelationships)
           .HasForeignKey(rp => rp.PersonId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonRelationship>()
                .HasOne(rp => rp.RelatedPerson)
                .WithMany()
                .HasForeignKey(rp => rp.RelatedPersonId)
                .OnDelete(DeleteBehavior.Restrict);

       }
    }
}
