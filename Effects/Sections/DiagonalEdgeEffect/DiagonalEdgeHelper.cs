#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj DiagonalEdgeHelper.cs create at 2026/04/16
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using System.Globalization;

namespace DMBEffectBuilder
{
    internal static class DiagonalEdgeHelper
    {
        internal static string BuildClipPath(DiagonalEdge edge, DiagonalDirection direction, decimal tilt, int pointCount)
        {
            var points = new List<string>();

            if (edge == DiagonalEdge.Top || edge == DiagonalEdge.Both)
            {
                for (int i = 0; i <= pointCount; i++)
                {
                    decimal x = 100m * i / pointCount;
                    decimal baseY = direction == DiagonalDirection.LeftToRight
                        ? tilt * ((decimal)i / pointCount)
                        : tilt * (1m - (decimal)i / pointCount);
                    decimal noise = pointCount > 0
                        ? (decimal)(Random.Shared.NextDouble() * (double)(tilt * 0.4m) - (double)(tilt * 0.2m))
                        : 0m;
                    decimal y = Math.Max(0m, baseY + noise);
                    points.Add(FormatPoint(x, y));
                }
            }
            else
            {
                points.Add("0 0");
                points.Add("100% 0");
            }

            if (edge == DiagonalEdge.Bottom || edge == DiagonalEdge.Both)
            {
                for (int i = pointCount; i >= 0; i--)
                {
                    decimal x = 100m * i / pointCount;
                    decimal baseY = direction == DiagonalDirection.LeftToRight
                        ? 100m - tilt * (1m - (decimal)i / pointCount)
                        : 100m - tilt * ((decimal)i / pointCount);
                    decimal noise = pointCount > 0
                        ? (decimal)(Random.Shared.NextDouble() * (double)(tilt * 0.4m) - (double)(tilt * 0.2m))
                        : 0m;
                    decimal y = Math.Min(100m, baseY + noise);
                    points.Add(FormatPoint(x, y));
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
    }
}
