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
        public AcqFifoParams AcqFifoParams { get; set; }
        public AcqFifo AcqFifo { get;set; }
        public ExcuteTool(string iD, string toolBlockPath, AcqFifo acqFifo)
        {
            if(string.IsNullOrEmpty(iD)) throw new ArgumentNullException(nameof(iD));
            if(string.IsNullOrEmpty(toolBlockPath)) throw new ArgumentNullException(nameof(toolBlockPath));
            this.AcqFifo = acqFifo;
            this.ID = iD;
            this.ToolBlock = Serialize.LoadToolBlock(toolBlockPath) as CogToolBlock;
            this.ExcuteToolParams = new ExcuteToolParams();
        }

        public ExcuteTool(string iD, CogToolBlock cogToolBlock, AcqFifo acqFifo)
        {
            if (string.IsNullOrEmpty(iD)) throw new ArgumentNullException(nameof(iD));
            this.AcqFifo = acqFifo;
            this.ID = iD;
            this.ToolBlock = cogToolBlock;
        }
    }
}