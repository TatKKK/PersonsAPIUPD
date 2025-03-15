using PersonsDAL.Entities;
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
        public List<Person> GetAll();
    }
}
