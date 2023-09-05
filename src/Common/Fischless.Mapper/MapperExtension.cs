using AutoMapper;
using System;
using System.Reflection;

namespace Fischless.Mapper;

public static class MapperExtension
{
    public static void ForAllMembersCustom<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression, Action<TSource, TDestination> function)
    {
        expression.BeforeMap(function).ForAllMembers(cfg => cfg.Ignore());
    }

    public static IMappingExpression<TSource, TDestination> ForAllMembersCloneable<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
    {
        expression.ValueTransformers.Add<object>(value => (value as ICloneable) != null ? ((ICloneable)value).Clone() : null);
        return expression;
    }

    public static IMappingExpression<TSource, TDestination> IgnoreAllNotMappedAttribute<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
    {
        foreach (PropertyInfo prop in typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (prop.GetCustomAttribute<NotMappedAttribute>() is NotMappedAttribute attr)
            {
                expression.ForMember(prop.Name, opt => opt.Ignore());
            }
        }
        return expression;
    }

    public static IMappingExpression<TSource, TDestination> IgnoreAllMembersNull<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
    {
        foreach (PropertyInfo prop in typeof(TDestination).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            expression.ForMember(prop.Name, opt => opt.Condition((src, dest, sourceMember) => sourceMember != null));
        }
        return expression;
    }

    public static void Forget<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
    {
    }
}
