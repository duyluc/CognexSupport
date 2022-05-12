using System.Collections.Generic;

namespace FifoGroup
{
    public class ExcuteGroup
    {
        public string ExcuteGroupID { get; set; }
        public Dictionary<string, ExcuteTool> ToolBlocks { get; set; }
        public AcqFifo AcqFifo { get; set; }
        public AcqFifoParams AcqFifoParams { get; set; }
        public ExcuteGroup(string groupID, AcqFifo acqFifo)
        {
            ExcuteGroupID = groupID;
            AcqFifo = acqFifo;
            ToolBlocks = new Dictionary<string, ExcuteTool>();
            this.AcqFifoParams = new AcqFifoParams();
        }
    }
}