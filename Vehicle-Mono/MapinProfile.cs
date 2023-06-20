using AutoMapper;
using Vehicle_Mono.ViewModel;
using Vehicle_Mono_BL.Paged;
using Vehicle_Mono_BL.Selector;
using Vehicle_Mono_DAL.Entity;
using Vehicle_Mono_DAL.Interface;
using Vehicle_Mono_DAL.Models;
using Vehicle_Mono_DAL.MODELS;

namespace Vehicle_Mono
{
    public class MapinProfile : Profile
    {
        public MapinProfile()
        {
            //CreateMap<IMake, MakeViewModel>().ReverseMap();
            CreateMap<MakeViewModel, Makes>().ReverseMap();
            CreateMap<ModelViewModel, Model>().ReverseMap();
            //CreateMap<Makes, MakeViewModel>().ReverseMap();
            CreateMap<MakeEntity, Makes>().ReverseMap();
            CreateMap<ModelEntity, Model>().ReverseMap();


            CreateMap<MakeViewModel, IMake>().As<Makes>();
            CreateMap<ModelViewModel, IModelV>().As<Model>();

            CreateMap<IMake, Makes>().ReverseMap();
            CreateMap<IModelV, Model>().ReverseMap();

            CreateMap<PaginatedList<IMake>, PaginatedList<MakeViewModel>>().ConvertUsing<PagingConverter<IMake, MakeViewModel>>();
            CreateMap<PaginatedList<IModelV>, PaginatedList<MakeViewModel>>().ConvertUsing<PagingConverter<IModelV, MakeViewModel>>();


            CreateMap<PaginatedList<Sorting>, PaginatedList<MakeViewModel>>().ConvertUsing<PagingConverter<Sorting, MakeViewModel>>();
            CreateMap<PaginatedList<Sorting>, PaginatedList<ModelViewModel>>().ConvertUsing<PagingConverter<Sorting, ModelViewModel>>();

            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PagingConverter<,>));

            // https://www.youtube.com/watch?v=zGdmsuA0qdA


        }


    }


}
