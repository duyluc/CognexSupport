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
        public ICogAcqFifo CogAcqFifo { get; set; }
        public AcqFifo(AcqFifoInfo acqFifoInfo)
        {
            try
            {
                if(acqFifoInfo == null) throw new ArgumentNullException(nameof(acqFifoInfo));
                if (FrameGrabbers.Count == 0) throw new NullReferenceException("Can Not Identify any Camera");
                foreach (ICogFrameGrabber frame in FrameGrabbers)
                {
                    if (frame.SerialNumber == acqFifoInfo.SerialNumber)
                    {
                        this.FrameGrabber = frame;
                    }
                }
                if (this.FrameGrabber == null) throw new NullReferenceException($"Can Not Identify {acqFifoInfo.SerialNumber}");
                if(acqFifoInfo.CameraPort != this.FrameGrabber.GetNumCameraPorts(acqFifoInfo.VIDEO_FORMAT))
                {
                    acqFifoInfo.CameraPort = this.FrameGrabber.GetNumCameraPorts(acqFifoInfo.VIDEO_FORMAT);
                }
                this.CogAcqFifo = FrameGrabber.CreateAcqFifo(acqFifoInfo.VIDEO_FORMAT, acqFifoInfo.IMAGE_FORMAT, acqFifoInfo.CameraPort, true);
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