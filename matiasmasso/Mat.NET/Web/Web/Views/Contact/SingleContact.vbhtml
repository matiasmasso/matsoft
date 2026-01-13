@ModelType DTOContact
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"

    Dim exs As New List(Of Exception)
    Dim oUser = Mvc.ContextHelper.FindUserSync()
    'Dim googletext = DTOAddress.GoogleNormalized(Model.Address)
    'Dim sGoogleStaticMap As String = MatHelperStd.GoogleMapsHelper.StaticImageUrl(googletext)  ' "http://maps.googleapis.com/maps/api/staticmap?center=Diagonal+403,Barcelona,Spain&scale=2&size=640x320&markers=color:red%7CDiagonal+403,Barcelona,Spain&sensor=false" ' BLL.BLLGoogleMaps.StaticImageUrl(Model.Address) ' "http://maps.googleapis.com/maps/api/staticmap?center=Doctor+Just,+10,03007,Alicante,Espa&#241;a&amp;zoom=14&amp;size=640x320&amp;markers=color:red%7CDoctor+Just,+10,03007,Alicante,Espa&#241;a&amp;sensor=true_or_false&amp;key=AIzaSyCUJivhLPW2XSvKRRr_K1Fdlw1UO5QuiU0" ' BLL.BLLGoogleMaps.StaticImageUrl(Model.Address)"
    Dim lang = Mvc.ContextHelper.Lang
End Code


<h1>@ViewBag.Title</h1>

<div class="Cell">
    @For Each line As String In DTOAddress.Lines(Model)
        @<div>@line</div>
    Next
</div>

<!-- telefons -->
<div class="Cell Phones">
    <a href="#" >@lang.Tradueix("Teléfonos", "Telèfons", "Phone numbers")</a>
</div>
@For Each oTel In Model.Tels
    @<div class="Cell Phone" hidden>
        tel: <a href="tel: @DTOContactTel.HtmlLink(oTel)">@DTOContactTel.Formatted(oTel)</a>
    </div>
Next
<!-- emails -->
<div class="Cell Emails">
    <a href="#">@lang.Tradueix("Emails")</a>
</div>
@For Each oEmail In Model.Emails
    @<div class="Cell Email" hidden>
        <a href="mailto: @oEmail.EmailAddress">@oEmail.EmailAddress</a>
    </div>
Next

<a href="/tarifas/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Tarifas", "Tarifes", "Price lists", "Tarifas")</a>
<a href="/pedidos/pendientes/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Pedidos pendientes de entrega", "Comandes pendents d'entrega", "Open orders book", "Pedidos pendientes de entrega")</a>
<a href="/RepBasketForCustomer/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Cursar pedido", "Cursar comanda", "New order", "Fazer pedido")</a>
<a href="/pedidos/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Pedidos", "Comandes", "Purchase orders", "Pedidos")</a>
<a href="/albaranes/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Envíos", "Enviaments", "Shipments", "Envios")</a>
<a href="/facturas/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Facturas", "Factures", "Invoices", "Faturas")</a>
<a href="/diari/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Diario", "Diari", "Diary", "Diário")</a>
<a href="/SumasYSaldos/FromContact/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Cuentas", "Comptes", "Accounts", "Contas")</a>
<a href="/customer/raports/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Informes de visita", "Raports de visita", "Visit reports", "Informes")</a>
<a href="/raport/forCustomer/@Model.Guid.ToString()" class="Cell menuItem">@lang.Tradueix("Redactar informe de visita", "Redactar raport de visita", "New visit report", "Novo informe")</a>


@Section Styles
    <style scoped>
        .Cell.Phone:before, .Cell.Phones:before {
            content: "\2706";
            display: inline-block;
        }

        .Cell.Email:before, .Cell.Emails:before {
            content: "\2709";
            display: inline-block;
        }

        .menuItem {
            display: block;
        }

        @@media screen and (max-width:700px) {
            .Cell {
                padding: 15px 7px 10px 0px;
                border-bottom: 1px solid gray;
            }
        }
    </style>

End Section

@Section Scripts
    <script>
        $(document).on('click', '.Phones', function (e) {
            $('.Phone').toggle();
        });
        $(document).on('click', '.Emails', function (e) {
            $('.Email').toggle();
        });
    </script>

End Section

