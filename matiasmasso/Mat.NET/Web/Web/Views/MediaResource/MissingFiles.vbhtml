@ModelType DTOMediaResource.Collection
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
End Code

<h1>@ViewBag.Title</h1>

<div class="grid-container">
@For Each item As DTOMediaResource In Model
    @<div class="grid-item">@item.Nom()</div>
    @<div class="grid-item">@item.Features</div>
Next
</div>

@Section Styles
    <style scoped>
        .grid-container {
            display: grid;
            grid-template-columns: auto auto;
            padding: 10px;
        }

        .grid-item {
            border: 1px solid rgba(0, 0, 0, 0.8);
            padding: 5px;
            font-size: 1em;
            text-align: left;
        }
    </style>
End Section