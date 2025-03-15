using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PersonsDAL.Data;
using PersonsDAL.Entities;
using PersonsDAL.Interfaces;
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

        public List<Person> GetAll()
        {
            throw new NotImplementedException();
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(Person person)
        {
            throw new NotImplementedException();
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
    }
}
