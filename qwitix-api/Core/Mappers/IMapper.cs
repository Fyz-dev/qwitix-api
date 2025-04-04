namespace qwitix_api.Core.Mappers
{
    public interface IMapper<TDto, TEntity>
    {
        TEntity ToEntity(TDto dto);

        TDto ToDto(TEntity entity);

        List<TDto> ToDtoList(IEnumerable<TEntity> entities);

        List<TEntity> ToEntityList(IEnumerable<TDto> dtos);
    }
}
