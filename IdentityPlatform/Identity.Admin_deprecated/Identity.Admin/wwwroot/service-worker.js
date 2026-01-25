self.addEventListener("install", event => {
    event.waitUntil(
        caches.open("identity-admin-cache").then(cache => {
            cache.addAll([
                "/",
                "/manifest.webmanifest"
            ]);
        })
    );
});

self.addEventListener("fetch", event => {
    event.respondWith(
        caches.match(event.request).then(response => {
            return response || fetch(event.request);
        })
    );
});