Imports System.Runtime.CompilerServices
Module StringExtensions

    <Extension()>
    Public Function Left(ByVal str As String, ByVal length As Integer) As String
        Return str.Substring(0, Math.Min(str.Length, length))
    End Function
End Module

