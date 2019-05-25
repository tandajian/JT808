using JT808.Protocol.Internal;
using JT808.Protocol.MessageBody;
using System;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("JT808.Protocol.Benchmark")]
[assembly: InternalsVisibleTo("JT808.Protocol.Test")]
namespace JT808.Protocol
{
    public class JT808GlobalConfig
    {
        public static readonly JT808GlobalConfig Instance = new JT808GlobalConfig();

        private JT808GlobalConfig()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MsgSNDistributed = new DefaultMsgSNDistributedImpl();
            Compress = new JT808GZipCompressImpl();
            SplitPackageStrategy = new DefaultSplitPackageStrategyImpl();
            SkipCRCCode = false;
            MsgIdFactory = new JT808MsgIdFactory();
            Encoding = Encoding.GetEncoding("GBK");
        }

        public IMsgSNDistributed MsgSNDistributed { get; private set; }

        public IJT808ICompress Compress { get; private set; }

        public ISplitPackageStrategy SplitPackageStrategy { get; private set; }

        public IJT808MsgIdFactory MsgIdFactory { get; private set; }

        public Encoding Encoding;

        /// <summary>
        /// 跳过校验码
        /// 测试的时候需要手动修改值，避免验证
        /// 默认：false
        /// </summary>
        public bool SkipCRCCode { get; private set; }

        /// <summary>
        /// 注册自定义定位信息附加数据
        /// </summary>
        /// <typeparam name="attachInfoId"></typeparam>
        public JT808GlobalConfig Register_0x0200_Attach(params byte[] attachInfoId)
        {
            if (attachInfoId != null && attachInfoId.Length > 0)
            {
                foreach (var id in attachInfoId)
                {
                    if (!JT808_0x0200_CustomBodyBase.CustomAttachIds.Contains(id))
                    {
                        JT808_0x0200_CustomBodyBase.CustomAttachIds.Add(id);
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// 注册自定义设置终端参数Id
        /// <see cref="typeof(JT808.Protocol.MessageBody.JT808_0x8103_BodyBase)"/>
        /// <see cref="typeof(实现JT808_0x8103_BodyBase)"/>
        /// <returns></returns>
        public JT808GlobalConfig Register_0x8103_ParamId(uint paramId, Type type)
        {
            JT808_0x8103_BodyBase.AddJT808_0x8103Method(paramId, type);
            return this;
        }

        /// <summary>
        /// 注册电子运单内容实现类
        /// </summary>
        /// <typeparam name="TJT808_0x0701Body"></typeparam>
        /// <returns></returns>
        public JT808GlobalConfig Register_JT808_0x0701Body<TJT808_0x0701Body>()
               where TJT808_0x0701Body : JT808_0x0701.JT808_0x0701Body
        {
            JT808_0x0701.JT808_0x0701Body.BodyImpl = typeof(TJT808_0x0701Body);
            return this;
        }

        /// <summary>
        /// 设置消息序列号
        /// </summary>
        /// <param name="msgSNDistributed"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetMsgSNDistributed(IMsgSNDistributed msgSNDistributed)
        {
            Instance.MsgSNDistributed = msgSNDistributed;
            return this;
        }

        /// <summary>
        /// 设置压缩算法
        /// 默认GZip
        /// </summary>
        /// <param name="compressImpl"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetCompress(IJT808ICompress compressImpl)
        {
            Instance.Compress = compressImpl;
            return this;
        }
        /// <summary>
        /// 设置分包算法
        /// 默认3*256
        /// </summary>
        /// <param name="splitPackageStrategy"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetSplitPackageStrategy(ISplitPackageStrategy splitPackageStrategy)
        {
            Instance.SplitPackageStrategy = splitPackageStrategy;
            return this;
        }
        /// <summary>
        /// 设置跳过校验码
        /// 场景：测试的时候，可能需要手动改数据，所以测试的时候有用
        /// </summary>
        /// <param name="skipCRCCode"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetSkipCRCCode(bool skipCRCCode)
        {
            Instance.SkipCRCCode = skipCRCCode;
            return this;
        }
        /// <summary>
        /// 设置消息工厂的实现
        /// </summary>
        /// <param name="msgIdFactory"></param>
        /// <returns></returns>
        public JT808GlobalConfig SetMsgIdFactory(IJT808MsgIdFactory  msgIdFactory)
        {
            if (msgIdFactory != null)
            {
                Instance.MsgIdFactory = msgIdFactory;
            }
            return this;
        }

    }
}
