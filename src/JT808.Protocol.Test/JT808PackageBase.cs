using System.Reflection;

namespace JT808.Protocol.Test
{
    public class JT808PackageBase
    {
        static JT808PackageBase()
        {
            JT808GlobalConfig.Instance.SetSkipCRCCode(true);
            JT808GlobalConfig.Instance.FormatterFactory.Register(assemblys: Assembly.GetAssembly(type: typeof(JT808PackageBase)));
        }
    }
}
