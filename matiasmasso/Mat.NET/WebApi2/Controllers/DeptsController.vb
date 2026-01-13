Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DeptController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Dept/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Dept.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Dept")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Dept/FromNom/{brand}/{nom}")>
    Public Function FromNom(brand As Guid, nom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim value = BEBL.Dept.FromNom(oBrand, nom)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Dept")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/dept/categories/{guid}")>
    Public Function Categories(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDept = BEBL.Dept.Find(guid)
            Dim oCategories = BEBL.Dept.Categories(oDept)
            retval = Request.CreateResponse(HttpStatusCode.OK, oCategories)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les categories del Dept")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Dept/banner/{guid}")>
    Public Function Banner(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDept As New DTODept(guid)
            Dim value = BEBL.Dept.Banner(oDept)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el banner del Dept")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Dept")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)

        Dim resultHash As String = ""
        Dim result As HttpResponseMessage = Nothing

        Try

            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim oDept = ApiHelper.Client.DeSerialize(Of DTODept)(json, exs)
            If oDept Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el departament")
            Else
                oDept.Banner = oHelper.GetImage("banner")
                BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Depts)
                If DAL.DeptLoader.Update(oDept, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el banner a DAL.DeptLoader.Upload")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.DocFileLoader.Upload")
        End Try

        Return result
    End Function

    <HttpPost>
    <Route("api/Dept/delete")>
    Public Function Delete(<FromBody> value As DTODept) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Dept.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Dept")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Dept")
        End Try
        Return retval
    End Function

End Class

Public Class DeptsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Depts")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Depts.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Depts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Depts/{brand}")>
    Public Function AllFromBrand(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim values = BEBL.Depts.All(oBrand)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Depts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Depts/AllWithFilters/{brand}")>
    Public Function AllWithFilters(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim values = BEBL.Depts.AllWithFilters(oBrand)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Depts")
        End Try
        Return retval
    End Function




    <HttpGet>
    <Route("api/Depts/headers/{brand}")>
    Public Function All(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim values = BEBL.Depts.Headers(oBrand)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Depts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Depts/Sprite/{brand}")>
    Public Function Sprite(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim oSprite = BEBL.Depts.Sprite(oBrand)
            retval = MyBase.HttpImageResponseMessage(oSprite)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir l'sprite dels Depts")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Depts/Swap/{dept1}/{dept2}")>
    Public Function Swap(dept1 As Guid, dept2 As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oDept1 As New DTODept(dept1)
            Dim oDept2 As New DTODept(dept2)
            If BEBL.Depts.Swap(exs, oDept1, oDept2) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al ordenar els departaments")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al ordenar els departaments")
        End Try
        Return retval
    End Function



End Class
