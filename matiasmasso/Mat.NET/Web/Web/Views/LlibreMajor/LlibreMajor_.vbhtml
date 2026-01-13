@ModelType  IEnumerable(Of DTOCcb)
@Code
    
    Dim oCcd As New DTOCcd
    oCcd.Exercici = DTOExercici.FromYear(Mvc.GlobalVariables.Emp, 1)
    oCcd.Cta = New DTOPgcCta
    Dim sCtaId As String
    Dim sCtaNom As String
End Code



<div class="Grid">

    <div class="RowHdr">
        <div class="CellShortTxt">
            @Mvc.ContextHelper.Tradueix("Cuenta", "Compte", "Account")
        </div>
        <div class="CellNum">
            @Mvc.ContextHelper.Tradueix("Asiento", "Assentament", "Log")
        </div>
        <div class="CellFch">
            @Mvc.ContextHelper.Tradueix("Fecha", "Data", "Date")
        </div>
        <div class="CellTxt">
            @Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
        </div>
        <div class="CellAmt">
            @Mvc.ContextHelper.Tradueix("Debe", "Deure", "Debits")
        </div>
        <div class="CellAmt">
            @Mvc.ContextHelper.Tradueix("Haber", "Haver", "Credits")
        </div>
    </div>

    @For Each item As DTOCcb In Model
        If oCcd.Unequals(item) Then
            oCcd = New DTOCcd(item.Cca.Exercici, item.Cta, item.Contact)
            sCtaId = "'" & DTOPgcCta.FormatAccountId(item.Cta, item.Contact)
            sCtaNom = DTOPgcCta.FormatAccountDsc(item.Cta, item.Contact, Mvc.ContextHelper.lang())
        End If
        @<div class="Row SelectableRow" data-url="@FEB2.Cca.Url(item.Cca)">
             <div class="CellShortTxt">
                 @DTOPgcCta.FormatAccountId(item.Cta, item.Contact)
             </div>
             <div class="CellShortTxt">
                 @item.Cca.Id
             </div>
             <div class="CellFch">
                 @item.cca.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
             </div>
             <div class="CellTxt">
                 @item.Cca.Concept
             </div>
             <div class="CellAmt">
                 @IIf(item.Dh = item.Cta.Act, DTOAmt.CurFormatted(item.Amt), "")
             </div>
             <div class="CellAmt">
                 @IIf(item.Dh = item.Cta.Act, "", DTOAmt.CurFormatted(item.Amt))
             </div>
        </div>
    Next
</div>


