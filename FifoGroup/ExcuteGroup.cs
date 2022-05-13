using System.Collections.Generic;

namespace FifoGroup
{
    public class ExcuteGroup
    {
        public string ExcuteGroupID { get; set; }
        public Dictionary<string, ExcuteTool> ToolBlocks { get; set; }
        public AcqFifoInfo AcqFifoInfo { get; set; }
        public AcqFifo AcqFifo { get; set; }
        public ExcuteGroup(string groupID)
        {
            ExcuteGroupID = groupID;
            AcqFifo = new AcqFifo(AcqFifoInfo);
            ToolBlocks = new Dictionary<string, ExcuteTool>();
        }
    }
}