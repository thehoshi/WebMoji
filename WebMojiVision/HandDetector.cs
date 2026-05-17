using OpenCvSharp;
using WebMojiCore;

namespace WebMojiVision
{
    public class HandDetector : IGestureDetector
    {
        public GestureResult Detect(Mat frame)
        {
            return new GestureResult
            {
                Gesture = GestureType.None,
                Confidence = 0f
            };
        }
    }
}