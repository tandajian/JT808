using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    interface IJT808_0x8103_Custom_Factory : IJT808ExternalRegister
    {
        IDictionary<uint, Type> ParamMethods { get;}
    }
}
