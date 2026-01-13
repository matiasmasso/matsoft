Public Class SearchController
    Inherits _MatController

    <HttpPost>
    Async Function SearchAction(SearchBox As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oRequest As New DTOSearchRequest()
        With oRequest
            If ContextHelper.Lang.Tag = "POR" Then
                .Lang = DTOLang.POR
            Else
                .Lang = DTOLang.ESP
            End If
            .SearchKey = SearchBox
        End With
        Dim oResult = Await FEB2.LangText.Search(exs, oRequest)
        If exs.Count = 0 Then
            Return View("Search", oResult)
        Else
            Return Await ErrorResult(exs)
        End If

    End Function

    Async Function Index(SearchKey As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oSearchRequest = DTOSearchRequest.Factory(GlobalVariables.Emp, oUser, ContextHelper.Lang, SearchKey)
        Dim model As DTOSearchRequest = Await FEB2.SearchRequest.Load(oSearchRequest, exs)
        If exs.Count = 0 Then
            ViewBag.Title = MatHelperStd.UrlHelper.Title(ContextHelper.Tradueix("página de búsqueda", "cercador", "search page"))
            ViewBag.SearchKey = SearchKey
            ViewBag.MetaDescription = "Importador exclusivo de Britax Römer, Bob, 4moms y Tommee Tippee para España, Portugal y Andorra"
            ContextHelper.NavViewModel.ResetCustomMenu()
            Return View("Search", model)
        Else
            Return Await MyBase.ErrorResult(exs)
        End If
    End Function

    Async Function SearchRequest(SearchKey As String) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        ViewBag.SearchKey = SearchKey
        Dim oUser = ContextHelper.GetUser()
        Dim oSearchRequest = DTOSearchRequest.Factory(GlobalVariables.Emp, oUser, ContextHelper.Lang, SearchKey)
        Dim model = Await FEB2.SearchRequest.Load(oSearchRequest, exs)
        Return PartialView("_Search", model)
    End Function

End Class