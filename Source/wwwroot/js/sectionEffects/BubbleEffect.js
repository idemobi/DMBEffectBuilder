document.addEventListener("DOMContentLoaded", function () {

    function initBubble(section) {
        section.querySelectorAll(".eb-bubble-orb").forEach(function (orb) { orb.remove(); });

        var count   = parseInt(section.getAttribute("data-bubble-count")    || "6",  10);
        var colors  = (section.getAttribute("data-bubble-colors") || "#ff6ecf,#efff5c,#00f5d4").split(",");
        var minSize = parseInt(section.getAttribute("data-bubble-min-size") || "100", 10);
        var maxSize = parseInt(section.getAttribute("data-bubble-max-size") || "300", 10);
        var blur    = parseInt(section.getAttribute("data-bubble-blur")     || "50",  10);

        for (var i = 0; i < count; i++) {
            var orb      = document.createElement("div");
            orb.className = "eb-bubble-orb";
            var size     = minSize + Math.random() * (maxSize - minSize);
            var color    = colors[Math.floor(Math.random() * colors.length)];
            var left     = Math.random() * 100;
            var top      = Math.random() * 100;
            var duration = (6 + Math.random() * 8).toFixed(2);
            var delay    = (Math.random() * -12).toFixed(2);
            var rnd      = function () { return ((Math.random() - 0.5) * 80).toFixed(1); };

            orb.style.cssText = [
                "width:"  + size + "px",
                "height:" + size + "px",
                "background-color:" + color,
                "left:"   + left + "%",
                "top:"    + top  + "%",
                "filter:blur(" + blur + "px)",
                "opacity:0.55",
                "--eb-bubble-duration:" + duration + "s",
                "--eb-bubble-delay:"    + delay    + "s",
                "--eb-bubble-dx1:" + rnd() + "px",
                "--eb-bubble-dy1:" + rnd() + "px",
                "--eb-bubble-dx2:" + rnd() + "px",
                "--eb-bubble-dy2:" + rnd() + "px",
                "--eb-bubble-dx3:" + rnd() + "px",
                "--eb-bubble-dy3:" + rnd() + "px"
            ].join(";");

            section.appendChild(orb);
        }
    }

    document.querySelectorAll(".eb-section-effect-bubble").forEach(function (section) {
        initBubble(section);

        var bubbleTimer = null;
        new MutationObserver(function (mutations) {
            var changed = mutations.some(function (m) {
                return m.attributeName && m.attributeName.startsWith("data-bubble-");
            });
            if (!changed) return;
            clearTimeout(bubbleTimer);
            bubbleTimer = setTimeout(function () { initBubble(section); }, 50);
        }).observe(section, { attributes: true });
    });

});
