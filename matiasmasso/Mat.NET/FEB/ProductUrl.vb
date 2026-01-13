Public Class ProductUrl
    Inherits _FeblBase

    Shared Async Function Search(exs As List(Of Exception), oEmp As DTOEmp, url As String) As Task(Of DTOProduct.ProductAndTab)
        Dim retval = Await Api.Execute(Of String, DTOProduct.ProductAndTab)(url, exs, "productUrl", oEmp.Id)
        Return retval
    End Function

End Class
