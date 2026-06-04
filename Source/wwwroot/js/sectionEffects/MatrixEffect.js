document.addEventListener("DOMContentLoaded", function () {

    function initMatrix(section) {
        var existing = section.querySelector("canvas.eb-matrix-canvas");
        if (existing) existing.remove();
        if (section._matrixRaf) { cancelAnimationFrame(section._matrixRaf); section._matrixRaf = null; }

        var canvas = document.createElement("canvas");
        canvas.className = "eb-matrix-canvas";
        section.appendChild(canvas);

        var ctx      = canvas.getContext("2d");
        var color    = section.getAttribute("data-matrix-color")          || "#00ff41";
        var fontSize = parseInt(section.getAttribute("data-matrix-font-size") || "14", 10);
        var speed    = parseFloat(section.getAttribute("data-matrix-speed")   || "1");

        var chars = "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        function resize() {
            canvas.width  = section.offsetWidth;
            canvas.height = section.offsetHeight;
        }
        resize();
        new ResizeObserver(resize).observe(canvas);

        var drops = [];
        var cols  = Math.floor(canvas.width / fontSize);
        for (var i = 0; i < cols; i++) {
            drops.push(Math.random() * -30);
        }

        function animate() {
            var currentCols = Math.floor(canvas.width / fontSize);
            while (drops.length < currentCols) drops.push(0);

            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.font = fontSize + "px monospace";

            for (var i = 0; i < drops.length; i++) {
                var trailLen = 14;
                for (var j = 0; j <= trailLen; j++) {
                    var yPos = (drops[i] - j) * fontSize;
                    if (yPos < 0 || yPos > canvas.height) continue;
                    var ch    = chars[Math.floor(Math.random() * chars.length)];
                    var alpha = j === 0 ? 1 : Math.max(0, 1 - j / trailLen);
                    ctx.fillStyle   = color;
                    ctx.globalAlpha = alpha;
                    ctx.fillText(ch, i * fontSize, yPos);
                }
                ctx.globalAlpha = 1;
                drops[i] += speed * 0.5;
                if (drops[i] * fontSize > canvas.height && Math.random() > 0.975) {
                    drops[i] = 0;
                }
            }
            section._matrixRaf = requestAnimationFrame(animate);
        }
        animate();
    }

    document.querySelectorAll(".eb-section-effect-matrix").forEach(function (section) {
        initMatrix(section);

        var matrixTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) { return m.attributeName && m.attributeName.startsWith("data-matrix-"); });
            if (!changed) return;
            clearTimeout(matrixTimer);
            matrixTimer = setTimeout(function () { initMatrix(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
