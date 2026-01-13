Public Class PgcExtracte
    Shared Function Url(oSaldo As DTOPgcSaldo, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String
        If oSaldo.Contact Is Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "extracte", "Index", oSaldo.Exercici.Year, oSaldo.Epg.Guid.ToString())
        Else
            retval = UrlHelper.Factory(AbsoluteUrl, "extracte", "Index", oSaldo.Exercici.Year, oSaldo.Epg.Guid.ToString, oSaldo.Contact.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function Url(year As Integer, oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String
        If oContact Is Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "extracte", "Index", year, oCta.Guid.ToString())
        Else
            retval = UrlHelper.Factory(AbsoluteUrl, "extracte", "Index", year, oCta.Guid.ToString, oContact.Guid.ToString())
        End If
        Return retval
    End Function
End Class
