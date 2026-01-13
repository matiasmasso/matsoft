Public Class Address

    Shared Async Function Find(oContact As DTOContact, oCod As DTOAddress.Codis, exs As List(Of Exception)) As Task(Of DTOAddress)
        Dim retval As DTOAddress = Nothing
        Dim oAddresses = Await Addresses.All(oContact, exs, oCod)
        If oAddresses.Count > 0 Then
            retval = oAddresses.First
        End If
        Return retval
    End Function

    Shared Function FindSync(oContact As DTOContact, oCod As DTOAddress.Codis, exs As List(Of Exception)) As DTOAddress
        Dim retval As DTOAddress = Nothing
        Dim oAddresses = Addresses.AllSync(oContact, exs, oCod)
        If oAddresses.Count > 0 Then
            retval = oAddresses.First
        End If
        Return retval
    End Function


    Shared Async Function Update(oAddress As DTOAddress, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOAddress)(oAddress.Trimmed, exs, "Address")
        oAddress.IsNew = False
    End Function


    Shared Async Function Delete(oAddress As DTOAddress, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAddress)(oAddress, exs, "Address")
    End Function

    Shared Async Function FromString(src As String, exs As List(Of Exception)) As Task(Of DTOAddress)
        Dim retval As New DTOAddress()
        Dim ziplessAdr As String = ""

        Dim oZip = Await Zip.FromString(src, exs)
        If oZip Is Nothing Then
            retval.Text = src
        Else
            retval.Zip = oZip
            Dim zipPos As Integer = src.IndexOf(oZip.ZipCod)
            retval.Text = src.Substring(0, zipPos).Trim
            If retval.Text.EndsWith(",") Then retval.Text = retval.Text.Substring(0, retval.Text.Length - 1).Trim
        End If

        Dim sNumericRegexPattern As String = "\b([0-9])\b"
        Dim oMatch As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(retval.Text, sNumericRegexPattern)
        If oMatch.Success Then
            retval.ViaNom = retval.Text.Substring(0, oMatch.Index).Trim
            If retval.ViaNom.EndsWith(",") Then retval.ViaNom = retval.ViaNom.Substring(0, retval.ViaNom.Length - 1).Trim
            retval.Num = retval.Text.Substring(oMatch.Index, retval.Text.Length - oMatch.Index).Trim
        Else
            retval.ViaNom = retval.Text
        End If

        Return retval
    End Function

End Class


Public Class Addresses
    Shared Async Function All(oContact As DTOContact, exs As List(Of Exception), Optional oCod As DTOAddress.Codis = DTOAddress.Codis.NotSet) As Task(Of List(Of DTOAddress))
        Return Await Api.Fetch(Of List(Of DTOAddress))(exs, "Addresses", oContact.Guid.ToString, oCod)
    End Function
    Shared Function AllSync(oContact As DTOContact, exs As List(Of Exception), Optional oCod As DTOAddress.Codis = DTOAddress.Codis.NotSet) As List(Of DTOAddress)
        Return Api.FetchSync(Of List(Of DTOAddress))(exs, "Addresses", oContact.Guid.ToString, oCod)
    End Function

End Class
