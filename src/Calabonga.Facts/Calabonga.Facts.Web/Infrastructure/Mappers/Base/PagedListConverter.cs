using System.Collections.Generic;
using AutoMapper;
using Calabonga.UnitOfWork;

namespace Calabonga.Facts.Web.Infrastructure.Mappers.Base
{
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
    {
        /// <summary>Performs conversion from source to destination type</summary>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        /// <param name="context">Resolution context</param>
        /// <returns>Destination object</returns>
        public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
        {
            return source == null
                ? PagedList.Empty<TDestination>()
                : PagedList.From(source, x => context.Mapper.Map<IEnumerable<TDestination>>(x));
        }
    }
}