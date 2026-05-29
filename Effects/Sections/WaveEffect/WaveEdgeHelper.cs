#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Globalization;

#endregion

namespace DMBEffectBuilder
{
    internal static class WaveEdgeHelper
    {
        #region Static methods

        private static double Bezier(double t, double p0, double p1, double p2, double p3)
        {
            double mt = 1.0 - t;
            return mt * mt * mt * p0
                   + 3.0 * mt * mt * t * p1
                   + 3.0 * mt * t * t * p2
                   + t * t * t * p3;
        }

        internal static string BuildClipPath(WaveEdge edge, int pointCount = 30)
        {
            var points = new List<string>();

            if (edge == WaveEdge.Bottom)
            {
                points.Add("0% 0%");
                points.Add("100% 0%");

                // Wave from right to left (t=1 to t=0)
                // Bezier: P0=(0,40) P1=(360,80) P2=(1080,0) P3=(1440,40) in viewBox 1440x80
                for (int i = pointCount; i >= 0; i--)
                {
                    double t = (double)i / pointCount;
                    double x = Bezier(t, 0, 360, 1080, 1440) / 1440.0 * 100.0;
                    double yRaw = Bezier(t, 40, 80, 0, 40);
                    double complement = 1.0 - yRaw / 80.0;
                    points.Add(
                        $"{x.ToString("F2", CultureInfo.InvariantCulture)}% " +
                        $"calc(100% - var(--eb-wave-height) * {complement.ToString("F4", CultureInfo.InvariantCulture)})"
                    );
                }
            }
            else // Top
            {
                // Wave from left to right (t=0 to t=1)
                // Bezier: P0=(0,40) P1=(360,0) P2=(1080,80) P3=(1440,40) in viewBox 1440x80
                for (int i = 0; i <= pointCount; i++)
                {
                    double t = (double)i / pointCount;
                    double x = Bezier(t, 0, 360, 1080, 1440) / 1440.0 * 100.0;
                    double yRaw = Bezier(t, 40, 0, 80, 40);
                    double yFrac = yRaw / 80.0;
                    points.Add(
                        $"{x.ToString("F2", CultureInfo.InvariantCulture)}% " +
                        $"calc(var(--eb-wave-height) * {yFrac.ToString("F4", CultureInfo.InvariantCulture)})"
                    );
                }

                points.Add("100% 100%");
                points.Add("0% 100%");
            }

            return $"polygon({string.Join(", ", points)})";
        }

        #endregion
    }
}