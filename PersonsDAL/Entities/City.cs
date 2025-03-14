using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Entities
{
    [Table("cities")]
    public class City : BaseEntity
    {
        [Column("city_code")]
        [MaxLength(50)]
        public string CityCode { get; set; } = string.Empty;

        [Column("city_name")]
        [MaxLength(50)]
        public string CityName { get; set; } = string.Empty;

    }
}
