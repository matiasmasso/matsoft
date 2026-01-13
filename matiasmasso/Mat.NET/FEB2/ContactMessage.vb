Public Class ContactMessage
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOContactMessage)
        Return Await Api.Fetch(Of DTOContactMessage)(exs, "ContactMessage", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oContactMessage As DTOContactMessage) As Boolean
        If Not oContactMessage.IsLoaded And Not oContactMessage.IsNew Then
            Dim pContactMessage = Api.FetchSync(Of DTOContactMessage)(exs, "ContactMessage", oContactMessage.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOContactMessage)(pContactMessage, oContactMessage, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oContactMessage As DTOContactMessage) As Task(Of Boolean)
        Return Await Api.Update(Of DTOContactMessage)(oContactMessage, exs, "ContactMessage")
        oContactMessage.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oContactMessage As DTOContactMessage) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOContactMessage)(oContactMessage, exs, "ContactMessage")
    End Function

    Shared Async Function Send(exs As List(Of Exception), oContactMessage As DTOContactMessage, oDomain As DTOWebDomain) As Task(Of Boolean)
        Dim retval As Boolean
        Dim subject = String.Format("Missatge Web de {0} ({1})", oContactMessage.Nom, oContactMessage.Location)
        Dim body = oContactMessage.Text & "<br/>" & String.Format("contestar a {0}", oContactMessage.Email)
        Select Case oDomain.Id
            Case DTOWebDomain.Ids.matiasmasso_pt
                retval = Await MailMessage.MailPortugal(DTOUser.Wellknown(DTOUser.Wellknowns.info), subject, body, exs)
            Case Else
                retval = Await MailMessage.MailInfo(DTOUser.Wellknown(DTOUser.Wellknowns.info), subject, body, exs)
        End Select
        Return retval
    End Function
End Class

Public Class ContactMessages
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOContactMessage))
        Return Await Api.Fetch(Of List(Of DTOContactMessage))(exs, "ContactMessages")
    End Function

End Class

