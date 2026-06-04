document.addEventListener("DOMContentLoaded", function () {

    function buildCurveClipPath(edge, curvature, tilt) {
        var pointCount = 40;
        var points = [];

        if (edge === 'Top' || edge === 'Both') {
            for (var i = 0; i <= pointCount; i++) {
                var x = i / pointCount;
                var y = curvature === 'Convex'
                    ? tilt * Math.pow(2 * x - 1, 2)
                    : tilt * (1 - Math.pow(2 * x - 1, 2));
                points.push((x * 100).toFixed(1) + '% ' + y.toFixed(1) + '%');
            }
        } else {
            points.push('0 0', '100% 0');
        }

        if (edge === 'Bottom' || edge === 'Both') {
            for (var i = pointCount; i >= 0; i--) {
                var x = i / pointCount;
                var y = curvature === 'Convex'
                    ? 100 - tilt * Math.pow(2 * x - 1, 2)
                    : 100 - tilt * (1 - Math.pow(2 * x - 1, 2));
                points.push((x * 100).toFixed(1) + '% ' + y.toFixed(1) + '%');
            }
        } else {
            points.push('100% 100%', '0 100%');
        }

        return 'polygon(' + points.join(', ') + ')';
    }

    document.querySelectorAll('.eb-section-effect-curve-edge').forEach(function (section) {
        function recompute() {
            var edge      = section.getAttribute('data-curve-edge')      || 'Bottom';
            var curvature = section.getAttribute('data-curve-curvature') || 'Convex';
            var tilt      = parseFloat(section.getAttribute('data-curve-tilt') || '8');
            section.style.clipPath = buildCurveClipPath(edge, curvature, tilt);
        }

        var observer = new MutationObserver(recompute);
        observer.observe(section, {
            attributes: true,
            attributeFilter: ['data-curve-edge', 'data-curve-curvature', 'data-curve-tilt']
        });
    });

});
