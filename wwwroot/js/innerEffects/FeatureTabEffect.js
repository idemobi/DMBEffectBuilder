(function () {
    'use strict';

    document.querySelectorAll('.eb-feature-tab').forEach(function (container) {
        var items = Array.from(container.querySelectorAll('.eb-ft-item'));
        var panels = Array.from(container.querySelectorAll('.eb-ft-panel'));
        if (!items.length || !panels.length) return;

        function setActive(newIndex) {
            items.forEach(function (item, i) {
                item.classList.toggle('is-active', i === newIndex);
            });
            panels.forEach(function (panel, i) {
                panel.classList.toggle('is-active', i === newIndex);
            });
        }

        items.forEach(function (item, i) {
            var trigger = item.querySelector('.eb-ft-trigger');
            if (trigger) {
                trigger.addEventListener('click', function () {
                    setActive(i);
                });
            }
        });
    });
})();
