using AutoMapper;
using CardStorage.Data.Entities;

namespace CardStorage.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Requests.ClientControllerRequests.ClientPostRequest, Client>();
            CreateMap<Requests.CardsControllerRequests.CardsPostRequest, ClientCard>();
        }
    }
}
