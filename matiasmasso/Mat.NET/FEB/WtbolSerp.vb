Public Class WtbolSerp
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOWtbolSerp)
        Return Await Api.Fetch(Of DTOWtbolSerp)(exs, "WtbolSerp", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oWtbolSerp As DTOWtbolSerp, exs As List(Of Exception)) As Boolean
        If Not oWtbolSerp.IsLoaded And Not oWtbolSerp.IsNew Then
            Dim pWtbolSerp = Api.FetchSync(Of DTOWtbolSerp)(exs, "WtbolSerp", oWtbolSerp.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWtbolSerp)(pWtbolSerp, oWtbolSerp, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function UpdateSync(oWtbolSerp As DTOWtbolSerp, exs As List(Of Exception)) As Boolean
        Return Api.UpdateSync(Of DTOWtbolSerp)(oWtbolSerp, exs, "WtbolSerp")
        oWtbolSerp.IsNew = False
    End Function

    Shared Async Function Delete(oWtbolSerp As DTOWtbolSerp, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWtbolSerp)(oWtbolSerp, exs, "WtbolSerp")
    End Function

    Shared Function LogSync(sIp As String, userAgent As String, oProduct As DTOProduct, oLandingPages As List(Of DTOWtbolLandingPage), exs As List(Of Exception)) As Boolean
        've de product/_StoreLocator.vbhtml

        Dim countryCode As String = ""
        Try
            'Dim oIpLookup As DTOIpLookup
            'oIpLookup = DTOIpLookup.IpLookup(sIp)
            'countryCode = oIpLookup.countryCode
        Catch ex As Exception

        End Try

        Dim oSerp As New DTOWtbolSerp
        With oSerp
            '.Session = oSession
            .Ip = sIp
            .UserAgent = userAgent
            .CountryCode = countryCode
            .Fch = DTO.GlobalVariables.Now()
            .Product = oProduct
        End With
        For Each olandingpage In oLandingPages
            Dim item As New DTOWtbolSerp.Item
            item.Site = olandingpage.Site
            oSerp.Items.Add(item)
        Next
        Dim retval = WtbolSerp.UpdateSync(oSerp, exs)
        Return retval
    End Function


    Shared Function Url(product As DTOProduct, oLang As DTOLang, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = product.GetUrl(DTOLang.ESP, DTOProduct.Tabs.distribuidores, AbsoluteUrl)
        Return retval
    End Function
End Class

Public Class WtbolSerps
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional oSite As DTOWtbolSite = Nothing) As Task(Of List(Of DTOWtbolSerp))
        Return Await Api.Fetch(Of List(Of DTOWtbolSerp))(exs, "WtbolSerps", OpcionalGuid(oSite))
    End Function

End Class
