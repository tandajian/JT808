using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.Interfaces
{
    interface IJT808_0x0200_Custom_Factory: IJT808ExternalRegister
    {
        HashSet<byte> AttachIds { get; }
    }
}
