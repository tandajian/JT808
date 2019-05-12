using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.Formatters
{
    /// <summary>
    /// 头部消息体属性的格式化器
    /// </summary>
    public class JT808HeaderMessageBodyPropertyFormatter : IJT808Formatter<JT808HeaderMessageBodyProperty>
    {
        private const string encrypt_none = "000";
        private const string encrypt_rsa = "001";
        private const char char_zero = '0';
        private const char char_one = '1';
        public JT808HeaderMessageBodyProperty Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT808HeaderMessageBodyProperty messageBodyProperty = new JT808HeaderMessageBodyProperty();
            ReadOnlySpan<char> msgMethod = Convert.ToString(JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset), 2).PadLeft(16, '0').AsSpan();
            messageBodyProperty.DataLength = Convert.ToInt32(msgMethod.Slice(6, 10).ToString(), 2);
            //  2.2. 数据加密方式
            switch (msgMethod.Slice(3, 3).ToString())
            {
                case encrypt_none:
                    messageBodyProperty.Encrypt = JT808EncryptMethod.None;
                    break;
                case encrypt_rsa:
                    messageBodyProperty.Encrypt = JT808EncryptMethod.RSA;
                    break;
                default:
                    messageBodyProperty.Encrypt = JT808EncryptMethod.None;
                    break;
            }
            messageBodyProperty.IsPackge = msgMethod[2] != char_zero;
            messageBodyProperty.PackgeCount = 0;
            messageBodyProperty.PackageIndex = 0;
            if (messageBodyProperty.IsPackge)
            {
                offset += 8;
                messageBodyProperty.PackgeCount = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
                messageBodyProperty.PackageIndex = JT808BinaryExtensions.ReadUInt16Little(bytes, ref offset);
            }
            readSize = 2;
            return messageBodyProperty;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808HeaderMessageBodyProperty value)
        {
            // 2.消息体属性
            Span<char> msgMethod = new char[16];
            //  2.1.保留
            msgMethod[0] = char_zero;
            msgMethod[1] = char_zero;
            //  2.2.是否分包
            msgMethod[2] = value.IsPackge ? char_one : char_zero;
            //  2.3.数据加密方式
            switch (value.Encrypt)
            {
                case JT808EncryptMethod.None:
                    msgMethod[3] = char_zero;
                    msgMethod[4] = char_zero;
                    msgMethod[5] = char_zero;
                    break;
                case JT808EncryptMethod.RSA:
                    msgMethod[3] = char_zero;
                    msgMethod[4] = char_zero;
                    msgMethod[5] = char_one;
                    break;
                default:
                    msgMethod[3] = char_zero;
                    msgMethod[4] = char_zero;
                    msgMethod[5] = char_zero;
                    break;
            }
            //  2.4.数据长度
            ReadOnlySpan<char> dataLen = Convert.ToString(value.DataLength, 2).PadLeft(10, char_zero).AsSpan();
            for (int i = 1; i <= 10; i++)
            {
                msgMethod[5 + i] = dataLen[i - 1];
            }
            offset += JT808BinaryExtensions.WriteUInt16Little(bytes, offset, Convert.ToUInt16(msgMethod.ToString(), 2));
            return offset;
        }
    }
}
