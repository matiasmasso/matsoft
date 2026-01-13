Public Class _BaseController
    Inherits ApiController

    Shared Function DateTimeFormat(DtFch As Date) As String
        Dim retval As String = DateFormat(DtFch.Date)
        Return retval
    End Function

    Shared Function DateFormat(DtFch As Date) As String
        Dim retval As String = DtFch.Date.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK")
        Return retval
    End Function

    Shared Function ParseDate(sFch As String) As Date
        Dim retval As Date = Today
        If IsDate(sFch) Then
            retval = CDate(sFch).Date
        End If
        Return retval
    End Function

    Shared Function ParseDateTime(sFch As String) As Date
        Dim retval As Date = Now
        If IsDate(sFch) Then
            retval = CDate(sFch)
        End If
        Return retval
    End Function
End Class
