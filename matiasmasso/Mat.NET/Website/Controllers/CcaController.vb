Public Class CcaController
    Inherits _MatController

    '
    ' GET: /Cca

    Async Function Index(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oCca = Await FEB.Cca.Find(guid, exs)
        Return View("Cca", oCca)
    End Function

End Class