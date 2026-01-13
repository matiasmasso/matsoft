@ModelType DTOTarifa
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oUser = ContextHelper.FindUserSync()
End Code

<div class="pagewrapper">
    @If Model Is Nothing Then
        @<p>No hay tarifas registradas con este usuario</p>
    Else
        If oUser.Rol.IsStaff Or oUser.Rol.IsRep Then
            @<section class="PageTitle">
                <a href="@FEB.Contact.Url(Model.Customer)">
                    @Model.Customer.FullNom
                </a>
            </section>
        End If
        @<div class="Grid">
             <div class="RowHdr">
                 <div class="CellTxt">@ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</div>
                 @Select Case oUser.Rol.id
                     Case DTORol.Ids.CliLite
                     Case Else
                        @<div Class="CellAmt">@ContextHelper.Tradueix("Coste", "Cost", "Cost")</div>
                 End Select
                 <div class="CellAmt">@ContextHelper.Tradueix("PVP", "PVP", "RRPP")</div>
             </div>
            @For Each oBrand As DTOProductBrand In Model.Brands

                @<div class="Row">
                    <div class="CellTxt">&nbsp;</div>
                    <div class="CellAmt">&nbsp;</div>
                    <div class="CellAmt">&nbsp;</div>
                </div>
                @<div class="Row">
                    <div class="CellTxt Brand">@oBrand.Nom</div>
                    <div class="CellAmt">&nbsp;</div>
                    <div class="CellAmt">&nbsp;</div>
                </div>
                @For Each oCategory As DTOProductCategory In oBrand.Categories
                    @<div class="Row">
                        <div class="CellTxt">&nbsp;</div>
                        <div class="CellAmt">&nbsp;</div>
                        <div class="CellAmt">&nbsp;</div>
                    </div>
                    @<div class="Row">
                        <div class="CellTxt Category">@oCategory.Nom</div>
                        <div class="CellAmt">&nbsp;</div>
                        <div class="CellAmt">&nbsp;</div>
                    </div>
                    For Each oSku As DTOProductSku In oCategory.Skus
                    @<div class="Row">
                        <div class="CellTxt Sku">
                            <a href="@oSku.GetUrl(ContextHelper.Lang)">
                                @oSku.nom.Tradueix(ContextHelper.lang())
                            </a>
                        </div>

                        @Select Case oUser.Rol.id
                            Case DTORol.Ids.CliLite
                            Case Else
                                @<div Class="CellAmt">
                                    @DTOAmt.CurFormatted(oSku.price)
                                 </div>
                        End Select

                        <div class="CellAmt">
                            @DTOAmt.CurFormatted(oSku.RRPP)
                        </div>
                    </div>
                    Next
                Next
            Next
        </div>
    End If
</div>

@Section Styles
    <style>
        .pagewrapper {
            max-width:500px;
            margin:auto;
        }
        .Brand {
            text-align:center;
            font-weight:700;
        }
        .Category {
            font-weight:600;
        }
        .Sku {
            padding-left:20px;
        }
    </style>
End Section
