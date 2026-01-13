Public Class _FeblBase


    Protected Shared Function FormatFch(DtFch As Date) As String
        Return DtFch.ToString("yyyy-MM-dd")
    End Function

    Protected Shared Function FormatFchTime(DtFch As DateTime) As String
        Dim retval = DtFch.ToString("yyyyMMddTHHmmss")
        Return retval
    End Function

    Protected Shared Function OpcionalBool(value As Boolean) As String
        Return If(value, 1, 0)
    End Function

    Protected Shared Function OpcionalGuid(Optional oBaseGuid As DTOBaseGuid = Nothing) As String
        Dim retval As Guid = Nothing
        If oBaseGuid IsNot Nothing Then retval = oBaseGuid.Guid
        Return retval.ToString
    End Function


End Class
