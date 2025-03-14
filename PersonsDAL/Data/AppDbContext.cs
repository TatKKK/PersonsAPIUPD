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
            // Ensure the correct primary key
            modelBuilder.Entity<PersonRelationship>()
                .HasKey(pr => new { pr.PersonId, pr.RelatedPersonId });

            // Correctly configure Foreign Key Constraints for PersonRelationship
            modelBuilder.Entity<PersonRelationship>()
                .HasOne(pr => pr.Person)
                .WithMany(p => p.PersonRelationships)
                .HasForeignKey(pr => pr.PersonId)
                .HasConstraintName("FK_person_relationships_persons_person_id") // Ensure correct name
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PersonRelationship>()
                .HasOne(pr => pr.RelatedPerson)
                .WithMany(p => p.RelatedPersons)
                .HasForeignKey(pr => pr.RelatedPersonId)
                .HasConstraintName("FK_person_relationships_persons_related_person_id") // Ensure correct name
                .OnDelete(DeleteBehavior.NoAction);

            // Ensure the City relationship is also correctly configured
            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany()
                .HasForeignKey(p => p.CityId)
                .HasConstraintName("FK_persons_cities_city_id") // Ensure correct name
                .OnDelete(DeleteBehavior.NoAction); // Change from Cascade to Restrict

            // Ensure correct delete behavior for Persons in relationships
            //modelBuilder.Entity<Person>()
            //    .HasMany(p => p.PersonRelationships)
            //    .WithOne(pr => pr.Person)
            //    .HasForeignKey(pr => pr.PersonId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.RelatedPersons)
                .WithOne(pr => pr.RelatedPerson)
                .HasForeignKey(pr => pr.RelatedPersonId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
