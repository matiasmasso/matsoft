@ModelType List(Of Exception)
@Code
    ViewBag.Title = "Error"
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim oUser = ContextHelper.FindUserSync()
    Dim lines = Model.Select(Function(x) x.Data("UserDisplayText"))
End Code

    <div class="Wrapper">

        <h2>Error</h2>

        Lamentamos informarle que se ha producido un error al intentar mostrar esta página:
        <br />
        <br />
        @For Each line In lines
            @<div class="Line">
                @Html.Raw(line)
            </div>
        Next
        <br />
        Si no puede solventarlo, por favor díganos en un email a info@matiasmasso.es qué estaba intentando consultar e intentaremos solventar el problema a la mayor brevedad
        <br />
        Rogamos disculpe las molestias

    </div>
@Code
    ContextHelper.LogErrorSync("Exs.vbhtml")
End Code

@Section Styles
    <style>
        .Wrapper {
            margin: 0 20px 0 20px;
        }
        .Line {
            margin: 20px;
            font-weight: 700;
            color: red;
        }
    </style>
End Section
