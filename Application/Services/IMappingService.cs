namespace Application.Services;

public interface IMappingService<TDto, TEntity>
{
    public TDto ToDto(TEntity entity);
    public List<TDto> ToDto(List<TEntity> entity);
    public TEntity ToEntity(TDto dto);
}

