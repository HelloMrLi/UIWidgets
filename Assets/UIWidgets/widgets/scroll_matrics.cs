using System;
using UIWidgets.painting;

namespace UIWidgets.widgets {
    public interface ScrollMetrics {
        double minScrollExtent { get; }

        double maxScrollExtent { get; }

        double pixels { get; }

        double viewportDimension { get; }

        AxisDirection axisDirection { get; }
    }

    public static class ScrollMetricsUtils {
        public static Axis axis(this ScrollMetrics it) {
            return AxisUtils.axisDirectionToAxis(it.axisDirection);
        }

        public static bool outOfRange(this ScrollMetrics it) {
            return it.pixels < it.minScrollExtent || it.pixels > it.maxScrollExtent;
        }

        public static bool atEdge(this ScrollMetrics it) {
            return it.pixels == it.minScrollExtent || it.pixels == it.maxScrollExtent;
        }

        public static double extentBefore(this ScrollMetrics it) {
            return Math.Max(it.pixels - it.minScrollExtent, 0.0);
        }

        public static double extentInside(this ScrollMetrics it) {
            return Math.Min(it.pixels, it.maxScrollExtent) -
                   Math.Max(it.pixels, it.minScrollExtent) +
                   Math.Min(it.viewportDimension, it.maxScrollExtent - it.minScrollExtent);
        }

        public static double extentAfter(this ScrollMetrics it) {
            return Math.Max(it.maxScrollExtent - it.pixels, 0.0);
        }
    }

    public class FixedScrollMetrics : ScrollMetrics {
        public FixedScrollMetrics(
            double minScrollExtent = 0.0,
            double maxScrollExtent = 0.0,
            double pixels = 0.0,
            double viewportDimension = 0.0,
            AxisDirection axisDirection = AxisDirection.down
        ) {
            this.minScrollExtent = minScrollExtent;
            this.maxScrollExtent = maxScrollExtent;
            this.pixels = pixels;
            this.viewportDimension = viewportDimension;
            this.axisDirection = axisDirection;
        }

        public double minScrollExtent { get; private set; }

        public double maxScrollExtent { get; private set; }

        public double pixels { get; private set; }

        public double viewportDimension { get; private set; }

        public AxisDirection axisDirection { get; private set; }

        public override string ToString() {
            return string.Format("{0}({1:F1})..[{2:F1}]..{3:F1})",
                this.GetType(), this.extentBefore(), this.extentInside(), this.extentAfter());
        }
    }
}