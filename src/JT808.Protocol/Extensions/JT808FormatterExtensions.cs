using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Formatters;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace JT808.Protocol.Extensions
{
    public static class JT808FormatterExtensions
    {
        public static IJT808Formatter<T> GetFormatter<T>()
        {
            IJT808Formatter<T> formatter;
            var attr = typeof(T).GetTypeInfo().GetCustomAttribute<JT808FormatterAttribute>();
            if (attr == null)
            {
                throw new JT808Exception(JT808ErrorCode.GetFormatterAttributeError, typeof(T).FullName);
            }
            formatter = (IJT808Formatter<T>)Activator.CreateInstance(attr.FormatterType);
            return formatter;
        }

        public static object GetFormatter(Type type)
        {
            object formatter;
            var attr = type.GetTypeInfo().GetCustomAttribute<JT808FormatterAttribute>();
            if (attr == null)
            {
                throw new JT808Exception(JT808ErrorCode.GetFormatterAttributeError, type.FullName);
            }
            formatter = Activator.CreateInstance(attr.FormatterType);
            return formatter;
        }

        public static JT808FormatterCaching Caching = new JT808FormatterCaching();

        public class JT808FormatterCaching
        {
            public JT808PackageFromatterPool JT808PackageFromatterPool => new JT808PackageFromatterPool();
            public JT808HeaderFormatterPool JT808HeaderFormatterPool => new JT808HeaderFormatterPool();
            public JT808HeaderMessageBodyPropertyFormatterPool JT808HeaderMessageBodyPropertyFormatterPool => new JT808HeaderMessageBodyPropertyFormatterPool();
        }
    }
}
