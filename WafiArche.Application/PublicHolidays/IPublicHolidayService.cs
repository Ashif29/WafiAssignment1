using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WafiArche.Application.Products.Dtos;
using WafiArche.Application.PublicHolidays.Dtos;
using WafiArche.Domain.Products;
using WafiArche.Domain.PublicHolidays;

namespace WafiArche.Application.PublicHolidays
{
    public interface IPublicHolidayService
    {
        Task<List<PublicHolidayDto>> GetAllAsync(Expression<Func<PublicHoliday, bool>> filter = null);
        Task<PublicHolidayDto> GetAsync(Expression<Func<PublicHoliday, bool>> filter = null, bool tracked = true);

        Task<PublicHolidayDto> CreateAsync(PublicHolidayCreateDto entity);
        Task<PublicHolidayDto> UpdateAsync(PublicHolidayUpdateDto entity);
        Task<bool> RemoveAsync(int id);
    }
}
