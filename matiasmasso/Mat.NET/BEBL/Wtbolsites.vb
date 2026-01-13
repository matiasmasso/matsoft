Public Class Wtbolsite
    Shared Function Find(oGuid As Guid) As DTOWtbolSite
        Dim retval As DTOWtbolSite = WtbolSiteLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromMerchantId(MerchantId As String) As DTOWtbolSite
        Dim retval As DTOWtbolSite = WtbolSiteLoader.FromMerchantId(MerchantId)
        Return retval
    End Function

    Shared Function Logo(oGuid As Guid) As Byte()
        Dim retval As Byte() = Nothing
        Dim oWtBolSite = WtbolSiteLoader.Find(oGuid)
        If oWtBolSite IsNot Nothing Then
            retval = oWtBolSite.Logo
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oWtbolSite As DTOWtbolSite) As Boolean
        Dim retval As Boolean = WtbolSiteLoader.Load(oWtbolSite)
        Return retval
    End Function

    Shared Function Update(oWtbolSite As DTOWtbolSite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolSiteLoader.Update(oWtbolSite, exs)
        Return retval
    End Function

    Shared Function Update(model As Models.Wtbol.Site, ByRef exs As List(Of Exception)) As Boolean
        Dim oWtbolsite = BEBL.Wtbolsite.Find(model.Guid)
        With oWtbolsite
            .Web = model.Website
            .ContactNom = model.ContactNom
            .ContactEmail = model.ContactEmail
            .ContactTel = model.ContactTel
            .UsrLog.UsrLastEdited = New DTOUser(model.UsrGuid)
        End With
        Dim retval As Boolean = WtbolSiteLoader.Update(oWtbolsite, exs)
        Return retval
    End Function

    Shared Function Delete(oWtbolSite As DTOWtbolSite, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolSiteLoader.Delete(oWtbolSite, exs)
        Return retval
    End Function

    Shared Function Log(oSite As DTOWtbolSite, Ip As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WtbolLogLoader.Log(oSite, Ip, exs)
        Return retval
    End Function

    Shared Function UpdateLandingPage(exs As List(Of Exception), oLandingPage As Newtonsoft.Json.Linq.JObject) As Boolean
        Return WtbolSiteLoader.UpdateLandingPage(exs, oLandingPage)
    End Function

    Shared Function DeleteLandingPage(exs As List(Of Exception), oLandingPage As Newtonsoft.Json.Linq.JObject) As Boolean
        Return WtbolSiteLoader.DeleteLandingPage(exs, oLandingPage)
    End Function

    Shared Function UpdateStock(exs As List(Of Exception), oStock As DTOWtbolStock) As Boolean
        Return WtbolSiteLoader.UpdateStock(exs, oStock)
    End Function


End Class



Public Class Wtbolsites
    Shared Function All() As List(Of DTOWtbolSite)
        Dim retval As List(Of DTOWtbolSite) = WtbolSitesLoader.All()
        Return retval
    End Function


End Class
