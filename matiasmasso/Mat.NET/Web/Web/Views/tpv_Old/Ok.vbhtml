@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim oLog As New MaxiSrvr.TpvLog(Request.QueryString)
    Dim oLang As DTO.DTOLang = oLog.Lang

    Dim esp As String = "Ha sido registrada su transaccion por importe de "
    Dim cat As String = "La seva transacció ha estat registrada per import de "
    Dim eng As String = "Your transaction has been logged for an amount of "
    Dim sText As String = oLang.Tradueix(esp, cat, eng) & oLog.Amt.CurFormatted

    Dim exs As New List(Of Exception)
    MaxiSrvr.BLL_Tpv.Procesa(Request.QueryString, exs)

End Code

<fieldset>
    <legend>Confirmación de pago mediante tarjeta de crédito</legend>
    <p>
        @sText
    </p>
</fieldset>



