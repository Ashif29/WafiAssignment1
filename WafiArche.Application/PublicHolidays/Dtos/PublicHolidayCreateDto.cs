using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WafiArche.Application.PublicHolidays.Dtos
{
    public class PublicHolidayCreateDto
    {
        [Required(ErrorMessage = "Holiday name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Holiday date is required.")]
        [DataType(DataType.Date)]
        [SwaggerSchema(Format = "date", Description = "Date in yyyy-MM-dd format")]
        public DateOnly Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public PublicHolidayCreateDto()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
