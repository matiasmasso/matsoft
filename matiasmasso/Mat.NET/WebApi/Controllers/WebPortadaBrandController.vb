Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WebPortadaBrandController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebPortadaBrand/{brand}")>
    Public Function Find(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim value = BEBL.WebPortadaBrand.Find(oBrand)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WebPortadaBrand")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/WebPortadaBrand/image/{brand}")>
    Public Function GetIcon(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim oWebPortadaBrand As New DTOWebPortadaBrand(oBrand)
            Dim value = BEBL.WebPortadaBrand.Image(oWebPortadaBrand)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del WebPortadaBrand")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WebPortadaBrand")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOWebPortadaBrand)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la WebPortadaBrand")
            Else
                value.Image = oHelper.GetImage("image")

                If DAL.WebPortadaBrandLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.WebPortadaBrandLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.WebPortadaBrandLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/WebPortadaBrand/delete")>
    Public Function Delete(<FromBody> value As DTOWebPortadaBrand) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebPortadaBrand.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WebPortadaBrand")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WebPortadaBrand")
        End Try
        Return retval
    End Function

End Class

Public Class WebPortadaBrandsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebPortadaBrands/{channel}")>
    Public Function All(channel As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oChannel As DTODistributionChannel = Nothing
            If channel <> Nothing Then oChannel = New DTODistributionChannel(channel)
            Dim values = BEBL.WebPortadaBrands.All(oChannel)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WebPortadaBrands")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/WebPortadaBrands/sprite/{channel}")>
    Public Function Sprite(channel As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oChannel As DTODistributionChannel = Nothing
            If channel <> Nothing Then oChannel = New DTODistributionChannel(channel)
            Dim value = BEBL.WebPortadaBrands.Sprite(oChannel)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WebPortadaBrands")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/WebPortadaBrands/sort")>
    Public Function Sort(<FromBody> values As List(Of DTOWebPortadaBrand)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebPortadaBrands.Sort(values, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir les WebPortadaBrands")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WebPortadaBrands")
        End Try
        Return retval
    End Function

End Class
