document.querySelectorAll('.eb-pcard').forEach(container => {
    const tabs   = container.querySelectorAll('.eb-pc-tab');
    const panels = container.querySelectorAll('.eb-pc-panel');

    tabs.forEach(tab => {
        tab.addEventListener('click', () => {
            const idx = tab.dataset.panel;
            tabs.forEach(t => t.classList.remove('is-active'));
            panels.forEach(p => p.classList.remove('is-active'));
            tab.classList.add('is-active');
            container.querySelector(`.eb-pc-panel[data-panel="${idx}"]`).classList.add('is-active');
        });
    });
});
