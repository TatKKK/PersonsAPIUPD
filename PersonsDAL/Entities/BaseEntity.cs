using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity(int id)
        {
            this.Id = id;
        }

        protected BaseEntity()
        {
            this.Id = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
