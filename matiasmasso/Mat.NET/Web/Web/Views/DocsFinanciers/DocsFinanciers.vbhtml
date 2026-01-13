@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)

    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Documentación Financiera", "Documentació Financera", "Finantial Documents")
    ViewBag.Title = sTitle

End Code


    <h1>@sTitle</h1>

    <div class="DocMercantil">
        <a href="/balances" target="_blank">
            @Mvc.ContextHelper.Tradueix("Balance", "Balanç", "Balance Sheet")
        </a>
    </div>



    @Section Styles
        <style>
            .ContentColumn {
                max-width: 320px;
                margin: 0 auto;
            }

            .DocMercantil {
                margin: 10px 0 10px;
            }
        </style>
    End Section
