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
    public class PersonRepository : IPersonRepository //Repository<Person>, IPersonRepository
    {
        AppDbContext context;

        public PersonRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void AddPerson(Person person)
        {
            context.Persons.Add(person);
            context.SaveChanges();
        }

        public List<Person> GetAllRelatedPersons(Person person)
        {
            var relatedPersons = new List<Person>();
            relatedPersons = context.PersonRelationships
                .Where(r => r.PersonId == person.Id)
                .Join(context.Persons, r => r.RelatedPersonId, p => p.Id,
                (r, p) => new Person
                {
                    Name = p.Name,
                    LastName = p.LastName
                }).ToList();
            return relatedPersons;
        }
        public List<PhoneNumber> GetAllPhoneNumbers(Person person)
        {
            var phoneNumbers = new List<PhoneNumber>();
            phoneNumbers = context.PhoneNumbers
                .Select(p => new PhoneNumber
                {
                    PersonId = p.PersonId,
                    Number = p.Number,
                    Type = p.Type
                })
                .Where(p => p.PersonId == person.Id).ToList();
            return phoneNumbers;
        }
        public void DeletePerson(int personId)
        {
            var relationships = context.PersonRelationships
                .Where(pr => pr.PersonId == personId || pr.RelatedPersonId == personId)
                .ToList();

            if (relationships.Any())
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
            return context.Persons
                .Include(p => p.City) // To get CityName
                .Include(p => p.PhoneNumbers)
                .Include(p => p.PersonRelationships)
                .Select(p => new PersonInfo
                {
                    Name = p.Name,
                    LastName = p.LastName,
                    IdCard = p.IdCard,
                    BirthDate = p.BirthDate,
                    CityName = p.City.CityName, // Mapping City object to CityName
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
                })
                .ToList();
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(Person person)
        {
            context.Entry(person).State = EntityState.Detached;

            context.Persons.Attach(person);

            context.Entry(person).Property(p => p.Name).IsModified = true;
            context.Entry(person).Property(p=>p.LastName).IsModified = true;
            context.Entry(person).Property(p=>p.BirthDate).IsModified = true;   
            context.Entry(person).Property(p=>p.IdCard).IsModified = true;
            context.Entry(person).Property(p=>p.CityId).IsModified = true;

            foreach(var phoneNumber in person.PhoneNumbers)
            {
                if(phoneNumber.Id > 0)
                {
                    context.PhoneNumbers.Attach(phoneNumber);
                    context.Entry(phoneNumber).Property(p=>p.Number).IsModified = true; 
                    context.Entry(phoneNumber).Property(p=>p.Type).IsModified = true;
                }
            }
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
