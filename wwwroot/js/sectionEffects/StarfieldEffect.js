document.addEventListener("DOMContentLoaded", function () {

    function initStarfield(section) {
        var existing = section.querySelector("canvas.eb-starfield-canvas");
        if (existing) existing.remove();
        if (section._starfieldRaf) { cancelAnimationFrame(section._starfieldRaf); section._starfieldRaf = null; }

        var canvas = document.createElement("canvas");
        canvas.className = "eb-starfield-canvas";
        section.appendChild(canvas);

        var ctx   = canvas.getContext("2d");
        var color = section.getAttribute("data-starfield-color") || "#ffffff";
        var count = parseInt(section.getAttribute("data-starfield-count") || "120", 10);
        var speed = parseFloat(section.getAttribute("data-starfield-speed") || "1.5");

        function resize() {
            canvas.width  = section.offsetWidth;
            canvas.height = section.offsetHeight;
        }
        resize();
        new ResizeObserver(resize).observe(canvas);

        var stars = [];
        for (var i = 0; i < count; i++) {
            stars.push({
                angle:   Math.random() * Math.PI * 2,
                dist:    Math.random() * Math.max(canvas.width || 300, canvas.height || 200) * 0.5,
                speed:   (0.5 + Math.random()) * speed,
                size:    Math.random() * 1.5 + 0.3,
                opacity: Math.random() * 0.6 + 0.2
            });
        }

        function animate() {
            var cx      = canvas.width  / 2;
            var cy      = canvas.height / 2;
            var maxDist = Math.max(canvas.width, canvas.height) * 0.75;

            ctx.clearRect(0, 0, canvas.width, canvas.height);
            stars.forEach(function (s) {
                s.dist += s.speed;
                if (s.dist > maxDist) {
                    s.dist    = 0;
                    s.angle   = Math.random() * Math.PI * 2;
                    s.opacity = Math.random() * 0.6 + 0.2;
                }

                var x    = cx + Math.cos(s.angle) * s.dist;
                var y    = cy + Math.sin(s.angle) * s.dist;
                var fade = s.dist / maxDist;
                var size = s.size + fade * 1.5;

                ctx.beginPath();
                ctx.arc(x, y, size, 0, Math.PI * 2);
                ctx.fillStyle   = color;
                ctx.globalAlpha = s.opacity * fade;
                ctx.fill();
            });
            section._starfieldRaf = requestAnimationFrame(animate);
        }
        animate();
    }

    document.querySelectorAll(".eb-section-effect-starfield").forEach(function (section) {
        initStarfield(section);

        var starTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) { return m.attributeName && m.attributeName.startsWith("data-starfield-"); });
            if (!changed) return;
            clearTimeout(starTimer);
            starTimer = setTimeout(function () { initStarfield(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
