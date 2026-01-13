Imports System.Net
Imports System.Net.Http

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

    <HttpGet>
    <Route("api/spvs/SpvsNotRead")>
    Public Function SpvsNotRead() As List(Of DTOSpv)
        Dim retval As New List(Of DTOSpv)
        Dim exs As New List(Of Exception)
        BLLSpvs.NotRead(retval, exs)
        Return retval
    End Function

    <HttpPost>
    <Route("api/spv/FromNum")>
    Public Function SpvFromNum(src As DTOSpv) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Dim oSpv As DTOSpv = BLLSpv.FromId(src.FchAvis.Year, src.Id)
        If oSpv Is Nothing Then
            Dim sMsg As String = String.Format("no s'ha trobat cap reparacio amb el numero {0}/{1}", src.FchAvis.Year, src.Id)
            retval = New HttpResponseMessage(HttpStatusCode.NotFound)
            retval.Content = New StringContent(sMsg)
            retval.ReasonPhrase = sMsg
        Else
            BLLSpv.Load(oSpv)
            oSpv.Product.Nom = BLLProduct.FullNom(oSpv.Product)
            retval = Request.CreateResponse(Of DTOSpv)(HttpStatusCode.OK, oSpv)
        End If
        Return retval
    End Function


End Class
