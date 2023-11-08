using AutoMapper;
using Dadata.Model;

namespace StandardizeAddressWebAPI
{
    //mapps Address from 
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<Address, AddressDto>();
        }
    }
}
