using Cognex.VisionPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FifoGroup
{
    public static class Serialize
    {
        static public object LoadToolBlock(string _toolpath)
        {
            return CogSerializer.LoadObjectFromFile(_toolpath);
        }
        static public void SaveToolBlock(object _ob, string _path)
        {
            CogSerializer.SaveObjectToFile(_ob, _path);
        }
    }
}