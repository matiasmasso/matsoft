// Increment this to force clients to update
const CACHE_VERSION = "v1";
const CACHE_NAME = `identity-cache-${CACHE_VERSION}`;

self.addEventListener("install", event => {
    console.log("Service worker installing…");

    event.waitUntil(
        caches.open(CACHE_NAME).then(cache => {
            return cache.addAll([
                "./",
                "index.html",
                "manifest.json",
                "favicon.ico",
                "favicon.png",
                "favicon.svg",
                "favicon-96x96.png",
                "icon-192.png",
                "icon-512.png",
                "web.app-manigfest-192x192.png",
                "web.app-manigfest-512x512.png",
                "_framework/blazor.webassembly.js",
                "_framework/dotnet.js",
                "_framework/dotnet.wasm",
                "_framework/dotnet.timezones.blat",
                "_framework/icudt.dat",
                "_framework/icudt_CJK.dat",
                "_framework/icudt_EFIGS.dat",
                "_framework/icudt_no_CJK.dat"
            ]);
        })
    );
});

self.addEventListener("activate", event => {
    console.log("Service worker activating…");

    event.waitUntil(
        caches.keys().then(keys =>
            Promise.all(
                keys
                    .filter(key => key !== CACHE_NAME)
                    .map(key => caches.delete(key))
            )
        )
    );
});

self.addEventListener("fetch", event => {
    const request = event.request;

    // Only handle GET requests
    if (request.method !== "GET") return;

    event.respondWith(
        caches.match(request).then(cached => {
            if (cached) {
                return cached;
            }

            return fetch(request).then(response => {
                // Cache static assets only
                if (response.ok && request.url.startsWith(self.location.origin)) {
                    caches.open(CACHE_NAME).then(cache => {
                        cache.put(request, response.clone());
                    });
                }

                return response;
            });
        })
    );
});
