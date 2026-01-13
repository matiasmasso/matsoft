Public Class Cce
    Shared Function Url(oCce As DTOCce, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "cce", oCce.Exercici.Emp.Id, oCce.Exercici.Year, oCce.Cta.Guid.ToString())
    End Function

    Shared Function Url(oCta As DTOPgcCta, Optional DtFch As Date = Nothing, Optional AbsoluteUrl As Boolean = False) As String
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim OADate As Long = DtFch.ToOADate
        Return UrlHelper.Factory(AbsoluteUrl, "cce", oCta.Guid.ToString, OADate)
    End Function
End Class
