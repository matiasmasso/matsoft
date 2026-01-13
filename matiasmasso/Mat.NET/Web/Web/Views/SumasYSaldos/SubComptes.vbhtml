@Modeltype DTOPgcSaldo
@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"
    
    Dim exs As New List(Of Exception)
    Dim oSaldos As List(Of DTOPgcSaldo) = FEB2.SumasYSaldos.SubComptesSync(exs, Model.Exercici, Model.Epg)
End Code

<div class="pagewrapper">

    <section>
        <a href="@FEB2.SumasYSaldos.Url(Model.Exercici)">
            @(Mvc.ContextHelper.Tradueix("Ejercicio", "Exercici", "Exercise") & ": " & Model.Exercici.Year)
        </a>
    </section>

    <section>
        @(Mvc.ContextHelper.Tradueix("Cuenta", "Compte", "Account") & ": " & DTOPgcCta.FullNom(Model.Epg, Mvc.ContextHelper.lang()))
    </section>

    <section class="Grid">

        <div class="RowHdr">
            <div class="CellTxt">@Mvc.ContextHelper.Tradueix("Cuenta", "Compte", "Account")</div>
            <div class="CellAmt CellDesktop">@Mvc.ContextHelper.Tradueix("Deudor", "Deutor", "Debitor")</div>
            <div class="CellAmt CellDesktop">@Mvc.ContextHelper.Tradueix("Acreedor", "Creditor", "Creditor")</div>
            <div class="CellAmt CellMobile">@Mvc.ContextHelper.Tradueix("Saldo", "Saldo", "Balance")</div>
        </div>

        @For Each item As DTOPgcSaldo In oSaldos
            @<div class="Row">
                <div class="CellTxt">
                    <a href="@FEB2.PgcExtracte.Url(item)">
                        @If item.Contact Is Nothing Then
                        @Mvc.ContextHelper.Tradueix("(sin subcuenta)", "(sense subcompte)", "(with no subaccount)")
                        Else
                        @item.Contact.FullNom
                        End If
                    </a>
                </div>
                 <div class="CellAmt CellDesktop">
                     @If item.IsDeutor Then
                         @DTOAmt.CurFormatted(item.SdoDeudor)
                     End If
                 </div>
                 <div class="CellAmt CellDesktop">
                     @If item.IsCreditor Then
                         @DTOAmt.CurFormatted(item.SdoCreditor)
                     End If
                 </div>
                 <div class="CellAmt CellMobile">
                     @If item.IsCreditor Then
                         @DTOAmt.CurFormatted(DTOPgcSaldo.Saldo(item))
                     End If
                 </div>
            </div>
        Next

    </section>
</div>

@Section Styles
    <style>
        .pagewrapper {
            max-width: 600px;
            margin: auto;
        }
    </style>
End Section

@Section Scripts
    <script src="~/Media/js/SumasYSaldos.js"></script>
End Section