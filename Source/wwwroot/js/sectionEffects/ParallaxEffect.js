document.addEventListener("DOMContentLoaded", function () {

    var parallaxSections = document.querySelectorAll(".eb-section-effect-parallax");

    function updateParallax() {
        parallaxSections.forEach(function (section) {
            var speed  = parseFloat(section.getAttribute("data-parallax-speed")  || "0.25");
            var baseY  = parseFloat(section.getAttribute("data-parallax-base-y") || "50");

            var rect             = section.getBoundingClientRect();
            var windowHeight     = window.innerHeight;
            var sectionCenter    = rect.top + rect.height / 2;
            var distanceFromCenter = sectionCenter - windowHeight / 2;
            var offset           = distanceFromCenter * speed;

            section.style.backgroundPosition = "center calc(" + baseY + "% + " + offset + "px)";
        });
    }

    window.addEventListener("scroll", updateParallax);
    window.addEventListener("resize", updateParallax);
    updateParallax();

});
