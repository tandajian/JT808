using JT808.Protocol.Extensions.DependencyInjection.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JT808.Protocol.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddJT808Configure(this IServiceCollection services, IOptions<JT808Options> jT808Options)
        {
            JT808GlobalConfig.Instance
                .Register(jT808Options.Value.ExternalAssemblies.ToArray())
                .SetSkipCRCCode(jT808Options.Value.SkipCRCCode);
            var servicesProvider = services.BuildServiceProvider();
            try
            {
                var msgSNDistributedImpl = servicesProvider.GetRequiredService<IMsgSNDistributed>();
                JT808GlobalConfig.Instance.SetMsgSNDistributed(msgSNDistributedImpl);
            }
            catch { }
            try
            {
                var compressImpl = servicesProvider.GetRequiredService<IJT808ICompress>();
                JT808GlobalConfig.Instance.SetCompress(compressImpl);
            }
            catch { }
            return services;
        }
    }
}
