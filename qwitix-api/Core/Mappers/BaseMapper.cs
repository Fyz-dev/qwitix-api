namespace qwitix_api.Core.Mappers
{
    public abstract class BaseMapper<TDto, TEntity> : IMapper<TDto, TEntity>
    {
        public abstract TEntity ToEntity(TDto dto);

        public abstract TDto ToDto(TEntity entity);

        public virtual List<TDto> ToDtoList(IEnumerable<TEntity> entities) =>
            entities.Select(ToDto).ToList();

        public virtual List<TEntity> ToEntityList(IEnumerable<TDto> dtos) =>
            dtos.Select(ToEntity).ToList();
    }
}
