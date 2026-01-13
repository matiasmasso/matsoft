@ModelType Mvc.IfemaGuestViewModel
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    
    Dim exs As New List(Of Exception)

    Dim oActivitats As New List(Of SelectListItem)
    For Each itm As DTOIfemaGuest.Activitats In [Enum].GetValues(GetType(DTOIfemaGuest.Activitats))
        If itm = DTOIfemaGuest.Activitats.NotSet Then
            oActivitats.Add(New SelectListItem() With {.Value = 0, .Text = Mvc.ContextHelper.Tradueix("(seleccionar una actividad)", "(triar una activitat)", "(pick your main activity)")})
        Else
            oActivitats.Add(New SelectListItem() With {.Value = CInt(itm), .Text = itm.ToString.Replace("_", " ")})
        End If
    Next

    Dim oCountryListItems As New List(Of SelectListItem)
    For Each oCountry As DTOCountry In FEB2.Countries.AllSync(Mvc.ContextHelper.lang(), exs)
        oCountryListItems.Add(New SelectListItem() With {
                              .Value = oCountry.ISO,
                              .Text = oCountry.LangNom.Tradueix(Mvc.ContextHelper.lang())})
    Next


End Code


@Using (Html.BeginForm()) ' "Edit", "State", FormMethod.Post,  Nothing))
    
    @<fieldset class="mat_form" id="mat_form">

        <legend>@Mvc.ContextHelper.Tradueix("Registro de Visitante", "Registre de Visitant", "Sign up form")</legend>

         @Html.Partial("_ValidationSummary", ViewData.ModelState)

        <ul>
            <li>
                <label for='TextboxEmail'>email</label>
                @Html.TextBoxFor(Function(Model) Model.EmailAddress, New With {.disabled = True})
            </li>
            <li>
                <label for='TextboxNom'>@Mvc.ContextHelper.Tradueix("Nombre", "Nom", "First Name")</label>
                @Html.TextBoxFor(Function(Model) Model.Nom)
            </li>
            <li>
                <label for='TextboxCognoms'>@Mvc.ContextHelper.Tradueix("Apellidos", "Cognoms", "Surname")</label>
                @Html.TextBoxFor(Function(Model) Model.Cognoms)
            </li>
            <li>
                <label for="TextboxCargo">@Mvc.ContextHelper.Tradueix("Cargo", "Carrec", "Position")</label>
                @Html.TextBoxFor(Function(Model) Model.Cargo)
            </li>
            <li>
                <label for="TextboxNIF">@Mvc.ContextHelper.Tradueix("NIF", "NIF", "VAT number")</label>
                @Html.TextBoxFor(Function(Model) Model.Nif)
            </li>
            <li>
                <label for="TextboxEmpresa">@Mvc.ContextHelper.Tradueix("Empresa", "Empresa", "Business name")</label>
                @Html.TextBoxFor(Function(Model) Model.Empresa)
            </li>
            <li>
                <label for="DropdownlistActividad">@Mvc.ContextHelper.Tradueix("Actividad", "Activitat", "Activity")</label>
                @Html.DropDownListFor(Function(Model) Model.Actividad, oActivitats, New With {.selectedValue = Model.Actividad})
            </li>
            <li>
                <label for="TextboxDireccion">@Mvc.ContextHelper.Tradueix("Dirección", "Adreça", "Address")</label>
                @Html.TextBoxFor(Function(Model) Model.Direccion)
            </li>
            <li>
                <label for="TextboxZip">@Mvc.ContextHelper.Tradueix("Código postal", "Codi postal", "Zip code")</label>
                @Html.TextBoxFor(Function(Model) Model.Zip)
            </li>
            <li>
                <label for="TextboxPoblacion">@Mvc.ContextHelper.Tradueix("Población", "Població", "Location")</label>
                @Html.TextBoxFor(Function(Model) Model.Poblacion)
            </li>
            <li>
                <label for="TextboxPais">@Mvc.ContextHelper.Tradueix("Pais", "Pais", "Country")</label>
                @Html.DropDownListFor(Function(Model) Model.CountryCod, oCountryListItems, New With {.selectedValue = Model.CountryCod})
            </li>
            <li>
                <label for="TextboxTel">@Mvc.ContextHelper.Tradueix("Teléfono", "Telefon", "Phone number")</label>
                @Html.TextBoxFor(Function(Model) Model.tel)
            </li>
            <li>
                <label for="TextboxMovil">@Mvc.ContextHelper.Tradueix("Movil", "Mobil", "Cell phone")</label>
                @Html.TextBoxFor(Function(Model) Model.movil)
            </li>
            <li>
                <label for="TextboxFax">Fax</label>
                @Html.TextBoxFor(Function(Model) Model.Fax)
            </li>
            <li>
                <label>&nbsp;</label>
                <input type="submit" id="signup" class="submit" value='@Mvc.ContextHelper.Tradueix("Aceptar","Acceptar","Submit","Aceitar")' />
            </li>
        </ul>
    </fieldset>
End Using

@Section Styles
<style>
    fieldset {
        margin:0 10px;
    }

    fieldset ul {
    list-style-type: none;
    }

    fieldset li {
     padding:10px 0 0 5px;
    }

    fieldset label {
        display:inline-block;
        width:120px;
    }

    fieldset input[type=text], fieldset select {
        display:inline-block;
        width:300px;
    }
    fieldset input[type=submit] {
        margin:20px 0 0 0;
        width:300px;
    }
    </style>
End Section