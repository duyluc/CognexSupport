using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FifoGroup
{
    public class ExcuteTool
    {
        public string ID { get; set; }
        public CogToolBlock ToolBlock { get; set; }
        public ExcuteGroup ExcuteGroup { get; set; }
        public ExcuteToolParams ExcuteToolParams { get; set; }
        public ExcuteTool(string iD, string toolBlockPath, ExcuteGroup excuteGroup)
        {
            if(string.IsNullOrEmpty(iD)) throw new ArgumentNullException(nameof(iD));
            if(string.IsNullOrEmpty(toolBlockPath)) throw new ArgumentNullException(nameof(toolBlockPath));
            this.ID = iD;
            this.ToolBlock = Serialize.LoadToolBlock(toolBlockPath) as CogToolBlock;
            this.ExcuteGroup = excuteGroup;
            this.ExcuteToolParams = new ExcuteToolParams();
        }

        public ExcuteTool(string iD, CogToolBlock cogToolBlock, ExcuteGroup excuteGroup)
        {
            if (string.IsNullOrEmpty(iD)) throw new ArgumentNullException(nameof(iD));
            this.ID = iD;
            this.ToolBlock = cogToolBlock;
            this.ExcuteGroup = excuteGroup;
        }
    }
}