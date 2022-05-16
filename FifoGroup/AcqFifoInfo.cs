using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cognex.VisionPro;

namespace FifoGroup
{
    public class AcqFifoInfo
    {
        public string SerialNumber { get; set; }
        public string VIDEO_FORMAT { get; set; } = "Generic GigEVision (Mono)";
        public CogAcqFifoPixelFormatConstants IMAGE_FORMAT { get; set; } = CogAcqFifoPixelFormatConstants.Format8Grey;
        public int CameraPort { get; set; } = 0;
        public AcqFifoInfo(string serialNumber, string vIDEO_FORMAT = "Generic GigEVision (Mono)", CogAcqFifoPixelFormatConstants iMAGE_FORMAT = CogAcqFifoPixelFormatConstants.Format8Grey, int cameraPort = 0)
        {
            if(string.IsNullOrEmpty(serialNumber)) throw new ArgumentNullException(nameof(serialNumber));
            if(string.IsNullOrEmpty(vIDEO_FORMAT)) throw new ArgumentNullException(nameof(vIDEO_FORMAT));
            if(cameraPort<0) throw new ArgumentOutOfRangeException(nameof(cameraPort));
            this.SerialNumber = serialNumber; 
            this.VIDEO_FORMAT = vIDEO_FORMAT;
            this.IMAGE_FORMAT = iMAGE_FORMAT;
            this.CameraPort = cameraPort;
        }
    }

}