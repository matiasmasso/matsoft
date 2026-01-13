Public Class XecsPresentacio
    Shared Function Update(value As DTOXecsPresentacio, ByRef exs As List(Of Exception)) As Boolean
        Dim oCtas = BEBL.PgcCtas.Current()
        Dim oCtaClientsXecsEnCartera = DTOPgcCta.FromCodi(oCtas, DTOPgcPlan.Ctas.ClientsXecsEnCartera, exs)
        Dim oCtaClientsXecsAlCobro = DTOPgcCta.FromCodi(oCtas, DTOPgcPlan.Ctas.ClientsXecsAlCobro, exs)
        Dim oCtaClientsXecsDescomptats = DTOPgcCta.FromCodi(oCtas, DTOPgcPlan.Ctas.ClientsXecsDescomptats, exs)
        Dim oCtaBancsPagaresDescomptats = DTOPgcCta.FromCodi(oCtas, DTOPgcPlan.Ctas.BancsPagaresDescomptats, exs)
        Dim oCtaBancs = DTOPgcCta.FromCodi(oCtas, DTOPgcPlan.Ctas.bancs, exs)

        Dim sb As New System.Text.StringBuilder
        sb.Append(value.Banc.Abr)

        Dim oCtaCarrecCod As DTOPgcPlan.Ctas = DTOPgcPlan.Ctas.NotSet
        Select Case value.Modalitat
            Case DTOXec.ModalitatsPresentacio.A_la_Vista
                sb.Append("-ingres de xecs a la vista")
            Case DTOXec.ModalitatsPresentacio.Al_Cobro
                sb.Append("-presentació de pagarés al cobro")
            Case DTOXec.ModalitatsPresentacio.Al_Descompte
                sb.Append("-presentació de pagarés al descompte")
        End Select

        Dim oCca As DTOCca = DTOCca.Factory(value.Fch, value.User, DTOCca.CcdEnum.RemesaXecs)
        With oCca
            .Concept = sb.ToString

            For Each oXec As DTOXec In value.Xecs
                oCca.AddCredit(oXec.Amt, oCtaClientsXecsEnCartera, oXec.Lliurador)
                Select Case value.Modalitat
                    Case DTOXec.ModalitatsPresentacio.Al_Cobro
                        oCca.AddDebit(oXec.Amt, oCtaClientsXecsAlCobro, oXec.Lliurador)
                    Case DTOXec.ModalitatsPresentacio.Al_Descompte
                        oCca.AddDebit(oXec.Amt, oCtaClientsXecsDescomptats, oXec.Lliurador)
                End Select
            Next

            Select Case value.Modalitat
                Case DTOXec.ModalitatsPresentacio.Al_Cobro, DTOXec.ModalitatsPresentacio.Al_Descompte
                    oCca.AddSaldo(oCtaBancsPagaresDescomptats, value.Banc)
                Case DTOXec.ModalitatsPresentacio.A_la_Vista
                    oCca.AddSaldo(oCtaBancs, value.Banc)
            End Select

        End With

        With value
            .Cca = oCca
            For Each oXec As DTOXec In .Xecs
                With oXec
                    BEBL.Xec.Load(oXec)
                    .StatusCod = DTOXec.StatusCods.EnCirculacio
                    .CodPresentacio = value.Modalitat
                    .CcaPresentacio = oCca
                    .NBanc = value.Banc
                End With
            Next
        End With

        Dim retval As Boolean = XecsPresentacioLoader.Update(value, exs)
        Return retval
    End Function
End Class
