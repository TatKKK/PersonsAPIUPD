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
    public class PersonRepository : AbstractRepository, IPersonRepository
    {
        private readonly DbSet<Person> dbSetPersons;
        private readonly DbSet<PhoneNumber> dbSetPhone;
        private readonly DbSet<PersonRelationship> dbSetRel;

        public PersonRepository(AppDbContext context)
            : base(context)
        {
            ArgumentNullException.ThrowIfNull(nameof(context));
            this.dbSetPersons = context.Set<Person>();
            this.dbSetPhone = context.Set<PhoneNumber>();
            this.dbSetRel = context.Set<PersonRelationship>();
        }

        public void AddPerson(Person person)
        {
            this.dbSetPersons.Add(person);
            context.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            //ArgumentNullException.ThrowIfNull(person);

            //var existingPerson = this.dbSetPersons.FirstOrDefault(p => p.Id == person.Id)
            //    ?? throw new KeyNotFoundException($"Entity with ID {person.Id} not found.");

            //this.dbSetPersons.Entry(existingPerson).CurrentValues.SetValues(person);
            //context.SaveChanges();

            context.Entry(person).State = EntityState.Detached;

            context.Persons.Attach(person);

            context.Entry(person).Property(p => p.Name).IsModified = true;
            context.Entry(person).Property(p => p.LastName).IsModified = true;
            context.Entry(person).Property(p => p.Gender).IsModified = true;
            context.Entry(person).Property(p => p.BirthDate).IsModified = true;
            context.Entry(person).Property(p => p.IdCard).IsModified = true;
            context.Entry(person).Property(p => p.CityId).IsModified = true;

            foreach (var phoneNumber in person.PhoneNumbers)
            {
                if (phoneNumber.Id > 0)
                {
                    context.PhoneNumbers.Attach(phoneNumber);
                    context.Entry(phoneNumber).Property(p => p.Number).IsModified = true;
                    context.Entry(phoneNumber).Property(p => p.Type).IsModified = true;
                }
            }

            context.SaveChanges();
        }

        public void DeletePerson(int personId)
        {
            var relationships = this.dbSetRel
                .Where(pr => pr.PersonId == personId || pr.RelatedPersonId == personId)
                .ToList();

            if (relationships.Any())
            {
                this.dbSetRel.RemoveRange(relationships);
                context.SaveChanges();
            }

            var person = this.dbSetPersons.FirstOrDefault(p => p.Id == personId);
            if (person != null)
            {
                this.dbSetPersons.Remove(person);
                context.SaveChanges();
            }
        }

        public Person? GetPersonInfoById(int id)
        {
            return this.dbSetPersons.FirstOrDefault(p => p.Id == id);
        }

        public void AddRelatedPerson(PersonRelationship personRelationship)
        {
            this.dbSetRel.Add(personRelationship);
            context.SaveChanges();
        }

        public void DeleteRelatedPerson(PersonRelationship personRelationship)
        {
            this.dbSetRel.Remove(personRelationship);
            context.SaveChanges();
        }

        public List<Person> GetAllRelatedPersons(Person person)
        {
            return [.. this.dbSetRel
                .Where(r => r.PersonId == person.Id)
                .Join(this.dbSetPersons, r => r.RelatedPersonId, p => p.Id,
                    (r, p) => new Person
                    {
                        Name = p.Name,
                        LastName = p.LastName
                    })];
        }

        public List<PhoneNumber> GetAllPhoneNumbers(Person person)
        {
            return [.. this.dbSetPhone
                .Where(p => p.PersonId == person.Id)
                .Select(p => new PhoneNumber
                {
                    PersonId = p.PersonId,
                    Number = p.Number,
                    Type = p.Type
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

            return this.dbSetPersons.Skip((pageNumber - 1) * rowCount).Take(rowCount);
        }

        // Reports
        public List<PersonsReport> GetRelationshipReport()
        {
            return (from p in this.dbSetPersons
                    join r in this.dbSetRel on p.Id equals r.PersonId
                    group r by new { p.IdCard, p.Name, p.LastName, r.Type } into g
                    select new PersonsReport
                    {
                        IdCard = g.Key.IdCard,
                        Name = g.Key.Name,
                        LastName = g.Key.LastName,
                        Type = (int)g.Key.Type,
                        Count = g.Count()
                    }).ToList();
        }

        // Get all persons with related data
        public List<PersonInfo> GetAll()
        {
            return [.. this.dbSetPersons
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

        public List<Person> QuickSearchPersons(int pageNumber, int rowCount, string? name, string lastname, string idCard)
        {
            IQueryable<Person> query = this.dbSetPersons;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(lastname))
            {
                query = query.Where(p => p.LastName.Contains(lastname));
            }
            if (!string.IsNullOrEmpty(idCard))
            {
                query = query.Where(p => EF.Functions.Like(p.IdCard, $"%{idCard}%"));
            }

            var persons = query.OrderBy(p => p.LastName)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount).ToList();

            return persons;
        }

        public List<Person> DetailedSearchPersons(
            int pageNumber,
            int rowCount,
            string? name,
            string? lastname,
            string? idCard,
            int? gender,
            DateTime? birthDate,
            int? cityId,
            string? imagePath)
        {
            IQueryable<Person> query = this.dbSetPersons;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(lastname))
            {
                query = query.Where(p => p.LastName.Contains(lastname));
            }
            if (!string.IsNullOrEmpty(idCard))
            {
                query = query.Where(p => EF.Functions.Like(p.IdCard, $"%{idCard}%"));
            }
            if (birthDate != null)
            {
                query = query.Where(p => p.BirthDate == birthDate.Value);
            }

            if (cityId != null && cityId != 0)
            {
                query = query.Where(p => p.CityId == cityId);
            }

            if (!string.IsNullOrEmpty(imagePath))
            {
                query = query.Where(p => EF.Functions.Like(p.ImagePath, $"%{imagePath}%"));
            }

            var persons = query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * rowCount)
                .Take(rowCount)
                .ToList();

            return persons;
        }

        public void UploadPhoto(Person person)
        {
            context.Entry(person).State = EntityState.Detached;
            context.Persons.Attach(person);
            context.Entry(person).Property(p => p.ImagePath).IsModified = true;
            context.SaveChanges();
        }
    }
}

