using Products.Application.Common.Mappers.Bases;
using Core.Entities.Bases;
using Core.Records.Bases;

namespace Products.Application.Common.Mappers
{
    public class AppMapper<TEntity, TResponse> : AppMapperBase<TEntity, TResponse> where TEntity : Entity, new() where TResponse : IRecord, new()
    {
    }
}
