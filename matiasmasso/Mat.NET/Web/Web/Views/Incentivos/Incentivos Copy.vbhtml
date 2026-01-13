@Code
    Layout = "~/Views/shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim Model As List(Of DTO.DTOIncentiu) = BLL.BLLIncentius.All()

    ViewData("Title") = oWebSession.Tradueix("incentivos", "incentius", "Incentives")
End Code


<div class="pagewrapper">
    <div class="PageTitle">
        Incentivos vigentes a @Today.ToShortDateString
    </div>

    @For Each oIncentiu As DTO.DTOIncentiu In Model

        @<div class='Collapsed truncate'>
            <a href="#" class="PlusMinus">&nbsp;</a>
            @BLL.BLLIncentiu.Nom(oIncentiu, oWebSession.Lang)
            <div class='Collapsed truncate'>
                <div class="TimeSpan">
                    @String.Format("válido desde {0}", oIncentiu.FchFrom.ToShortDateString)
                    @If oIncentiu.FchTo <> Nothing Then
                        String.Format(" hasta {0}", oIncentiu.FchTo.ToShortDateString)
                    End If
                    @If oIncentiu.OnlyInStk Then
                        @<div>
                            sólo para unidades en existencia
                        </div>
                    End If
                </div>
                <div Class="QtyDtos">
                    @For Each oQtyDto As DTO.DTOQtyDto In oIncentiu.QtyDtos
                        @<div>
                            @String.Format("{0:N1}% dto. a partir de {1:N0} unidades", oQtyDto.Dto, oQtyDto.Qty)
                        </div>
                    Next
                </div>
                <div class="Products">
                    @For Each oProduct As DTO.DTOProduct In oIncentiu.Products
                        @<div>
                            <a href="@BLL.BLLProduct.Url(oProduct)" target="_blank">
                                @BLL.BLLProduct.FullNom(oProduct)
                            </a>
                        </div>
                    Next
                </div>
            </div>
        </div>
    Next
</div>

@Section Styles
<link href="~/Media/Css/PlusMinus.css" rel="stylesheet" />
    <style>
        .TimeSpan {
            color:green;
            margin:10px 0 10px 20px;
        }
        .QtyDtos {
            color:red;
            margin:10px 0 10px 30px;
        }
        .Products {
            margin:10px 0 20px 40px;
        }
    </style>
End Section

@Section Scripts
    <script src="~/Media/js/PlusMinus.js"></script>
End Section
