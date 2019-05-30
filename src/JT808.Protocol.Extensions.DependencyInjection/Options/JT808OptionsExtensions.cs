using JT808.Protocol.MessageBody;

namespace JT808.Protocol.Extensions.DependencyInjection.Options
{
    public static  class JT808OptionsExtensions
    {
        /// <summary>
        /// 注册自定义消息
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="customMsgId"></param>
        /// <returns></returns>
        public static JT808Options Register_CustomMsgId<TJT808Bodies>(this JT808Options jT808Options, ushort customMsgId, string terminalPhoneNo = null)
            where TJT808Bodies : JT808Bodies
        {
            JT808GlobalConfig.Instance.MsgIdFactory.SetMap<TJT808Bodies>(customMsgId, terminalPhoneNo);
            return jT808Options;
        }
        /// <summary>
        /// 重写消息
        /// </summary>
        /// <typeparam name="TJT808Bodies"></typeparam>
        /// <param name="overwriteMsgId"></param>
        /// <returns></returns>
        public static JT808Options Overwrite_MsgId<TJT808Bodies>(this JT808Options jT808Options,ushort overwriteMsgId, string terminalPhoneNo=null)
            where TJT808Bodies : JT808Bodies
        {
            JT808GlobalConfig.Instance.MsgIdFactory.ReplaceMap<TJT808Bodies>(overwriteMsgId, terminalPhoneNo);
            return jT808Options;
        }
    }
}
