using Application.Dtos;
using Application.Services;

namespace Infrastructure.Mappings;
using AutoMapper;

public class MappingAdapter<TDto, TEntity> : IMappingService<TDto, TEntity>
{
    public Mapper _mapper = new Mapper(new MapperConfiguration(
         configuration => {
             configuration.CreateMap<TDto, TEntity>().ReverseMap();
         }
        ));

    public TDto ToDto(TEntity entity)
    {
        return _mapper.Map<TDto>(entity);
    }
    
    public TEntity ToEntity(TDto dto)
    {
        return _mapper.Map<TEntity>(dto);
    }

    public List<TDto> ToDto(List<TEntity> entity)
    {
        return _mapper.Map<List<TDto>>(entity);
    }
}
