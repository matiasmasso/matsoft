
Public Class SpvController
    Inherits _BaseController

    <HttpPost>
    <Route("api/spvs/ArrivalPending")>
    Public Function Spvs_ArrivalPending(data As DTOBaseGuid) As List(Of DTOSpv)
        Dim retval As List(Of DTOSpv) = BLLSpvs.ArrivalPending
        Return retval
    End Function

    <HttpGet>
    <Route("api/spvs/ArrivalPending")>
    Public Function Spvs_ArrivalPending() As List(Of DTOSpv)
        Dim retval As List(Of DTOSpv) = BLLSpvs.ArrivalPending
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/spvs")>
    Public Function ContactSpvs(customer As DTOBaseGuid) As List(Of DTOSpv)
        Dim oCustomer As New DTOCustomer(customer.Guid)
        Dim retval As List(Of DTOSpv) = BLLSpvs.All(oCustomer)
        Return retval
    End Function


End Class
