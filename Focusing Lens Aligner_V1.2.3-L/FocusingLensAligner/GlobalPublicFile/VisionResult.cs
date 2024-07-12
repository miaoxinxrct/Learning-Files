using System.Drawing;

namespace FocusingLensAligner
{
    class VisionResult
    {
        public struct LensResult
        {
            //Lens信息
            public PointF center;
            public PointF imagecenter;           
        }

        public struct BoxResult
        {
            //Box信息
            public PointF point;
            public PointF imagecenter;
            public float diameter;
        }

        public struct EpoxyResult
        {
            //胶斑信息
            public PointF center;
            public PointF imagecenter;
            public float diameter;
        }

        public struct GripperResult
        {
            //夹爪信息
            public PointF center;
            public PointF imagecenter;
        }

        public struct LaserSpotResult
        {
            //光斑信息
            public PointF center;
            public PointF imagecenter;
        }
    }
}
