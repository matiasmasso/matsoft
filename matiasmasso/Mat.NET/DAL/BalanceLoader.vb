Public Class BalanceLoader

    Shared Function Tree(oEmp As DTOEmp, YearMonthFrom As DTOYearMonth, YearMonthTo As DTOYearMonth) As DTOBalance
        Dim oPgcClasses As New List(Of DTOPgcClass)
        Dim oPgcClass As New DTOPgcClass
        Dim oCta As New DTOPgcCta
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM VwBalance ")
        sb.AppendLine("WHERE (VwBalance.Emp  IS NULL OR VwBalance.Emp = " & oEmp.Id & " )")
        sb.AppendLine("AND (VwBalance.yearmonth IS NULL OR VwBalance.yearmonth BETWEEN " & YearMonthFrom.AddMonths(-12).RawTag & " AND " & YearMonthTo.RawTag & ") ") 'comença a l'any anterior per calcular el saldo a primer d'any
        sb.AppendLine("ORDER BY VwBalance.ClassGuid, VwBalance.CtaId, VwBalance.CtaGuid, VwBalance.Year, VwBalance.Month ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oPgcClass.Guid.Equals(oDrd("ClassGuid")) Then
                oPgcClass = New DTOPgcClass(oDrd("ClassGuid"))
                With oPgcClass
                    .Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "ClassNomEsp", "ClassNomCat", "ClassNomEng")
                    .Cod = SQLHelper.GetIntegerFromDataReader(oDrd("ClassCod"))
                    If Not IsDBNull(oDrd("ClassParent")) Then
                        .Parent = New DTOPgcClass(oDrd("ClassParent"))
                    End If
                    .HideFigures = SQLHelper.GetBooleanFromDatareader(oDrd("ClassHideFigures"))
                    .Ord = SQLHelper.GetIntegerFromDataReader(oDrd("ClassOrd"))
                    .Children = New List(Of DTOPgcClass)
                End With
                oPgcClasses.Add(oPgcClass)
            End If

            If Not IsDBNull(oDrd("CtaGuid")) AndAlso Not oCta.Guid.Equals(oDrd("CtaGuid")) Then
                oCta = New DTOPgcCta(DirectCast(oDrd("CtaGuid"), Guid))
                oCta.id = oDrd("CtaId")
                oCta.nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CtaEsp", "CtaCat", "CtaEng")
                oCta.yearMonths = New List(Of DTOYearMonth)
                oPgcClass.Ctas.Add(oCta)
            End If

            If Not IsDBNull(oDrd("Year")) Then
                Dim oYearMonth As New DTOYearMonth(oDrd("Year"), oDrd("Month"), oDrd("Eur"))
                oCta.yearMonths.Add(oYearMonth)
            End If
        Loop

        oDrd.Close()

        'arrossega els saldos als comptes de balanç
        Dim oBalCtas = oPgcClasses.SelectMany(Function(x) x.Ctas).Where(Function(y) DTOPgcCta.Bal(y) = DTOPgcCta.Bals.Balance).ToList
        For Each oCta In oBalCtas
            Dim DcSaldo As Decimal = 0
            Dim oYearMonths = DTOYearMonth.Range(YearMonthFrom.AddMonths(-12), YearMonthTo) 'comença a l'any anterior per calcular el saldo a primer d'any
            For Each oYearMonth In oYearMonths
                Dim value = oCta.YearMonthValue(oYearMonth)
                If oYearMonth.month = 1 Then
                    DcSaldo = value ' reset saldo a gener doncs el primer apunt ja conté els saldos de desembre
                Else
                    DcSaldo += value
                End If
                oYearMonth.Eur = DcSaldo
            Next
            oCta.yearMonths = oYearMonths
        Next

        Dim retval = DTOBalance.Factory(oEmp, YearMonthFrom, YearMonthTo)
        retval.items = DTOPgcClass.Tree(oPgcClasses)

        Return retval
    End Function
End Class
