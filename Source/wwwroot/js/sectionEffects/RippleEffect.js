document.addEventListener("DOMContentLoaded", function () {

    function initRipple(section) {
        var existing = section.querySelector("canvas.eb-ripple-canvas");
        if (existing) existing.remove();
        if (section._rippleRaf) { cancelAnimationFrame(section._rippleRaf); section._rippleRaf = null; }
        if (section._rippleClickHandler) {
            section.removeEventListener("click", section._rippleClickHandler);
            section._rippleClickHandler = null;
        }

        var canvas = document.createElement("canvas");
        canvas.className = "eb-ripple-canvas";
        section.appendChild(canvas);

        var ctx        = canvas.getContext("2d");
        var color      = section.getAttribute("data-ripple-color")   || "#ffffff";
        var maxOpacity = parseFloat(section.getAttribute("data-ripple-opacity") || "0.5");
        var speed      = parseInt(section.getAttribute("data-ripple-speed")    || "3", 10);
        var autoMode   = section.getAttribute("data-ripple-auto") === "true";

        function resize() {
            canvas.width  = section.offsetWidth;
            canvas.height = section.offsetHeight;
        }
        resize();
        new ResizeObserver(resize).observe(canvas);

        var ripples = [];

        function addRipple(x, y) {
            ripples.push({ x: x, y: y, r: 0, opacity: maxOpacity });
        }

        if (autoMode) {
            section._rippleAutoTimer = setInterval(function () {
                addRipple(Math.random() * canvas.width, Math.random() * canvas.height);
            }, 800 + Math.random() * 1200);
        } else {
            section._rippleClickHandler = function (e) {
                var rect = section.getBoundingClientRect();
                addRipple(e.clientX - rect.left, e.clientY - rect.top);
            };
            section.addEventListener("click", section._rippleClickHandler);
        }

        function animate() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ripples = ripples.filter(function (r) { return r.opacity > 0.01; });
            ripples.forEach(function (r) {
                ctx.beginPath();
                ctx.arc(r.x, r.y, r.r, 0, Math.PI * 2);
                ctx.strokeStyle  = color;
                ctx.globalAlpha  = r.opacity;
                ctx.lineWidth    = 2;
                ctx.stroke();
                r.r       += speed;
                r.opacity -= 0.008;
            });
            section._rippleRaf = requestAnimationFrame(animate);
        }
        animate();
    }

    document.querySelectorAll(".eb-section-effect-ripple").forEach(function (section) {
        initRipple(section);

        var rippleTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) {
                return m.attributeName && m.attributeName.startsWith("data-ripple-");
            });
            if (!changed) return;
            clearTimeout(rippleTimer);
            rippleTimer = setTimeout(function () { initRipple(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
