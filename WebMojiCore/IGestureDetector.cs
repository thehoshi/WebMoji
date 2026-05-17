using OpenCvSharp;

namespace WebMojiCore
{
    public interface IGestureDetector
    {
        GestureResult Detect(Mat frame);
    }
}