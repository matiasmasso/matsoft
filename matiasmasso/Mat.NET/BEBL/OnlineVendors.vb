Public Class OnlineVendor

    Shared Function Find(oGuid As Guid, Optional IncludeLogo As Boolean = False) As DTOOnlineVendor
            Dim retval As DTOOnlineVendor = OnlineVendorLoader.Find(oGuid, IncludeLogo)
            Return retval
        End Function

        Shared Function Load(ByRef oOnlineVendor As DTOOnlineVendor, Optional IncludeLogo As Boolean = False) As Boolean
            Dim retval As Boolean = OnlineVendorLoader.Load(oOnlineVendor, IncludeLogo)
            Return retval
        End Function

        Shared Function Update(oOnlineVendor As DTOOnlineVendor, ByRef exs As List(Of Exception)) As Boolean
            Dim retval As Boolean = OnlineVendorLoader.Update(oOnlineVendor, exs)
            Return retval
        End Function

        Shared Function Delete(oOnlineVendor As DTOOnlineVendor, ByRef exs As List(Of Exception)) As Boolean
            Dim retval As Boolean = OnlineVendorLoader.Delete(oOnlineVendor, exs)
            Return retval
        End Function



End Class

Public Class OnlineVendors

    Shared Function All(oSku As DTOProductSku, Optional oDomain As DTOWebPageAlias.domains = Nothing) As List(Of DTOOnlineVendor)
        Dim retval As List(Of DTOOnlineVendor) = OnlineVendorsLoader.All(oSku, oDomain)
        Return retval
    End Function

End Class

