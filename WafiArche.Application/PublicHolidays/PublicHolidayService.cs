using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
using WafiArche.EntityFrameworkCore.Data;

namespace WafiArche.Application.PublicHolidays
{
    public class PublicHolidayService : IPublicHolidayService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<PublicHolidayService> _logger;
        public PublicHolidayService(
            AppDbContext db,
            IMapper mapper,
            ILogger<PublicHolidayService> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<PublicHolidayDto>> GetAllAsync(Expression<Func<PublicHoliday, bool>> filter = null)
        {
            _logger.LogInformation("Received request to fetch all public holidays");

            IQueryable<PublicHoliday> query = _db.PublicHolidays;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var publicHolidays = await query.ToListAsync();

            _logger.LogInformation("Fetched {PublicHolidayCount} public holidays", publicHolidays.Count());

            return _mapper.Map<List<PublicHolidayDto>>(publicHolidays);

        }

        public async Task<PublicHolidayDto> GetAsync(Expression<Func<PublicHoliday, bool>> filter = null, bool tracked = true)
        {
            _logger.LogInformation("Starting query for Public Holiday with tracked: {tracked}", tracked);

            IQueryable<PublicHoliday> query = _db.PublicHolidays;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                _logger.LogInformation("Applying filter to query.");
                query = query.Where(filter);
            }
            var publicHoliday = await query.FirstOrDefaultAsync();

            return _mapper.Map<PublicHolidayDto>(publicHoliday);
        }

        public async Task<PublicHolidayDto> CreateAsync(PublicHolidayCreateDto publicHolidayCreateDto)
        {
            var publicHoliday = _mapper.Map<PublicHoliday>(publicHolidayCreateDto);
            await _db.PublicHolidays.AddAsync(publicHoliday);
            await _db.SaveChangesAsync();
            return _mapper.Map<PublicHolidayDto>(publicHoliday);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            _logger.LogInformation("Attempting to remove public holiday with ID: {PublicHolidayId}", id);
            var publicHoliday = _db.PublicHolidays.FirstOrDefault(x => x.Id == id);

            if (publicHoliday == null)
            {
                _logger.LogWarning("Public holiday with ID: {PublicHolidayId} not found.", id);
                return false;
            }

            _db.PublicHolidays.Remove(publicHoliday);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Public holiday with ID: {PublicHolidayId} removed successfully.", id);
            return true;
        }

        public async Task<PublicHolidayDto> UpdateAsync(PublicHolidayUpdateDto publicHolidayUpdateDto)
        {
            _logger.LogInformation("Attempting to update public holiday with ID: {PublicHolidayId}", publicHolidayUpdateDto.Id);

            var publicHoliday = await _db.PublicHolidays.FirstOrDefaultAsync(x => x.Id == publicHolidayUpdateDto.Id);

            if (publicHoliday == null)
            {
                _logger.LogWarning("Public holiday with ID: {PublicHolidayId} not found.", publicHolidayUpdateDto.Id);
                return null;
            }

            _mapper.Map(publicHolidayUpdateDto, publicHoliday);

            _db.PublicHolidays.Update(publicHoliday);

            await _db.SaveChangesAsync();

            _logger.LogInformation("Public holiday with ID: {PublicHolidayId} updated successfully.", publicHolidayUpdateDto.Id);

            return _mapper.Map<PublicHolidayDto>(publicHoliday);
        }

    }
}
