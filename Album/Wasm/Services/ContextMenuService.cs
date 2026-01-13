using Microsoft.AspNetCore.Components;

namespace Wasm.Services
{

    public class ContextMenuService
    {
        public event Func<ContextMenuRequest, Task>? OnShow;
        public event Func<Task>? OnHide;

        public Task ShowAsync(double x, double y, RenderFragment content)
        {
            return OnShow?.Invoke(new ContextMenuRequest
            {
                X = x,
                Y = y,
                Content = content
            }) ?? Task.CompletedTask;
        }

        public Task HideAsync()
        {
            return OnHide?.Invoke() ?? Task.CompletedTask;
        }
    }

    public class ContextMenuRequest
    {
        public double X { get; set; }
        public double Y { get; set; }
        public RenderFragment? Content { get; set; }
    }

}
