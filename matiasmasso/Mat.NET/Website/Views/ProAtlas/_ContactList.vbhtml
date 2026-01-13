@ModelType List(Of DTOContact)

<div></div>
@For Each item In Model
    @<div class="Row" data-guid="@item.Guid.ToString()" >
        <div>@Html.Raw(item.FullNom())</div>
    </div>
Next