Public Class Risc

    Shared Async Function CreditLimit(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "Risc/CreditLimit", oCustomer.Guid.ToString())
    End Function

    Shared Async Function CreditDisponible(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "Risc/CreditDisponible", oCustomer.Guid.ToString())
    End Function

    Shared Async Function CreditDisposat(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "Risc/CreditDisposat", oCustomer.Guid.ToString())
    End Function

    Shared Async Function DipositIrrevocable(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "Risc/DipositIrrevocable", oCustomer.Guid.ToString())
    End Function

    Shared Async Function EntregatACompte(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "Risc/EntregatACompte", oCustomer.Guid.ToString())
    End Function

    Shared Async Function FrasPendentsDeVencer(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "Risc/FrasPendentsDeVencer", oCustomer.Guid.ToString())
    End Function

    Shared Async Function SdoAlbsNoCredit(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "Risc/SdoAlbsNoCredit", oCustomer.Guid.ToString())
    End Function

    Shared Async Function SdoAlbsACredit(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "Risc/SdoAlbsACredit", oCustomer.Guid.ToString())
    End Function


End Class
