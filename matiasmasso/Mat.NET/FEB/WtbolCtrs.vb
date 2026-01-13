Public Class WtbolCtr
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOWtbolCtr)
        Dim retval = Await Api.Fetch(Of DTOWtbolCtr)(exs, "WtbolCtr", oGuid.ToString())
        retval.RestoreObjects()
        Return retval
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWtbolCtr As DTOWtbolCtr) As Boolean
        If Not oWtbolCtr.IsLoaded And Not oWtbolCtr.IsNew Then
            Dim pWtbolCtr = Api.FetchSync(Of DTOWtbolCtr)(exs, "WtbolCtr", oWtbolCtr.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWtbolCtr)(pWtbolCtr, oWtbolCtr, exs)
                oWtbolCtr.RestoreObjects()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Log(exs As List(Of Exception), oLandingPage As DTOWtbolLandingPage, sIp As String) As Task(Of Boolean)
        Return Await Api.Execute(Of String, Boolean)(sIp, exs, "WtbolCtr/log", oLandingPage.Guid.ToString())
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oWtbolCtr As DTOWtbolCtr) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWtbolCtr)(oWtbolCtr, exs, "WtbolCtr")
    End Function

End Class

Public Class WtbolCtrs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional site As DTOWtbolSite = Nothing, Optional FchFrom As Date = Nothing, Optional FchTo As Date = Nothing) As Task(Of List(Of DTOWtbolCtr))
        Dim retval = Await Api.Fetch(Of List(Of DTOWtbolCtr))(exs, "WtbolCtrs", OpcionalGuid(site), FormatFch(FchFrom), FormatFch(FchTo))
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

End Class
