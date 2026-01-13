Public Class Tax

    Shared Function Find(oGuid As Guid) As DTOTax
        Dim retval As DTOTax = TaxLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oTax As DTOTax, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TaxLoader.Update(oTax, exs)
        Return retval
    End Function

    Shared Function Delete(oTax As DTOTax, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TaxLoader.Delete(oTax, exs)
        Return retval
    End Function

End Class


Public Class Taxes

    Shared Function All() As List(Of DTOTax)
        Dim retval = TaxesLoader.All
        Return retval
    End Function

End Class
