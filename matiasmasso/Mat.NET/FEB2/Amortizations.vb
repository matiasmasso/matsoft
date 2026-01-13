Public Class Amortization
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOAmortization)
        Return Await Api.Fetch(Of DTOAmortization)(exs, "Amortization", oGuid.ToString())
    End Function

    Shared Function FromAltaSync(exs As List(Of Exception), oCca As DTOCca) As DTOAmortization
        Return Api.FetchSync(Of DTOAmortization)(exs, "Amortization/FromAlta", oCca.Guid.ToString())
    End Function

    Shared Function FromBaixaSync(exs As List(Of Exception), oCca As DTOCca) As DTOAmortization
        Return Api.FetchSync(Of DTOAmortization)(exs, "Amortization/FromBaixa", oCca.Guid.ToString())
    End Function

    Shared Function Load(ByRef oAmortization As DTOAmortization, exs As List(Of Exception)) As Boolean
        If Not oAmortization.IsLoaded And Not oAmortization.IsNew Then
            Dim pAmortization = Api.FetchSync(Of DTOAmortization)(exs, "Amortization", oAmortization.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOAmortization)(pAmortization, oAmortization, exs)
                For Each item In oAmortization.Items
                    item.Parent = oAmortization
                Next

            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oAmortization As DTOAmortization, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOAmortization)(oAmortization, exs, "Amortization")
        oAmortization.IsNew = False
    End Function


    Shared Async Function Delete(oAmortization As DTOAmortization, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAmortization)(oAmortization, exs, "Amortization")
    End Function

    Shared Async Function Amortitza(exs As List(Of Exception), oUser As DTOUser, oEmp As DTOEmp, year As Integer, oAmortization As DTOAmortization) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOAmortization)(oAmortization, exs, "Amortization/amortiza", oUser.Guid.ToString, oEmp.Id, year)
        oAmortization.IsNew = False
    End Function

End Class

Public Class Amortizations

    Shared Async Function DefaultTipus(exs As List(Of Exception)) As Task(Of List(Of DTOAmortizationTipus))
        Return Await Api.Fetch(Of List(Of DTOAmortizationTipus))(exs, "Amortizations/DefaultTipus")
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOAmortization))
        Dim retval = Await Api.Fetch(Of List(Of DTOAmortization))(exs, "Amortizations", oEmp.Id)
        For Each oamortitzacio In retval
            For Each item In oamortitzacio.Items
                item.Parent = oamortitzacio
            Next
        Next
        Return retval
    End Function

    Shared Async Function Pendents(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOAmortization))
        Dim retval = Await Api.Fetch(Of List(Of DTOAmortization))(exs, "Amortizations/pendents", oExercici.Emp.Id, oExercici.Year)
        Return retval
    End Function

    Shared Async Function LastItems(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOAmortizationItem))
        Dim oAll = Await FEB2.Amortizations.All(exs, oExercici.Emp)
        oAll = oAll.Where(Function(x) x.Items.Count > 0).ToList
        Dim DtFch As New Date(oExercici.Year, 12, 31)
        Dim retval = oAll.SelectMany(Function(x) x.Items).Where(Function(y) y.Cod = DTOAmortizationItem.Cods.Amortitzacio And y.Fch = DtFch).ToList
        Return retval
    End Function

    Shared Async Function Amortitza(exs As List(Of Exception), oUser As DTOUser, oExercici As DTOExercici) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Amortizations/amortiza", oUser.Guid.ToString, oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function RevertAmortitzacions(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Amortizations/RevertAmortizations", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Function Excel(items As List(Of DTOAmortization)) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet("catalogo M+O")
        With retval
            .AddColumn("Compte", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Concepte", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Data Adquisició", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Valor Adquisició", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Valor Amortitzat", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Saldo", ExcelHelper.Sheet.NumberFormats.Euro)
        End With

        For Each item In items
            Dim valorAdquisicio As Decimal = item.Amt.Eur
            Dim valorAmortitzat As Decimal = item.Items.Sum(Function(x) x.Amt.Eur)
            Dim oRow As ExcelHelper.Row = retval.AddRow()
            oRow.AddCell(DTOPgcCta.FullNom(item.Cta, DTOLang.CAT))
            oRow.AddCell(item.Dsc)
            oRow.AddCell(item.Fch)
            oRow.AddCell(valorAdquisicio)
            oRow.AddCell(valorAmortitzat)
            oRow.AddCell(valorAdquisicio - valorAmortitzat)
        Next
        Return retval
    End Function

End Class
