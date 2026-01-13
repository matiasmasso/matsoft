@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code



    <h1>@ViewBag.Title</h1>

    <p>
@Html.Raw(Mvc.ContextHelper.Tradueix("Gracias por su registro.<br/>Arriba a la derecha encontrará un menú donde podrá acceder a sus datos y su contraseña", _
                      "Gracies per registrar-se.<br/>A dalt a la dreta hi trobará un menú on tenir acces a les seves dades i la seva clau de pas", _
                      "Thanks for signing up. On the top right corner you'll find a menu to manage your data and password"))
    </p>

