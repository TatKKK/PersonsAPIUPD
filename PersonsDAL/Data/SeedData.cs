using Microsoft.EntityFrameworkCore;
using PersonsDAL.Entities;
using PersonsDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var cities = new List<City>
                {
                    new City { CityCode = "32", CityName = "Tbilisi" },
                    new City { CityCode = "373", CityName = "Mtskheta" },
                    new City { CityCode = "222", CityName = "Batumi" },
                    new City { CityCode = "370", CityName = "Gori" }
                };

                context.Cities.AddRange(cities);
                context.SaveChanges(); 
            }

            var cityDictionary = context.Cities.ToDictionary(c => c.CityName, c => c.Id);

            if (!context.Persons.Any())
            {
                var persons = new List<Person>
                {
                    new Person
                    {
                        Name = "Tamar",
                        LastName = "Kaldani",
                        Gender = Gender.Female,
                        IdCard = "01010014128",
                        BirthDate = new DateTime(1984, 07, 19),
                        CityId = 1,
                        ImagePath = "images/tamar.jpg",
                        PhoneNumbers = new List<PhoneNumber>
                        {
                            new PhoneNumber { Type = PhoneType.Mobile, Number = "555-123-456", PersonId = 1 },
                            new PhoneNumber { Type = PhoneType.Home, Number = "032-456-789", PersonId = 1 }
                        }
                    },
                    new Person
                    {
                        Name = "Giorgi",
                        LastName = "Bakradze",
                        Gender = Gender.Male,
                        IdCard = "01010014127",
                        BirthDate = new DateTime(1990, 12, 5),
                        CityId = 2,
                        ImagePath = "images/giorgi.jpg",
                        PhoneNumbers = new List<PhoneNumber>
                        {
                            new PhoneNumber { Type = PhoneType.Mobile, Number = "599-222-333", PersonId = 2 },
                            new PhoneNumber { Type = PhoneType.Office, Number = "032-888-999", PersonId = 2 }
                        },
                        PersonRelationships = new List<PersonRelationship>
                        {
                            new PersonRelationship{Type = RelationshipType.Coworker, PersonId = 2, RelatedPersonId = 1}
                        }
                    },
                    new Person
                    {
                        Name = "Nino",
                        LastName = "Chikovani",
                        Gender = Gender.Female,
                        IdCard = "01010014126",
                        BirthDate = new DateTime(1992, 03, 10),
                        CityId = 3,
                        ImagePath = "images/nino.jpg",
                        PhoneNumbers = new List<PhoneNumber>
                        {
                            new PhoneNumber { Type = PhoneType.Mobile, Number = "577-444-555", PersonId = 3 },
                            new PhoneNumber { Type = PhoneType.Home, Number = "032-111-222", PersonId = 3 }
                        },
                        PersonRelationships = new List<PersonRelationship>
                        {
                            new PersonRelationship{Type = RelationshipType.Coworker, PersonId = 3, RelatedPersonId = 1},
                            new PersonRelationship{Type = RelationshipType.Family, PersonId = 3, RelatedPersonId = 2}
                        }
                            },
                     new Person
                    {
                        Name = "test",
                        LastName = "test",
                        Gender = Gender.Female,
                        IdCard = "01010014125",
                        BirthDate = new DateTime(1990, 03, 10),
                        CityId = 3,
                        ImagePath = "images/nino.jpg",
                        PhoneNumbers = new List<PhoneNumber>
                        {
                            new PhoneNumber { Type = PhoneType.Mobile, Number = "577-444-575", PersonId = 4 },
                            new PhoneNumber { Type = PhoneType.Home, Number = "032-111-822", PersonId = 4 }
                        },
                        PersonRelationships = new List<PersonRelationship>
                        {
                            new PersonRelationship{Type = RelationshipType.Coworker, PersonId = 4, RelatedPersonId = 2},
                            new PersonRelationship{Type = RelationshipType.Family, PersonId = 4, RelatedPersonId = 3}
                        }
                            }
                };

                context.Persons.AddRange(persons);
                context.SaveChanges();
            }


        }
    }
}
