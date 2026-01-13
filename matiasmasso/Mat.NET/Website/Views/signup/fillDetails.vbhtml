@ModelType LeadViewModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)

    Dim i As LeadViewModel.Fases = Model.Fase

    Dim exs As New List(Of Exception)
    Dim oCountryListItems As New List(Of SelectListItem)
    For Each oCountry As DTOCountry In FEB.Countries.AllSync(ContextHelper.Lang(), exs)
        oCountryListItems.Add(New SelectListItem() With {
                              .Value = oCountry.ISO,
                              .Text = oCountry.LangNom.Tradueix(ContextHelper.Lang())})
    Next
    ' Dim oLangTags = DTOLang.All.Select(Function(x) x.Tag).ToList()
End Code


@Using (Html.BeginForm())

    @<div>

    <h1>@ContextHelper.Tradueix("Formulario de registro", "Formulari de registre", "Sign up form", "formulário de inscrição")</h1>

    @Html.Partial("_ValidationSummary", ViewData.ModelState)
    @Html.HiddenFor(Function(Model) Model.Fase)

    <div class="row">
        <div class="label">
            email
        </div>
        <div class="control">
            @Html.TextBoxFor(Function(Model) Model.EmailAddress, New With {.readonly = True, .class = "fullWidth"})
        </div>
    </div>

    <div class="row">
        <div class="label">
            @ContextHelper.Tradueix("nombre", "nom", "first name", "nome")
        </div>
        <div class="control">
            @Html.TextBoxFor(Function(Model) Model.Nom, New With {.maxLength = "50", .class = "fullWidth"})
        </div>
    </div>

    <div class="row">
        <div class="label">
            @ContextHelper.Tradueix("apellidos", "cognoms", "surname", "apelidos")
        </div>
        <div class="control">
            @Html.TextBoxFor(Function(Model) Model.Cognoms, New With {.maxLength = "50", .class = "fullWidth"})
        </div>
    </div>

    <div class="row">
        <div class="label">
            &nbsp;
        </div>
        <div class="control">
            <label>@Html.RadioButtonFor(Function(Model) Model.sex, DTOUser.Sexs.male)@ContextHelper.Tradueix("hombre", "home", "male")</label>
            <label>@Html.RadioButtonFor(Function(Model) Model.sex, DTOUser.Sexs.female)@ContextHelper.Tradueix("mujer", "dona", "female")</label>
        </div>
    </div>

    <div class="row">
        <div class="label">
            <label for="TextboxLang">@ContextHelper.Tradueix("Idioma", "Idioma", "Language", "Idioma")</label>
        </div>
        <div class="control">
            @Html.DropDownList("LangTag", New SelectList(Model.Langs, "Value", "Text", Model.Lang), New With {.width = "200px"})
        </div>
    </div>

    <div class="row">
        <div class="label">
            <label for="TextboxPais">@ContextHelper.Tradueix("Pais", "Pais", "Country", "País")</label>
        </div>
        <div class="control">
            @Html.DropDownListFor(Function(Model) Model.CountryCod, oCountryListItems, New With {.selectedValue = Model.CountryCod, .class = "fullWidth"})
        </div>
    </div>

    <div class="row">
        <div class="label">
            <label for="BirthYear">@ContextHelper.Tradueix("Año de nacimiento", "Any de neixament", "Year of birth", "Ano nascimento")</label>
        </div>
        <div class="control">
            @Html.TextBoxFor(Function(Model) Model.BirthYear, New With {.width = "200px", .maxLength = "4"})
        </div>
    </div>

    <div class="row">
        <div class="label">
            <label for="TextboxTel">@ContextHelper.Tradueix("Teléfono", "Telefon", "Phone number", "Telefone")</label>
        </div>
        <div class="control">
            @Html.TextBoxFor(Function(Model) Model.tel, New With {.width = "200px", .maxLength = "20"})
        </div>
    </div>

    <div class="row">
        <div id="submit">
            <input type="submit" value='@ContextHelper.Tradueix("Aceptar","Acceptar","Submit","Aceitar")' />
        </div>
    </div>

</div>

End Using

@Section Styles
    <style scoped>
        .ContentColumn {
            max-width: 320px;
            margin:0 auto;
        }

        .fullWidth {
            width: 100%;
            min-width: 100%;
            max-width: 100%;
            box-sizing: border-box;
        }

        .row {
            margin-top: 10px;
        }

        #submit {
            display: flex;
            justify-content: flex-end;
            margin-right:0;
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
