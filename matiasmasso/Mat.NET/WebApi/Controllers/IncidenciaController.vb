Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class IncidenciaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/incidencia/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Incidencia.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la incidencia")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/incidencia/FromNum/{emp}/{id}")>
    Public Function FromNum(emp As DTOEmp.Ids, id As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Incidencia.FromNum(oEmp, id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la incidencia")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/incidencia/Trackings/{incidencia}")>
    Public Function Trackings(incidencia As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIncidencia = New DTOIncidencia(incidencia)
            Dim values = BEBL.Incidencia.Trackings(oIncidencia)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els tracking de la incidencia")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/incidencia/SpriteImage/{incidencia}")>
    Public Function SpriteImage(incidencia As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oIncidencia = New DTOIncidencia(incidencia)
            Dim value = BEBL.Incidencia.Sprite(oIncidencia)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la incidencia")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/incidencia")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOIncidencia)(json, exs)

            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Incidencia")
            Else
                Dim idx As Integer
                For Each oDocFile In value.Attachments
                    idx += 1
                    oDocFile.Thumbnail = oHelper.GetImage(String.Format("docfile_thumbnail_{0:000}", idx))
                    oDocFile.Stream = oHelper.GetFileBytes(String.Format("docfile_stream_{0:000}", idx))
                Next

                If BEBL.Incidencia.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar la incidencia")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error al pujar la incidencia")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/incidencia/delete")>
    Public Function Delete(<FromBody> value As DTOIncidencia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Incidencia.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Incidencia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Incidencia")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/incidencia/Catalog/{procedencia}/{customer}")>
    Public Function Catalog(procedencia As DTOIncidencia.Procedencias, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = BEBL.Customer.Find(customer)
            Dim values = BEBL.Incidencia.Catalog(procedencia, oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les marques")
        End Try
        Return retval
    End Function
End Class

Public Class IncidenciasController
    Inherits _BaseController

    <HttpPost>
    <Route("api/incidencias")>
    Public Function Model(<FromBody> oRequest As Models.IncidenciesModel.Request) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Incidencias.Model(oRequest)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les incidencies")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/incidencias/compact/fromUser/{user}")> ' per iMat
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Incidencias.Compact(oUser, DTOEnums.TitularCods.User)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les incidencies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/incidencias/compact/fromCustomer/{customer}")> ' per iMat
    Public Function fromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.Incidencias.Compact(oCustomer, DTOEnums.TitularCods.Contact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les incidencies")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/incidencias/query")>
    Public Function Query(<FromBody> oQuery As DTOIncidenciaQuery) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            BEBL.Incidencias.LoadQuery(oQuery)
            retval = Request.CreateResponse(HttpStatusCode.OK, oQuery)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les incidencies")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/incidencias/CodisDeTancament")>
    Public Function CodisDeTancament() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Incidencias.CodisDeTancament()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els codis de tancament de les  incidencies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/incidencias/Reposicions/{emp}/{year}")>
    Public Function Reposicions(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Incidencias.Reposicions(oEmp, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els codis de tancamentles reposicions de les incidencies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/incidencias/Ratios/{fchfrom}/{fchto}")>
    Public Function Ratios(fchfrom As Date, fchto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Incidencias.Ratios(fchfrom, fchto)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els ratios de incidencies")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/incidencias")> 'TO DEPRECATE
    Public Function ContactIncidencias(customer As DTOBaseGuid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer.Guid)
            Dim values = BEBL.Incidencias.Headers(Nothing, oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les incidencies")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/incidencias/withVideos")> 'TO DEPRECATE
    Public Function withvideos() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Incidencias.withVideos()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les incidencies")
        End Try
        Return retval
    End Function

End Class
