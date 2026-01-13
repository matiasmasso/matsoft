
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class JornadaLaboralController
    Inherits _BaseController

    <HttpGet>
    <Route("api/JornadaLaboral/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.JornadaLaboral.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la JornadaLaboral")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/JornadaLaboral/log/{usr}")>
    Public Function Log(usr As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oUser = BEBL.User.Find(usr)
            Dim status = BEBL.JornadaLaboral.Log(exs, oUser)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, status)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al registrar la Jornada Laboral")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al registrar la Jornada Laboral")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/JornadaLaboral/log/{mode}/{usr}")>
    Public Function LogDeprecated(mode As Integer, usr As Guid) As HttpResponseMessage 'Deprecated
        'Modes: 1=entrada, 2=sortida
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oUser As New DTOUser(usr)
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            Dim oStaff = BEBL.User.GetStaff(oUser)
            If MyBase.ClientIP = oEmp.Ip Or oStaff.Teletrabajo Then
                Dim value = BEBL.JornadaLaboral.Log(exs, mode, oStaff)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al registrar la Jornada Laboral")
                End If
            Else
                retval = MyBase.HttpErrorResponseMessage("No està permés fitxar des de la Ip '" & MyBase.ClientIP & "' ")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al registrar la Jornada Laboral")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/JornadaLaboral")>
    Public Function Update(<FromBody> value As DTOJornadaLaboral) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.JornadaLaboral.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la JornadaLaboral")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la JornadaLaboral")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/JornadaLaboral/delete")>
    Public Function Delete(<FromBody> value As DTOJornadaLaboral) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.JornadaLaboral.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la JornadaLaboral")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la JornadaLaboral")
        End Try
        Return retval
    End Function

End Class

Public Class JornadesLaboralsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/JornadesLaborals/{staff?}")>
    Public Function All(Optional staff As Guid? = Nothing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.JornadesLaborals.All(staff)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Jornades Laborals")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/JornadesLaborals/fromUser/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim oStaff = BEBL.User.GetStaff(oUser)
            If oStaff Is Nothing Then
            Else
                Dim value = BEBL.JornadesLaborals.All(oStaff.Guid)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Jornades Laborals")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/JornadesLaborals/removeLast/{user}")>
    Public Function removeLast(user As Guid) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim oStaff = BEBL.User.GetStaff(oUser)
            If oStaff Is Nothing Then
            Else
                Dim value = BEBL.JornadesLaborals.All(oStaff.Guid)
                If value.Staffs.Count > 0 Then
                    Dim lastLog As Models.JornadesLaboralsModel.Item = value.Staffs.First.Items.First
                    Dim oLastLog = value.Staffs.First.Value(lastLog)
                    If lastLog.IsOpen Then
                        BEBL.JornadaLaboral.Delete(oLastLog, exs)
                    Else
                        oLastLog.FchTo = Nothing
                        BEBL.JornadaLaboral.Update(oLastLog, exs)
                    End If
                End If
                value = BEBL.JornadesLaborals.All(oStaff.Guid)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Jornades Laborals")
        End Try
        Return retval
    End Function

End Class
