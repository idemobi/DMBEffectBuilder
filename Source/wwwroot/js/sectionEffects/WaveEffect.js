document.addEventListener("DOMContentLoaded", function () {

    function bezier(t, p0, p1, p2, p3) {
        var mt = 1 - t;
        return mt * mt * mt * p0 + 3 * mt * mt * t * p1 + 3 * mt * t * t * p2 + t * t * t * p3;
    }

    function buildWaveClipPath(edge, pointCount) {
        pointCount = pointCount || 30;
        var points = [];

        if (edge === 'Bottom') {
            points.push('0% 0%', '100% 0%');
            // Wave right to left (t=1 to t=0), P1=(360,80) P2=(1080,0)
            for (var i = pointCount; i >= 0; i--) {
                var t = i / pointCount;
                var x    = bezier(t, 0, 360, 1080, 1440) / 1440 * 100;
                var yRaw = bezier(t, 40, 80, 0, 40);
                var complement = (1 - yRaw / 80).toFixed(4);
                points.push(x.toFixed(2) + '% calc(100% - var(--eb-wave-height) * ' + complement + ')');
            }
        } else {
            // Wave left to right (t=0 to t=1), P1=(360,0) P2=(1080,80)
            for (var i = 0; i <= pointCount; i++) {
                var t = i / pointCount;
                var x    = bezier(t, 0, 360, 1080, 1440) / 1440 * 100;
                var yRaw = bezier(t, 40, 0, 80, 40);
                var yFrac = (yRaw / 80).toFixed(4);
                points.push(x.toFixed(2) + '% calc(var(--eb-wave-height) * ' + yFrac + ')');
            }
            points.push('100% 100%', '0% 100%');
        }

        return 'polygon(' + points.join(', ') + ')';
    }

    document.querySelectorAll('.eb-section-effect-wave').forEach(function (section) {
        function recompute() {
            var edge = section.getAttribute('data-wave-edge') || 'Bottom';
            section.style.clipPath = buildWaveClipPath(edge);
        }

        var observer = new MutationObserver(recompute);
        observer.observe(section, {
            attributes: true,
            attributeFilter: ['data-wave-edge']
        });
    });

});
