Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BancController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Banc/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Banc.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Banc")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Banc")>
    Public Function Update(<FromBody> value As DTOBanc) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Banc.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Banc")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Banc")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Banc/delete")>
    Public Function Delete(<FromBody> value As DTOBanc) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Banc.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Banc")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Banc")
        End Try
        Return retval
    End Function


End Class

Public Class BancsController
    Inherits _BaseController


    <HttpGet>
    <Route("api/bancs/all/{emp}")> 'for Mat.NET
    Public Function all(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Bancs.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els bancs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/bancs/{emp}")> 'for Mat.NET
    Public Function active(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Bancs.Active(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els bancs")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/bancs/BancsToReceiveTransfer/{emp}")>
    Public Function BancsToReceiveTransfer(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Bancs.BancsToReceiveTransfer(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els bancs")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/bancs/saldos/{user}")> 'for iMat 2019
    Public Function saldos(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.BancSdos.Last(oUser.Emp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els bancs")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/bancs/sprite/{emp}")> 'for Mat.NET
    Public Function activeSprite(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oImage = BEBL.Bancs.ActiveSprite(oEmp)
            retval = MyBase.HttpImageResponseMessage(oImage)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els logos dels bancs")
        End Try
        Return retval
    End Function


End Class


