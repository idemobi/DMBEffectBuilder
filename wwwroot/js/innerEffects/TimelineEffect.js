(function () {
    'use strict';

    document.querySelectorAll('.eb-timeline').forEach(function (container) {
        var dots = Array.from(container.querySelectorAll('.eb-tl-dot'));
        var panels = Array.from(container.querySelectorAll('.eb-tl-panel'));
        var fill = container.querySelector('.eb-tl-fill');
        if (!dots.length || !panels.length) return;

        var activeIndex = 0;
        var total = dots.length;
        var autoPlayTimer = null;

        function getAutoPlayMs() {
            var val = parseInt(getComputedStyle(container).getPropertyValue('--eb-tl-autoplay').trim(), 10);
            return isNaN(val) ? 0 : val;
        }

        function setFill(index) {
            if (fill) {
                fill.style.width = total <= 1 ? '100%' : (index / (total - 1) * 100) + '%';
            }
        }

        function setActive(newIndex) {
            dots.forEach(function (dot, i) {
                dot.classList.toggle('is-active', i === newIndex);
            });
            panels.forEach(function (panel, i) {
                panel.classList.toggle('is-active', i === newIndex);
            });
            setFill(newIndex);
            activeIndex = newIndex;
        }

        function next() {
            setActive((activeIndex + 1) % total);
        }

        function startAutoPlay() {
            var ms = getAutoPlayMs();
            if (ms <= 0) return;
            autoPlayTimer = setInterval(next, ms);
        }

        function stopAutoPlay() {
            if (autoPlayTimer) {
                clearInterval(autoPlayTimer);
                autoPlayTimer = null;
            }
        }

        dots.forEach(function (dot, i) {
            dot.addEventListener('click', function () {
                stopAutoPlay();
                setActive(i);
                startAutoPlay();
            });
        });

        setFill(0);
        startAutoPlay();
    });
})();
