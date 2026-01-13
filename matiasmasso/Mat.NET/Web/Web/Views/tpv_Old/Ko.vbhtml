@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oLog As New MaxiSrvr.TpvLog(Request.QueryString)

    Dim oLang As DTO.DTOLang = oLog.Lang

    Dim sAmt As String = oLog.Amt.CurFormatted
    Dim esp As String = "La transacción no se ha llevado a cabo.<br/>Puede volverlo a intentar o cursar transferencia por " & sAmt & " a la siguiente cuenta:"
    Dim cat As String = "La seva transacció no s'ha dut a terme.<br/>Pot tornar-ho a provar o cursar una transferecia per " & sAmt & " al següent compte bancari:"
    Dim eng As String = "Your transaction has failed.<br/>You may try it again or you may transfer the amount of " & sAmt & " to next account:"
    Dim sText As String = oLang.Tradueix(esp, cat, eng)

    Dim bancGuid As String = BLL.BLLDefault.EmpValue(DTO.DTODefault.Codis.BancToReceiveTransfers)
    Dim oBanc As DTO.DTOBanc = BLL.BLLBanc.Find(New Guid(bancGuid))
End Code

<fieldset>
    <legend>Pago denegado</legend>
    <p>
        @Html.Raw(sText)
    </p>

    <img src="@BLL.BLLIban.ImgUrl(oBanc.Iban.Digits)"/>

    <input type="hidden" value="@oLog.paramValue(MaxiSrvr.TpvLog.Params.Ds_Order)"/>
</fieldset>