using AutoMapper;

namespace Fischless.Mapper;

public abstract class MapperIndicator : IMapperIndicator
{
    public abstract void CreateMap(IMapperConfigurationExpression cfg);
}
