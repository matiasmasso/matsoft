Public Class ComputerController
    Inherits _MatController

    '
    ' GET: /Cca

    Async Function Index(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim model = Await FEB.Computer.Find(exs, guid)
        Return View(model)
    End Function

End Class