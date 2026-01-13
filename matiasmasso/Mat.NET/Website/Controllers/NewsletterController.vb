Public Class NewsletterController
    Inherits _MatController

    '
    ' GET: /Newsletter

    Async Function Index(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oNewsletter = Await FEB.Newsletter.Find(guid, exs)
        Return View(oNewsletter)
    End Function

End Class