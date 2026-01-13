Public Class PgcExtracteController
    Inherits _MatController

    Function Index(year As Integer, cta As Guid, contact As Nullable(Of Guid)) As ActionResult
        Dim Model As DTOPgcExtracte = Nothing
        If contact Is Nothing Then
            'Model = BLL.BLLPgcExtracte.Load(year, cta)
        Else
            'Model = BLL.BLLPgcExtracte.Load(year, cta, contact)
        End If
        Return View("extracte", Model)
    End Function

    Function YearChanged(year As Integer, cta As Guid, contact As Nullable(Of Guid)) As PartialViewResult
        Dim Model As DTOPgcExtracte = Nothing
        If contact Is Nothing Then
            'Model = BLL.BLLPgcExtracte.Load(year, cta)
        Else
            'Model = BLL.BLLPgcExtracte.Load(year, cta, contact)
        End If
        Return PartialView("_extracte", Model)
    End Function

End Class