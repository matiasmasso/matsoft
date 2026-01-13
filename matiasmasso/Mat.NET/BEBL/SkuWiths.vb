Public Class SkuWiths
#Region "CRUD"
    Shared Function Find(oSkuParent As DTOProductSku, oMgz As DTOMgz, Optional DtFch As Date = Nothing) As List(Of DTOSkuWith)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim retval As List(Of DTOSkuWith) = SkuWithsLoader.Find(oSkuParent, oMgz, DtFch)
        Return retval
    End Function

    Shared Function Update(oParent As Guid, oChildren As List(Of GuidQty), exs As List(Of Exception)) As Boolean
        Return SkuWithsLoader.Update(oParent, oChildren, exs)
    End Function

    Shared Function Delete(oParent As Guid, exs As List(Of Exception)) As Boolean
        Return SkuWithsLoader.Delete(oParent, exs)
    End Function
#End Region
End Class
