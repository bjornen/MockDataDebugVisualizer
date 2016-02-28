using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class DumperFactory
    {
        public static DumperBase GetDumper( object o, string name)
        {
            if (o == null) return null;

            var type = o.GetType();

            if (o is Guid) return new GuidType(o, name);

            if (o is DateTime) return new DateTimeType(o, name);

            if (o is Enum) return new EnumType(o, name);

            if (type.IsValueType && !type.IsEnum && !type.IsPrimitive) return new ObjectType(o, name); //Struct

            if (o is System.ValueType) return new OneLineInitDumpers.ValueType(o, name);

            if (o is string) return new StringType(o, name);

            if (o is Array) return new ArrayType(o , name);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>)) return new DictionaryType(o, name);

            if (o is IEnumerable) return new EnumerableType(o, name);

            return new ObjectType(o, name);
        }
    }
}