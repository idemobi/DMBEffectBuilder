(function () {
    'use strict';

    function initFan(fan) {
        const cards      = Array.from(fan.querySelectorAll('.eb-fan-card'));
        const hasEntrance = !!fan.dataset.entrance;

        // Hover: lift the hovered card, dim the rest
        cards.forEach(card => {
            card.addEventListener('mouseenter', () => {
                cards.forEach(c => { if (c !== card) c.classList.add('eb-fan-dimmed'); });
            });
            card.addEventListener('mouseleave', () => {
                cards.forEach(c => c.classList.remove('eb-fan-dimmed'));
            });
        });

        if (!hasEntrance) return;

        function slideIn() {
            fan.classList.add('eb-fan--visible');

            // Once the last card's animation ends, mark the fan as fully entered
            // so hover transitions are no longer blocked by the animation rule.
            const lastCard = cards[cards.length - 1];
            lastCard.addEventListener('animationend', () => {
                fan.classList.add('eb-fan--entered');
            }, { once: true });
        }

        function reset() {
            fan.classList.remove('eb-fan--visible', 'eb-fan--entered');
        }

        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (entry.isIntersecting) slideIn();
                else reset();
            });
        }, { threshold: 0.2 });

        observer.observe(fan);
    }

    document.addEventListener('DOMContentLoaded', () => {
        document.querySelectorAll('.eb-fan').forEach(initFan);
    });
}());
