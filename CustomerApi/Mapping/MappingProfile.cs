using AutoMapper;
using CustomerApi.Core.Models;
using CustomerResources;

namespace CustomerApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerResource>().ReverseMap();
        }
    }
}
