Imports System.IO
Imports System.Net

Public Class MailMessageHelper


    Shared Async Function MailAdmin(oEmp As DTOEmp, subject As String, body As String, Optional exs As List(Of Exception) = Nothing) As Task(Of Boolean)
        Dim oMailMessage = DTOMailMessage.Factory(DTOMailMessage.wellknownRecipients.Admin, subject, body)
        Return Await Send(oEmp, oMailMessage, exs)
    End Function

    Shared Async Function MailAdmin(subject As String, body As String, Optional exs As List(Of Exception) = Nothing) As Task(Of Boolean)
        Dim sRecipient = DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Admin)
        Dim oMailMessage = DTOMailMessage.Factory(sRecipient, subject, body)
        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        If exs Is Nothing Then exs = New List(Of Exception)
        Return Await Send(oEmp, oMailMessage, exs)
    End Function

    Shared Async Function MailInfo(exs As List(Of Exception), oEmp As DTOEmp, subject As String, body As String, Optional bodyFormat As DTOMailMessage.MessageBodyFormats = DTOMailMessage.MessageBodyFormats.Html) As Task(Of Boolean)
        Dim sRecipient = DTOMailMessage.wellknownAddress(DTOMailMessage.wellknownRecipients.Info)
        Dim oMailMessage = DTOMailMessage.Factory(sRecipient, subject, body, bodyFormat)
        Return Await Send(oEmp, oMailMessage, exs)
    End Function

    Shared Async Function Send(oEmp As DTOEmp, oMailMessage As DTOMailMessage, exs As List(Of Exception)) As Task(Of Boolean)
        Try
            Dim oSmtpClient = SmtpClient(oEmp)
            Dim oMsg = Await SmtpMailMessage(oEmp, oMailMessage, exs)
            oSmtpClient.Send(oMsg)
        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al enviar el missatge a {0}", oMailMessage.ConcatenatedSemicolonTo)))
            exs.Add(ex)
        End Try
        Return exs.Count = 0
    End Function

    Shared Function SmtpClient(oEmp As DTOEmp) As System.Net.Mail.SmtpClient
        If oEmp.MailboxPwd = "" Then
            oEmp.IsLoaded = False
            BEBL.Emp.Load(oEmp)
        End If
        Dim retval As New System.Net.Mail.SmtpClient()
        With retval
            .UseDefaultCredentials = False
            .Credentials = New System.Net.NetworkCredential(oEmp.MailboxUsr, oEmp.MailboxPwd)
            .Port = 25 ' oEmp.MailBoxPort '; // You can use Port 25 if 587 is blocked (mine is!)
            .Host = oEmp.MailBoxSmtp ' "smtp.office365.com"
            .DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
            .EnableSsl = True
        End With
        Return retval
    End Function

    Shared Async Function SmtpMailMessage(oEmp As DTOEmp, oMailMessage As DTOMailMessage, exs As List(Of Exception)) As Task(Of System.Net.Mail.MailMessage)
        Dim retval As System.Net.Mail.MailMessage = Nothing
        Try
            If oMailMessage.BodyUrl > "" Then
                oMailMessage.Body = Await DownloadBody(oMailMessage.BodyUrl, exs)
                oMailMessage.BodyFormat = DTOMailMessage.MessageBodyFormats.Html
            End If

            retval = New System.Net.Mail.MailMessage(oEmp.MailboxUsr, oMailMessage.ConcatenatedCommaTo, oMailMessage.Subject, oMailMessage.Body)
            'retval.BodyEncoding = System.Text.Encoding.UTF8
            'retval.SubjectEncoding = System.Text.Encoding.UTF8

            If oMailMessage.Cc IsNot Nothing AndAlso oMailMessage.Cc.Count > 0 Then
                For Each s In oMailMessage.Cc
                    retval.CC.Add(s)
                Next
            End If

            If oMailMessage.Bcc IsNot Nothing AndAlso oMailMessage.Bcc.Count > 0 Then
                For Each s In oMailMessage.Bcc
                    retval.Bcc.Add(s)
                Next
            End If

            If oMailMessage.BodyFormat = DTOMailMessage.MessageBodyFormats.Html Then
                retval.IsBodyHtml = True
            End If

            For Each item In oMailMessage.Attachments
                If item.ByteArray IsNot Nothing Then
                    Dim oMemoryStream As New MemoryStream(item.ByteArray)
                    Dim oAttachment As New Net.Mail.Attachment(oMemoryStream, New Net.Mime.ContentType(Net.Mime.MediaTypeNames.Application.Octet))
                    With oAttachment
                        .ContentDisposition.FileName = item.Friendlyname
                        .ContentDisposition.Size = .ContentStream.Length
                    End With
                    retval.Attachments.Add(oAttachment)
                End If
            Next

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Shared Function AttachExcel(oMailMessage As DTOMailMessage, oSheet As MatHelper.Excel.Sheet, exs As List(Of Exception)) As Boolean
        Try
            Dim oByteArray() As Byte = MatHelper.Excel.ClosedXml.Bytes(oSheet)
            Dim sFilename As String = oSheet.Filename
            If sFilename = "" Then sFilename = "M+O attachment"
            If Not sFilename.EndsWith(".xlsx") Then sFilename += ".xlsx"
            oMailMessage.AddAttachment(sFilename, oByteArray)
        Catch ex As Exception
            exs.Add(New Exception("BEBL.MailMsg.AttachExcel: Error al adjuntar Excel a correu"))
            exs.Add(ex)
        End Try
        Return exs.Count = 0
    End Function


    Shared Async Function DownloadBody(url As String, exs As List(Of Exception)) As Task(Of String)
        Dim retval As String = ""
        Try
            Using client As New WebClient()
                client.Encoding = Text.Encoding.UTF8
                retval = Await client.DownloadStringTaskAsync(url)
            End Using

        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al descarregar el cos del missatge de {0}", url)))
            exs.Add(ex)
        End Try
        Return retval
    End Function


End Class
