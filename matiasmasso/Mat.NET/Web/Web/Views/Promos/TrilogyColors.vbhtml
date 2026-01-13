@ModelType List(Of DTO.DTOProductSku)
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oUser As DTO.DTOUser = oWebsession.User
    Dim oCustomers As List(Of DTO.DTOCustomer) = BLL.BLLUser.CustomersForBasket(oUser)

End Code

<div class="pagewrapper">
    <div class="PageTitle">
        Promoción Trilogy Colors
    </div>

    <div class="DataCollection">

        <section id="CustomerSelection" @IIf(oCustomers.Count = 1, "", "hidden = 'hidden'") data-customer='@IIf(oCustomers.Count = 1, oCustomers(0).Guid.ToString, "")'>
            @Html.Partial("_UsuariCustomerSelection", oCustomers)
        </section>

        <p>
            Por favor seleccione su pedido de implantación.<br />
            Mínimo una unidad del color que desee.<br />
            10% de descuento a partir de 2 unidades surtidas.
        </p>

        <div class="query">
            @For Each oSku As DTO.DTOProductSku In Model
                @<div>
                    @Html.Partial("_ProductQty", oSku)
                </div>
            Next
        </div>

        <div Class="submit">
            <input type="button" value="¡Apúntame!" />
        </div>

    </div>


    <div class="Closure" hidden="hidden">
        Gracias por su pedido. Confirmamos su participación en la promoción Trilogy Colors.
    </div>

</div>

@Section scripts
    <script>
        $(document).on("click", ".submit input", function () {
            var json=[];
            $("[data-sku]").each(function (index) {
                var j=new Object;
                j.cli = $("#CustomerSelection").data("customer")
                j.sku = $(this).data("sku")
                j.qty = $(this).find("select option:selected").val();

                json.push(j);
            });

            var data = JSON.stringify(json);

            $('.loading').show();
            $.post('TrilogyColorsUpdate',
                { data: data },
                function () {
                    $('.loading').hide();
                    $('.DataCollection').hide();
                    $('.Closure').show();
                });
        });
    </script>
End Section

@Section Styles
    <style>
        .pagewrapper {
            width:320px;
        }
        .query {
        }
        div[data-sku] {
           margin:10px 0;
        }
        div[data-sku] img,div[data-sku] a, div[data-sku] select {
            vertical-align:middle;
        }
        .SkuThumb {
            display:inline-block;
            width:50px;
        }
        .SkuThumb img {
            width:100%;
        }
 .submit {
     margin-top:20px;
    text-align:right;
    }
    </style>

End Section
