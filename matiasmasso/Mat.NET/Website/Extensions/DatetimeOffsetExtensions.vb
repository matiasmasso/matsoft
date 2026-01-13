Imports System.Runtime.CompilerServices

Module DatetimeOffsetExtensions

    <Extension()>
    Public Function BcnDateTime(ByVal value As DateTimeOffset) As DateTime
        Dim oTimeZoneBcn = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time")
        Dim oBcnDateTimeOffset = TimeZoneInfo.ConvertTime(value, oTimeZoneBcn)
        Dim retval As DateTime = oBcnDateTimeOffset.DateTime
        Return retval
    End Function

End Module