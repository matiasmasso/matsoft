Public Class WebErrsController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim model = Await FEB2.WebErrs.All(exs)
        If exs.Count = 0 Then
            ViewBag.Title = "Web errors"
            ContextHelper.NavViewModel.ResetCustomMenu()
            retval = View(model)
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    <HttpPost>
    Async Function Delete(guid As Nullable(Of Guid)) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim result As Object = Nothing
        Dim oWebErr As New DTOWebErr(guid)
        If Await FEB2.WebErr.Delete(exs, oWebErr) Then
            result = New With {.success = True}
        Else
            result = New With {.success = False, .exs = exs}
        End If
        Dim retval = Json(result, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    <HttpPost>
    Async Function Reset() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim result As Object = Nothing
        If Await FEB2.WebErrs.Reset(exs) Then
            result = New With {.success = True}
        Else
            result = New With {.success = False, .exs = exs}
        End If
        Dim retval = Json(result, JsonRequestBehavior.AllowGet)
        Return retval
    End Function
End Class
