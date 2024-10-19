using AutoMapper;
using MediatR;
using Core.Entities.Bases;
using Core.Records.Bases;
using Core.Responses.Bases;

namespace Products.Application.Common.Mappers.Bases
{
	public abstract class AppMapperBase<TEntity, TResponse> where TEntity : Entity, new() where TResponse : IRecord, new()
	{
		public MapperConfiguration Configuration { get; set; }

		protected AppMapperBase(params Profile[] profiles)
		{
			AddQueryProfiles(profiles);
		}

		public virtual void AddQueryProfiles(params Profile[] profiles)
		{
			Configuration = new MapperConfiguration(c =>
			{
				c.CreateMap(typeof(TEntity), typeof(TResponse));
				c.AddProfiles(profiles);
			});
		}

		public virtual TEntity Map(IRequest<Response> request, params Profile[] profiles)
		{
			Configuration = new MapperConfiguration(c =>
			{
				c.CreateMap(request.GetType(), typeof(TEntity));
				c.AddProfiles(profiles);
			});
			Mapper mapper = new Mapper(Configuration);
			return mapper.Map<TEntity>(request);
		}

		public virtual TResponse Map(TEntity entity, params Profile[] profiles)
		{
			Configuration = new MapperConfiguration(c =>
			{
				c.CreateMap(entity.GetType(), typeof(TResponse));
				c.AddProfiles(profiles);
			});
			Mapper mapper = new Mapper(Configuration);
			return mapper.Map<TResponse>(entity);
		}
	}
}
