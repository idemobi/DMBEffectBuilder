document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll(".eb-section-effect-spotlight").forEach(function (el) {
        var autoMove = el.getAttribute("data-spotlight-auto") === "true";

        el.style.setProperty("--eb-spotlight-x", "50%");
        el.style.setProperty("--eb-spotlight-y", "50%");

        if (autoMove) {
            var t = 0;
            setInterval(function () {
                t += 0.02;
                var x = 50 + 35 * Math.sin(t);
                var y = 50 + 25 * Math.cos(t * 0.7);
                el.style.setProperty("--eb-spotlight-x", x + "%");
                el.style.setProperty("--eb-spotlight-y", y + "%");
            }, 50);
        } else {
            el.addEventListener("mousemove", function (e) {
                var rect = el.getBoundingClientRect();
                var x = ((e.clientX - rect.left) / rect.width) * 100;
                var y = ((e.clientY - rect.top) / rect.height) * 100;
                el.style.setProperty("--eb-spotlight-x", x + "%");
                el.style.setProperty("--eb-spotlight-y", y + "%");
            });
            el.addEventListener("mouseleave", function () {
                el.style.setProperty("--eb-spotlight-x", "50%");
                el.style.setProperty("--eb-spotlight-y", "50%");
            });
        }
    });

});
