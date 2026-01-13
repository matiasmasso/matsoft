Public Class ImportPrevisioExcel
    Shared Function Factory(oSheet As MatHelper.Excel.Sheet) As List(Of DTOImportPrevisio)
        Dim retval As New List(Of DTOImportPrevisio)
        For Each oRow As MatHelper.Excel.Row In oSheet.Rows
            If IsNumeric(oRow.Cells(0).Content) Then
                If Not String.IsNullOrEmpty(oRow.Cells(1).Content) Then
                    Dim item As New DTOImportPrevisio
                    With item
                        .Qty = oRow.Cells(0).Content
                        .SkuRef = oRow.Cells(1).Content
                        If oRow.Cells.Count > 2 Then
                            .SkuNom = oRow.Cells(2).Content
                            If oRow.Cells.Count > 3 Then

                                .NumComandaProveidor = oRow.Cells(3).Content
                            End If

                        End If
                    End With
                    retval.Add(item)
                End If
            End If
        Next
        Return retval
    End Function

End Class
