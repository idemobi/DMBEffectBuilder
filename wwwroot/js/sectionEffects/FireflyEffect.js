document.addEventListener("DOMContentLoaded", function () {

    function initFirefly(section) {
        var existing = section.querySelector("canvas.eb-firefly-canvas");
        if (existing) existing.remove();
        if (section._fireflyRaf) { cancelAnimationFrame(section._fireflyRaf); section._fireflyRaf = null; }

        var canvas = document.createElement("canvas");
        canvas.className = "eb-firefly-canvas";
        section.appendChild(canvas);

        var ctx   = canvas.getContext("2d");
        var color = section.getAttribute("data-firefly-color") || "#aaff88";
        var count = parseInt(section.getAttribute("data-firefly-count") || "20", 10);
        var size  = parseFloat(section.getAttribute("data-firefly-size") || "3");

        function resize() {
            canvas.width  = section.offsetWidth;
            canvas.height = section.offsetHeight;
        }
        resize();
        new ResizeObserver(resize).observe(canvas);

        var flies = [];
        for (var i = 0; i < count; i++) {
            flies.push({
                x:          Math.random(),
                y:          Math.random(),
                vx:         (Math.random() - 0.5) * 0.0015,
                vy:         (Math.random() - 0.5) * 0.0015,
                phase:      Math.random() * Math.PI * 2,
                freq:       0.015 + Math.random() * 0.025,
                amp:        0.025 + Math.random() * 0.035,
                blink:      Math.random() * Math.PI * 2,
                blinkSpeed: 0.03 + Math.random() * 0.05
            });
        }

        function animate() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            flies.forEach(function (f) {
                f.phase += f.freq;
                f.blink += f.blinkSpeed;
                f.x += f.vx + Math.sin(f.phase) * f.amp * 0.008;
                f.y += f.vy + Math.cos(f.phase * 0.7) * f.amp * 0.005;

                if (f.x < 0) f.x = 1;
                if (f.x > 1) f.x = 0;
                if (f.y < 0) f.y = 1;
                if (f.y > 1) f.y = 0;

                var alpha = 0.35 + 0.65 * Math.abs(Math.sin(f.blink));
                var px    = f.x * canvas.width;
                var py    = f.y * canvas.height;

                ctx.save();
                ctx.shadowColor = color;
                ctx.shadowBlur  = size * 6;
                ctx.fillStyle   = color;
                ctx.globalAlpha = alpha;
                ctx.beginPath();
                ctx.arc(px, py, size, 0, Math.PI * 2);
                ctx.fill();
                ctx.restore();
            });
            section._fireflyRaf = requestAnimationFrame(animate);
        }
        animate();
    }

    document.querySelectorAll(".eb-section-effect-firefly").forEach(function (section) {
        initFirefly(section);

        var fireflyTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) { return m.attributeName && m.attributeName.startsWith("data-firefly-"); });
            if (!changed) return;
            clearTimeout(fireflyTimer);
            fireflyTimer = setTimeout(function () { initFirefly(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
