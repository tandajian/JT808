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
        public static IJT808Formatter<T> GetFormatter<T>()
        {
            return (IJT808Formatter<T>)GetFormatter(typeof(T)); 
        }
        public static object GetFormatter(Type type)
        {
            if (!JT808GlobalConfig.Instance.FormatterFactory.FormatterDict.TryGetValue(type.GUID, out var formatter))
            {
                throw new JT808Exception(JT808ErrorCode.NotGlobalRegisterFormatterAssembly, type.FullName);
            }
            return formatter;
        }
    }
}
