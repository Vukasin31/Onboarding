using AutoMapper;
using ContosoPizza.Models;

namespace ContosoPizza.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Pizza, MetaDataObject>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.PizzaName))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.IsGlutenFree))
                .ForMember(dest => dest.Date, act => act.Ignore());

            CreateMap<Beverage, MetaDataObject>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.BeverageId))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.BeverageName))
                .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Containance))
                .ForMember(dest => dest.Date, act => act.Ignore());
        }
    }
}
