document.addEventListener("DOMContentLoaded", function () {

    function initPulseRing(section) {
        section.querySelectorAll(".eb-pulse-ring-el").forEach(function (el) { el.remove(); });

        var count   = parseInt(section.getAttribute("data-pulse-count") || "3", 10);
        var speed   = parseFloat(section.getAttribute("data-pulse-speed") || "3");
        var color   = section.getAttribute("data-pulse-color") || "#ffffff";
        var opacity = parseFloat(section.getAttribute("data-pulse-opacity") || "0.6");

        section.style.setProperty("--eb-pulse-color", color);
        section.style.setProperty("--eb-pulse-opacity", opacity);

        var w    = section.offsetWidth;
        var h    = section.offsetHeight;
        var size = Math.max(w, h) * 0.3;

        for (var i = 0; i < count; i++) {
            var ring = document.createElement("div");
            ring.className = "eb-pulse-ring-el";
            ring.style.cssText = [
                "width:"  + size + "px",
                "height:" + size + "px",
                "left:"   + (w / 2 - size / 2) + "px",
                "top:"    + (h / 2 - size / 2) + "px",
                "--eb-pulse-speed:" + speed + "s",
                "animation-delay:" + (i * (speed / count)).toFixed(2) + "s"
            ].join(";");
            section.appendChild(ring);
        }
    }

    document.querySelectorAll(".eb-section-effect-pulse-ring").forEach(function (section) {
        initPulseRing(section);

        var pulseTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) { return m.attributeName && m.attributeName.startsWith("data-pulse-"); });
            if (!changed) return;
            clearTimeout(pulseTimer);
            pulseTimer = setTimeout(function () { initPulseRing(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
