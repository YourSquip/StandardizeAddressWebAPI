using AutoMapper;
using Dadata.Model;

namespace StandardizeAddressWebAPI
{
    //mapps Address to AddressDto
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<Address, AddressDto>();
        }
    }
}
