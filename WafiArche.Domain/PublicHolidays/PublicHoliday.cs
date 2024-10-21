using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WafiArche.Domain.PublicHolidays
{
    public class PublicHoliday
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Holiday name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Holiday date is required.")]
        [DataType(DataType.Date)]

        public DateOnly Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
    }
}

