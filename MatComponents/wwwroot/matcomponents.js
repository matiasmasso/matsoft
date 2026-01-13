(function () {
    let dragging = false;
    let startX = 0;
    let startWidth = 0;
    let container = null;

    document.addEventListener("mousedown", e => {
        if (!e.target.classList.contains("mat-propertygrid-splitter"))
            return;

        // Agafem el contenidor principal (el primer que trobem)
        container = document.querySelector(".mat-propertygrid-container");
        if (!container) return;

        // Agafem el primer label per saber l'amplada actual
        const firstLabel = container.querySelector(".mat-propertygrid-label");
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
        container.style.setProperty("--mat-propertygrid-label-width", newWidth + "px");
    }

    function onUp() {
        dragging = false;
        document.removeEventListener("mousemove", onMove);
        document.removeEventListener("mouseup", onUp);
    }
})();