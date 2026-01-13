Imports Newtonsoft.Json.Linq


Public Class WtbolLandingPage
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOWtbolLandingPage)
        Dim retval = Await Api.Fetch(Of DTOWtbolLandingPage)(exs, "WtbolLandingPage", oGuid.ToString())
        If retval IsNot Nothing Then
            retval.RestoreObjects()
        End If
        Return retval
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWtbolLandingPage As DTOWtbolLandingPage) As Boolean
        If Not oWtbolLandingPage.IsLoaded And Not oWtbolLandingPage.IsNew Then
            Dim pWtbolLandingPage = Api.FetchSync(Of DTOWtbolLandingPage)(exs, "WtbolLandingPage", oWtbolLandingPage.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWtbolLandingPage)(pWtbolLandingPage, oWtbolLandingPage, exs)
                oWtbolLandingPage.RestoreObjects()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oWtbolLandingPage As DTOWtbolLandingPage) As Task(Of Boolean)
        Return Await Api.Update(Of DTOWtbolLandingPage)(oWtbolLandingPage, exs, "WtbolLandingPage")
        oWtbolLandingPage.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oWtbolLandingPage As DTOWtbolLandingPage) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWtbolLandingPage)(oWtbolLandingPage, exs, "WtbolLandingPage")
    End Function
End Class

Public Class WtbolLandingPages
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct, Optional includeStocksFromMgz As DTOMgz = Nothing) As Task(Of List(Of DTOWtbolLandingPage))
        Dim retval = Await Api.Fetch(Of List(Of DTOWtbolLandingPage))(exs, "WtbolLandingPages")
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Function AllSync(exs As List(Of Exception), oProduct As DTOProduct, Optional includeStocksFromMgz As DTOMgz = Nothing) As List(Of DTOWtbolLandingPage)
        Dim retval = Api.FetchSync(Of List(Of DTOWtbolLandingPage))(exs, "WtbolLandingPages", oProduct.Guid.ToString, OpcionalGuid(includeStocksFromMgz))
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function Upload(oSite As DTOWtbolSite, oSheet As MatHelper.Excel.Sheet, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim jObject = JLandingPages(oSite, oSheet, exs)
        If exs.Count = 0 Then
            retval = Await Api.Execute(Of JObject)(jObject, exs, "wtbol/upload/landingpages")
        End If
        Return retval
    End Function

    Shared Function JLandingPages(oSite As DTOWtbolSite, oSheet As MatHelper.Excel.Sheet, exs As List(Of Exception)) As JObject
        Dim retval = New JObject
        If WtbolSite.Load(oSite, exs) Then

            Dim jSite = New JObject()
            jSite.Add("Guid", oSite.Guid.ToString())

            Dim jItems = New JArray

            retval.Add("site", jSite)
            retval.Add("items", jItems)

            For iRow As Integer = 1 To oSheet.Rows.Count - 1
                Dim oRow As MatHelper.Excel.Row = oSheet.Rows(iRow)
                If TextHelper.VbIsNumeric(oRow.cells(0).content) Then
                    Dim jItem = New JObject
                    jItem.Add("sku", oRow.cells(0).content.ToString())
                    jItem.Add("url", oRow.cells(1).content.ToString.Replace("""", ""))
                    jItems.Add(jItem)
                End If
            Next
        End If
        Return retval
    End Function


End Class
