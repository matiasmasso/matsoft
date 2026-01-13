@ModelType DTOCliProduct
@Code
    ViewBag.Title = "LogoDistribuidorOficial"
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim exs As New List(Of Exception)
    
    Dim oExclusivas = FEB.CliProductsBlocked.Allsync(exs, Model.Customer)
    Dim oBrand As DTOProductBrand = DirectCast(Model.Product, DTOProductBrand)
    Dim validated As Boolean = FEB.CliProductsBlocked.IsAllowed(oExclusivas, Model.Product)
    If validated Then ContextHelper.SetDistribuidorCookie(exs, Me.Context, Model.Customer.Guid)
    FEB.Contact.Load(Model.Customer, exs)
End Code

@If validated Then
    @<div class="pagewrapper">
        <div class="PageTitle">Logotipo de Distribuidor Oficial</div>

        <p>
            MATIAS MASSO, S.A., como importador de @oBrand.Proveidor.FullNom , certifica que el siguiente establecimiento consta como Distribuidor Oficial de la marca @oBrand.Nom :
        </p>

         <p>
             @For Each line As String In DTOAddress.Lines(Model.Customer)
                 @line
                 @<br />
             Next
         </p>
    </div>
Else

    @<div class="pagewrapper Fake">
        Atención:
    <p>
    El establecimiento desde el que ha enlazado a esta página no está autorizado a utilizar nuestro logotipo de Distribuidor Oficial<br />
    Recomendamos que adquiera los productos de @oBrand.Nom exclusivamente en la Red de Distribuidores Oficiales de la marca, que tienen todo el soporte del fabricante y le pueden ofrecer toda la asistencia y garantía.
</p>
    </div>

End If

@Section Styles
    <style>
        .Fake {
            padding: 20px 10px;
            color: red;
            font-weight: bolder;
        }

        .logos {
            text-align: center;
        }

            .logos a {
                display: inline-block;
                width: 150px;
                height: 117px;
            }

                .logos a img {
                    width: 100%;
                }
    </style>
End Section


