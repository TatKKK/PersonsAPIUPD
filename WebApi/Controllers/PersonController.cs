using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonsBLL.Interfaces;
using PersonsBLL.Models;
using PersonsDAL.Entities;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using PersonsBLL.Dtos;
using PersonsBLL.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController(IPersonService personService) : ControllerBase
    {
        private readonly IPersonService personService = personService;

        [HttpPost]
        public IActionResult AddPerson(AddPersonDto person)
        {
            personService.AddPerson(person);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePerson(UpdatePersonDto personDto)
        {
            personService.UpdatePerson(personDto);
            return Ok();
        }

        [HttpGet("PersonInfo")]
        public IActionResult GerPersonById(int id)
        {
            var person = personService.GetPersonInfoById(id);
            return Ok(person);
        }

        [HttpPost]
        public IActionResult AddRelatedPerson(AddRelatedPersonDto person)
        {
            personService.AddRelatedPerson(person);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteRelatedPerson(DeleteRelatedPersonDto person)
        {
            personService.DeleteRelatedPerson(person);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var persons =  personService.GetAll();

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    WriteIndented = true
            //};

            //string json = JsonSerializer.Serialize(persons, options);

            return Ok(persons);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            personService.DeletePerson(id);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetRelationshipReport()
        {
            var x = personService.GetRelationshipReport();
            return Ok(x);
        }

    }
}
