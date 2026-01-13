Public Class ImportPrevisions

    Shared Function All(oSku As DTOProductSku) As List(Of DTOImportPrevisio)
        Dim retval As List(Of DTOImportPrevisio) = ImportPrevisionsLoader.All(oSku)
        Return retval
    End Function

    Shared Sub Load(oImportacio As DTOImportacio)
        ImportacioLoader.Load(oImportacio)
        ImportPrevisionsLoader.Load(oImportacio)
        'ImportValidacionsLoader.Load(oImportacio)
    End Sub

    Shared Function UploadExcel(exs As List(Of Exception), oImportacio As DTOImportacio, oSheet As MatHelper.Excel.Sheet) As Boolean
        If BEBL.Importacio.Load(oImportacio) Then
            oImportacio.Previsions = LegacyHelper.ImportPrevisioExcel.Factory(oSheet)
            If BEBL.ImportPrevisions.Update(oImportacio, exs) Then
                BEBL.ImportPrevisions.SetSkus(oImportacio, exs)
            End If
        End If
        Return exs.Count = 0
    End Function

    Shared Function Update(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportPrevisionsLoader.Update(oImportacio, exs)
        Return retval
    End Function

    Shared Function Delete(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportPrevisionsLoader.Delete(oImportacio, exs)
        Return retval
    End Function

    Shared Function Delete(oPrevisions As List(Of DTOImportPrevisio), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportPrevisionsLoader.Delete(oPrevisions, exs)
        Return retval
    End Function

    Shared Function SetSkus(oImportacio As DTOImportacio, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ImportPrevisionsLoader.SetSkus(oImportacio, exs)
        Return retval
    End Function

End Class

