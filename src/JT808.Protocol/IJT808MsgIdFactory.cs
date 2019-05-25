using System;

namespace JT808.Protocol
{
    public interface IJT808MsgIdFactory
    {
        Type GetBodiesImplTypeByMsgId(ushort msgId, string terminalPhoneNo);
        IJT808MsgIdFactory SetMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo)
            where TJT808Bodies : JT808Bodies;
        IJT808MsgIdFactory ReplaceMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo)
            where TJT808Bodies : JT808Bodies;
    }
}
