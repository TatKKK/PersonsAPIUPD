using PersonsDAL.Entities;
using PersonsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Interfaces
{
    public interface IPersonRepository 
    {
        public void DeletePerson(int id);

        void AddRelatedPerson(PersonRelationship personRelationship);
        void DeleteRelatedPerson(PersonRelationship personRelationship);

        public void AddPerson(Person person);
        Person? GetPersonInfoById(int id);
        List<PhoneNumber> GetAllPhoneNumbers(Person person);

        List<Person> GetAllRelatedPersons(Person person);

        public Person GetPerson(int id);
        public void UpdatePerson(Person person);
        public List<PersonInfo> GetAll();

        List<PersonsReport> GetRelationshipReport();

        IEnumerable<Person> GetPersonsPaginated(int pageNumber, int rowCount);
    }
}
