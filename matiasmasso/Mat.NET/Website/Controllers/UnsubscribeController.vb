Public Class UnsubscribeController
    Inherits _MatController


    Async Function Index(subscription As Guid, user As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)

        Dim Model = Await FEB.Subscriptor.Find(exs, subscription, user)
        retval = View("Unsubscribe", Model)

        Return retval
    End Function

    <HttpPost>
    Async Function Index(model As DTOSubscriptor) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)

        If Await FEB.Subscriptor.UnSubscribe(exs, model) Then
            retval = View("Unsubscribe", model)
        Else
            retval = Await MyBase.ErrorResult(exs)
        End If

        Return retval
    End Function


End Class



