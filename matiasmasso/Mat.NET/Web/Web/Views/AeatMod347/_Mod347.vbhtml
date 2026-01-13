@ModelType List(Of DTOAeatMod347Item)
@Code

End Code

<div>&nbsp;</div>

@If Model.Count = 0 Then
    @<div>
        <p>
            @Mvc.ContextHelper.Tradueix("No nos constan operaciones declarables devengadas durante este ejercicio.",
                                                      "Not ens consten operacions declarables devengades durant aquest exercici",
                                                      "No operations found during this exercise")
        </p>
        <p>
            @Mvc.ContextHelper.Tradueix("Si cree que se trata de un error, por favor póngase en contacto con nuestras oficinas.",
                                                                              "Si creu que es tracta de un error, si us plau posis en contacte amb les nostres oficines",
                                                                              "If you feel this might not be the case please contact our offices.")
        </p>
    </div>
Else
    @<div>
        <div class="Row">
            <div class="Label">
                @Mvc.ContextHelper.Tradueix("Código pais", "Codi pais", "Country code")
            </div>
            <div class="CellText">
                @Model(0).CodPais
            </div>
        </div>

        <div class="Row">
            <div class="Label">
                @Mvc.ContextHelper.Tradueix("Código Provincia", "Codi Provincia", "Province code")
            </div>
            <div class="CellText">
                @Model(0).CodProvincia
            </div>
        </div>

        @For Each item As DTOAeatMod347Item In Model
            @<div>
                <p class="Epigraf">
                    @If item.ClauOp = DTOAeatMod347Item.ClauOps.Vendes Then
                        @Mvc.ContextHelper.Tradueix("Ventas", "Vendes", "Sales")
                    ElseIf item.ClauOp = DTOAeatMod347Item.ClauOps.Compres Then
                        @Mvc.ContextHelper.Tradueix("Compras", "Compres", "Purchases")
                    End If
                </p>

                <div class="Row">
                    <div class="Label">
                        @Mvc.ContextHelper.Tradueix("Trimestre 1", "Trimestre 1", "Quarter 1")
                    </div>
                    <div class="CellAmt">
                        @(If(DTOAmt.Factory(item.T1).isZero, Html.Raw("&nbsp;"), DTOAmt.CurFormatted(DTOAmt.Factory(item.T1))))
                    </div>
                </div>

                <div class="Row">
                    <div class="Label">
                        @Mvc.ContextHelper.Tradueix("Trimestre 2", "Trimestre 2", "Quarter 2")
                    </div>
                    <div class="CellAmt">
                        @(If(DTOAmt.Factory(item.T2).isZero, Html.Raw("&nbsp;"), DTOAmt.CurFormatted(DTOAmt.Factory(item.T2))))
                    </div>
                </div>

                <div class="Row">
                    <div class="Label">
                        @Mvc.ContextHelper.Tradueix("Trimestre 3", "Trimestre 3", "Quarter 3")
                    </div>
                    <div class="CellAmt">
                        @(If(DTOAmt.Factory(item.T3).isZero, Html.Raw("&nbsp;"), DTOAmt.CurFormatted(DTOAmt.Factory(item.T3))))
                    </div>
                </div>

                <div class="Row">
                    <div class="Label">
                        @Mvc.ContextHelper.Tradueix("Trimestre 4", "Trimestre 4", "Quarter 4")
                    </div>
                    <div class="CellAmt">
                        @(If(DTOAmt.Factory(item.T4).isZero, Html.Raw("&nbsp;"), DTOAmt.CurFormatted(DTOAmt.Factory(item.T4))))
                    </div>
                </div>

                <div class="Row">
                    <div class="Label">
                        Total
                    </div>
                    <div class="CellAmt">
                        @(DTOAmt.CurFormatted(DTOAmt.Factory(item.T1 + item.T2 + item.T3 + item.T4)))
                    </div>
                </div>
            </div>
        Next

    </div>
End If




