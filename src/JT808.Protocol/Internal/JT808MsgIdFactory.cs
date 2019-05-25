using JT808.Protocol.Attributes;
using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Internal
{
    internal class JT808MsgIdFactory: IJT808MsgIdFactory
    {
        private static Dictionary<ushort, Type> map;

        internal JT808MsgIdFactory()
        {
            map = new Dictionary<ushort, Type>();
            InitMap();
        }

        private void InitMap()
        {
            foreach (var item in Enum.GetNames(typeof(JT808MsgId)))
            {
                JT808MsgId msgId = item.ToEnum<JT808MsgId>();
                JT808BodiesTypeAttribute jT808BodiesTypeAttribute = msgId.GetAttribute<JT808BodiesTypeAttribute>();
                map.Add((ushort)msgId, jT808BodiesTypeAttribute?.JT808BodiesType);
            }
        }

        public Type GetBodiesImplTypeByMsgId(ushort msgId, string terminalPhoneNo)
        {
           return map.TryGetValue(msgId, out Type type) ? type : null;
        }

        public IJT808MsgIdFactory SetMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo) where TJT808Bodies : JT808Bodies
        {
            if (!map.ContainsKey(msgId))
                map.Add(msgId, typeof(TJT808Bodies));
            return this;
        }

        public IJT808MsgIdFactory ReplaceMap<TJT808Bodies>(ushort msgId, string terminalPhoneNo) where TJT808Bodies : JT808Bodies
        {
            if (!map.ContainsKey(msgId))
                map.Add(msgId, typeof(TJT808Bodies));
            else
                map[msgId] = typeof(TJT808Bodies);
            return this;
        }
    }
}
