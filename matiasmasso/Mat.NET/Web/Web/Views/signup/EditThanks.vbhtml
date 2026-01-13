@ModelType Mvc.LeadViewModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


    <h1>@Mvc.ContextHelper.Tradueix("Formulario de registro", "Formulari de registre", "Sign up form", "formulário de inscrição")</h1>
    <p>
        @Mvc.ContextHelper.Tradueix("Sus datos han sido modificados correctamente.",
                                            "Les seves dades han estat actualitzades correctament",
                                            "Your details have been successfully updated")
    </p>



@Section Styles
    <style scoped>
        .ComntentColumn {
            max-width: 320px;
            margin: 0 auto;
        }
    </style>
End Section
