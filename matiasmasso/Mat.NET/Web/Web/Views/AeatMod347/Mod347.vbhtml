@ModelType List(Of DTOAeatMod347Item)
@Code
    ViewBag.Title = "Modelo 347"
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim exs As New List(Of Exception)

    Dim oLang As DTOLang = Mvc.ContextHelper.lang()
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oContacts As List(Of DTOContact) = FEB2.Contacts.RaonsSocialsSync(exs, oUser)
End Code



    @If Model Is Nothing Then

        @<div>
            <h1>@Mvc.ContextHelper.Tradueix("Modelo", "Model", "Form") 347</h1>
            <p>
                Lo sentimos, no nos consta ninguna razón social asignada a @oUser.EmailAddress
            </p>
            <p>
                Si cree que se trata de un error, por favor póngase en contacto con nuestras oficinas.
            </p>
        </div>

    ElseIf Model.Count = 0 Then

        @<div>
    <h1>@Mvc.ContextHelper.Tradueix("Modelo", "Model", "Form") 347</h1>
    <p>
        No nos constan operaciones declarables devengadas durante este ejercicio.
    </p>
    <p>
        Si cree que se trata de un error, por favor póngase en contacto con nuestras oficinas.
    </p>
</div>

    Else
        Dim item As DTOAeatMod347Item = Model(0)
        @<div>
            <h1 class="PageTitle">@Mvc.ContextHelper.Tradueix("Modelo", "Model", "Form") 347: @Mvc.ContextHelper.Tradueix("ejercicio", "exercici", "exercise") @item.Parent.Exercici.Year</h1>

            <p>
                @oLang.Tradueix("A continuación detallamos los importes que declaramos a Hacienda sobre nuestra relación comercial durante el ejercicio de referencia.",
                                                                                                                                                    "A continuació detallem els imports que declarem a Hisenda sobre la nostre relació comercial durant l'exercici de referència.",
                                                                                                                                                    "Please see below details we declare to tax authorities about our commercial relationship during referred exercise.")
            </p>

            @If oContacts.Count > 1 Then
                @<p>
                @oLang.Tradueix("Consulte sus distintas cuentas seleccionándolas mediante el desplegable de Razón Social o de Nif.",
                                                                        "Consulti diferents comptes seleccionandoles per el desplegable de Rao Social o de Nif.",
                                                                        "Check your distinct accounts selecting them through either the Legal Name or the VAT Number box.")
                </p>
            End If

    <div class="Row">
        <div class="Label">
            @Mvc.ContextHelper.Tradueix("Razón Social", "Rao Social", "Legal Name")
        </div>
        <div class="CellText FullWidth">
            <select id="ContactNom">
                @For Each oContact As DTOContact In oContacts
                    @<option value="@oContact.Guid.ToString" @IIf(oContact.Guid.Equals(item.Contact.Guid), "selected", "")>@oContact.Nom </option>
                Next
            </select>
        </div>
    </div>

    <div class="Row">
        <div class="Label">
            @Mvc.ContextHelper.Tradueix("NIF", "NIF", "VAT Number")
        </div>
        <div class="CellText">
            <select id="ContactNif">
                @For Each oContact As DTOContact In oContacts
                    @<option value="@oContact.Guid.ToString" @IIf(oContact.Guid.Equals(item.Contact.Guid), "selected", "")>@oContact.Nifs.PrimaryNifValue() </option>
                Next
            </select>
        </div>
    </div>

    <div id="Mod347Details">
        @Html.Partial("_Mod347", Model)
    </div>

</div>
    End If

    <p>
        @oLang.Tradueix("Si detecta cualquier discrepancia por favor póngase en contacto con nuestro Departamento de Contabilidad:",
                                                                                                                   "Si detecta qualsevol discrepancia si us plau contacti amb el nostre Departament de Comptabilitat:",
                                                                                                                   "On any mismatch details please contact our Accounts Department:")
    </p>
    <div>Soledad Lizano</div>
    <div>tel.: 932541520</div>
    <div><a href="mailto:cuentas@matiasmasso.es">cuentas@matiasmasso.es</a></div>



@Section Styles
    <link href="~/Media/Css/Aeat.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            max-width: 450px;
            margin:auto;
        }

            .ContentColumn Select {
                padding: 4px 7px 2px 4px;
            }

    </style>
End Section

@Section Scripts
    <script src="~/Media/js/AeatMod347.js"></script>
End Section