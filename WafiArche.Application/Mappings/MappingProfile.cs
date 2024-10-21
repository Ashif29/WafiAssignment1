using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafiArche.Application.Products.Dtos;
using WafiArche.Application.PublicHolidays.Dtos;
using WafiArche.Domain.Products;
using WafiArche.Domain.PublicHolidays;

namespace WafiArche.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();

            CreateMap<PublicHolidayDto, PublicHoliday>().ReverseMap();
            CreateMap<PublicHolidayCreateDto, PublicHoliday>().ReverseMap();
            CreateMap<PublicHolidayUpdateDto, PublicHoliday>().ReverseMap();
        }
    }
}
