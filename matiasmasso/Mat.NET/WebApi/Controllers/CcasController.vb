Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CcaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Cca/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Cca.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Cca")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Cca/fromNum/{emp}/{year}/{id}")>
    Public Function FromNum(emp As Integer, year As Integer, id As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Cca.FromNum(oEmp, year, id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'assentament")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Cca/pdf/{guid}")>
    Public Function Pdf(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca As New DTOCca(guid)
            Dim value = BEBL.Cca.Pdf(oCca)
            retval = MyBase.HttpPdfResponseMessage(value, "M+O-justificant")
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al baixar el justificant de l'assentament")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Cca")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOCca)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Cca")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.CcaLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK, value.Id)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.CcaLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.CcaLoader")
        End Try

        Return result
    End Function


    'Uploads the docfile and updates just the Cca table docfile hash 
    <HttpPost>
    <Route("api/Cca/Docfile")>
    Public Function UploadDocfile() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOCca)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Cca")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.CcaLoader.UpdateDocfile(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.CcaLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.CcaLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Cca/delete")>
    Public Function Delete(<FromBody> value As DTOCca) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cca.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Cca")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Cca")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Cca/IvaFchUltimaDeclaracio/{emp}")>
    Public Function IvaFchUltimaDeclaracio(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Cca.IvaFchUltimaDeclaracio(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la data ultima declaracio Iva")
        End Try
        Return retval
    End Function

End Class

Public Class CcasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Ccas/{year}")>
    Public Function Model(year As Integer) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = MyBase.GetUser(exs)
            If exs.Count = 0 Then
                Dim values = BEBL.Ccas.Model(oUser, year)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els assentaments")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els assentaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <CustomCompression> 'envia 20 vegades menys volum de dades pero està un 50% més de temps? per deserialitzar-les
    <Route("api/Ccas2/{year}")>
    Public Function Model2(year As Integer) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = MyBase.GetUser(exs)
            If exs.Count = 0 Then
                Dim values = BEBL.Ccas.Model(oUser, year)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els assentaments")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els assentaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ccas/Headers/{emp}/{year}")>
    Public Function Headers(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Ccas.Headers(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els assentaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <CustomCompression> 'envia 20 vegades menys volum de dades pero està un 50% més de temps per deserialitzar-les
    <Route("api/Ccas/Headers2/{emp}/{year}")>
    Public Function Headers2(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Ccas.Headers(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els assentaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ccas/{emp}/{year}")>
    Public Function All(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Ccas.All(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els assentaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ccas/descuadres/{emp}/{year}")>
    Public Function Descuadres(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Ccas.Descuadres(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els descuadres de l'any")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Ccas")>
    Public Function Update(<FromBody> values As List(Of DTOCca)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Ccas.Update(exs, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar els assentaments")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els assentaments")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Ccas/LlibreDiari/{emp}/{year}")>
    Public Function LlibreDiari(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oExercici = MyBase.GetExercici(emp, year)
            Dim values = BEBL.Ccas.LlibreDiari(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els assentaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ccas/LlibreDiari/Excel/{emp}/{year}/{lang}")>
    Public Function LlibreDiariExcel(emp As DTOEmp.Ids, year As Integer, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oExercici = MyBase.GetExercici(emp, year)
            Dim oLang = DTOLang.Factory(lang)

            Dim oSheet = BEBL.Ccas.LlibreDiariExcel(oExercici, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, oSheet)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els assentaments")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Ccas/LlibreMajor/Excel/{emp}/{year}/{lang}")>
    Public Function LlibreMajorExcel(emp As DTOEmp.Ids, year As Integer, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oExercici = MyBase.GetExercici(emp, year)
            Dim oLang = DTOLang.Factory(lang)

            Dim oSheet = BEBL.Ccas.LlibreMajorExcel(oExercici, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, oSheet)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els assentaments")
        End Try
        Return retval
    End Function
End Class
