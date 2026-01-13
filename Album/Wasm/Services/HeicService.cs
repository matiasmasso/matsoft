namespace Wasm.Services
{
    using DTO;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.JSInterop;

    public class HeicService
    {
        private readonly IJSRuntime _js;

        public HeicService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<string> ConvertHeicAsync(IBrowserFile file)
        {
            var maxSize = 300 * 1024 * 1024; // 300 MB
            if(file.Size > maxSize)
            {
                throw new Exception($"estas pujant un fitxer de {Media.FormattedSize(file.Size)} que supera el limit de {Media.FormattedSize(maxSize)}");
            }
            using var stream = file.OpenReadStream(maxAllowedSize: maxSize);
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            try
            {
                var base64 = await _js.InvokeAsync<string>(
                    "heicConverter.convert",
                    ms.ToArray()
                );
                return base64; // data:image/jpeg;base64,...

            } catch(Exception ex)
            {
            return null; // data:image/jpeg;base64,...
            }
        }

        public bool IsHeic(IBrowserFile file)
        {
            return file.ContentType.Contains("heic", StringComparison.OrdinalIgnoreCase)
                || file.Name.EndsWith(".heic", StringComparison.OrdinalIgnoreCase);
        }
    }

}
