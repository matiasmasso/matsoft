Public Class WtbolSite
    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOWtbolSite)
        Dim retval = Await Api.Fetch(Of DTOWtbolSite)(exs, "WtbolSite", oGuid.ToString())
        If retval IsNot Nothing Then
            retval.RestoreObjects()
        End If
        Return retval
    End Function

    Shared Async Function FromMerchantId(exs As List(Of Exception), MerchantId As String) As Task(Of DTOWtbolSite)
        Dim retval = Await Api.Fetch(Of DTOWtbolSite)(exs, "WtbolSite/FromMerchantId", MerchantId)
        retval.RestoreObjects()
        Return retval
    End Function

    Shared Async Function Logo(exs As List(Of Exception), oWtbolSite As DTOWtbolSite) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "WtbolSite/logo", oWtbolSite.Guid.ToString())
    End Function

    Shared Function Load(ByRef oWtbolSite As DTOWtbolSite, exs As List(Of Exception)) As Boolean
        If Not oWtbolSite.IsLoaded And Not oWtbolSite.IsNew Then
            Dim pWtbolSite = Api.FetchSync(Of DTOWtbolSite)(exs, "WtbolSite", oWtbolSite.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWtbolSite)(pWtbolSite, oWtbolSite, exs)
                oWtbolSite.restoreObjects()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOWtbolSite, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOWtbolSite)(value, exs, "WtbolSite")
        value.IsNew = False
    End Function

    Shared Async Function Delete(oWtbolSite As DTOWtbolSite, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWtbolSite)(oWtbolSite, exs, "WtbolSite")
    End Function

    Shared Async Function Log(exs As List(Of Exception), oWtbolSite As DTOWtbolSite, sIp As String) As Task(Of Boolean)
        Return Await Api.Execute(Of String, Boolean)(sIp, exs, "WtbolSite/log", oWtbolSite.Guid.ToString())
    End Function

    Shared Function HatchFeedUrl(oSite As DTOWtbolSite, AbsoluteUrl As Boolean) As String
        Dim retval As String = UrlHelper.Factory(AbsoluteUrl, "britax/stocks", oSite.MerchantId)
        Return retval
    End Function

    Shared Function logoUrl(oSite As DTOWtbolSite, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = UrlHelper.Image(DTO.Defaults.ImgTypes.WtbolSiteLogo, oSite.Guid, AbsoluteUrl)
        Return retval
    End Function
End Class

Public Class WtbolSites

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOWtbolSite))
        Dim retval = Await Api.Fetch(Of List(Of DTOWtbolSite))(exs, "WtbolSites")
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Function EmailsForBcc(oSites As List(Of DTOWtbolSite)) As String
        Dim sb As New Text.StringBuilder
        For Each item In oSites
            If item.ContactEmail > "" Then
                If sb.Length > 0 Then sb.Append("; ")
                sb.Append(item.ContactEmail)
            End If
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class
