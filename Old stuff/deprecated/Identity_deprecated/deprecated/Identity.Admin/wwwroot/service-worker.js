// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

// In development, always bypass the service worker
self.addEventListener("install", () => {
    console.log("Service worker installed (dev mode)");
});

self.addEventListener("activate", () => {
    console.log("Service worker activated (dev mode)");
});

// No caching in dev mode
self.addEventListener("fetch", () => { });
