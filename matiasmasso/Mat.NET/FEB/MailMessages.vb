Public Class MailMessage




    Shared Function MailAdminSync(subject As String, Optional body As String = "", Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim oUser = DTOUser.Wellknown(DTOUser.Wellknowns.info)
        Return SendSync(oUser, DTOMailMessage.wellknownRecipients.Admin, subject, body, exs)
    End Function

    Shared Async Function MailAdmin(subject As String, Optional body As String = "", Optional exs As List(Of Exception) = Nothing) As Task(Of Boolean)
        Dim oUser = DTOUser.Wellknown(DTOUser.Wellknowns.info)
        Return Await Send(oUser, DTOMailMessage.wellknownRecipients.Admin, subject, body, exs)
    End Function

    Shared Async Function MailInfo(oUser As DTOUser, subject As String, body As String, Optional exs As List(Of Exception) = Nothing) As Task(Of Boolean)
        Return Await Send(oUser, DTOMailMessage.wellknownRecipients.Info, subject, body, exs)
    End Function

    Shared Async Function MailPortugal(oUser As DTOUser, subject As String, body As String, Optional exs As List(Of Exception) = Nothing) As Task(Of Boolean)
        Return Await Send(oUser, DTOMailMessage.wellknownRecipients.Portugal, subject, body, exs)
    End Function

    Shared Function SendSync(oUser As DTOUser, oRecipient As DTOMailMessage.wellknownRecipients, Optional ByVal Subject As String = "", Optional ByVal Body As String = "", Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim sTo As String = DTOMailMessage.wellknownAddress(oRecipient)
        Dim oMailMessage = DTOMailMessage.Factory({sTo}.ToList, Subject, Body)
        Dim retval = MailMessage.SendSync(oUser, oMailMessage, exs)
        Return retval
    End Function

    Shared Async Function Send(oUser As DTOUser, oRecipient As DTOMailMessage.wellknownRecipients, Optional ByVal Subject As String = "", Optional ByVal Body As String = "", Optional exs As List(Of Exception) = Nothing) As Task(Of Boolean)
        Dim sTo As String = DTOMailMessage.wellknownAddress(oRecipient)
        Dim oMailMessage = DTOMailMessage.Factory({sTo}.ToList, Subject, Body)
        Dim retval = Await MailMessage.Send(exs, oUser, oMailMessage)
        Return retval
    End Function

    Shared Async Function Send(exs As List(Of Exception), oUser As DTOUser, value As DTOMailMessage) As Task(Of Boolean)
        Dim retval As Boolean
        If exs Is Nothing Then exs = New List(Of Exception)
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            retval = Await Api.Upload(oMultipart, exs, "MailMessage/Send", oUser.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function SendSync(oUser As DTOUser, value As DTOMailMessage, Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean
        If exs Is Nothing Then exs = New List(Of Exception)
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            retval = Api.UploadSync(oMultipart, exs, "MailMessage/Send", oUser.Guid.ToString())
        End If
        Return retval
    End Function

End Class
