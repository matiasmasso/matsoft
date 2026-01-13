Public Class SkuController
    Inherits _MatController

    Async Function Index(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oSku = Await FEB.ProductSku.Find(exs, guid)
        Dim url As String = oSku.GetUrl(ContextHelper.Lang)
        Dim retval As ActionResult = Redirect(url)
        Return retval
    End Function

End Class
