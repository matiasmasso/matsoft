Public Class DeptsController
    Inherits _MatController

    Async Function Index(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim Model = Await FEB2.Dept.Find(guid, exs)
        If exs.Count = 0 Then
            Return View(Model)
        Else
            Return View("Error")
        End If
    End Function



End Class
