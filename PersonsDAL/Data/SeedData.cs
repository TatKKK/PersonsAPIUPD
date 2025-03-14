using Microsoft.EntityFrameworkCore;
using PersonsDAL.Entities;
using PersonsDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Data
{
    public static class SeedData
    {
        public static void SeedAppData(AppDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Cities.Any())
            {
                var cities = new[]
                {
                    new City { CityCode = "32", CityName = "Tbilisi" },
                    new City { CityCode = "373", CityName = "Mtskheta" },
                    new City { CityCode = "222", CityName = "Batumi" },
                    new City { CityCode = "370", CityName = "Gori" }
                };

                context.Cities.AddRange(cities);
                context.SaveChanges(); 
            }

            if (!context.Persons.Any())
            {
                var persons = new[]
                {
                    new Person
                    {
                        Name = "Tamar",
                        LastName = "Kaldani",
                        Gender = Gender.Female,
                        IdCard = "12345678901",
                        BirthDate = new DateTime(1984, 07, 19),
                        CityId = context.Cities.First(c => c.CityName == "Tbilisi").Id,
                        ImagePath = "images/tamar.jpg"
                    },
                    new Person
                    {
                        Name = "Giorgi",
                        LastName = "Bakradze",
                        Gender = Gender.Male,
                        IdCard = "98765432109",
                        BirthDate = new DateTime(1990, 12, 5),
                        CityId = context.Cities.First(c => c.CityName == "Mtskheta").Id,
                        ImagePath = "images/tamar.jpg"
                    },
                    new Person
                    {
                        Name = "Nino",
                        LastName = "Chikovani",
                        Gender = Gender.Female,
                        IdCard = "11223344556",
                        BirthDate = new DateTime(1992, 03, 10),
                        CityId = context.Cities.First(c => c.CityName == "Batumi").Id,
                        ImagePath = "images/tamar.jpg"
                    }
                };

                context.Persons.AddRange(persons);
                context.SaveChanges();
            }

            if (!context.PhoneNumbers.Any())
            {
                var persons = context.Persons.ToList();

                var phoneNumbers = new[]
                {
                    new PhoneNumber { Type = PhoneType.Mobile, Number = "555-123-456", PersonId = persons[0].Id },
                    new PhoneNumber { Type = PhoneType.Home, Number = "032-456-789", PersonId = persons[0].Id },

                    new PhoneNumber { Type = PhoneType.Mobile, Number = "599-222-333", PersonId = persons[1].Id },
                    new PhoneNumber { Type = PhoneType.Office, Number = "032-888-999", PersonId = persons[1].Id },

                    new PhoneNumber { Type = PhoneType.Mobile, Number = "577-444-555", PersonId = persons[2].Id },
                    new PhoneNumber { Type = PhoneType.Home, Number = "032-111-222", PersonId = persons[2].Id }
                };

                context.PhoneNumbers.AddRange(phoneNumbers);
                context.SaveChanges();
            }

            if (!context.PersonRelationships.Any())
            {
                var persons = context.Persons.ToList();

                var relationships = new[]
                {
                    new PersonRelationship { Type = RelationshipType.Family, PersonId = persons[0].Id, RelatedPersonId = persons[1].Id },
                    new PersonRelationship { Type = RelationshipType.Friend, PersonId = persons[0].Id, RelatedPersonId = persons[2].Id },
                    new PersonRelationship { Type = RelationshipType.Coworker, PersonId = persons[1].Id, RelatedPersonId = persons[2].Id }
                };

                context.PersonRelationships.AddRange(relationships);
                context.SaveChanges();
            }
        }
    }
}
