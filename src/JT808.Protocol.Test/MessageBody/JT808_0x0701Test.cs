using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Test.MessageBody.JT808_0x0701BodiesImpl;
using Xunit;

namespace JT808.Protocol.Test.MessageBody
{
    public class JT808_0x0701Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0701 jT808_0X0701 = new JT808_0x0701();
            var body = new JT808_0x0701TestBodiesImpl
            {
                Id = 333,
                UserName = "汉smallchi"
            };
            jT808_0X0701.ElectronicContentObj = body;
            var hex = JT808Serializer.Serialize(jT808_0X0701).ToHexString();
            Assert.Equal("000000100000014D000ABABA736D616C6C636869", hex);
        }

        [Fact]
        public void Test2()
        {
            byte[] bytes = "000000100000014D000ABABA736D616C6C636869".ToHexBytes();
            JT808_0x0701 jT808_0X0701 = JT808Serializer.Deserialize<JT808_0x0701>(bytes);
            Assert.Equal((uint)16, jT808_0X0701.ElectronicWaybillLength);
            JT808_0x0701TestBodiesImpl jT808_0X0701_content = JT808Serializer.Deserialize<JT808_0x0701TestBodiesImpl>(jT808_0X0701.ElectronicContent);
            Assert.Equal((uint)333, jT808_0X0701_content.Id);
            Assert.Equal("汉smallchi", jT808_0X0701_content.UserName);
            Assert.Equal(10, jT808_0X0701_content.UserNameLength);
        }
    }
}
