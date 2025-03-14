using PersonsDAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Repository
{
    public abstract class AbstractRepository
    {
        protected readonly AppDbContext context;

        protected AbstractRepository(AppDbContext context)
        {
            this.context = context;
        }
    }
}
