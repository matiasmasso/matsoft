Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class ZipController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Zip/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Zip.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Zip")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Zip/{country}/{zip}")>
    Public Function FromZipCod(country As Guid, zip As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim value = BEBL.Zip.FromZipCod(oCountry, zip)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Zip")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/zip")>
    Public Function Update(<FromBody> value As DTOZip) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Zip.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Zip")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Zip")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Zip/delete")>
    Public Function Delete(<FromBody> value As DTOZip) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Zip.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Zip")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Zip")
        End Try
        Return retval
    End Function

End Class

Public Class ZipsController
    Inherits _BaseController


    <HttpGet>
    <Route("api/zips/{lang}")>
    Public Function All(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.Zips.All(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els Zips")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Zips/FromZipcod/{country}")>
    Public Function FromZipcod(country As Guid, <FromBody> ZipCod As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim values = BEBL.Zips.All(oCountry, ZipCod)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els codis postals")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/zips/tree/{lang}")>
    Public Function Tree(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.Zips.Tree(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els Zips")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/zips/merge")>
    Public Function Merge(<FromBody> guids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Zips.Merge(exs, guids) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al combinar els Zips")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error  al combinar els Zips")
        End Try
        Return retval
    End Function
End Class