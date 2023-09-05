using System;

namespace Fischless.Mapper;

[AttributeUsage(AttributeTargets.Property)]
public class NotMappedAttribute : Attribute
{
}
