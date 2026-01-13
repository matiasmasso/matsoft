Public Class PgcGeos

    Shared Async Function All(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of List(Of DTOPgcGeo))
        Return Await Api.Fetch(Of List(Of DTOPgcGeo))(exs, "PgcGeos/FromExercici", oExercici.Emp.Id, oExercici.Year)
    End Function


    Shared Function ExcelSheet(values As List(Of DTOPgcGeo), year As Integer) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet(year.ToString, "Distribució geográfica comptes")
        With retval
            .AddColumn("Compte", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("")
            .AddColumn("Totals", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("CEE", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Espanya", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Comunitat Autónoma", MatHelper.Excel.Cell.NumberFormats.Euro)

        End With
        Dim oRow As MatHelper.Excel.Row = retval.AddRow()
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
