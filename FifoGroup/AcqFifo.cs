using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cognex.VisionPro;

namespace FifoGroup
{
    //public 
    public class AcqFifo
    {
        static public CogFrameGrabbers FrameGrabbers { get;set;} = new CogFrameGrabbers();
        public ICogFrameGrabber FrameGrabber { get; set; }
        public AcqFifoInfo AcqFifoInfo { get; set; }
        public ICogAcqFifo CogAcqFifo { get; set; }
        /// <summary>
        /// Dictionary<GroupId,GroupObject> ExcuteGroups
        /// </summary>
        public Dictionary<string,ExcuteGroup> ExcuteGroups { get; set; }
        public AcqFifo(AcqFifoInfo acqFifoInfo)
        {
            try
            {
                if(acqFifoInfo == null) throw new ArgumentNullException(nameof(acqFifoInfo));
                if (FrameGrabbers.Count == 0) throw new NullReferenceException("Can Not Identify any Camera");
                foreach (ICogFrameGrabber frame in FrameGrabbers)
                {
                    if (frame.SerialNumber == AcqFifoInfo.SerialNumber)
                    {
                        this.FrameGrabber = frame;
                    }
                }
                if (this.FrameGrabber == null) throw new NullReferenceException($"Can Not Identify {acqFifoInfo.SerialNumber}");
                if(this.AcqFifoInfo.CameraPort != this.FrameGrabber.GetNumCameraPorts(this.AcqFifoInfo.VIDEO_FORMAT))
                {
                    this.AcqFifoInfo.CameraPort = this.FrameGrabber.GetNumCameraPorts(this.AcqFifoInfo.VIDEO_FORMAT);
                }
                this.CogAcqFifo = FrameGrabber.CreateAcqFifo(this.AcqFifoInfo.VIDEO_FORMAT, this.AcqFifoInfo.IMAGE_FORMAT, this.AcqFifoInfo.CameraPort, true);
                this.ExcuteGroups = new Dictionary<string, ExcuteGroup>();
            }
            catch (NullReferenceException nex)
            {
                throw nex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}