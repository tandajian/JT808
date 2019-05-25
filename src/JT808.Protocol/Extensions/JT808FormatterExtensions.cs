using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace JT808.Protocol.Extensions
{
    public static class JT808FormatterExtensions
    {
        private static readonly ConcurrentDictionary<Guid, object> formatterCache = new ConcurrentDictionary<Guid, object>();
        public static IJT808Formatter<T> GetFormatter<T>()
        {
            return (IJT808Formatter<T>)GetFormatter(typeof(T)); 
        }
        public static object GetFormatter(Type type)
        {
            if (!formatterCache.TryGetValue(type.GUID, out var formatter))
            {
                var attr = type.GetTypeInfo().GetCustomAttribute<JT808FormatterAttribute>();
                if (attr == null)
                {
                    throw new JT808Exception(JT808ErrorCode.GetFormatterAttributeError, type.FullName);
                }
                formatter = Activator.CreateInstance(attr.FormatterType);
                formatterCache.TryAdd(type.GUID, formatter);
            }
            return formatter;
        }
    }
}
