using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonsDAL.Data;
using PersonsDAL.Entities;
using PersonsDAL.Interfaces;
using PersonsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Repository
{
    public class PersonRepository(AppDbContext context) : IPersonRepository //Repository<Person>, IPersonRepository
    {
        public void AddPerson(Person person)
        {
            context.Persons.Add(person);
            context.SaveChanges();
        }

        public List<Person> GetAllRelatedPersons(Person person)
        {
            var relatedPersons = new List<Person>();
            relatedPersons = [.. context.PersonRelationships
                .Where(r => r.PersonId == person.Id)
                .Join(context.Persons, r => r.RelatedPersonId, p => p.Id,
                (r, p) => new Person
                {
                    Name = p.Name,
                    LastName = p.LastName
                })];
            return relatedPersons;
        }
        public List<PhoneNumber> GetAllPhoneNumbers(Person person)
        {
            var phoneNumbers = new List<PhoneNumber>();
            phoneNumbers = [.. context.PhoneNumbers
                .Select(p => new PhoneNumber
                {
                    PersonId = p.PersonId,
                    Number = p.Number,
                    Type = p.Type
                })
                .Where(p => p.PersonId == person.Id)];
            return phoneNumbers;
        }
        public void DeletePerson(int personId)
        {
            var relationships = context.PersonRelationships
                .Where(pr => pr.PersonId == personId || pr.RelatedPersonId == personId)
                .ToList();

            if (relationships.Count != 0)
            {
                context.PersonRelationships.RemoveRange(relationships);
                context.SaveChanges();
            }

            var person = context.Persons.FirstOrDefault(p => p.Id == personId);
            if (person != null)
            {
                context.Persons.Remove(person);
                context.SaveChanges();
            }
        }

        public List<PersonInfo> GetAll()
        {
            return [.. context.Persons
                .Include(p => p.City) 
                .Include(p => p.PhoneNumbers)
                .Include(p => p.PersonRelationships)
                .Select(p => new PersonInfo
                {
                    Name = p.Name,
                    LastName = p.LastName,
                    IdCard = p.IdCard,
                    BirthDate = p.BirthDate,
                    CityName = p.City.CityName,
                    ImagePath = p.ImagePath,
                    PhoneNumbers = p.PhoneNumbers.Select(ph => new PhoneNumber
                    {
                        Id = ph.Id,
                        Type = ph.Type,
                        Number = ph.Number,
                        PersonId = ph.PersonId
                    }).ToList(),
                    PersonRelationships = p.PersonRelationships.Select(pr => new PersonRelationship
                    {
                        Id = pr.Id,
                        Type = pr.Type,
                        PersonId = pr.PersonId,
                        RelatedPersonId = pr.RelatedPersonId
                    }).ToList()
                })];
        }

        public IEnumerable<Person> GetPersonsPaginated(int pageNumber, int rowCount)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0.", nameof(pageNumber));
            }

            if (rowCount <= 0)
            {
                throw new ArgumentException("Row count must be greater than 0.", nameof(rowCount));
            }

            return [.. context.Persons.Skip((pageNumber - 1) * rowCount).Take(rowCount)];


        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(Person person)
        {
            ArgumentNullException.ThrowIfNull(person);

            var existingPerson = context.Persons.FirstOrDefault(p => p.Id == person.Id) ?? throw new KeyNotFoundException($"Entity with ID {person.Id} not found.");
            context.Entry(existingPerson).CurrentValues.SetValues(person);
            context.SaveChanges();

        }

        public Person? GetPersonInfoById(int id)
        {
            var person = context.Persons.FirstOrDefault(p => p.Id == id);
            return person;
        }

        public void AddRelatedPerson(PersonRelationship personRelationship)
        {
            context.PersonRelationships.Add(personRelationship);
            context.SaveChanges();
        }

        public void DeleteRelatedPerson(PersonRelationship personRelationship)
        {
            context.PersonRelationships.Remove(personRelationship);
            context.SaveChanges();
        }

        public List<PersonsReport> GetRelationshipReport()
        {
            var report = (from p in context.Persons
                          join r in context.PersonRelationships
                          on p.Id equals r.PersonId
                          group r by new { p.IdCard, p.Name, p.LastName, r.Type } into g
                          select new PersonsReport
                          {
                              IdCard = g.Key.IdCard,
                              Name = g.Key.Name,
                              LastName = g.Key.LastName,
                              Type = (int)g.Key.Type,
                              Count = g.Count()
                          }).ToList();
            return report;
        }
    }
}
