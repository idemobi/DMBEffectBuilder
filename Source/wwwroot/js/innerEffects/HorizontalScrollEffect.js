(function () {
    'use strict';

    function setup(outer) {
        var sticky = outer.querySelector('.eb-hscroll-sticky');
        var track  = outer.querySelector('.eb-hscroll-track');
        if (!sticky || !track) return;

        var scrollDist = 0;

        function resize() {
            var trackW  = track.scrollWidth;
            var stickyW = sticky.offsetWidth;
            scrollDist         = Math.max(0, trackW - stickyW);
            outer.style.height = sticky.offsetHeight + scrollDist + 'px';
        }

        function update() {
            if (scrollDist <= 0) return;
            var rect      = outer.getBoundingClientRect();
            var scrolled  = -rect.top;
            var maxScroll = outer.offsetHeight - sticky.offsetHeight;
            if (maxScroll <= 0) return;
            var progress  = Math.min(1, Math.max(0, scrolled / maxScroll));
            track.style.transform = 'translateX(' + (-progress * scrollDist) + 'px)';
        }

        resize();
        update();

        window.addEventListener('scroll', update, { passive: true });
        window.addEventListener('resize', function () { resize(); update(); }, { passive: true });
    }

    function init() {
        document.querySelectorAll('.eb-hscroll-outer').forEach(setup);
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
