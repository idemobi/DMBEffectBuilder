#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Collections.Generic;
using System.Globalization;

#endregion

namespace DMBEffectBuilder
{
    internal static class CurveEdgeHelper
    {
        #region Static methods

        internal static string BuildClipPath(CurveEdge edge, Curvature curvature, decimal tilt, int pointCount = 40)
        {
            var points = new List<string>();

            // Top edge points (left to right)
            if (edge == CurveEdge.Top || edge == CurveEdge.Both)
            {
                for (int i = 0; i <= pointCount; i++)
                {
                    decimal x = (decimal)i / pointCount;
                    decimal y;
                    if (curvature == Curvature.Convex)
                    {
                        // Convex top: curves upward (y decreases at center)
                        y = tilt * (decimal)Math.Pow((double)(2m * x - 1m), 2);
                    }
                    else
                    {
                        // Concave top: curves downward (y increases at center)
                        y = tilt * (1m - (decimal)Math.Pow((double)(2m * x - 1m), 2));
                    }

                    points.Add(FormatPoint(x * 100m, y));
                }
            }
            else
            {
                points.Add("0 0");
                points.Add("100% 0");
            }

            // Bottom edge points (right to left)
            if (edge == CurveEdge.Bottom || edge == CurveEdge.Both)
            {
                for (int i = pointCount; i >= 0; i--)
                {
                    decimal x = (decimal)i / pointCount;
                    decimal y;
                    if (curvature == Curvature.Convex)
                    {
                        // Convex bottom: curves downward (y increases at center)
                        y = 100m - tilt * (decimal)Math.Pow((double)(2m * x - 1m), 2);
                    }
                    else
                    {
                        // Concave bottom: curves upward (y decreases at center)
                        y = 100m - tilt * (1m - (decimal)Math.Pow((double)(2m * x - 1m), 2));
                    }

                    points.Add(FormatPoint(x * 100m, y));
                }
            }
            else
            {
                points.Add("100% 100%");
                points.Add("0 100%");
            }

            return $"polygon({string.Join(", ", points)})";
        }

        private static string FormatPoint(decimal x, decimal y)
        {
            return $"{x.ToString("F1", CultureInfo.InvariantCulture)}% {y.ToString("F1", CultureInfo.InvariantCulture)}%";
        }

        #endregion
    }
}