using OpenCvSharp;
using WebMojiCore;

namespace WebMojiVision
{
    public class HandDetector : IGestureDetector
    {
        public GestureResult Detect(Mat frame)
        {
            using var hsv = new Mat();
            Cv2.CvtColor(frame, hsv, ColorConversionCodes.BGR2HSV);

            using var mask1 = new Mat();
            using var mask2 = new Mat();
            using var mask = new Mat();

            Cv2.InRange(hsv, new Scalar(0, 15, 60), new Scalar(25, 220, 255), mask1);
            Cv2.InRange(hsv, new Scalar(160, 15, 60), new Scalar(180, 220, 255), mask2);
            Cv2.BitwiseOr(mask1, mask2, mask);

            using var kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(5, 5));
            Cv2.MorphologyEx(mask, mask, MorphTypes.Open, kernel);
            Cv2.MorphologyEx(mask, mask, MorphTypes.Close, kernel);

            Cv2.ImShow("mask", mask);
            Cv2.WaitKey(1);

            Cv2.FindContours(mask, out var contours, out _,
                RetrievalModes.External,
                ContourApproximationModes.ApproxSimple);

            if (contours.Length == 0)
                return new GestureResult { Gesture = GestureType.None, Confidence = 0f };

            var hand = contours.OrderByDescending(c => Cv2.ContourArea(c)).First();

            if (Cv2.ContourArea(hand) < 5000)
                return new GestureResult { Gesture = GestureType.None, Confidence = 0f };

            Cv2.DrawContours(frame, new[] { hand }, -1, Scalar.Lime, 2);

            int fingers = CountFingers(hand);

            var gesture = fingers switch
            {
                0 => GestureType.Fist,
                1 => GestureType.OneFinger,
                4 => GestureType.Swag,
                _ => GestureType.None
            };

            return new GestureResult { Gesture = gesture, Confidence = 0.8f };
        }

        private int CountFingers(Point[] contour)
        {
            using var hull = new Mat();
            Cv2.ConvexHull(InputArray.Create(contour), hull, returnPoints: false);

            using var defectMat = new Mat();
            Cv2.ConvexityDefects(InputArray.Create(contour), hull, defectMat);

            if (defectMat.Empty()) return 0;

            int count = 0;
            for (int i = 0; i < defectMat.Rows; i++)
            {
                var depth = defectMat.At<Vec4i>(i)[3] / 256.0;
                if (depth > 20) count++;
            }

            return count;
        }
    }
}