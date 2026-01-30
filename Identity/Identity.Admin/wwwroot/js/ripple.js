window.sidebarRipple = function (elementId, clientX, clientY) {
    const el = document.getElementById(elementId);
    if (!el) return;

    const rect = el.getBoundingClientRect();
    const x = clientX - rect.left;
    const y = clientY - rect.top;

    el.style.setProperty("--ripple-x", `${x}px`);
    el.style.setProperty("--ripple-y", `${y}px`);

    el.classList.remove("rippling");
    // force reflow to restart animation
    void el.offsetWidth;
    el.classList.add("rippling");

    setTimeout(() => {
        el.classList.remove("rippling");
    }, 500);
};
