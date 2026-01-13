@ModelType DTOLang
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

    <h1>@ViewBag.Title</h1>

    <p>
        @Mvc.ContextHelper.Tradueix("Dinos en qué idioma prefieres que nos comuniquemos:",
                                           "Tria en quin idioma t'estimes mes que ens comuniquem:",
                                           "Please select your favourite language:",
                                           "Por favor selecione seu idioma favorito:")
    </p>
    @Using Html.BeginForm
        @<div class="Form">
            <div>@Html.RadioButtonFor(Function(Model) Model.id, "ESP", New With {.id = DTOLang.ESP})Español</div>
            <div>@Html.RadioButtonFor(Function(Model) Model.id, "CAT", New With {.id = DTOLang.CAT})Català</div>
            <div>@Html.RadioButtonFor(Function(Model) Model.id, "ENG", New With {.id = DTOLang.ENG})English</div>
            <div>@Html.RadioButtonFor(Function(Model) Model.id, "POR", New With {.id = DTOLang.POR})Português</div>

            <div id="submit">
                <input type="submit" />
            </div>
        </div>
    End Using
 

@Section Styles
    <style>
        .ContentColumn {
            max-width:250px;
            margin:0 auto;
        }

        #submit {
            display: flex;
            justify-content: flex-end;
            margin-right: 0;
        }

            #submit input {
                padding: 7px 20px;
                margin-right: 0;
                border: 1px solid cornflowerblue;
                border-radius: 5px;
                background-color: cornflowerblue;
                color: white;
            }

                #submit input:hover {
                    background-color: aqua;
                    color: white;
                }

    </style>
End Section
 