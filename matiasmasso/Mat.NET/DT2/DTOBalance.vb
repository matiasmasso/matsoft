Public Class DTOBalance

    Property emp As DTOEmp
    Property yearMonthFrom As DTOYearMonth
    Property yearMonthTo As DTOYearMonth
    Property items As List(Of DTOPgcClass)

    Shared Function Factory(oEmp As DTOEmp, YearMonthFrom As DTOYearMonth, YearMonthTo As DTOYearMonth) As DTOBalance
        Dim retval As New DTOBalance
        With retval
            .emp = oEmp
            .yearMonthFrom = YearMonthFrom
            .yearMonthTo = YearMonthTo
            .items = New List(Of DTOPgcClass)
        End With
        Return retval
    End Function

    Public Function YearMonths(oPgcClass As DTOPgcClass) As List(Of DTOYearMonth)
        Dim oCtas As New List(Of DTOPgcCta)
        If oPgcClass.Ctas IsNot Nothing AndAlso oPgcClass.Ctas.Count > 0 Then
            oCtas = oPgcClass.Ctas
        Else
            GetPgcCtas(oPgcClass, oCtas)
        End If
        Dim oYearMonths = oCtas.SelectMany(Function(x) x.yearMonths).ToList
        Dim retval = oYearMonths.GroupBy(Function(g) New With {Key g.year, Key g.month}).Select(Function(group) New DTOYearMonth With {.year = group.Key.year, .month = group.Key.month, .Eur = group.Sum(Function(x) x.Eur)}).ToList
        Return retval
    End Function

    Public Function YearMonths(oCod As DTOPgcClass.Cods) As List(Of DTOYearMonth)
        Dim oCtas = Ctas(oCod)
        Dim oYearMonths = oCtas.SelectMany(Function(x) x.yearMonths).ToList
        Dim retval = oYearMonths.GroupBy(Function(g) New With {Key g.year, Key g.month}).Select(Function(group) New DTOYearMonth With {.year = group.Key.year, .month = group.Key.month, .Eur = group.Sum(Function(x) x.Eur)}).ToList
        Return retval
    End Function

    Public Function Ctas(oCod As DTOPgcClass.Cods) As List(Of DTOPgcCta)
        Dim retval As New List(Of DTOPgcCta)
        Dim oClass = GetPgcClass(oCod)
        If oClass IsNot Nothing Then
            GetPgcCtas(oClass, retval)
        End If
        Return retval
    End Function

    Public Function GetPgcClass(oCod As DTOPgcClass.Cods, Optional oParentsFrom As List(Of DTOPgcClass) = Nothing) As DTOPgcClass
        Dim retval As DTOPgcClass = Nothing

        If oParentsFrom Is Nothing Then
            oParentsFrom = _items
        End If

        For Each oClass As DTOPgcClass In oParentsFrom
            If oClass.Cod = oCod Then
                retval = oClass
                Exit For
            ElseIf oClass.Children.Count > 0 Then
                retval = GetPgcClass(oCod, oClass.Children)
                If retval IsNot Nothing Then Exit For
            End If
        Next
        Return retval
    End Function

    Public Sub GetPgcCtas(oParentFrom As DTOPgcClass, oCtas As List(Of DTOPgcCta))
        For Each oClass As DTOPgcClass In oParentFrom.Children
            If oClass.Ctas IsNot Nothing AndAlso oClass.Ctas.Count > 0 Then
                oCtas.AddRange(oClass.Ctas)
            End If
            If oClass.Children IsNot Nothing AndAlso oClass.Children.Count > 0 Then
                GetPgcCtas(oClass, oCtas)
            End If
        Next
    End Sub

    Public Function YearMonthsCount() As Integer
        Return DTOYearMonth.MonthsDiff(_yearMonthFrom, _yearMonthTo)
    End Function


    Public Function getKpi(id As DTOKpi.Ids) As DTOKpi
        Dim retval As New DTOKpi
        retval.Id = id
        retval.Caption = id.ToString.Replace("_", " ")
        Select Case id
            Case DTOKpi.Ids.Activo_Corriente
                retval.YearMonths = YearMonths(DTOPgcClass.Cods.aAB_Activo_Corriente)
            Case DTOKpi.Ids.Pasivo_Corriente
                retval.YearMonths = YearMonths(DTOPgcClass.Cods.aBC_Pasivo_Corriente)
        End Select
        Return retval
    End Function

    Public Function ExcelBook(oKpis As List(Of DTOKpi), oLang As DTOLang) As ExcelHelper.Book
        Dim retval As New ExcelHelper.Book
        If oKpis.Count > 0 Then retval.Sheets.Add(ExcelSheetKpis(oKpis, oLang))
        retval.Sheets.Add(ExcelSheet(oLang))
        Return retval
    End Function

    Public Function ExcelSheet(oLang As DTOLang) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet
        retval.AddColumn("concepte")
        Dim oYearMonths = DTOYearMonth.Range(_yearMonthFrom, _yearMonthTo)
        For Each oYearMonth In oYearMonths
            retval.AddColumn(oYearMonth.Caption(oLang), ExcelHelper.Sheet.NumberFormats.Euro)
        Next

        For Each oClass In _items
            AddRow(retval, oClass, oLang)
        Next
        Return retval
    End Function

    Private Sub AddRow(oSheet As ExcelHelper.Sheet, oClass As DTOPgcClass, oLang As DTOLang)
        Dim oRow = oSheet.AddRow()
        Dim paddingLeft = New String(" ", oClass.GetLevel * 4)
        oRow.AddCell(paddingLeft & oClass.Nom.Tradueix(oLang))
        Dim oYearMonths = DTOYearMonth.Range(_yearMonthFrom, _yearMonthTo)
        If oClass.YearMonths IsNot Nothing AndAlso oClass.YearMonths.Count > 0 Then
            For Each oYearMonth In oYearMonths
                Dim pYearMonth = oClass.YearMonths.FirstOrDefault(Function(x) x.Equals(oYearMonth))
                If pYearMonth Is Nothing Then
                    oRow.AddCell()
                Else
                    oRow.AddCell(pYearMonth.Eur)
                End If
            Next
        End If

        If oClass.Ctas IsNot Nothing AndAlso oClass.Ctas.Count > 0 Then
            paddingLeft = New String(" ", (oClass.GetLevel + 1) * 4)
            For Each oCta In oClass.Ctas
                oRow = oSheet.AddRow()
                oRow.AddCell(String.Format("{0} {1} {2}", paddingLeft, oCta.id, oCta.nom.Tradueix(oLang)))
                For Each oYearMonth In oYearMonths
                    Dim pYearMonth = oCta.yearMonths.FirstOrDefault(Function(x) x.Equals(oYearMonth))
                    If pYearMonth Is Nothing Then
                        oRow.AddCell()
                    Else
                        oRow.AddCell(pYearMonth.Eur)
                    End If
                Next
            Next
        End If
        If oClass.Children IsNot Nothing AndAlso oClass.Children.Count > 0 Then
            For Each oChild In oClass.Children
                AddRow(oSheet, oChild, oLang)
            Next
        End If
    End Sub




    Public Function ExcelSheetKpis(oKpis As List(Of DTOKpi), oLang As DTOLang) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet
        retval.AddColumn("any")
        retval.AddColumn("mes")
        For Each oKpi In oKpis
            retval.AddColumn(oKpi.Caption, ExcelHelper.Sheet.NumberFormats.Euro)
        Next
        For i As Integer = 0 To YearMonthsCount() - 1
            Dim oYearMonth = _yearMonthFrom.AddMonths(i)
            Dim oRow = retval.AddRow
            oRow.AddCell(oYearMonth.year)
            oRow.AddCell(oLang.MesAbr(oYearMonth.month))
            For Each oKpi In oKpis
                Dim kYearMonth = oKpi.YearMonths.FirstOrDefault(Function(x) x.Equals(oYearMonth))
                If kYearMonth Is Nothing Then
                    oRow.AddCell()
                Else
                    oRow.AddCell(kYearMonth.Eur)
                End If
            Next
        Next
        Return retval
    End Function
End Class

