using Microsoft.EntityFrameworkCore;
using PersonsDAL.Data;
using PersonsDAL.Entities;
using PersonsDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Repository
{
    public class CityRepository : ICityRepository
    {
        //public CityRepository(AppDbContext context) : base(context)
        //{
        //}

        //public IEnumerable<City> GetAllCitiesOrderedByName()
        //{
        //    return dbSet.OrderBy(c => c.CityName).ToList();
        //}
    }
}
