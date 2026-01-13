Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports MatHelperStd

Module StringExtensions

    <Extension()>
    Public Function isNotEmpty(src As String) As Boolean
        Return Not String.IsNullOrEmpty(src)
    End Function

    <Extension()>
    Public Function ContainsIgnoringCaseAndDiacritics(value As String, containedCandidate As String) As Boolean
        Dim compareInfo = CultureInfo.InvariantCulture.CompareInfo
        Dim retval = compareInfo.IndexOf(value, containedCandidate, CompareOptions.IgnoreNonSpace Or CompareOptions.IgnoreCase) >= 0
        Return retval
    End Function

End Module
