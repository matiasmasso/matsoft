@ModelType DTOCliApertura.Collection
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<h1>@ViewBag.Title</h1>


<div class="Info">
    @If Model.Open.Count = 0 Then
        @String.Format(lang.tradueix("Se han encontrado {0} aperturas, ninguna de ellas por gestionar", "S'han trobat {0} apertures, cap d'elles per gestionar", "{0} cases found, all of them already managed"), Model.Closed.Count)
    Else
        @String.Format(lang.tradueix("Se han encontrado {0} aperturas, {1} de ellas por gestionar", "S'han trobat {0} apertures, {1} de elles per gestionar", "{0} cases found, {1} of them to be managed yet"), Model.Closed.Count, Model.Open.Count)
    End If
</div>


@For Each item In Model.Open()

    @<a href="@item.Url()">
        <div class="Cell">
            <div>@Format(item.FchCreated, "dd/MM/yy")</div>
            <div>@item.FullNom()</div>
            <div>@item.FullLocation()</div>
        </div>
    </a>

Next

<a href="#" class="ShowMore" onclick="(function () { $('.Cell.Closed').show(); return false;})()">@lang.tradueix("Ver aperturas gestionadas", "Veure apertures gestionades", "See closed openings")</a>

@For Each item In Model.Closed()

    @<a href="@item.Url()">
         <div class="Cell" hidden>
             <div>@item.StatusLabel(lang)</div>
             <div>@Format(item.FchCreated, "dd/MM/yy")</div>
             <div>@item.FullNom()</div>
             <div>@item.FullLocation()</div>
         </div>
    </a>

Next

@Html.Partial("_AvailableOnAppstore")

@Section Styles
    <style scoped>
        .Cell {
            padding: 10px 7px 7px 0;
            border-bottom: 1px solid grey;
        }

        .ShowMore, .Info {
            display: block;
            padding: 20px 7px 20px 0;
        }
    </style>

End Section

