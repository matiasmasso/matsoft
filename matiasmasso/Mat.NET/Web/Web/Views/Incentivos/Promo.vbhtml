@ModelType DTOIncentiu
@Code
    Dim exs As New List(Of Exception)
    Layout = "~/Views/shared/_Layout.vbhtml"

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim oLang As DTOLang = Mvc.ContextHelper.lang()

    ViewBag.Title = Mvc.ContextHelper.Tradueix("incentivo", "incentiu", "Incentive")
End Code


<div class="pagewrapper">
    <div class="PageTitle">
        @Model.Title.Tradueix(oLang)
    </div>

    <section class="Header">
        <div class="Thumbnail">
            <img src="@FEB2.Incentiu.ThumbnailUrl(Model)" />
        </div>
        <div class="Fchs">
            <div class="FchFrom">
                <div class = "Label">
                    @oLang.Tradueix("desde:", "des de:", "from:", "desde:")
                </div>
                <div class="Value">
                    @Model.fchFrom.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                </div>
            </div>
            @If Model.fchTo <> Nothing Then
                @<div Class="FchTo">
                    <div Class = "Label">
                        @oLang.Tradueix("hasta:", "fins a:", "to:", "até:")
                    </div>
                    <div class="Value">
                        @Model.fchTo.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                    </div>
                </div>
            End If
        </div>

   </section>
   
    <section>
        @Html.Raw(Model.Bases.Tradueix(oLang))
    </section>

    @If oUser.Rol.id = DTORol.Ids.Manufacturer Or oUser.Rol.IsStaff Or oUser.Rol.IsRep Then
        Dim oOrders = FEB2.Incentiu.PurchaseOrdersSync(exs, Model, oUser)
        @<section id = "Orders" >
             @If oOrders.Count = 0 Then
                 @<div>
                    @Mvc.ContextHelper.lang().Tradueix("Ningún pedido registrado aún", "Encara no s'ha registrat ninguna comanda", "No orders logged under this promo yet")
                </div>
             Else
                @<div>
                    @String.Format(Mvc.ContextHelper.lang().Tradueix("Registrados {0} pedidos dentro de esta promoción:", "Registrades {0} comandes dins aquesta promoció:", "{0} orders already logged under this promotion:"), oOrders.Count)
                </div>
                 For Each oOrder As DTOPurchaseOrder In oOrders
                    @<div>
                        @String.Format("{0:dd/MM/yy} {0:HH}:{0:mm} {1}", oOrder.UsrLog.fchCreated, oOrder.customer.FullNom)
                    </div>
                 Next
             End If
        </section>
    End If

</div>

@Section Styles

    <style>
        
       
        .pagewrapper .Thumbnail {
            display:inline-block;
            width: 178px;
        }

        .pagewrapper .Thumbnail img {
            width: 100%;
        }

        .pagewrapper .Fchs {
            display:inline-block;
            max-width: 200px;
            vertical-align:top;
            margin-left:10px;
        }
        .pagewrapper .Fchs .FchFrom {
            margin-bottom:10px;
        }


        #Orders div:first-child {
            margin-top: 15px;
            margin-bottom: 10px;
        }
    </style>
End Section


