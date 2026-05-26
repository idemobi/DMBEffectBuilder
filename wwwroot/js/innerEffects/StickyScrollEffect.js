(function () {
    'use strict';

    document.querySelectorAll('.eb-sticky-scroll').forEach(function (container) {
        var items = Array.from(container.querySelectorAll('.eb-ss-item'));
        var visuals = Array.from(container.querySelectorAll('.eb-ss-visual'));
        if (!items.length || !visuals.length) return;

        var activeIndex = 0;

        function getTransitionMs() {
            var val = getComputedStyle(container).getPropertyValue('--eb-ss-duration').trim();
            return (parseFloat(val) || 0.5) * 1000;
        }

        function setActive(newIndex) {
            if (newIndex === activeIndex) return;

            var direction = newIndex > activeIndex ? 1 : -1;
            var prev = visuals[activeIndex];
            var next = visuals[newIndex];

            prev.style.transform = 'translateY(' + (-direction * 100) + '%)';
            items[activeIndex].classList.remove('is-active');

            next.style.transition = 'none';
            next.style.transform = 'translateY(' + (direction * 100) + '%)';
            next.offsetHeight;
            next.style.transition = '';
            next.style.transform = 'translateY(0)';

            items[newIndex].classList.add('is-active');
            activeIndex = newIndex;
        }

        visuals[0].style.transform = 'translateY(0)';
        for (var i = 1; i < visuals.length; i++) {
            visuals[i].style.transform = 'translateY(100%)';
        }
        items[0].classList.add('is-active');

        var observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    setActive(parseInt(entry.target.dataset.index, 10));
                }
            });
        }, {
            rootMargin: '-40% 0px -40% 0px'
        });

        items.forEach(function (item) { observer.observe(item); });
    });
})();
