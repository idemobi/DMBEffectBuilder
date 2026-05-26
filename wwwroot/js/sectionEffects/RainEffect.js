document.addEventListener("DOMContentLoaded", function () {

    function initRain(section) {
        var existing = section.querySelector("canvas.eb-rain-canvas");
        if (existing) existing.remove();
        if (section._rainRaf) { cancelAnimationFrame(section._rainRaf); section._rainRaf = null; }

        var canvas = document.createElement("canvas");
        canvas.className = "eb-rain-canvas";
        section.appendChild(canvas);

        var ctx        = canvas.getContext("2d");
        var color      = section.getAttribute("data-rain-color")   || "#00ff41";
        var maxOpacity = parseFloat(section.getAttribute("data-rain-opacity") || "0.6");
        var dropCount  = parseInt(section.getAttribute("data-rain-drops")    || "80", 10);

        function resize() {
            canvas.width  = section.offsetWidth;
            canvas.height = section.offsetHeight;
        }
        resize();
        new ResizeObserver(resize).observe(canvas);

        var drops = [];
        for (var i = 0; i < dropCount; i++) {
            drops.push({
                x:       Math.random(),
                y:       Math.random(),
                length:  Math.random() * 15 + 8,
                speed:   (Math.random() * 2 + 1.5) / 1000,
                opacity: Math.random() * maxOpacity
            });
        }

        function animate() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            drops.forEach(function (drop) {
                ctx.save();
                ctx.strokeStyle = color;
                ctx.globalAlpha = drop.opacity;
                ctx.lineWidth   = 1.5;
                ctx.beginPath();
                ctx.moveTo(drop.x * canvas.width, drop.y * canvas.height);
                ctx.lineTo(drop.x * canvas.width - 1, drop.y * canvas.height + drop.length);
                ctx.stroke();
                ctx.restore();
                drop.y += drop.speed;
                if (drop.y > 1) {
                    drop.y = -drop.length / canvas.height;
                    drop.x = Math.random();
                }
            });
            section._rainRaf = requestAnimationFrame(animate);
        }
        animate();
    }

    document.querySelectorAll(".eb-section-effect-rain").forEach(function (section) {
        initRain(section);

        var rainTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) {
                return m.attributeName && m.attributeName.startsWith("data-rain-");
            });
            if (!changed) return;
            clearTimeout(rainTimer);
            rainTimer = setTimeout(function () { initRain(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
