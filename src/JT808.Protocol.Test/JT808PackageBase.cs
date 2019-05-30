using System.Reflection;

namespace JT808.Protocol.Test
{
    public class JT808PackageBase
    {
        static JT808PackageBase()
        {
            JT808GlobalConfig.Instance
                .Register(Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly())
                .SetSkipCRCCode(true);
        }
    }
}
