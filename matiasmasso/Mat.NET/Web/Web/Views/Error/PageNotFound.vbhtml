@ModelType DTOWebErr
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<h1>@ViewBag.Title</h1>

<p>
    @lang.Tradueix("Lamentamos informarle que no hemos podido encontrar la página que nos solicita.",
                               "Lamentem informar que no hem trobat la pàgina que ens demana.",
                               "We are sorry we could not find the page you are requesting",
                                "Lamentamos informar que não podemos encontrar a página solicitada.")
</p>
<p>
<a href="@Model.Url">@Model.Url</a>
</p>
<p>
    @lang.Tradueix("Rogamos disculpe las molestias.", "Disculpi les molesties.", "Sorry for any inconveniences.", "Rogamos aceite as nossas desculpas pelo inconveniente.")
</p>

