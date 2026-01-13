Public Class CustomerRanking

    Shared Function Load(ByRef value As DTOCustomerRanking, exs As List(Of Exception)) As Boolean
        Dim areaGuid As Guid = Nothing
        If value.Area IsNot Nothing Then areaGuid = value.Area.Guid
        Dim productGuid As Guid = Nothing
        If value.Product IsNot Nothing Then productGuid = value.Product.Guid
        value = Api.FetchSync(Of DTOCustomerRanking)(exs, "CustomerRanking", value.User.Guid.ToString, productGuid.ToString, areaGuid.ToString, value.FchFrom.ToString("yyyy-MM-dd"), value.FchTo.ToString("yyyy-MM-dd"))
        Return True
    End Function

    Shared Async Function Csv(value As DTOCustomerRanking, exs As List(Of Exception)) As Task(Of DTOCsv)
        Dim areaGuid As Guid = Nothing
        If value.Area IsNot Nothing Then areaGuid = value.Area.Guid
        Dim productGuid As Guid = Nothing
        If value.Product IsNot Nothing Then productGuid = value.Product.Guid
        Return Await Api.Fetch(Of DTOCsv)(exs, "CustomerRanking/Csv", value.User.Guid.ToString, productGuid.ToString, areaGuid.ToString, value.FchFrom.ToString("yyyy-MM-dd"), value.FchTo.ToString("yyyy-MM-dd"))
    End Function

    Shared Function CsvUrl(value As DTOCustomerRanking) As String
        Dim areaGuid As Guid = Nothing
        If value.Area IsNot Nothing Then areaGuid = value.Area.Guid
        Dim productGuid As Guid = Nothing
        If value.Product IsNot Nothing Then productGuid = value.Product.Guid
        Dim retval = Api.ApiUrl("CustomerRanking/Csv", value.User.Guid.ToString, productGuid.ToString, areaGuid.ToString, value.FchFrom.ToString("yyyy-MM-dd"), value.FchTo.ToString("yyyy-MM-dd"))
        Return retval
    End Function



End Class
