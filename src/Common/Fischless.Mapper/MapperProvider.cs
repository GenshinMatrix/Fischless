using AutoMapper;

#if NETSTANDARD2_1 || NET6_0

using AutoMapper.Internal;

#endif

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Fischless.Mapper;

public static class MapperProvider
{
    public static IMapper Service { get; }

    static MapperProvider()
    {
        try
        {
            MapperConfiguration configuration = new(CreateMap);
#if DEBUG
            configuration.AssertConfigurationIsValid();
#endif
            Service = configuration.CreateMapper();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            Debugger.Break();
        }
    }

    private static void CreateMap(IMapperConfigurationExpression cfg)
    {
        foreach (Assembly assembly in MapperAssemblyResolver.Resolve())
        {
            if (assembly == null)
            {
                continue;
            }

            var types = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<MapperIndicatorAttribute>() != null
                        || typeof(IMapperIndicator).IsAssignableFrom(t));

            foreach (var type in types)
            {
                try
                {
                    var instance = (IMapperIndicator)Activator.CreateInstance(type);
                    instance.CreateMap(cfg);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }

    public static TDestination MapDefault<TSource, TDestination>(TSource source, TDestination destination)
    {
        return Map(source, destination, cfg =>
        {
            cfg.CreateMap<TSource, TDestination>();
        });
    }

    public static TDestination MapDefault<TSource, TDestination>(TSource source)
    {
        return Map<TSource, TDestination>(source, cfg =>
        {
            cfg.CreateMap<TSource, TDestination>();
        });
    }

    public static TDestination MapCloneable<TSource, TDestination>(TSource source, TDestination destination)
    {
        return Map(source, destination, cfg =>
        {
            cfg.CreateMap<TSource, TDestination>().ForAllMembersCloneable();
        });
    }

    public static TDestination MapCloneable<TSource, TDestination>(TSource source)
    {
        return Map<TSource, TDestination>(source, cfg =>
        {
            cfg.CreateMap<TSource, TDestination>().ForAllMembersCloneable();
        });
    }

    public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMapperConfigurationExpression> configure = null!)
    {
        if (configure != null)
        {
            MapperConfiguration configuration = new(configure);
            IMapper mapper = configuration.CreateMapper();
            return mapper.Map(source, destination);
        }
#if NETSTANDARD2_0
        if (Service?.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() != null)
#elif NETSTANDARD2_1 || NET6_0
        if (Service?.ConfigurationProvider.Internal().FindTypeMapFor<TSource, TDestination>() != null)
#endif
        {
            return Service.Map(source, destination);
        }
        else
        {
            Debug.WriteLine($"[MapperProvider] Mapper requested an unregistered type from {typeof(TSource)} to {typeof(TDestination)}.");
            return MapDefault(source, destination);
        }
    }

    public static TDestination Map<TSource, TDestination>(TSource source, Action<IMapperConfigurationExpression> configure = null!)
    {
        if (configure != null)
        {
            MapperConfiguration configuration = new(configure);
            IMapper mapper = configuration.CreateMapper();
            return mapper.Map<TSource, TDestination>(source);
        }
#if NETSTANDARD2_0
        if (Service?.ConfigurationProvider.FindTypeMapFor<TSource, TDestination>() != null)
#elif NETSTANDARD2_1 || NET6_0
        if (Service?.ConfigurationProvider.Internal().FindTypeMapFor<TSource, TDestination>() != null)
#endif
        {
            return Service.Map<TSource, TDestination>(source);
        }
        else
        {
            return MapDefault<TSource, TDestination>(source);
        }
    }
}
