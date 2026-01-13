@ModelType List(Of DTO.DTOPromo)
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    ViewData("Title") = oWebSession.Tradueix("Promociones", "Promocions", "Promotions")
    
End Code


<div class="pagewrapper">
    <div class="PageTitle">@oWebSession.Tradueix("Promociones", "Promocions", "Promotions")</div>
    @If Model.Count = 0 Then
        @<div>
            @oWebSession.Tradueix("No nos constan promociones en su cuenta", "No ens consten promocions al seu compte", "No promotions available on your account")
        </div>
    Else
        @<div>
            <div id="Items">
                <div class="Grid">

                    <div class="RowHdr">
                        <div class="CellTxt">
                            @oWebSession.Tradueix("Concepto", "Concepte", "Concept")
                        </div>
                        <div class="CellFch">
                            @oWebSession.Tradueix("Desde", "Des de", "From")
                        </div>
                        <div class="CellFch">
                            @oWebSession.Tradueix("Hasta", "Fins", "Deadline")
                        </div>
                    </div>

                    @For Each item As DTO.DTOPromo In Model
                        @<div class="Row">
                             <div class="CellTxt">
                                 <a href="@BLL.BLLPromo.Url(item)">
                                     @item.Caption
                                 </a>
                             </div>
                             <div class="CellFch">
                                 @If item.FchFrom <> Nothing Then
                                     @Format(item.FchFrom, "dd/MM/yy")
                                 End If
                             </div>
                             <div class="CellFch">
                                 @If item.FchTo <> Nothing Then
                                    @Format(item.FchTo, "dd/MM/yy")
                                 End If
                             </div>
                        </div>
                    Next
                    
                </div>
            </div>

        </div>
    End If
</div>



