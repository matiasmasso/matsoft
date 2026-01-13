Imports System.Net.Mail

Public Class Mailing
    Inherits _FeblBase

    Shared Async Function Load(exs As List(Of Exception), oEmp As DTOEmp, value As DTOMailing) As Task(Of DTOMailing)
        Return Await Api.Execute(Of DTOMailing, DTOMailing)(value, exs, "mailing", oEmp.Id)
    End Function

    Shared Async Function XarxaDistribuidors(exs As List(Of Exception), fch As Date) As Task(Of List(Of DTOLeadChecked))
        Return Await Api.Fetch(Of List(Of DTOLeadChecked))(exs, "mailing/XarxaDistribuidors", FormatFch(fch))
    End Function

    Shared Async Function Reps(exs As List(Of Exception), oChannels As List(Of DTODistributionChannel), oBrands As List(Of DTOProductBrand)) As Task(Of List(Of DTOLeadChecked))
        Dim value As New DTOChannelsBrands()
        With value
            .Channels = oChannels
            .Brands = oBrands
        End With
        Return Await Api.Execute(Of DTOChannelsBrands, List(Of DTOLeadChecked))(value, exs, "mailing/reps")
    End Function


    Shared Function BodyUrl(oTemplate As DTODefault.MailingTemplates, ParamArray UrlSegments() As String) As String
        Dim retval = MmoUrl.BodyTemplateUrl(oTemplate, UrlSegments)
        Return retval
    End Function

End Class
Public Class Mailings
    Shared Async Function Log(oGuid As Guid, oUsers As IEnumerable(Of DTOUser), exs As List(Of Exception)) As Task(Of Boolean)
        Dim value As New Tuple(Of Guid, IEnumerable(Of DTOUser))(oGuid, oUsers)
        Dim retval = Await Api.Execute(Of Tuple(Of Guid, IEnumerable(Of DTOUser)))(value, exs, "mailings/log")
        Return retval
    End Function

    Shared Function BodyUrl(oTemplate As DTODefault.MailingTemplates, ParamArray UrlSegments() As String) As String
        Dim oSegments As New List(Of String)
        oSegments.Add("mail")
        oSegments.Add(oTemplate.ToString())
        oSegments.AddRange(UrlSegments)
        Dim retval As String = UrlHelper.Factory(True, oSegments.ToArray)
        Return retval
    End Function

    Shared Function SendMail(oEmp As DTOEmp, oSubscriptor As DTOUser, sSubject As String, sBody As String, oAttachments As IEnumerable(Of Attachment)) As DTOTaskResult
        Dim retval As New DTOTaskResult

        Dim oMsg As New System.Net.Mail.MailMessage
        oMsg.From = New System.Net.Mail.MailAddress(oEmp.MailboxUsr, "M+O MATIAS MASSO, S.A.")
        oMsg.To.Add(New System.Net.Mail.MailAddress(oSubscriptor.EmailAddress))
        'oMsg.CC.Add(New System.Net.Mail.MailAddress("matias@matiasmasso.es"))
        oMsg.Subject = sSubject

        If oAttachments IsNot Nothing Then
            For Each oAttachment In oAttachments
                oMsg.Attachments.Add(oAttachment)
            Next
        End If

        oMsg.Body = sBody
        oMsg.IsBodyHtml = True

        Dim client As New System.Net.Mail.SmtpClient()
        client.UseDefaultCredentials = False
        client.Credentials = New System.Net.NetworkCredential(oEmp.MailboxUsr, oEmp.MailboxPwd) '("info@matiasmasso.es", "gT67e4DDxvr")
        client.Port = 25 ' oEmp.MailBoxPort '; // You can use Port 25 if 587 is blocked (mine is!)
        client.Host = oEmp.MailBoxSmtp ' "smtp.office365.com"
        client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
        client.EnableSsl = True

        Try
            client.Send(oMsg)
            retval.Succeed("missatge enviat a {0}", oSubscriptor.EmailAddress)
        Catch ex As Exception
            retval.Fail(ex, "")
        End Try


        Return retval
    End Function


End Class
