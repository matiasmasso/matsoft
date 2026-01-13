Public Class Cataleg

    Shared Async Function FromEmp(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of DTOProductCatalog)
        Return Await Api.Fetch(Of DTOProductCatalog)(exs, "Cataleg/fromEmp", CInt(oEmp.Id).ToString())
    End Function

    Shared Async Function FromContact(oContact As DTOContact, exs As List(Of Exception)) As Task(Of DTOCompactCataleg)
        Return Await Api.Fetch(Of DTOCompactCataleg)(exs, "Cataleg/fromContact", oContact.Guid.ToString())
    End Function

    Shared Async Function CustomerBasicTree(exs As List(Of Exception), oCustomer As DTOCustomer, oLang As DTOLang) As Task(Of DTOBasicCatalog)
        Return Await Api.Fetch(Of DTOBasicCatalog)(exs, "ProductCatalog/CustomerBasicTree", oCustomer.Guid.ToString, oLang.Tag)
    End Function
End Class
