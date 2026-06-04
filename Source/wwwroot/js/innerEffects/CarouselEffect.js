(function () {
    'use strict';

    document.querySelectorAll('.eb-carousel').forEach(function (carousel) {
        var track = carousel.querySelector('.eb-ca-track');
        var slides = Array.from(carousel.querySelectorAll('.eb-ca-slide'));
        var dots = Array.from(carousel.querySelectorAll('.eb-ca-dot'));
        var prevBtn = carousel.querySelector('.eb-ca-prev');
        var nextBtn = carousel.querySelector('.eb-ca-next');
        if (!track || !slides.length) return;

        var total = slides.length;
        var activeIndex = 0;
        var isFade = carousel.classList.contains('fade-transition');
        var autoPlayTimer = null;
        var isVisible = true;

        function getAutoPlayMs() {
            var val = parseInt(getComputedStyle(carousel).getPropertyValue('--eb-ca-autoplay').trim(), 10);
            return isNaN(val) ? 0 : val;
        }

        function setActive(newIndex) {
            newIndex = ((newIndex % total) + total) % total;

            slides.forEach(function (slide, i) {
                slide.classList.toggle('is-active', i === newIndex);
            });
            dots.forEach(function (dot, i) {
                dot.classList.toggle('is-active', i === newIndex);
            });

            if (!isFade) {
                track.style.transform = 'translateX(-' + (newIndex * 100) + '%)';
            }

            activeIndex = newIndex;
        }

        function next() { setActive(activeIndex + 1); }
        function prev() { setActive(activeIndex - 1); }

        function startAutoPlay() {
            var ms = getAutoPlayMs();
            if (ms <= 0 || !isVisible) return;
            autoPlayTimer = setInterval(next, ms);
        }

        function stopAutoPlay() {
            if (autoPlayTimer) { clearInterval(autoPlayTimer); autoPlayTimer = null; }
        }

        function resetAutoPlay() { stopAutoPlay(); startAutoPlay(); }

        if (prevBtn) prevBtn.addEventListener('click', function () { prev(); resetAutoPlay(); });
        if (nextBtn) nextBtn.addEventListener('click', function () { next(); resetAutoPlay(); });

        dots.forEach(function (dot, i) {
            dot.addEventListener('click', function () { setActive(i); resetAutoPlay(); });
        });

        // Touch swipe
        var touchStartX = 0;
        carousel.addEventListener('touchstart', function (e) {
            touchStartX = e.touches[0].clientX;
        }, { passive: true });
        carousel.addEventListener('touchend', function (e) {
            var diff = touchStartX - e.changedTouches[0].clientX;
            if (Math.abs(diff) > 40) { diff > 0 ? next() : prev(); resetAutoPlay(); }
        }, { passive: true });

        // Pause when out of viewport
        if ('IntersectionObserver' in window) {
            new IntersectionObserver(function (entries) {
                isVisible = entries[0].isIntersecting;
                isVisible ? startAutoPlay() : stopAutoPlay();
            }, { threshold: 0.3 }).observe(carousel);
        }

        setActive(0);
        startAutoPlay();
    });
})();
