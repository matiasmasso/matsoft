@ModelType List(Of DTO.DTOCustomer)
@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim sTitle As String = oWebSession.Tradueix("Formulario de pedido", "Formulari de Comanda", "Customer Order Form")
    ViewData("Title") = "M+O | " & sTitle
End Code

<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>

    <section id="CustomerSelection">
        @Html.Partial("_UsuariCustomerSelection", Model)
    </section>

    <section id="Details"></section>

    <input type="hidden" id="Basket" value="{}" />
</div>
@Section Styles
    <link href="~/Media/Css/CustomerBasket.css" rel="stylesheet" />
End Section

@Section Scripts
    <script src="~/Media/js/CustomerBasket.js"></script>
    <script src="~/Media/js/UsuariCustomerSelection.js"></script>
End Section