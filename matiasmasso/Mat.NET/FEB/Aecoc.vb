Public Class Aecoc

    Shared Async Function NextEanToContact(oEmp As DTOEmp, oContact As DTOContact, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "aecoc/NextEanToContact", CInt(oEmp.Id), oContact.Guid.ToString())
    End Function

    Shared Async Function NextEanToSku(oEmp As DTOEmp, oSku As DTOProductSku, exs As List(Of Exception)) As Task(Of DTOEan)
        Return Await Api.Fetch(Of DTOEan)(exs, "aecoc/NextEanToSku", oEmp.Id, oSku.Guid.ToString())
    End Function

End Class
