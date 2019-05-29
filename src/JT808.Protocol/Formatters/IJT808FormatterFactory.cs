using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol.Formatters
{
    public interface IJT808FormatterFactory
    {
        Dictionary<Guid,object> FormatterDict { get;}
        IJT808FormatterFactory Register(params Assembly[] assemblys);
        IJT808FormatterFactory SetMap<TJT808Bodies>()
                    where TJT808Bodies : JT808Bodies;
        IJT808FormatterFactory ReplaceMap<TJT808Bodies>()
            where TJT808Bodies : JT808Bodies;
    }
}
