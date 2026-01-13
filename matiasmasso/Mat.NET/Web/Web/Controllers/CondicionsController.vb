Public Class CondicionsController
    Inherits _MatController

    Async Function Index(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oCondicio = Await FEB2.Condicio.Find(guid, exs)
        If exs.Count = 0 Then
            ViewBag.Title = oCondicio.Title.Tradueix(Lang)
            Return View(oCondicio)
        Else
            Return Await MyBase.ErrorResult(exs)
        End If
    End Function

End Class