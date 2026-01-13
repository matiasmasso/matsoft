window.heicConverter = {
    convert: async function (uint8) {
        console.log("[heicConverter] Starting conversion…");

        try {
            // Convert Uint8Array → pure ArrayBuffer
            const buffer = uint8.buffer.slice(
                uint8.byteOffset,
                uint8.byteOffset + uint8.byteLength
            );

            console.log("[heicConverter] Received bytes:", uint8.length);

            const blob = new Blob([buffer], { type: "image/heic" });
            console.log("[heicConverter] Blob created:", blob);

            const result = await heic2any({
                blob: blob,
                toType: "image/jpeg",
                quality: 0.9
            });

            console.log("[heicConverter] Conversion result:", result);

            return await new Promise((resolve) => {
                const reader = new FileReader();
                reader.onload = () => resolve(reader.result);
                reader.readAsDataURL(result);
            });

        } catch (err) {
            // 🔥 This is the important part
            console.error("[heicConverter] ERROR:", err);

            // If it's a wrapped error (common in heic2any)
            if (err?.message) {
                console.error("[heicConverter] Error message:", err.message);
            }
            if (err?.stack) {
                console.error("[heicConverter] Stack trace:", err.stack);
            }
            if (err?.cause) {
                console.error("[heicConverter] Cause:", err.cause);
            }

            // Re-throw so Blazor sees it
            throw err;
        }
    }
};
