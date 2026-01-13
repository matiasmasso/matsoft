
window.imageGallery = {
    initializeItem: function (element) {
        if (!element) return;

        // dragover highlight
        element.addEventListener("dragover", () => {
            element.classList.add("drag-over");
        });

        element.addEventListener("dragleave", () => {
            element.classList.remove("drag-over");
        });

        // dragstart ghost image
        element.addEventListener("dragstart", (event) => {
            const img = new Image();
            img.src =
                "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMB/axuE2cAAAAASUVORK5CYII=";
            event.dataTransfer.setDragImage(img, 0, 0);
        });
    },


    setDragOver: function (element) {
        if (!element || !element.classList) return;
        element.classList.add("drag-over");
    },

    clearDragOver: function (element) {
        if (!element || !element.classList) return;
        element.classList.remove("drag-over");
    },

    setDragging: function (element) {
        if (!element || !element.classList) return;
        element.classList.add("dragging-item");
    },

    clearDragging: function (element) {
        if (!element || !element.classList) return;
        element.classList.remove("dragging-item");
    },

    addDraggingClass: function () {
        const root = document.querySelector(".imageGallery");
        if (root) root.classList.add("dragging");
    },

    removeDraggingClass: function () {
        const root = document.querySelector(".imageGallery");
        if (root) root.classList.remove("dragging");
    }
};

// Global safety cleanup
window.addEventListener("dragend", () => {
    document.querySelector(".imageGallery")?.classList.remove("dragging");
});
