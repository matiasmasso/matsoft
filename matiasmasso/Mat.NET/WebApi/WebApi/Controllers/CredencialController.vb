Public Class CredencialController
    Inherits _BaseController

    <HttpPost>
    <Route("api/credencials")>
    Public Function Credencials(user As DTOGuidNom) As List(Of DUI.Credencial)
        Dim retval As New List(Of DUI.Credencial)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        If oUser IsNot Nothing Then
            Dim oCredencials As List(Of DTOCredencial) = BLLCredencials.Headers(oUser)
            For Each oCredencial As DTOCredencial In oCredencials
                Dim item As New DUI.Credencial
                With item
                    .Guid = oCredencial.Guid.ToString
                    .Referencia = oCredencial.Referencia
                End With
                retval.Add(item)
            Next
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/credencial")>
    Public Function Credencial(oUserCredencial As DUI.UserCredencial) As DUI.Credencial ' oUserCredencial As DUI.UserCredencial) As DUI.Credencial
        Dim retval As DUI.Credencial = Nothing
        If oUserCredencial IsNot Nothing Then
            '
            Dim oUserGuid As Guid = oUserCredencial.User.Guid
            Dim oUser As DTOUser = BLLUser.Find(oUserGuid)
            If oUser IsNot Nothing Then
                Dim oCredencialGuid As New Guid(oUserCredencial.Credencial.Guid)
                Dim oCredencial As DTOCredencial = BLLCredencial.Find(oCredencialGuid, oUser)
                If oCredencial IsNot Nothing Then
                    retval = oUserCredencial.Credencial
                    With retval
                        .Referencia = oCredencial.Referencia
                        .Url = oCredencial.Url
                        .Usuari = oCredencial.Usuari
                        .Password = oCredencial.Password
                        .Obs = oCredencial.Obs
                    End With
                End If
            End If
        End If
        Return retval
    End Function


    <HttpPost>
    <Route("api/credencial/update")>
    Public Function Update(duiUserCredencial As DUI.UserCredencial) As DUI.TaskResult
        Dim retval As New DUI.TaskResult

        If duiUserCredencial Is Nothing Then
            retval.Message = "empty parameter"
        Else
            Dim oUserGuid As Guid = duiUserCredencial.User.Guid
            Dim oUser As DTOUser = BLLUser.Find(duiUserCredencial.User.Guid)


            If oUser Is Nothing Then
                retval.Message = oUser.Lang.Tradueix("usuario desconocido", "usuari desconegut", "unknown user")
            Else
                Dim oCredencial As DTOCredencial = Nothing
                If GuidHelper.IsGuid(duiUserCredencial.Credencial.Guid) Then
                    Dim oCredencialGuid As New Guid(duiUserCredencial.Credencial.Guid)
                    oCredencial = BLLCredencial.Find(oCredencialGuid)
                Else
                    oCredencial = New DTOCredencial()
                    oCredencial.Owners.Add(oUser)
                End If

                With oCredencial
                    .Referencia = duiUserCredencial.Credencial.Referencia
                    .Usuari = duiUserCredencial.Credencial.Usuari
                    .Password = duiUserCredencial.Credencial.Password
                    .Url = duiUserCredencial.Credencial.Url
                    .Obs = duiUserCredencial.Credencial.Obs
                    .UsrLastEdited = oUser
                End With

                Dim exs As New List(Of Exception)
                If BLLCredencial.Update(oCredencial, exs) Then
                    retval.Success = True
                Else
                    retval.Message = BLLExceptions.ToFlatString(exs)
                End If

            End If
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/credencial/delete")>
    Public Function Delete(duiUserCredencial As DUI.UserCredencial) As DUI.TaskResult
        Dim retval As New DUI.TaskResult

        Dim oUserGuid As Guid = duiUserCredencial.User.Guid
        Dim oUser As DTOUser = BLLUser.Find(duiUserCredencial.User.Guid)

        If oUser Is Nothing Then
            retval.Message = oUser.Lang.Tradueix("usuario desconocido", "usuari desconegut", "unknown user")
        Else
            Dim oCredencialGuid As New Guid(duiUserCredencial.Credencial.Guid)
            Dim oCredencial As DTOCredencial = BLLCredencial.Find(oCredencialGuid)

            Dim exs As New List(Of Exception)
            If BLLCredencial.Delete(oCredencial, exs) Then
                retval.Success = True
            Else
                retval.Message = BLLExceptions.ToFlatString(exs)
            End If
        End If
        Return retval
    End Function

End Class
