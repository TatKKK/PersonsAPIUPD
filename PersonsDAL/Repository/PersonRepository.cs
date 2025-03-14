using Microsoft.EntityFrameworkCore;
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

        //public IEnumerable<Person> GetAllPersonsOrderedByCity()
        //{
        //    return dbSet.OrderBy(c => c.CityId).ToList();
        //}

        //public override void DeleteById(int personId)
        //{
        //    var relationships = context.PersonRelationships
        //        .Where(pr => pr.PersonId == personId || pr.RelatedPersonId == personId)
        //        .ToList();

        //    if (relationships.Any())
        //    {
        //        context.PersonRelationships.RemoveRange(relationships);
        //        context.SaveChanges();
        //    }

        //    var person = dbSet.Find(personId);
        //    if (person != null)
        //    {
        //        dbSet.Remove(person);
        //        context.SaveChanges();
        //    }
        //}

        public object GetAll()
        {
            var persons = context.Persons.Include(p => p.PhoneNumbers)
                  .Select(p => new
                  {
                      PersonId = p.Id,
                      PersonName = p.Name,
                      PhoneNumbers = p.PhoneNumbers.Select(ph => new
                      {
                          PhoneId = ph.Id,
                          PhoneNumber = ph.Number
                      }).ToList()
                  })
                .ToList();

            //var persons = context.Persons
            //    .Include(p => p.PhoneNumbers)
            //    .Include(p => p.RelatedPersons)
            //    .ThenInclude(r => r.Person)
            //    .Select(p => new
            //    {
            //        PersonId = p.Id,
            //        PersonName = p.Name,
            //        PersonLastName = p.LastName,
            //        PhoneNumbers = p.PhoneNumbers.Select(pn => new
            //        {
            //            PhoneNumber = pn.Number,
            //            PhoneType = pn.Type
            //        }).ToList(),
            //        Relationships = p.RelatedPersons.Select(r => new
            //        {
            //            RelatedPersonName = r.RelatedPerson.Name,
            //            RelatedPersonLastName = r.RelatedPerson.LastName,
            //            RelationshipType = r.Type
            //        }).ToList()
            //    })
            //    .ToList();


            //var persons = context.Persons
            //       .Join(
            //           context.PhoneNumbers,
            //           p => p.Id,
            //           pn => pn.PersonId,
            //           (p, pn) => new { Person = p, PhoneNumber = pn }
            //       )
            //       .Join(
            //           context.PersonRelationships,
            //           p_pn => p_pn.Person.Id,
            //           r => r.PersonId,
            //           (p_pn, r) => new { p_pn.Person, p_pn.PhoneNumber, Relationship = r }
            //       )
            //       .Join(
            //           context.Persons,
            //           p_pn_r => p_pn_r.Relationship.RelatedPersonId,
            //           p2 => p2.Id,
            //           (p_pn_r, p2) => new
            //           {
            //               PersonId = p_pn_r.Person.Id,
            //               PersonName = p_pn_r.Person.Name,
            //               PersonLastName = p_pn_r.Person.LastName,

            //              /* PhoneNumber = p_pn_r.PhoneNumber.Number,
            //               PhoneType = p_pn_r.PhoneNumber.Type,
            //               RelatedPersonName = p2.Name,
            //               RelatedPersonLastName = p2.LastName,
            //               RelationshipType = p_pn_r.Relationship.Type*/
            //           }
            //       )
            //       .ToList();


            return persons;
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
    }
}
