using PersonsDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PersonsDAL.Entities
{
    [Table("person_relationships")]
    public class PersonRelationship : BaseEntity
    {
        [Required]
        public RelationshipType Type { get; set; } // Friend, Family, Coworker

        [Column("person_id")]
        public int PersonId { get; set; }

        public Person Person { get; set; }

        [Column("related_person_id")]
        public int RelatedPersonId { get; set; }

        public Person RelatedPerson { get; set; }
    }
}
