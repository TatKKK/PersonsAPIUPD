using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Models
{
    public abstract class AbstractModel
    {
        protected AbstractModel(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }

}
