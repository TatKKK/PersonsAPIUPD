using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Models
{
    public class CityModel : AbstractModel
    {
        public CityModel(int id, string cityCode, string cityName) : base(id)
        {
            CityCode = cityCode;
            CityName = cityName;
        }

        public string CityCode { get; set; }
        public string CityName { get; set; }
    }
}
