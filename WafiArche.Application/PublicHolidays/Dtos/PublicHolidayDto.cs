using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WafiArche.Application.PublicHolidays.Dtos
{
    public class PublicHolidayDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateOnly Date { get; set; }
    }
}
