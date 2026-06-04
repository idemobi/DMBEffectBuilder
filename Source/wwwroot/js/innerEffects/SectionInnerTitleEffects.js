document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".eb-scramble").forEach(function (el) {
        const targetText = el.getAttribute("data-scramble-text");
        const duration = parseFloat(el.getAttribute("data-scramble-duration") || "2") * 1000;
        const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#$%&";
        const steps = 20;
        const intervalMs = duration / steps;
        let step = 0;

        const interval = setInterval(function () {
            step++;
            if (step >= steps) {
                el.textContent = targetText;
                clearInterval(interval);
                return;
            }
            const progress = step / steps;
            let result = "";
            for (let i = 0; i < targetText.length; i++) {
                if (targetText[i] === " ") {
                    result += " ";
                } else if (i < targetText.length * progress) {
                    result += targetText[i];
                } else {
                    result += chars[Math.floor(Math.random() * chars.length)];
                }
            }
            el.textContent = result;
        }, intervalMs);
    });

    document.querySelectorAll(".eb-typewriter").forEach(function (el) {
        const duration = parseFloat(el.getAttribute("data-typewriter-duration"));
        const fullText = el.textContent.trim();
        const intervalMs = (duration * 1000) / fullText.length;

        el.style.width = "auto";
        el.style.whiteSpace = "normal";
        el.style.overflow = "visible";
        el.style.borderRight = "none";
        el.style.animation = "none";

        el.innerHTML =
            '<span class="eb-tw-text"></span>' +
            '<span class="eb-tw-cursor" style="border-right: 3px solid; margin-left: 1px; animation: eb-typewriter-blink 0.5s step-end infinite alternate;"></span>';

        const textSpan = el.querySelector(".eb-tw-text");
        const cursorSpan = el.querySelector(".eb-tw-cursor");

        let i = 0;
        const interval = setInterval(function () {
            if (i < fullText.length) {
                textSpan.textContent += fullText[i];
                i++;
            } else {
                clearInterval(interval);
                cursorSpan.style.animation = "none";
                cursorSpan.style.borderRight = "none";
            }
        }, intervalMs);
    });
});
