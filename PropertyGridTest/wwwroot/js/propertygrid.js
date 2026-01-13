(function () {
    let dragging = false;
    let startX = 0;
    let startWidth = 0;
    let container = null;

    document.addEventListener("mousedown", e => {
        if (!e.target.classList.contains("pg-splitter"))
            return;

        // Agafem el contenidor principal (el primer que trobem)
        container = document.querySelector(".pg-container");
        if (!container) return;

        // Agafem el primer label per saber l'amplada actual
        const firstLabel = container.querySelector(".pg-label");
        if (!firstLabel) return;

        dragging = true;
        startX = e.clientX;
        startWidth = firstLabel.offsetWidth;

        document.addEventListener("mousemove", onMove);
        document.addEventListener("mouseup", onUp);
    });

    function onMove(e) {
        if (!dragging) return;

        const delta = e.clientX - startX;
        const newWidth = Math.max(80, startWidth + delta);

        // TOTES les files comparteixen aquesta variable
        container.style.setProperty("--pg-label-width", newWidth + "px");
    }

    function onUp() {
        dragging = false;
        document.removeEventListener("mousemove", onMove);
        document.removeEventListener("mouseup", onUp);
    }
})();