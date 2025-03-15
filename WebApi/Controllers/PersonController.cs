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
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }

        [HttpPost]
        public IActionResult AddPerson(AddPersonDto person)
        {
            personService.AddPerson(person);
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

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var persons = _personService.GetAll();
        //    return Ok(persons);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var person = _personService.GetById(id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(person);
        //}

        //[HttpPost]
        //public IActionResult Create(PersonModel model)
        //{
        //    _personService.Add(model);
        //    return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, PersonModel model)
        //{
        //    if (id != model.Id)
        //    {
        //        return BadRequest();
        //    }
        //    _personService.Update(model);
        //    return NoContent();
        //}


    }
}
