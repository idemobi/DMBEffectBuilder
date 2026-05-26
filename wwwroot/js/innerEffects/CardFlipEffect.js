(function () {
    'use strict';

    document.querySelectorAll('.eb-card-flip-grid.click-trigger').forEach(function (grid) {
        grid.querySelectorAll('.eb-cf-card').forEach(function (card) {
            card.addEventListener('click', function () {
                card.classList.toggle('is-flipped');
            });
        });
    });
})();
