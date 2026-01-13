Public Class EscripturesController
    Inherits _MatController

    '
    ' GET: /Escriptures

    Function Index() As ActionResult
        Dim exs As New List(Of Exception)
        Dim Model = FEB.Escripturas.AllSync(Website.GlobalVariables.Emp, exs)
        If exs.Count = 0 Then
            Return View("Escriptures", Model)
        Else
            Return View("Error")
        End If
    End Function

End Class