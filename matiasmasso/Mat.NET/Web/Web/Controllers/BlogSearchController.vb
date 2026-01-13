Public Class BlogSearchController
    Inherits _MatController

    <HttpPost>
    Async Function Index(searchKey As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oRequest As New DTOSearchRequest()
        With oRequest
            .User = MyBase.User
            If ContextHelper.Lang.Tag = "POR" Then
                .Lang = DTOLang.POR
            Else
                .Lang = DTOLang.ESP
            End If
            .SearchKey = searchKey
        End With
        Dim oResult = Await FEB2.LangText.Search(exs, oRequest)
        If exs.Count = 0 Then
            Return View("Search", oResult)
        Else
            Return Await ErrorResult(exs)
        End If

    End Function
End Class
