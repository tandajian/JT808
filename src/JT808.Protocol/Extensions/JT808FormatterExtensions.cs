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

        static JT808FormatterExtensions()
        {
            PackageFormatter = (JT808PackageFormatter)GetFormatter<JT808Package>();
            HeaderPackageFormatter = (JT808HeaderPackageFormatter)GetFormatter<JT808HeaderPackage>();
            HeaderFormatter = (JT808HeaderFormatter)GetFormatter<JT808Header>();
            HeaderMessageBodyPropertyFormatter = (JT808HeaderMessageBodyPropertyFormatter)GetFormatter<JT808HeaderMessageBodyProperty>();
            SplitPackageBodiesFormatter = (JT808SplitPackageBodiesFormatter)GetFormatter<JT808SplitPackageBodies>();
        }

        public static JT808HeaderFormatter HeaderFormatter { get; }

        public static JT808HeaderMessageBodyPropertyFormatter HeaderMessageBodyPropertyFormatter { get; }

        public static JT808HeaderPackageFormatter HeaderPackageFormatter { get; }

        public static JT808PackageFormatter PackageFormatter { get; }

        public static JT808SplitPackageBodiesFormatter SplitPackageBodiesFormatter { get; }
    }
}
