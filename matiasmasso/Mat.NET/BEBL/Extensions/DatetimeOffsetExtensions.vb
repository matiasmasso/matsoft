Imports System.Runtime.CompilerServices

Module DatetimeOffsetExtensions

    <Extension()>
    Public Function BcnDateTime(ByVal value As DateTimeOffset) As DateTime
        Dim oTimeZoneBcn = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time")
        Dim oBcnDateTimeOffset = TimeZoneInfo.ConvertTime(value, oTimeZoneBcn)
        Dim retval As DateTime = oBcnDateTimeOffset.DateTime
        Return retval
    End Function

    <Extension()>
    Public Sub truncateMilisecfractions(ByRef value As DateTimeOffset)
        value = New DateTimeOffset(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Offset)
    End Sub

End Module