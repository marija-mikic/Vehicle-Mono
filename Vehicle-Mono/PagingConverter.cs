using AutoMapper;

namespace Vehicle_Mono_BL.Paged
{
    public class PagingConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>> where TSource : class where TDestination : class
    {
        public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
        {
            var collection = context.Mapper.Map<List<TSource>, List<TDestination>>(source.ToList());
            return new PaginatedList<TDestination>(collection, source.PageIndex, source.TotalPages);
        }
    }
}
