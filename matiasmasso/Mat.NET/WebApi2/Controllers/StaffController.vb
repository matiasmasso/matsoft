Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StaffController
    Inherits _BaseController

    <HttpGet>
    <Route("api/staff/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Staff.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la staff")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/staff/avatar/{guid}")>
    Public Function GetIcon(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim bytes = BEBL.Staff.Avatar(guid)
            Dim value = ImageMime.Factory(bytes)
            retval = MyBase.HttpImageMimeResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el avatar del staff")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/staff/upload")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOStaff)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la staff")
            Else
                value.Avatar = oHelper.GetImage("avatar")

                If DAL.StaffLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.staffLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.staffLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/staff/delete")>
    Public Function Delete(<FromBody> value As DTOStaff) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Staff.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la staff")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la staff")
        End Try
        Return retval
    End Function


End Class

Public Class StaffsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Staffs/{emp}/{year}")>
    Public Function All(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As DTOExercici = Nothing
            If year > 0 Then
                oExercici = New DTOExercici(oEmp, year)
            End If
            Dim values = BEBL.Staffs.All(oEmp, oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Staff")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Staffs/active/{emp}")>
    Public Function Active(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Staffs.Active(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Staff")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Staffs/{emp}")>
    Public Function AllStaffs(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Staffs.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Staff")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/staffs/active/sprite/{emp}/{width}/{height}")> 'for Mat.NET
    Public Function sprite(emp As Integer, width As Integer, height As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oSprite = BEBL.Staffs.Sprite(oEmp, True, width, height)
            retval = MyBase.HttpImageResponseMessage(oSprite)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els avatars del personal")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/staffs/saldos/{emp}/{year}")> 'for Mat.NET
    Public Function saldos(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Staffs.Saldos(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els saldos dels reps")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/staffs/ibans/{emp}")> 'for Mat.NET
    Public Function ibans(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Staffs.Ibans(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els ibans dels reps")
        End Try
        Return retval
    End Function


End Class

