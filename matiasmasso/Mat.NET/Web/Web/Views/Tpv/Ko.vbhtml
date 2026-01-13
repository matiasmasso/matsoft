@ModelType DTOTpvLog
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oLang = DTOTpvLog.Lang(Model)

    Dim sAmt As String = DTOAmt.CurFormatted(DTOTpvLog.Amt(Model))
    Dim esp As String = "La transacción no se ha llevado a cabo.<br/>Puede volverlo a intentar o cursar transferencia por " & sAmt & " a la siguiente cuenta:"
    Dim cat As String = "La seva transacció no s'ha dut a terme.<br/>Pot tornar-ho a provar o cursar una transferecia per " & sAmt & " al següent compte bancari:"
    Dim eng As String = "Your transaction has failed.<br/>You may try it again or you may transfer the amount of " & sAmt & " to next account:"
    Dim sText As String = oLang.Tradueix(esp, cat, eng)

    Dim exs As New List(Of Exception)
    Dim bancGuid As String = FEB2.Default.EmpValueSync(Mvc.GlobalVariables.Emp, DTODefault.Codis.BancToReceiveTransfers, exs)
    Dim oBanc As DTOBanc = FEB2.Banc.FindSync(New Guid(bancGuid), exs)
End Code

<fieldset>
    <legend>Pago denegado</legend>
    <p>
        @Html.Raw(sText)
    </p>

    <img src="@FEB2.Iban.ImgUrl(oBanc.Iban.Digits)"/>

    <input type="hidden" value="@Model.Ds_Order"/>
</fieldset>