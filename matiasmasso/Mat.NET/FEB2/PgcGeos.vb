Public Class PgcGeos

    Shared Async Function All(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOPgcGeo))
        Return Await Api.Fetch(Of List(Of DTOPgcGeo))(exs, "PgcGeos/FromExercici", oExercici.Emp.Id, oExercici.Year)
    End Function


    Shared Function ExcelSheet(values As List(Of DTOPgcGeo), year As Integer) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet(year.ToString, "Distribució geográfica comptes")
        With retval
            .AddColumn("Compte", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("")
            .AddColumn("Totals", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("CEE", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Espanya", ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Comunitat Autónoma", ExcelHelper.Sheet.NumberFormats.Euro)

        End With
        Dim oRow As ExcelHelper.Row = retval.AddRow()
        For Each item In values
            oRow = retval.AddRow()
            With item
                oRow.AddCell(.CtaId)
                oRow.AddCell(.CtaNom)
                oRow.AddCell(.Tot)
                oRow.AddCell(.CEE)
                oRow.AddCell(.Esp)
                oRow.AddCell(.CCAA)
            End With
        Next

        Return retval
    End Function
End Class
