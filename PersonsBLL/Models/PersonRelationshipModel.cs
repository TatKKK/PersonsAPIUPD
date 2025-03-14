using PersonsBLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Models
{
    public class PersonRelationshipModel : AbstractModel
    {
        public PersonRelationshipModel(int id, RelationshipType type, int personId, int relatedPersonId)
           : base(id)
        {
            Type = type;
            PersonId = personId;
            RelatedPersonId = relatedPersonId;
        }

        public RelationshipType Type { get; set; }
        public int PersonId { get; set; }
        public int RelatedPersonId { get; set; }
    }
}
