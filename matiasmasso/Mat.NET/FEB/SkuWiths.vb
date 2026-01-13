Public Class SkuWiths
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oMgz As DTOMgz, oSkuParent As DTOProductSku, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOSkuWith))
        Return Await Api.Fetch(Of List(Of DTOSkuWith))(exs, "SkuWiths", oSkuParent.Guid.ToString, oMgz.Guid.ToString, FormatFch(DtFch))
    End Function

    Shared Async Function Update(exs As List(Of Exception), parent As Guid, children As List(Of GuidQty)) As Threading.Tasks.Task(Of Boolean)
        Return Await Api.Execute(Of List(Of GuidQty), Boolean)(children, exs, "SkuWiths", parent.ToString())
    End Function

    Shared Async Function Delete(exs As List(Of Exception), parent As Guid) As Threading.Tasks.Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "SkuWiths/Delete", parent.ToString())
    End Function

End Class

