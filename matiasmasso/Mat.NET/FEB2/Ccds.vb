Public Class Ccd
    Inherits _FeblBase
    Shared Function Url(oCta As DTOPgcCta, oContact As DTOContact, DtFch As Date, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "ccd", OpcionalGuid(oCta), OpcionalGuid(oContact), DtFch.ToOADate)
    End Function

End Class
