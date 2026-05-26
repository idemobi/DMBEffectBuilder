document.addEventListener("DOMContentLoaded", function () {

    function buildDiagonalClipPath(edge, direction, tilt, pointCount) {
        var points = [];

        if (edge === 'Top' || edge === 'Both') {
            for (var i = 0; i <= pointCount; i++) {
                var x = 100 * i / pointCount;
                var baseY = direction === 'LeftToRight'
                    ? tilt * (i / pointCount)
                    : tilt * (1 - i / pointCount);
                var noise = pointCount > 0
                    ? (Math.random() * tilt * 0.4 - tilt * 0.2)
                    : 0;
                var y = Math.max(0, baseY + noise);
                points.push(x.toFixed(1) + '% ' + y.toFixed(1) + '%');
            }
        } else {
            points.push('0 0', '100% 0');
        }

        if (edge === 'Bottom' || edge === 'Both') {
            for (var i = pointCount; i >= 0; i--) {
                var x = 100 * i / pointCount;
                var baseY = direction === 'LeftToRight'
                    ? 100 - tilt * (1 - i / pointCount)
                    : 100 - tilt * (i / pointCount);
                var noise = pointCount > 0
                    ? (Math.random() * tilt * 0.4 - tilt * 0.2)
                    : 0;
                var y = Math.min(100, baseY + noise);
                points.push(x.toFixed(1) + '% ' + y.toFixed(1) + '%');
            }
        } else {
            points.push('100% 100%', '0 100%');
        }

        return 'polygon(' + points.join(', ') + ')';
    }

    document.querySelectorAll('.eb-section-effect-diagonal-edge').forEach(function (section) {
        function recompute() {
            var edge       = section.getAttribute('data-diagonal-edge')        || 'Bottom';
            var direction  = section.getAttribute('data-diagonal-direction')   || 'LeftToRight';
            var tilt       = parseFloat(section.getAttribute('data-diagonal-tilt') || '13');
            var tornPoints = parseInt(section.getAttribute('data-diagonal-torn-points') || '30', 10);
            section.style.clipPath = buildDiagonalClipPath(edge, direction, tilt, tornPoints);
        }

        var observer = new MutationObserver(recompute);
        observer.observe(section, {
            attributes: true,
            attributeFilter: ['data-diagonal-edge', 'data-diagonal-direction', 'data-diagonal-tilt', 'data-diagonal-torn-points']
        });
    });

});
