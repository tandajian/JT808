using JT808.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.Extensions
{
    public class JT808PackageExtensionsTest
    {
        [Fact]
        public void CreatePackage()
        {
           var package= JT808MsgId.终端心跳.Create_终端心跳("123456789", new Protocol.MessageBody.JT808_0x0002 {
                 
            });
        }
    }
}
