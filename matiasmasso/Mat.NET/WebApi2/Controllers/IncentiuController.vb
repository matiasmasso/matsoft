Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class IncentiuController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Incentiu/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Incentiu.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'incentiu")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Incentiu")>
    Public Function Update(<FromBody> value As DTOIncentiu) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Incentiu.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Incentiu")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Incentiu")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Incentiu/delete")>
    Public Function Delete(<FromBody> value As DTOIncentiu) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Incentiu.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Incentiu")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Incentiu")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Incentiu/PurchaseOrders/{incentiu}/{user}")>
    Public Function PurchaseOrders(incentiu As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIncentiu As New DTOIncentiu(incentiu)
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Incentiu.PurchaseOrders(oIncentiu, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'incentiu")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Incentiu/Participants/{incentiu}")>
    Public Function Participants(incentiu As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIncentiu As New DTOIncentiu(incentiu)
            Dim values = BEBL.Incentiu.Participants(oIncentiu)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els participants en l'Incentiu")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Incentiu/DeliveryAddresses/{incentiu}")>
    Public Function DeliveryAddresses(incentiu As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIncentiu As New DTOIncentiu(incentiu)
            Dim values = BEBL.Incentiu.DeliveryAddresses(oIncentiu)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al redactar l'Excel de adreces de l'Incentiu")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Incentiu/PncProducts/{incentiu}")>
    Public Function PncProducts(incentiu As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIncentiu As New DTOIncentiu(incentiu)
            Dim values = BEBL.Incentiu.PncProducts(oIncentiu)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els productes demanats a l'Incentiu")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Incentiu/ExcelResults/{incentiu}")>
    Public Function ExcelResults(incentiu As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIncentiu As New DTOIncentiu(incentiu)
            Dim value = BEBL.Incentiu.ExcelResults(oIncentiu)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al redactar l'Excel dels productes demanats a l'Incentiu")
        End Try
        Return retval
    End Function

End Class

Public Class IncentiusController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Incentius/{user}/{IncludeObsolets}/{IncludeFutureIncentius}")>
    Public Function All(user As Guid, IncludeObsolets As Integer, IncludeFutureIncentius As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Incentius.All(oUser, (IncludeObsolets = 1), (IncludeFutureIncentius = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Incentius")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Incentius/Headers/{user}/{IncludeObsolets}/{IncludeFutureIncentius}")>
    Public Function Headers(user As Guid, IncludeObsolets As Integer, IncludeFutureIncentius As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Incentius.Headers(oUser, (IncludeObsolets = 1), (IncludeFutureIncentius = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Incentius")
        End Try
        Return retval
    End Function

End Class
