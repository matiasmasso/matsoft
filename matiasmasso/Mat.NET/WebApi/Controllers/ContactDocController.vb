
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContactDocController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContactDoc/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ContactDoc.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ContactDoc")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ContactDoc")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOContactDoc)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la ContactDoc")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.ContactDocLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.ContactDocLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.ContactDocLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/ContactDoc/delete")>
    Public Function Delete(<FromBody> value As DTOContactDoc) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ContactDoc.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ContactDoc")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ContactDoc")
        End Try
        Return retval
    End Function

End Class

Public Class ContactDocsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContactDocs/{contact}/{year}/{oType}")>
    Public Function All(contact As Guid, year As Integer, oType As DTOContactDoc.Types) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As DTOContact = Nothing
            If contact <> Nothing Then oContact = New DTOContact(contact)
            Dim values = BEBL.ContactDocs.All(oContact, year, oType)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ContactDocs")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/ContactDocs/fromUser/{user}/{oType}")>
    Public Function FromUser(user As Guid, oType As DTOContactDoc.Types) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.ContactDocs.All(oUser, oType)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Mod145")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ContactDocs/fromContact/{contact}/{oType}")>
    Public Function FromContact(contact As Guid, oType As DTOContactDoc.Types) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.ContactDocs.All(oContact, oType:=oType)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Mod145")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/ContactDocs/Mod145s/{emp}/{year}")>
    Public Function Mod145s(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici = New DTOExercici(oEmp, year)
            Dim values = BEBL.ContactDocs.Mod145s(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Mod145")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/ContactDocs/Mod145s/Excel/{emp}/{year}")>
    Public Function Excel(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici = New DTOExercici(oEmp, year)
            Dim values = BEBL.ContactDocs.ExcelMod145s(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Mod145")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ContactDocs/Retencions")>
    Public Function Retencions(<FromBody> oContacts As List(Of DTOContact)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ContactDocs.Retencions(oContacts)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les retencions")
        End Try
        Return retval
    End Function



End Class
