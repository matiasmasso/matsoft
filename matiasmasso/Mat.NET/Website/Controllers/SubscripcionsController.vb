Public Class SubscripcionsController
    Inherits _MatController

    <HttpGet>
    Public Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim model = Await FEB.Subscriptions.All(exs, oUser)
        If exs.Count = 0 Then
            ViewBag.Title = ContextHelper.Tradueix("Subscripciones", "Subscripcions", "Subscriptions", "Subscrições")
            Return LoginOrView("Subscripcions", model)
        Else
            Return Await MyBase.ErrorResult(exs)
        End If
    End Function

    <HttpPost>
    Public Async Function UpdateSubscripcions(subscripcions As String) As Threading.Tasks.Task(Of String)
        Dim retval As String = ""

        Dim oSscs As New List(Of DTOSubscription)
        If subscripcions > "" Then
            Dim oGuids As List(Of Guid) = subscripcions.Split(",").Select(Function(x) New Guid(x)).ToList
            For Each oGuid As Guid In oGuids
                oSscs.Add(New DTOSubscription(oGuid))
            Next
        End If

        Dim exs As New List(Of Exception)
        If Await FEB.Subscriptions.Update(exs, ContextHelper.GetUser, oSscs) Then
            retval = New MatJSonObject("value", 1, "text", ContextHelper.Tradueix("subscripciones actualizadas correctamente", "subscripcions actualitzades correctament", "subscriptions updated successfully")).ToString
        Else
            retval = New MatJSonObject("value", 2, "text", ContextHelper.Tradueix("error al actualizar las subscripciones", "s'ha produit una errada al actualitzar les subscripcions", "subscriptions could not be updated")).ToString
        End If
        Return retval
    End Function

End Class
