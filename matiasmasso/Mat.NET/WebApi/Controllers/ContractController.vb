Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContractController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Contract/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Contract.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Contract")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Contract")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOContract)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Contract")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.ContractLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.ContractLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.ContractLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Contract/delete")>
    Public Function Delete(<FromBody> value As DTOContract) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Contract.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Contract")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Contract")
        End Try
        Return retval
    End Function

End Class

Public Class ContractsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Contracts/{user}/{codi}/{contact}")>
    Public Function All(user As Guid, codi As Guid, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                Dim oContact As DTOContact = Nothing
                If contact <> Guid.Empty Then
                    oContact = New DTOContact(contact)
                End If

                Dim oCodi As DTOContractCodi = Nothing
                If codi <> Guid.Empty Then
                    oCodi = New DTOContractCodi(codi)
                End If

                Dim values = BEBL.Contracts.All(oUser, oCodi, oContact)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contracts")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Contracts/fromContact/{contact}")>
    Public Function All(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.Contracts.All(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contracts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Contracts/fromUser/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Contracts.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contracts")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Contracts/{user}/{codi}")>
    Public Function All(user As Guid, codi As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                Dim oCodi As DTOContractCodi = Nothing
                If codi <> Guid.Empty Then
                    oCodi = New DTOContractCodi(codi)
                End If

                Dim values = BEBL.Contracts.All(oUser, oCodi)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contracts")
        End Try
        Return retval
    End Function





End Class
