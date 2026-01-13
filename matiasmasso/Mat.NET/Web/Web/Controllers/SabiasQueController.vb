Public Class SabiasQueController
    Inherits _MatController

    Async Function Index(Optional tag As String = "") As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim model = Await FEB2.SabiasQue.Search(exs, tag)

        If model Is Nothing Then
            Return Await ErrorResult(String.Format("Content '{0}' not found", tag))
        Else
            If exs.Count = 0 Then
                ViewBag.Title = model.Title.Tradueix(ContextHelper.Lang)
                Return View(model)
            Else
                Return Await ErrorResult(exs)
            End If
        End If

    End Function

End Class