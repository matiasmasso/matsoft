Public Class Stocks 'TO DEPRECATE ============
    Inherits _FeblBase

    Shared Async Function ForWeb(oUser As DTOUser, exs As List(Of Exception)) As Task(Of DTOProductCatalog) 'TO DEPRECATE ============
        'to deprecate, mirar de passar a ForWeb2 que es mes lleuger i dona menys problemes
        Dim retval = Await Api.Fetch(Of DTOProductCatalog)(exs, "SkuStocks/ForWeb", oUser.Guid.ToString)
        Return retval
    End Function

End Class
