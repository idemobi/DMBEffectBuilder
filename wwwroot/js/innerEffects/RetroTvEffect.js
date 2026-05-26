(function () {
    'use strict';

    function hexToRgb(hex) {
        const v = parseInt((hex || '#808080').replace('#', ''), 16);
        return { r: (v >> 16) & 255, g: (v >> 8) & 255, b: v & 255 };
    }

    class RetroTv {
        constructor(el) {
            this.el     = el;
            this.screen = el.querySelector('.eb-retrotv-screen');
            this.canvas = el.querySelector('.eb-retrotv-canvas');
            this.ctx    = this.canvas.getContext('2d');
            this.glow   = el.querySelector('.eb-retrotv-glow');
            this.strip  = el.querySelector('.eb-retrotv-strip');

            const raw       = el.querySelector('.eb-retrotv-channels');
            this.channels   = raw ? JSON.parse(raw.textContent) : [];
            this.brand      = el.dataset.brand || '';
            this.autoMs     = parseInt(el.dataset.auto) || 0;

            this.currentCh   = 0;
            this.staticBurst = 0;
            this.switching   = false;
            this.scanOffset  = 0;
            this.glitchTimer = 0;
            this.staticNoise = null;
            this.noiseAge    = 0;
            this.images      = {};
            this.CW = this.CH = 0;

            this._preloadImages();
            this._resize();
            this._buildStrip();
            this._setGlow(this.channels[0]);
            this._bindInputs();
            if (this.autoMs > 0) this._startAuto();
            this._loop();
        }

        _preloadImages() {
            this.channels.forEach((ch, i) => {
                if (ch.image) {
                    const img = new Image();
                    img.src   = ch.image;
                    this.images[i] = img;
                }
            });
        }

        _resize() {
            const rect  = this.screen.getBoundingClientRect();
            this.CW = this.canvas.width  = rect.width  || 520;
            this.CH = this.canvas.height = rect.height || 390;
        }

        _buildStrip() {
            this.channels.forEach((ch, i) => {
                const btn       = document.createElement('button');
                btn.className   = 'eb-retrotv-ch-btn' + (i === 0 ? ' eb-retrotv-active' : '');
                btn.textContent = String(ch.ch || i + 1).padStart(2, '0');
                btn.style.setProperty('--ch-color', ch.color || '#c4893a');
                btn.addEventListener('click', () => this.switchTo(i));
                this.strip.appendChild(btn);
            });
        }

        _updateButtons(idx) {
            this.strip.querySelectorAll('.eb-retrotv-ch-btn').forEach((b, i) => {
                b.classList.toggle('eb-retrotv-active', i === idx);
            });
        }

        _setGlow(ch) {
            if (!ch) return;
            const { r, g, b } = hexToRgb(ch.color || '#9c7c5e');
            const luma = (r * 0.299 + g * 0.587 + b * 0.114) / 255;
            const a    = 0.25 + luma * 0.3;
            this.glow.style.boxShadow = [
                `0 0 18px rgba(${r},${g},${b},${a})`,
                `0 0 45px rgba(${r},${g},${b},${(a * 0.6).toFixed(2)})`,
                `0 0 90px rgba(${r},${g},${b},${(a * 0.3).toFixed(2)})`,
            ].join(',');
        }

        _makeNoise() {
            const off = document.createElement('canvas');
            off.width = this.CW; off.height = this.CH;
            const oc  = off.getContext('2d');
            const id  = oc.createImageData(this.CW, this.CH);
            const d   = id.data;
            for (let i = 0; i < d.length; i += 4) {
                const v = Math.random() * 255 | 0;
                d[i] = d[i+1] = d[i+2] = v;
                d[i+3] = 255;
            }
            oc.putImageData(id, 0, 0);
            return off;
        }

        _drawScreen(ch, staticAlpha) {
            const { ctx, CW, CH } = this;

            // ── Background ──
            if (ch.image) {
                const img = this.images[this.currentCh];
                if (img && img.complete && img.naturalWidth) {
                    ctx.drawImage(img, 0, 0, CW, CH);
                } else {
                    ctx.fillStyle = '#1a1814';
                    ctx.fillRect(0, 0, CW, CH);
                }
            } else {
                ctx.fillStyle = ch.color || '#1a1814';
                ctx.fillRect(0, 0, CW, CH);

                // Phosphor warmth (color channels only)
                const { r, g, b } = hexToRgb(ch.color);
                const luma = (r * 0.299 + g * 0.587 + b * 0.114) / 255;
                const pgA  = 0.12 + luma * 0.15;
                const pg   = ctx.createRadialGradient(CW/2, CH/2, 0, CW/2, CH/2, CH * 0.55);
                pg.addColorStop(0, `rgba(255,255,255,${pgA.toFixed(2)})`);
                pg.addColorStop(1, 'rgba(255,255,255,0)');
                ctx.fillStyle = pg;
                ctx.fillRect(0, 0, CW, CH);
            }

            // ── CRT vignette ──
            const vg = ctx.createRadialGradient(CW/2, CH/2, CH * 0.1, CW/2, CH/2, CH * 0.72);
            vg.addColorStop(0, 'rgba(0,0,0,0)');
            vg.addColorStop(1, 'rgba(0,0,0,0.38)');
            ctx.fillStyle = vg;
            ctx.fillRect(0, 0, CW, CH);

            // ── Moving scanline band ──
            this.scanOffset = (this.scanOffset + 0.4) % CH;
            ctx.fillStyle = 'rgba(255,255,255,0.025)';
            for (let y = this.scanOffset % 80; y < CH; y += 80) {
                ctx.fillRect(0, y, CW, 2);
            }

            // ── Glitch line ──
            this.glitchTimer++;
            if (this.glitchTimer > 180 && Math.random() < 0.03) {
                this.glitchTimer = 0;
                const gy = Math.random() * CH | 0;
                ctx.fillStyle = `rgba(255,255,255,${(Math.random() * 0.4).toFixed(2)})`;
                ctx.fillRect(0, gy, CW, (Math.random() * 3 + 1) | 0);
            }

            // ── Text color palette ──
            const luma = ch.image ? 0.15 : (() => {
                const { r, g, b } = hexToRgb(ch.color);
                return (r * 0.299 + g * 0.587 + b * 0.114) / 255;
            })();
            const dark      = luma >= 0.45;
            const mainColor = dark ? 'rgba(26,24,20,0.80)'    : 'rgba(232,226,217,0.92)';
            const subColor  = dark ? 'rgba(46,24,20,0.55)'    : 'rgba(200,191,173,0.65)';
            const dimColor  = dark ? 'rgba(46,24,20,0.38)'    : 'rgba(200,191,173,0.40)';
            const chNum     = ch.ch || (this.currentCh + 1);

            // ── Channel number (top-left) ──
            ctx.font      = `bold ${CW * 0.075}px 'VT323', monospace`;
            ctx.textAlign = 'left';
            ctx.fillStyle = 'rgba(0,0,0,0.2)';
            ctx.fillText(`CH ${String(chNum).padStart(2, '0')}`, CW*0.05 + 1, CW*0.1 + 1);
            ctx.fillStyle = dark ? 'rgba(26,24,20,0.12)' : 'rgba(232,226,217,0.15)';
            ctx.fillText(`CH ${String(chNum).padStart(2, '0')}`, CW*0.05, CW*0.1);

            // ── Color name (centered) ──
            const nameSize = CW * 0.155;
            ctx.font      = `${nameSize}px 'VT323', monospace`;
            ctx.textAlign = 'center';
            ctx.fillStyle = 'rgba(0,0,0,0.22)';
            ctx.fillText((ch.name || '').toUpperCase(), CW/2 + 2, CH/2 + nameSize*0.34 + 2);
            ctx.fillStyle = mainColor;
            ctx.fillText((ch.name || '').toUpperCase(), CW/2, CH/2 + nameSize*0.34);

            // ── Hex value (color channels) ──
            if (ch.color) {
                ctx.font      = `${CW * 0.055}px 'Share Tech Mono', monospace`;
                ctx.fillStyle = subColor;
                ctx.fillText(ch.color.toUpperCase(), CW/2, CH/2 + nameSize*0.34 + CW*0.075);
            }

            // ── Description ──
            if (ch.desc) {
                ctx.font      = `${CW * 0.038}px 'Share Tech Mono', monospace`;
                ctx.fillStyle = dimColor;
                ctx.fillText(ch.desc, CW/2, CH/2 + nameSize*0.34 + CW*0.13);
            }

            // ── RGB bars (color channels) ──
            if (ch.color) {
                const { r, g, b } = hexToRgb(ch.color);
                const barY = CH - CW * 0.085;
                const barH = CW * 0.025;
                const barW = CW * 0.65;
                const barX = (CW - barW) / 2;
                const segs = [
                    { label: 'R', val: r, color: 'rgba(212,100,100,0.7)' },
                    { label: 'G', val: g, color: 'rgba(100,180,120,0.7)' },
                    { label: 'B', val: b, color: 'rgba(100,150,220,0.7)' },
                ];
                ctx.font = `${CW * 0.032}px 'Share Tech Mono', monospace`;
                segs.forEach((seg, i) => {
                    const by = barY + i * (barH + CW * 0.018);
                    ctx.fillStyle = 'rgba(0,0,0,0.2)';
                    ctx.beginPath(); ctx.roundRect(barX, by, barW, barH, barH/2); ctx.fill();
                    ctx.fillStyle = seg.color;
                    ctx.beginPath(); ctx.roundRect(barX, by, barW * (seg.val / 255), barH, barH/2); ctx.fill();
                    ctx.textAlign = 'right';
                    ctx.fillStyle = dimColor;
                    ctx.fillText(`${seg.label} ${seg.val}`, barX - CW*0.016, by + barH*0.82);
                });
                ctx.textAlign = 'left';
            }

            // ── Static overlay ──
            if (staticAlpha > 0.01) {
                if (!this.staticNoise || this.noiseAge++ > 3) {
                    this.staticNoise = this._makeNoise();
                    this.noiseAge    = 0;
                }
                ctx.globalAlpha = staticAlpha;
                ctx.drawImage(this.staticNoise, 0, 0);
                ctx.globalAlpha = 1;
            }
        }

        switchTo(idx) {
            if (this.switching) return;
            this.switching   = true;
            this.currentCh   = ((idx % this.channels.length) + this.channels.length) % this.channels.length;
            this.staticBurst = 1.0;

            this._updateButtons(this.currentCh);
            this._setGlow(this.channels[this.currentCh]);

            this.screen.classList.add('eb-retrotv-switching');
            setTimeout(() => {
                this.screen.classList.remove('eb-retrotv-switching');
                this.switching = false;
            }, 500);
        }

        next() { this.switchTo(this.currentCh + 1); }
        prev() { this.switchTo(this.currentCh - 1); }

        _bindInputs() {
            this.el.querySelector('[data-action="next"]')?.addEventListener('click', e => { e.stopPropagation(); this.next(); });
            this.el.querySelector('[data-action="prev"]')?.addEventListener('click', e => { e.stopPropagation(); this.prev(); });
            this.screen.addEventListener('click', () => this.next());
            this.screen.addEventListener('keydown', e => {
                if (e.key === 'ArrowRight' || e.key === ' ' || e.key === 'ArrowUp')   { e.preventDefault(); this.next(); }
                if (e.key === 'ArrowLeft'  || e.key === 'ArrowDown')                   { e.preventDefault(); this.prev(); }
                const n = parseInt(e.key);
                if (!isNaN(n) && n >= 1 && n <= 9) this.switchTo(n - 1);
                if (e.key === '0') this.switchTo(9);
            });
        }

        _startAuto() {
            this.autoTimer = setInterval(() => this.next(), this.autoMs);
            this.screen.addEventListener('click', () => {
                clearInterval(this.autoTimer);
                this.autoTimer = setInterval(() => this.next(), this.autoMs);
            });
        }

        _loop() {
            this.staticBurst = this.staticBurst > 0.01 ? this.staticBurst * 0.82 : 0;
            this._drawScreen(this.channels[this.currentCh] || {}, this.staticBurst);
            requestAnimationFrame(() => this._loop());
        }
    }

    function init() {
        document.querySelectorAll('.eb-retrotv:not([data-tv-init])').forEach(el => {
            el.dataset.tvInit = '1';
            new RetroTv(el);
        });
    }

    if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', init);
    else init();
})();
