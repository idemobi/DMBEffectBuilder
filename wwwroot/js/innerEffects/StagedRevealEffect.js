(function () {
    'use strict';

    const CLASS_SLOT_IN = 'eb-sr-slot-in';
    const CLASS_ACTIVE  = 'eb-sr-active';
    const CLASS_EXIT    = 'eb-sr-exit';

    function initContainer(container) {
        const TRANSITION_MS = parseInt(container.dataset.transition) || 700;
        const HOLD_MS       = parseInt(container.dataset.hold)       || 2500;
        const slots   = Array.from(container.querySelectorAll('.eb-staged-reveal-slot'));
        const groups  = Array.from(container.querySelectorAll('.eb-staged-reveal-group'));
        const indices = groups.map(() => 0);
        let   cycleTimer  = null;
        let   slideTimer  = null;

        groups.forEach(group => {
            const first = group.querySelector('.eb-staged-reveal-item');
            if (first) first.classList.add(CLASS_ACTIVE);
        });

        function advance() {
            groups.forEach((group, gi) => {
                const items = Array.from(group.querySelectorAll('.eb-staged-reveal-item'));
                if (items.length <= 1) return;

                const currentIdx = indices[gi];
                const nextIdx    = (currentIdx + 1) % items.length;
                const current    = items[currentIdx];
                const next       = items[nextIdx];

                current.classList.remove(CLASS_ACTIVE);
                current.classList.add(CLASS_EXIT);

                setTimeout(() => {
                    current.style.transition = 'none';
                    current.classList.remove(CLASS_EXIT);
                    requestAnimationFrame(() => requestAnimationFrame(() => {
                        current.style.transition = '';
                    }));
                }, TRANSITION_MS);

                next.classList.add(CLASS_ACTIVE);
                indices[gi] = nextIdx;
            });
        }

        function slideIn() {
            slots.forEach(slot => slot.classList.add(CLASS_SLOT_IN));
            slideTimer = setTimeout(() => {
                cycleTimer = setInterval(advance, HOLD_MS + TRANSITION_MS);
            }, TRANSITION_MS);
        }

        function reset() {
            clearInterval(cycleTimer);
            clearTimeout(slideTimer);
            cycleTimer = null;
            slideTimer = null;

            slots.forEach(slot => slot.classList.remove(CLASS_SLOT_IN));

            setTimeout(() => {
                groups.forEach((group, gi) => {
                    const items = Array.from(group.querySelectorAll('.eb-staged-reveal-item'));
                    items.forEach(item => {
                        item.style.transition = 'none';
                        item.classList.remove(CLASS_ACTIVE, CLASS_EXIT);
                    });
                    if (items.length > 0) items[0].classList.add(CLASS_ACTIVE);
                    indices[gi] = 0;

                    requestAnimationFrame(() => requestAnimationFrame(() => {
                        items.forEach(item => { item.style.transition = ''; });
                    }));
                });
            }, TRANSITION_MS);
        }

        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    slideIn();
                } else {
                    reset();
                }
            });
        }, { threshold: 0.3 });

        observer.observe(container);
    }

    document.addEventListener('DOMContentLoaded', () => {
        document.querySelectorAll('.eb-staged-reveal').forEach(initContainer);
    });
}());
