@ModelType DTO.DTOFairGuest
@Code
    ViewData("Title") = "QuizConfirmationFairGuest"
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim FchMax As New Date(216, 6, 9)
End Code

<div>

    <p>
        Gracias por registrarse en el Programa de Compradores Invitados de Puericultura Madrid @(FchMax.Year).
    </p>

    <p>
        Confirmamos los datos que vamos a pasar a Ifema el próximo @(FchMax.day) de @(BLL.BLLLang.Mes(BLL.BLLLang.ESP, FchMax.Month)):
    </p>

    <table style="width:100%;font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                Nombre
            </td>
            <td>
                @Model.FirstName
            </td>
        </tr>

        <tr>
            <td>
                Apellidos
            </td>
            <td>
                @Model.LastName
            </td>
        </tr>

        <tr>
            <td>
                Cargo
            </td>
            <td>
                @Model.Position
            </td>
        </tr>

        <tr>
            <td>
                NIF de la empresa
            </td>
            <td>
                @Model.NIF
            </td>
        </tr>

        <tr>
            <td>
                Nombre de la empresa
            </td>
            <td>
                @Model.RaoSocial
            </td>
        </tr>

        <tr>
            <td>
                Actividad
            </td>
            <td>
                @Choose(Model.ActivityCode, "Tienda física", "Tienda online", "Compradores de Franquicias o grandes almacenes","Otras actividades")
            </td>
        </tr>

        <tr>
            <td>
                Dirección
            </td>
            <td>
                @Model.Address
            </td>
        </tr>

        <tr>
            <td>
                Código postal
            </td>
            <td>
                @Model.Zip
            </td>
        </tr>

        <tr>
            <td>
                Población
            </td>
            <td>
                @Model.Location

            </td>
        </tr>

        <tr>
            <td>
                Pais
            </td>
            <td>
                @Model.Country.Nom.Esp
            </td>
        </tr>

        <tr>
            <td>
                Teléfono
            </td>
            <td>
                @Model.Phone
            </td>
        </tr>

        <tr>
            <td>
                Movil
            </td>
            <td>
                @Model.CellPhone
            </td>
        </tr>

        <tr>
            <td>
                Fax
            </td>
            <td>
                @Model.Fax
            </td>
        </tr>

        <tr>
            <td>
                Email
            </td>
            <td>
                @Model.Email
            </td>
        </tr>
        <tr>
            <td>
                Residencia
            </td>
            <td>
                @Choose(Model.CodeDistance, "Resido a menos de 200Km de Madrid", "Resido a más de 200Km de Madrid")
            </td>
        </tr>
    </table>

    <p>
        Reciba un cordial saludo.<br />
        MATIAS MASSO, S.A.
    </p>

</div>

