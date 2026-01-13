Public Class AtlasController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim model = Await FEB.Atlas.Compact(exs, ContextHelper.GetUser())
        If exs.Count = 0 Then
            ViewBag.Title = ContextHelper.Tradueix("Listado de clientes", "Llistat de clients", "Customer list")
            Return View("Contacts", model)
        Else
            Await ErrorResult(exs)
        End If
        Return retval
    End Function


End Class
