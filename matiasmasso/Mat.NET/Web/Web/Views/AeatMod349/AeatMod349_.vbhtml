@Modeltype List(Of DTO.DTOModel349)
@Code
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oContact As New DTO.DTOContact
End Code


<section class="Grid">

    <div class="RowHdr">
        <div class="CellTxt">@oWebSession.Tradueix("Nif", "Nif", "Vat #")</div>
        <div class="CellTxt ">@oWebSession.Tradueix("Proveedor", "Proveidor", "Suplier")</div>
        <div class="CellAmt ">@oWebSession.Tradueix("Total anual", "Total anual", "Yearly Total")</div>
        @For i As Integer = 1 To 12
            @<div Class="CellAmt ">@oWebSession.Lang.Mes(i)</div>
        Next
    </div>

    @For Each item As DTO.DTOModel349 In Model
        If item.Contact.UnEquals(oContact) Then
            oContact = item.Contact
            @<div class="Row">
                <div class="CellTxt Nif">
                    @oContact.Nif
                </div>
                <div class="CellTxt Nom">
                    @oContact.Nom
                </div>
                <div class="CellAmt">
                    @BLL.Defaults.AmtFormat(Model.Where(Function(x) x.Contact.Equals(oContact)).Sum(Function(x) x.Amt.Eur))
                </div>
                @For i As Integer = 1 To 12
                    @<div Class="CellAmt">
                        @BLL.Defaults.AmtFormat(Model.Where(Function(x) x.Contact.Equals(oContact) And x.Month = i).Sum(Function(x) x.Amt.Eur))
                    </div>
                Next
            </div>
        End If
    Next

    <div class="Row">
        <div class="CellTxt Nif">
            &nbsp;
        </div>
        <div class="CellTxt Nom">
            @oWebSession.Tradueix("totales", "totals", "totals")
        </div>
        <div class="CellAmt">
            @BLL.Defaults.AmtFormat(Model.Sum(Function(x) x.Amt.Eur))
        </div>
        @For i As Integer = 1 To 12
            @<div Class="CellAmt">
                @BLL.Defaults.AmtFormat(Model.Where(Function(x) x.Month = i).Sum(Function(x) x.Amt.Eur))
            </div>
        Next
    </div>




</section>




