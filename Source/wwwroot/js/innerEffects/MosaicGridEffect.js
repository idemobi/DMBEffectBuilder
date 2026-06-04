(() => {
    'use strict';

    const GSAP_CDN = 'https://cdn.jsdelivr.net/npm/gsap@3.13.0/dist/gsap.min.js';
    const FLIP_CDN = 'https://cdn.jsdelivr.net/npm/gsap@3.13.0/dist/Flip.min.js';

    function loadScript(src) {
        return new Promise((resolve, reject) => {
            if (document.querySelector(`script[src="${src}"]`)) { resolve(); return; }
            const s = document.createElement('script');
            s.src = src;
            s.onload = resolve;
            s.onerror = reject;
            document.head.appendChild(s);
        });
    }

    async function ensureGsap() {
        if (!window.gsap) await loadScript(GSAP_CDN);
        if (!window.gsap.plugins || !window.gsap.plugins.flip) await loadScript(FLIP_CDN);
        gsap.registerPlugin(Flip);
    }

    function initMosaic(container) {
        const items   = container.querySelectorAll('[data-row]');
        const rowCls  = ['rows-3-1-1', 'rows-1-3-1', 'rows-1-1-3'];
        const colCls  = ['cols-3-1-1', 'cols-1-3-1', 'cols-1-1-3'];
        let activeCol = 1;
        let activeRow = 1;
        let doFlip    = false;

        items.forEach(item => {
            item.addEventListener('mouseenter', () => {
                const col = item.dataset.column;
                const row = item.dataset.row;
                const state = Flip.getState(items);

                if (col !== activeCol) {
                    container.classList.remove(colCls[activeCol]);
                    container.classList.add(colCls[col]);
                    activeCol = col;
                    doFlip = true;
                }
                if (row !== activeRow) {
                    container.classList.remove(rowCls[activeRow]);
                    container.classList.add(rowCls[row]);
                    activeRow = row;
                    doFlip = true;
                }

                if (doFlip) {
                    Flip.from(state, {
                        duration: 0.4,
                        ease: 'power2.out',
                        onStart: () => { doFlip = false; },
                        absolute: true,
                    });
                }
            });
        });

        container.addEventListener('mouseleave', () => {
            doFlip = true;
            const state = Flip.getState(items);

            container.classList.remove(colCls[activeCol]);
            container.classList.remove(rowCls[activeRow]);
            container.classList.add(colCls[1]);
            container.classList.add(rowCls[1]);
            activeCol = 1;
            activeRow = 1;

            Flip.from(state, {
                duration: 0.4,
                ease: 'power2.out',
                onStart: () => { doFlip = false; },
                absolute: true,
            });
        });
    }

    async function initAll() {
        await ensureGsap();
        document.querySelectorAll('.eb-mosaic-container:not([data-mosaic-init])').forEach(container => {
            container.setAttribute('data-mosaic-init', '');
            initMosaic(container);
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initAll);
    } else {
        initAll();
    }
})();
