(() => {
    'use strict';

    const GSAP_CDN  = 'https://cdn.jsdelivr.net/npm/gsap@3.13.0/dist/gsap.min.js';
    const SPLIT_CDN = 'https://cdn.jsdelivr.net/npm/gsap@3.13.0/dist/SplitText.min.js';
    const EASE_CDN  = 'https://cdn.jsdelivr.net/npm/gsap@3.13.0/dist/CustomEase.min.js';

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
        if (!window.gsap)       await loadScript(GSAP_CDN);
        if (!window.SplitText)  await loadScript(SPLIT_CDN);
        if (!window.CustomEase) await loadScript(EASE_CDN);
        gsap.registerPlugin(SplitText, CustomEase);
        if (!CustomEase.get('fstrip-wipe')) {
            CustomEase.create('fstrip-wipe', '0.625, 0.05, 0, 1');
        }
    }

    class FilmstripReveal {
        constructor(el, wrapper) {
            this.el               = el;
            this.wrapper          = wrapper;
            this._tl              = null;
            this._split           = null;
            this._animating       = false;
            this._ready           = false;
            // slideshow state
            this._slides          = [];
            this._inner           = [];
            this._thumbs          = [];
            this._slideCurrent    = 0;
            this._slideInitial    = 0;
            this._slideAnimating  = false;
        }

        async init() {
            await ensureGsap();
            await document.fonts.ready;
            this._ready = true;
            this._collectSlideshow();
            this._bindThumbs();
            this._observe();
        }

        // ── Slideshow setup ──────────────────────────────────────────────
        _collectSlideshow() {
            const el       = this.el;
            this._slides   = Array.from(el.querySelectorAll('[data-slideshow="slide"]'));
            this._inner    = Array.from(el.querySelectorAll('[data-slideshow="parallax"]'));
            this._thumbs   = Array.from(el.querySelectorAll('[data-slideshow="thumb"]'));
            this._slides.forEach((s, i) => s.setAttribute('data-index', i));
            this._thumbs.forEach((t, i) => t.setAttribute('data-index', i));
            const idx = this._slides.findIndex(s => s.classList.contains('is--current'));
            this._slideInitial = idx >= 0 ? idx : 0;
            this._slideCurrent = this._slideInitial;
        }

        _bindThumbs() {
            this._thumbs.forEach(thumb => {
                thumb.addEventListener('click', e => {
                    const idx = parseInt(e.currentTarget.getAttribute('data-index'), 10);
                    if (idx === this._slideCurrent || this._slideAnimating) return;
                    this._navigateTo(idx > this._slideCurrent ? 1 : -1, idx);
                });
            });
        }

        _navigateTo(direction, targetIndex) {
            if (this._slideAnimating) return;
            this._slideAnimating = true;

            const previous  = this._slideCurrent;
            this._slideCurrent = targetIndex;

            const curSlide  = this._slides[previous];
            const curInner  = this._inner[previous];
            const nextSlide = this._slides[this._slideCurrent];
            const nextInner = this._inner[this._slideCurrent];
            const self      = this;

            gsap.timeline({
                defaults: { duration: 1.5, ease: 'fstrip-wipe' },
                onStart() {
                    nextSlide.classList.add('is--current');
                    self._thumbs[previous].classList.remove('is--current');
                    self._thumbs[self._slideCurrent].classList.add('is--current');
                },
                onComplete() {
                    curSlide.classList.remove('is--current');
                    self._slideAnimating = false;
                },
            })
                .to(curSlide,   { xPercent: -direction * 100 }, 0)
                .to(curInner,   { xPercent:  direction * 75  }, 0)
                .fromTo(nextSlide, { xPercent:  direction * 100 }, { xPercent: 0 }, 0)
                .fromTo(nextInner, { xPercent: -direction * 75  }, { xPercent: 0 }, 0);
        }

        // ── Loading animation ────────────────────────────────────────────
        _runAnimation() {
            if (this._animating || !this._ready) return;
            this._animating = true;

            const el = this.el;
            const heading      = el.querySelectorAll('.eb-fstrip-header__h1');
            const revealImages = el.querySelectorAll('.eb-fstrip-loader__group > *');
            const isScaleUp    = el.querySelectorAll('.eb-fstrip-loader__media');
            const isScaleDown  = el.querySelectorAll('.eb-fstrip-loader__media .is--scale-down');
            const isRadius     = el.querySelectorAll('.eb-fstrip-loader__media.is--scaling.is--radius');
            const sliderNav    = el.querySelectorAll('.eb-fstrip-header__slider-nav > *');
            const self         = this;

            const tl = gsap.timeline({
                defaults: { ease: 'expo.inOut' },
                onStart: () => el.classList.remove('is--hidden'),
            });
            this._tl = tl;

            if (heading.length) {
                this._split = new SplitText(heading, { type: 'words', mask: 'words' });
                gsap.set(this._split.words, { yPercent: 110 });
            }

            if (revealImages.length) {
                tl.fromTo(revealImages, { xPercent: 500 }, {
                    xPercent: -500,
                    duration: 2.5,
                    stagger: 0.05,
                });
            }

            if (isScaleDown.length) {
                tl.to(isScaleDown, {
                    scale: 0.5,
                    duration: 2,
                    stagger: { each: 0.05, from: 'edges', ease: 'none' },
                    onComplete: () => isRadius.forEach(m => m.classList.remove('is--radius')),
                }, '-=0.1');
            }

            if (isScaleUp.length) {
                tl.fromTo(isScaleUp,
                    { width: '10em', height: '10em' },
                    { width: '100vw', height: '100dvh', duration: 2 },
                    '< 0.5');
            }

            if (sliderNav.length) {
                tl.from(sliderNav, {
                    yPercent: 150,
                    stagger: 0.05,
                    ease: 'expo.out',
                    duration: 1,
                }, '-=0.9');
            }

            if (this._split && this._split.words.length) {
                tl.to(this._split.words, {
                    yPercent: 0,
                    stagger: 0.075,
                    ease: 'expo.out',
                    duration: 1,
                }, '< 0.1');
            }

            tl.call(() => {
                el.classList.remove('is--loading');
                self._animating = false;
                self._tl = null;
            }, null, '+=0.45');
        }

        // ── Reset ────────────────────────────────────────────────────────
        _reset() {
            if (!this._ready) return;
            const el = this.el;

            if (this._tl) { this._tl.kill(); this._tl = null; }
            this._animating = false;

            // Revert split text first (removes word spans from DOM)
            if (this._split) { this._split.revert(); this._split = null; }

            // Clear GSAP inline styles from loader and nav elements
            const toClean = [
                ...el.querySelectorAll('.eb-fstrip-loader__group > *'),
                ...el.querySelectorAll('.eb-fstrip-loader__media'),
                ...el.querySelectorAll('.eb-fstrip-loader__cover-img.is--scale-down'),
                ...el.querySelectorAll('.eb-fstrip-header__slider-nav > *'),
            ];
            gsap.killTweensOf(toClean);
            gsap.set(toClean, { clearProps: 'all' });

            // Clear slideshow slides inline styles
            const slideEls = [...this._slides, ...this._inner];
            gsap.killTweensOf(slideEls);
            gsap.set(slideEls, { clearProps: 'all' });

            // Reset slideshow state to initial slide (center)
            this._slides.forEach((s, i) => {
                s.classList.toggle('is--current', i === this._slideInitial);
            });
            this._thumbs.forEach((t, i) => {
                t.classList.toggle('is--current', i === this._slideInitial);
            });
            this._slideCurrent   = this._slideInitial;
            this._slideAnimating = false;

            // Restore is--radius on the scaling element
            el.querySelectorAll('.eb-fstrip-loader__media.is--scaling')
              .forEach(m => m.classList.add('is--radius'));

            // Back to initial hidden+loading state
            el.classList.add('is--loading', 'is--hidden');
        }

        // ── IntersectionObserver ─────────────────────────────────────────
        _observe() {
            const obs = new IntersectionObserver(entries => {
                for (const entry of entries) {
                    if (entry.isIntersecting) {
                        this._runAnimation();
                    } else {
                        this._reset();
                    }
                }
            }, { threshold: 0.1 });
            obs.observe(this.wrapper);
        }
    }

    async function initAll() {
        document.querySelectorAll('.eb-fstrip-wrapper:not([data-fstrip-init])').forEach(wrapper => {
            wrapper.setAttribute('data-fstrip-init', '');
            const el = wrapper.querySelector('.eb-fstrip-header');
            if (!el) return;
            new FilmstripReveal(el, wrapper).init();
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initAll);
    } else {
        initAll();
    }
})();
