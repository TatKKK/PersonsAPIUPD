using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Dtos
{
    public class DeleteRelatedPersonDto
    {
        public int PersonId { get; set; }

        public int RelatedPersonId { get; set; }
    }
}
