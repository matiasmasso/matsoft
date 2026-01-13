@ModelType DTOTpvLog
@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim oLang = DTOTpvLog.Lang(Model)

    Dim esp As String = "Ha sido registrada su transaccion por importe de "
    Dim cat As String = "La seva transacció ha estat registrada per import de "
    Dim eng As String = "Your transaction has been logged for an amount of "
    Dim sText As String = oLang.Tradueix(esp, cat, eng) & DTOAmt.CurFormatted(DTOTpvLog.Amt(Model))


End Code

<fieldset>
    <legend>Confirmación de pago mediante tarjeta de crédito</legend>
    <p>
        @sText
    </p>
</fieldset>



