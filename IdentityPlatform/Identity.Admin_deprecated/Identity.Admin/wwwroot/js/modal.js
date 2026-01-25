/* tanca el modal amb escape */
/* registrat a App.razor com <script src="js/modal.js"></script> */

window.addModalEscHandler = function (dotnetRef) {
    document.addEventListener("keydown", function (e) {
        if (e.key === "Escape") {
            dotnetRef.invokeMethodAsync("CloseFromEsc");
        }
    });
};