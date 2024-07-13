using AutoMapper;
using mlsaDemo.DTO;
using mlsaDemo.Models;

namespace mlsaDemo.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ItemsModel, ItemDto>();

            CreateMap<CreateItemDto, ItemsModel>().ReverseMap();
            CreateMap<UpdateItemDto, ItemsModel>().ReverseMap();

        }
    }
}

