document.addEventListener("DOMContentLoaded", function () {

    function initLavaLamp(section) {
        var existing = section.querySelector(".eb-lavalamp-layer");
        if (existing) existing.remove();
        if (section._lavaRaf) { cancelAnimationFrame(section._lavaRaf); section._lavaRaf = null; }

        var count  = parseInt(section.getAttribute("data-lavalamp-count")  || "5",  10);
        var colors = (section.getAttribute("data-lavalamp-colors") || "#ff6ecf,#00f5d4,#efff5c").split(",");
        var blur   = parseInt(section.getAttribute("data-lavalamp-blur")   || "30", 10);

        var layer = document.createElement("div");
        layer.className  = "eb-lavalamp-layer";
        layer.style.filter = "blur(" + blur + "px) contrast(15)";
        layer.style.backgroundColor = section.style.backgroundColor || getComputedStyle(section).getPropertyValue("--eb-lavalamp-bg").trim() || "#0d0d0d";
        section.prepend(layer);

        var sW = section.offsetWidth;
        var sH = section.offsetHeight;

        var blobs = [];
        for (var i = 0; i < count; i++) {
            var blob  = document.createElement("div");
            var size  = 80 + Math.random() * 120;
            var color = colors[i % colors.length];
            blob.style.cssText = [
                "position:absolute",
                "border-radius:50%",
                "width:"  + size + "px",
                "height:" + size + "px",
                "background-color:" + color
            ].join(";");
            layer.appendChild(blob);

            blobs.push({
                el:   blob,
                x:    Math.random() * (sW - size),
                y:    Math.random() * (sH - size),
                vx:   (Math.random() - 0.5) * 1.2,
                vy:   (Math.random() - 0.5) * 0.8,
                size: size
            });
        }

        function animateLava() {
            var w = section.offsetWidth;
            var h = section.offsetHeight;
            blobs.forEach(function (b) {
                b.x += b.vx;
                b.y += b.vy;
                if (b.x < 0)          { b.x = 0;          b.vx =  Math.abs(b.vx); }
                if (b.x > w - b.size) { b.x = w - b.size; b.vx = -Math.abs(b.vx); }
                if (b.y < 0)          { b.y = 0;          b.vy =  Math.abs(b.vy); }
                if (b.y > h - b.size) { b.y = h - b.size; b.vy = -Math.abs(b.vy); }
                b.el.style.left = b.x + "px";
                b.el.style.top  = b.y + "px";
            });
            section._lavaRaf = requestAnimationFrame(animateLava);
        }
        animateLava();
    }

    document.querySelectorAll(".eb-section-effect-lavalamp").forEach(function (section) {
        initLavaLamp(section);

        var lavaTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) {
                return m.attributeName && m.attributeName.startsWith("data-lavalamp-");
            });
            if (!changed) return;
            clearTimeout(lavaTimer);
            lavaTimer = setTimeout(function () { initLavaLamp(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
