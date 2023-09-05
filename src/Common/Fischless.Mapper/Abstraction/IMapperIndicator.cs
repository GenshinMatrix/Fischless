using AutoMapper;

namespace Fischless.Mapper;

public interface IMapperIndicator
{
    public void CreateMap(IMapperConfigurationExpression cfg);
}
