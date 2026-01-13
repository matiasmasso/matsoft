@ModelType Mvc.LeadViewModel
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    

    Dim oCountryListItems As New List(Of SelectListItem)
    Dim exs As New List(Of Exception)
    For Each oCountry As DTOCountry In FEB2.Countries.AllSync(Mvc.ContextHelper.lang(), exs)
        oCountryListItems.Add(New SelectListItem() With {
                              .Value = oCountry.ISO,
                              .Text = oCountry.LangNom.Tradueix(Mvc.ContextHelper.lang())})
    Next
End Code

<style scoped>
    .row {
        margin-bottom: 15px;
    }

    .label {
    }

    .control {
        margin-left: 20px;
    }

        .control input[type=text], .control input[type=email], .control select, textarea {
            width: 300px;
            font-size: 1.0em;
            color: navy;
        }

    #signup {
        margin-bottom: 20px;
        text-align: right;
    }
</style>

@Using (Html.BeginForm()) ' "Edit", "State", FormMethod.Post,  Nothing))

    @<fieldset class="mat_form" id="mat_form">

        <legend>@Mvc.ContextHelper.Tradueix("Formulario de registro", "Formulari de registre", "Sign up form", "formulário de inscrição")</legend>

        @Html.Partial("_ValidationSummary", ViewData.ModelState)

        <div class="row">
            <div class="label">
                <label for='TextboxEmail'>email</label>
            </div>
            <div class="control">
                @Html.TextBoxFor(Function(Model) Model.EmailAddress)
            </div>
        </div>

        <div class="row">
            <div class="label">
                <label for='TextboxNom'>@Mvc.ContextHelper.Tradueix("Nombre", "Nom", "First Name", "Nome")</label>
            </div>
            <div class="control">
                @Html.TextBoxFor(Function(Model) Model.Nom)
            </div>
        </div>

        <div class="row">
            <div class="label">
                <label for='TextboxCognoms'>@Mvc.ContextHelper.Tradueix("Apellidos", "Cognoms", "Surname", "Apelidos")</label>
            </div>
            <div class="control">
                @Html.TextBoxFor(Function(Model) Model.Cognoms)
            </div>
        </div>

        <div class="row">
            <div class="label">
                <label>@Html.RadioButtonFor(Function(Model) Model.sex, DTOUser.Sexs.Male)@Mvc.ContextHelper.Tradueix("hombre", "home", "male", "homem")</label>
                <label>@Html.RadioButtonFor(Function(Model) Model.sex, DTOUser.Sexs.Female)@Mvc.ContextHelper.Tradueix("mujer", "dona", "female", "mulher")</label>
            </div>
        </div>

        <div class="row">
            <div class="label">
                <label for="TextboxPais">@Mvc.ContextHelper.Tradueix("Pais", "Pais", "Country", "País")</label>
            </div>
            <div class="control">
                @Html.DropDownListFor(Function(Model) Model.CountryCod, oCountryListItems, New With {.selectedValue = Model.CountryCod})
            </div>
        </div>


        <div class="row">
            <div class="label">
                <label for="BirthYear">@Mvc.ContextHelper.Tradueix("Año de nacimiento", "Any de neixament", "Year of birth", "Ano nascimento")</label>
            </div>
            <div class="control">
                @Html.TextBoxFor(Function(Model) Model.BirthYear, New With {.width = "6em", .maxLength = "4"})
            </div>
        </div>


        <div class="row">
            <div class="label">
                <label for="TextboxTel">@Mvc.ContextHelper.Tradueix("Teléfono", "Telefon", "Phone number", "Telefone")</label>
            </div>
            <div class="control">
                @Html.TextBoxFor(Function(Model) Model.tel)
            </div>
        </div>


        <div class="row">
            <div class="control">
                <input type="submit" id="signup" class="submit" value='@Mvc.ContextHelper.Tradueix("Aceptar", "Acceptar", "Submit", "Aceitar")' />
            </div>
        </div>
    </fieldset>

End Using
