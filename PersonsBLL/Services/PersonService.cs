using Microsoft.VisualBasic;
using PersonsBLL.Interfaces;
using PersonsBLL.Models;
using PersonsDAL.Entities;
using PersonsDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonsBLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        
       
        public object  GetAll()
        {
            return personRepository.GetAll();
        }

        public void DeletePerson (int id)
        {
            personRepository.DeletePerson(id);
        }

        //public IEnumerable<AbstractModel> GetAll()
        //{
        //    return personRepository.GetAll().Select(p => new PersonModel(
        //        p.Id, p.Name, p.LastName, (Models.Enums.Gender)p.Gender, p.IdCard, p.BirthDate, p.CityId,
        //         p.PhoneNumbers.Select(ph => new PhoneNumberModel(ph.Id, (Models.Enums.PhoneType)ph.Type, ph.Number, ph.PersonId)).ToList(),
        //    p.RelatedPersons.Select(pr => new PersonRelationshipModel(pr.Id, (Models.Enums.RelationshipType)pr.Type, pr.RelatedPersonId, pr.RelatedPersonId)).ToList())).ToList();
        //}

        //public AbstractModel GetById(int id)
        //{
        //    var person = personRepository.GetById(id);
        //    if (person == null) return null;
        //    return new PersonModel(person.Id, person.Name, person.LastName, (Models.Enums.Gender)person.Gender, person.IdCard, person.BirthDate,
        //                           person.CityId, new List<PhoneNumberModel>(), new List<PersonRelationshipModel>());
        //}

        //public void Add(AbstractModel model)
        //{
        //    if (model is PersonModel personModel)
        //    {
        //        var person = new Person
        //        {
        //            Id = personModel.Id,
        //            Name = personModel.Name,
        //            LastName = personModel.LastName,
        //            Gender = (PersonsDAL.Entities.Enums.Gender)personModel.Gender,
        //            IdCard = personModel.IdCard,
        //            BirthDate = personModel.BirthDate,
        //            CityId = personModel.CityId,
        //            //ImagePath = personModel.ImagePath
        //        };
        //        personRepository.Add(person);
        //    }
        //}

        //public void Update(AbstractModel model)
        //{
        //    if (model is PersonModel personModel)
        //    {
        //        var person = new Person
        //        {
        //            Id = personModel.Id,
        //            Name = personModel.Name,
        //            LastName = personModel.LastName,
        //            Gender = (PersonsDAL.Entities.Enums.Gender)personModel.Gender,
        //            IdCard = personModel.IdCard,
        //            BirthDate = personModel.BirthDate,
        //            CityId = personModel.CityId,
        //            ImagePath = personModel.ImagePath
        //        };
        //        personRepository.Update(person);
        //    }
        //}

        //public void DeletePerson(int modelId)
        //{
        //    personRepository.DeletePerson(modelId);
        //}

        //public void Delete(int modelId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
