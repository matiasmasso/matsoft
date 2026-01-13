@ModelType DTOContactMessage
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

End Code

<h1 class="title">@ViewBag.Title</h1>

<div>
    @ContextHelper.Tradueix("Gracias por su mensaje, contestaremos lo antes posible")
</div>

<div>
    @ContextHelper.Tradueix("dirección email:")
    @Model.Email
</div>
<div>
    @ContextHelper.Tradueix("nombre:")
    @Model.Nom
</div>
<div>
    @ContextHelper.Tradueix("población:")
    @Model.Location
</div>
<div>
    @ContextHelper.Tradueix("su consulta:")
    @Model.Text
</div>

@Section Styles
    <style>
        .ContentColumn {
            width: 100%;
            max-width: 600px;
        }
    </style>
End Section 