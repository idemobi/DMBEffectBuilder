document.addEventListener("DOMContentLoaded", function () {

    function initConfetti(section) {
        var existing = section.querySelector("canvas.eb-confetti-canvas");
        if (existing) existing.remove();
        if (section._confettiRaf) { cancelAnimationFrame(section._confettiRaf); section._confettiRaf = null; }

        var canvas = document.createElement("canvas");
        canvas.className = "eb-confetti-canvas";
        section.appendChild(canvas);

        var ctx    = canvas.getContext("2d");
        var defaults = ["#ff6ecf", "#efff5c", "#00f5d4", "#ff9f43", "#a29bfe"];
        var colors = [1,2,3,4,5].map(function (i) { return section.getAttribute("data-confetti-color" + i) || defaults[i - 1]; });
        var count  = parseInt(section.getAttribute("data-confetti-count") || "60", 10);
        var speed  = parseFloat(section.getAttribute("data-confetti-speed") || "1");

        function resize() {
            canvas.width  = section.offsetWidth;
            canvas.height = section.offsetHeight;
        }
        resize();
        new ResizeObserver(resize).observe(canvas);

        var pieces = [];
        for (var i = 0; i < count; i++) {
            pieces.push({
                x:           Math.random() * (canvas.width  || 400),
                y:           Math.random() * (canvas.height || 200) - (canvas.height || 200),
                w:           6 + Math.random() * 8,
                h:           3 + Math.random() * 5,
                color:       colors[Math.floor(Math.random() * colors.length)],
                vy:          (1 + Math.random() * 2) * speed,
                vx:          (Math.random() - 0.5) * 1.5,
                angle:       Math.random() * Math.PI * 2,
                spin:        (Math.random() - 0.5) * 0.15,
                wobble:      Math.random() * Math.PI * 2,
                wobbleSpeed: 0.05 + Math.random() * 0.05
            });
        }

        function animate() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            pieces.forEach(function (p) {
                p.y      += p.vy;
                p.x      += p.vx + Math.sin(p.wobble) * 0.8;
                p.angle  += p.spin;
                p.wobble += p.wobbleSpeed;

                if (p.y > canvas.height + 20) {
                    p.y     = -20;
                    p.x     = Math.random() * canvas.width;
                    p.color = colors[Math.floor(Math.random() * colors.length)];
                }

                ctx.save();
                ctx.translate(p.x + p.w / 2, p.y + p.h / 2);
                ctx.rotate(p.angle);
                ctx.fillStyle   = p.color;
                ctx.globalAlpha = 0.85;
                ctx.fillRect(-p.w / 2, -p.h / 2, p.w, p.h);
                ctx.restore();
            });
            section._confettiRaf = requestAnimationFrame(animate);
        }
        animate();
    }

    document.querySelectorAll(".eb-section-effect-confetti").forEach(function (section) {
        initConfetti(section);

        var confettiTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) { return m.attributeName && m.attributeName.startsWith("data-confetti-"); });
            if (!changed) return;
            clearTimeout(confettiTimer);
            confettiTimer = setTimeout(function () { initConfetti(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
