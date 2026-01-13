Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class VisaCardController
    Inherits _BaseController

    <HttpGet>
    <Route("api/VisaCard/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.VisaCard.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la VisaCard")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/VisaCard")>
    Public Function Update(<FromBody> value As DTOVisaCard) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.VisaCard.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la VisaCard")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la VisaCard")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/VisaCard/delete")>
    Public Function Delete(<FromBody> value As DTOVisaCard) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.VisaCard.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la VisaCard")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la VisaCard")
        End Try
        Return retval
    End Function

End Class

Public Class VisaCardsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/VisaCards/{emp}/{contact}/{active}")>
    Public Function All(emp As DTOEmp.Ids, contact As Guid, active As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = New DTOContact(contact)
            Dim values = BEBL.VisaCards.All(oEmp, oContact, active <> 0)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les VisaCards")
        End Try
        Return retval
    End Function

End Class
