using OpenCvSharp;
using System;
using System.Threading;

namespace WebMojiVision
{
    public class CameraService
    {
        private VideoCapture? capture;
        private Thread? cameraThread;
        private bool isRunning = false;
        public event Action<Mat>? FrameReady;

        public void Start()
        {
            capture = new VideoCapture(0);

            if (!capture.IsOpened())
                throw new Exception("Камера не найдена");

            isRunning = true;

            cameraThread = new Thread(() =>
            {
                using var frame = new Mat();

                while (isRunning)
                {
                    capture.Read(frame);

                    if (frame.Empty()) continue;

                    Cv2.Flip(frame, frame, FlipMode.Y);

                    FrameReady?.Invoke(frame);

                    Thread.Sleep(33);
                }
            });

            cameraThread.IsBackground = true;
            cameraThread.Start();
        }

        public void Stop()
        {
            isRunning = false;
            cameraThread?.Join(500);
            capture?.Release();
        }
    }
}