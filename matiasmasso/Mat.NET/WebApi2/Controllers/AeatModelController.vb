Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AeatModelController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AeatModel/{guid}/{user}")>
    Public Function WithDocs(guid As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                Throw New Exception("Usuario no autorizado")
            Else
                Select Case oUser.Rol.id
                    Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.auditor
                    Case DTORol.Ids.banc
                    Case Else
                        Throw New Exception("Usuario no autorizado")
                End Select
            End If

            Dim value = BEBL.AeatModel.Find(guid, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AeatModel")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AeatModel")>
    Public Function Update(<FromBody> value As DTOAeatModel) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AeatModel.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la AeatModel")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la AeatModel")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AeatModel/delete")>
    Public Function Delete(<FromBody> value As DTOAeatModel) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AeatModel.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la AeatModel")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la AeatModel")
        End Try
        Return retval
    End Function

End Class

Public Class AeatModelsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AeatModels/{user}")>
    Public Function All(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                Throw New Exception("Usuario no autorizado")
            Else
                Select Case oUser.Rol.id
                    Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.accounts, DTORol.Ids.auditor
                    Case DTORol.Ids.banc
                    Case Else
                        Throw New Exception("Usuario no autorizado")
                End Select
            End If

            Dim values = BEBL.AeatModels.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els models d'Hisenda")
        End Try
        Return retval
    End Function


End Class
