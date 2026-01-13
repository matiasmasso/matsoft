Public Class CustomerPlatform
    Shared Function Find(oGuid As Guid) As DTOCustomerPlatform
        Dim retval As DTOCustomerPlatform = CustomerPlatformloader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oCustomerPlatform As DTOCustomerPlatform) As Boolean
        Dim retval As Boolean = CustomerPlatformloader.Load(oCustomerPlatform)
        Return retval
    End Function

    Shared Function Update(oCustomerPlatform As DTOCustomerPlatform, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CustomerPlatformloader.Update(oCustomerPlatform, exs)
        Return retval
    End Function

    Shared Function Delete(oCustomerPlatform As DTOCustomerPlatform, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CustomerPlatformloader.Delete(oCustomerPlatform, exs)
        Return retval
    End Function

End Class

Public Class CustomerPlatforms

    Shared Function All(oParent As DTOContact) As List(Of DTOCustomerPlatform)
        Dim retval As List(Of DTOCustomerPlatform) = CustomerPlatformsLoader.All(oParent)
        Return retval
    End Function

End Class

