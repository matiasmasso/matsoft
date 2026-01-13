Public Class StatQuotasOnline
    Shared Function All(Optional oProveidor As DTOContact = Nothing) As List(Of DTOStatQuotaOnline)
        Dim retval As List(Of DTOStatQuotaOnline) = StatQuotaOnlineLoader.Quotas(oProveidor)
        Return retval
    End Function

End Class