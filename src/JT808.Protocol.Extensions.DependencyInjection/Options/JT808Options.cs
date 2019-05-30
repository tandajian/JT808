using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JT808.Protocol.Extensions.DependencyInjection.Options
{
    public class JT808Options:IOptions<JT808Options>
    {
        public JT808Options()
        {
            SkipCRCCode = false;
            ExternalAssemblies = new List<Assembly>();
        }
        public List<Assembly> ExternalAssemblies{ get; set; }
        /// <summary>
        /// 设置跳过校验码
        /// 场景：测试的时候，可能需要收到改数据，所以测试的时候有用
        /// </summary>
        public bool SkipCRCCode { get; set; }

        JT808Options IOptions<JT808Options>.Value
        {
            get { return this; }
        }
    }
}
