using MatComponents.PropertyGrid.Internal.Metadata;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MatComponents.PropertyGrid;

public abstract class EditorBase<TValue> : ComponentBase
{
    [CascadingParameter] public EditContext EditContext { get; set; } = default!;

    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }

    [Parameter] public PgPropertyMetadata Metadata { get; set; } = default!;

    protected FieldIdentifier Field => Metadata.Field;

    protected async Task NotifyValueChangedAsync(TValue? value)
    {
        Value = value;

        if (ValueChanged.HasDelegate)
            await ValueChanged.InvokeAsync(value);

        EditContext?.NotifyFieldChanged(Field);
    }
}