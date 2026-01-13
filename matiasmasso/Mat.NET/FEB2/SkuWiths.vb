Public Class SkuWiths
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oMgz As DTOMgz, oSkuParent As DTOProductSku, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOSkuWith))
        Return Await Api.Fetch(Of List(Of DTOSkuWith))(exs, "SkuWiths", oSkuParent.Guid.ToString, oMgz.Guid.ToString, FormatFch(DtFch))
    End Function

End Class

