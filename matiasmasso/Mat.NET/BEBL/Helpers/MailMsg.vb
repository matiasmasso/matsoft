Imports System.IO
Imports System.Net
Imports System.Net.Mail

Public Class MailMsg
    Private _Emp As DTOEmp
    Private _MailMessage As MailMessage

    Public Enum Recipients
        [From]
        [To]
        Cc
        Bcc
    End Enum

    Public Sub New(oEmp As DTOEmp)
        MyBase.New
        _Emp = oEmp
        _MailMessage = New MailMessage
    End Sub

    Shared Function Factory(oEmp As DTOEmp, Optional sTo As String = "", Optional sSubject As String = "", Optional sBody As String = "") As MailMsg
        Dim retval As New MailMsg(oEmp)
        With retval
            If sTo > "" Then
                .Add(Recipients.To, sTo)
            End If
            .Subject = sSubject
            .Body = sBody
            .BodyEncoding = System.Text.Encoding.UTF8
            .IsBodyHtml = True
        End With
        Return retval
    End Function

    Public Property From As MailAddress
        Get
            Return _MailMessage.From
        End Get
        Set(value As MailAddress)
            _MailMessage.From = value
        End Set
    End Property

    Public Property [To] As IEnumerable(Of MailAddress)
        Get
            Return _MailMessage.To.ToArray.ToList
        End Get
        Set(value As IEnumerable(Of MailAddress))
            _MailMessage.To.Clear()
            For Each item In value
                _MailMessage.To.Add(item)
            Next
        End Set
    End Property

    Public Property Cc As IEnumerable(Of MailAddress)
        Get
            Return _MailMessage.CC.ToArray.ToList
        End Get
        Set(value As IEnumerable(Of MailAddress))
            _MailMessage.CC.Clear()
            For Each item In value
                _MailMessage.CC.Add(item)
            Next
        End Set
    End Property

    Public Property Bcc As IEnumerable(Of MailAddress)
        Get
            Return _MailMessage.Bcc.ToArray.ToList
        End Get
        Set(value As IEnumerable(Of MailAddress))
            _MailMessage.Bcc.Clear()
            For Each item In value
                _MailMessage.Bcc.Add(item)
            Next
        End Set
    End Property

    Public Sub AddRange(oRecipient As Recipients, oUsers As IEnumerable(Of DTOUser))
        If oRecipient = Recipients.From Then
            _MailMessage.From = New MailAddress(oUsers.First.EmailAddress, oUsers.First.EmailAddress)
        Else
            For Each item In oUsers
                Add(oRecipient, item.EmailAddress)
            Next
        End If
    End Sub

    Public Sub Add(oRecipient As Recipients, oUser As DTOUser)
        Dim value As New MailAddress(oUser.EmailAddress, oUser.EmailAddress)
        If oRecipient = Recipients.From Then
            _MailMessage.From = value
        Else
            Add(oRecipient, value)
        End If
    End Sub

    Public Sub AddRange(oRecipient As Recipients, sEmailAddresses As IEnumerable(Of String))
        If oRecipient = Recipients.From Then
            _MailMessage.From = New MailAddress(sEmailAddresses.First, sEmailAddresses.First)
        Else
            For Each s In sEmailAddresses
                Add(oRecipient, s)
            Next
        End If
    End Sub

    Public Sub Add(oRecipient As Recipients, sEmailAddress As String)
        Dim value As New MailAddress(sEmailAddress, sEmailAddress)
        If oRecipient = Recipients.From Then
            _MailMessage.From = value
        Else
            Add(oRecipient, value)
        End If
    End Sub

    Public Sub AddRange(oRecipient As Recipients, oEmailAddresses As IEnumerable(Of MailAddress))
        If oRecipient = Recipients.From Then
            _MailMessage.From = oEmailAddresses.First
        Else
            For Each item In oEmailAddresses
                Add(oRecipient, item)
            Next
        End If
    End Sub

    Public Sub Add(oRecipient As Recipients, value As MailAddress)
        Select Case oRecipient
            Case Recipients.From
                _MailMessage.From = value
            Case Recipients.To
                _MailMessage.To.Add(value)
            Case Recipients.Cc
                _MailMessage.CC.Add(value)
            Case Recipients.Bcc
                _MailMessage.Bcc.Add(value)
        End Select
    End Sub

    Public Property Subject As String
        Get
            Return _MailMessage.Subject
        End Get
        Set(value As String)
            _MailMessage.Subject = value
        End Set
    End Property

    Public Property Body As String
        Get
            Return _MailMessage.Body
        End Get
        Set(value As String)
            _MailMessage.Body = value
        End Set
    End Property

    Public Property BodyEncoding As System.Text.Encoding
        Get
            Return _MailMessage.BodyEncoding
        End Get
        Set(value As System.Text.Encoding)
            _MailMessage.BodyEncoding = value
        End Set
    End Property

    Public Function DownloadBody(exs As List(Of Exception), oTemplate As DTODefault.MailingTemplates, ParamArray UrlSegments() As String) As Boolean
        Dim url = BEBL.Mailing.BodyUrl(oTemplate, UrlSegments)
        Dim retval = DownloadBody(url, exs)
        Return retval
    End Function

    Public Function DownloadBody(oLang As DTOLang, txtId As DTOTxt.Ids, ParamArray sParamValues() As String) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Dim oTxt As DTOTxt = TxtLoader.Find(DTOTxt.Ids.MailStocks)
        If oTxt Is Nothing Then
            retval.Fail("No s'ha trobat la plantilla {0}", txtId.ToString())
        Else
            _MailMessage.Body = oTxt.ToHtml(oLang, sParamValues)
            retval.Succeed("Cos del missatge descarregat correctament de la plantilla {0}", txtId.ToString())
        End If
        Return retval
    End Function

    Public Function DownloadBody(ByVal url As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            Dim request As WebRequest = WebRequest.Create(url)
            Dim encoding = System.Text.UTF8Encoding.UTF8
            Using response As WebResponse = request.GetResponse()
                Using reader As New StreamReader(response.GetResponseStream(), encoding)
                    _MailMessage.Body = reader.ReadToEnd()
                End Using
            End Using
            retval = True
        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al descarregar el cos del missatge de {0}", url)))
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Property IsBodyHtml As Boolean
        Get
            Return _MailMessage.IsBodyHtml
        End Get
        Set(value As Boolean)
            _MailMessage.IsBodyHtml = value
        End Set
    End Property

    Public Function AttachPdf(oByteArray As Byte(), sFilename As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            If sFilename = "" Then sFilename = "M+O attachment.pdf"
            If Not sFilename.EndsWith(".pdf") Then sFilename += ".pdf"
            Dim oMemoryStream As New MemoryStream(oByteArray)
            Dim oAttachment As New Attachment(oMemoryStream, New Net.Mime.ContentType(Net.Mime.MediaTypeNames.Application.Octet))
            With oAttachment
                .ContentDisposition.FileName = sFilename
                .ContentDisposition.Size = .ContentStream.Length
            End With
            _MailMessage.Attachments.Add(oAttachment)
            retval = True
        Catch ex As Exception
            exs.Add(New Exception("BEBL.MailMsg.AttachPdf: Error al adjuntar Pdf a correu"))
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Function AttachExcel(oSheet As MatHelper.Excel.Sheet) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Try
            Dim oByteArray() As Byte = MatHelper.Excel.ClosedXml.Bytes(oSheet)
            Dim sFilename As String = oSheet.Filename
            If sFilename = "" Then sFilename = "M+O attachment"
            If Not sFilename.EndsWith(".xlsx") Then sFilename += ".xlsx"
            Dim oMemoryStream As New MemoryStream(oByteArray)
            Dim oAttachment As New Attachment(oMemoryStream, New Net.Mime.ContentType(Net.Mime.MediaTypeNames.Application.Octet))
            With oAttachment
                .ContentDisposition.FileName = sFilename
                .ContentDisposition.Size = .ContentStream.Length
            End With
            _MailMessage.Attachments.Add(oAttachment)
            retval.ResultCod = DTOTask.ResultCods.Success

        Catch ex As Exception
            retval.Fail(ex, "BEBL.MailMsg.AttachExcel: Error al adjuntar Excel a correu")
        End Try
        Return retval
    End Function

    Public Function AttachXmlDocument(oDoc As System.Xml.XmlDocument, sFilename As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            Dim sXml = oDoc.OuterXml
            Dim oAttachment = System.Net.Mail.Attachment.CreateAttachmentFromString(sXml, "text/xml")
            With oAttachment
                .ContentDisposition.FileName = sFilename
                .ContentDisposition.CreationDate = DTO.GlobalVariables.Now()
                '.ContentDisposition.Size = .ContentStream.Length
            End With
            _MailMessage.Attachments.Add(oAttachment)
            retval = True
        Catch ex As Exception
            exs.Add(New Exception(String.Format("BEBL.MailMsg.AttachXmlDocument: Error al adjuntar fitxer {0}", sFilename)))
        End Try
        Return retval
    End Function

    Public Function Send(exs As List(Of Exception), Optional SendCompletedCallback As System.Net.Mail.SendCompletedEventHandler = Nothing) As Boolean
        Dim retval As Boolean
        Try
            _Emp.IsLoaded = False
            EmpLoader.Load(_Emp)
            If _MailMessage.From Is Nothing Then Add(Recipients.From, _Emp.MailboxUsr)
            Dim oSmtpClient = SmtpClient(_Emp)
            oSmtpClient.Send(_MailMessage)
            retval = True
        Catch ex As Exception
            exs.Add(New Exception(String.Format("BEBL.MailMsg.Send: Missatge no enviat a {0}", RecipientAddresses)))
            exs.Add(ex)
        End Try
        Return retval
    End Function
    Public Function Send(Optional SendCompletedCallback As System.Net.Mail.SendCompletedEventHandler = Nothing) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Try
            EmpLoader.Load(_Emp)
            If _MailMessage.From Is Nothing Then Add(Recipients.From, _Emp.MailboxUsr)
            Dim oSmtpClient = SmtpClient(_Emp)
            oSmtpClient.Send(_MailMessage)
            retval.Succeed("Missatge enviat a {0}", RecipientAddresses)
        Catch ex As Exception
            retval.Fail(ex, "BEBL.MailMsg.Send: Missatge no enviat a {0}", RecipientAddresses)
        End Try
        Return retval
    End Function

    Public Function RecipientAddresses() As String
        Dim sEmailAddresses = _MailMessage.To.ToArray.Select(Function(x) x.Address)
        Dim retval = String.Join(";", sEmailAddresses)
        Return retval
    End Function

    Private Function SmtpClient(oEmp As DTOEmp) As System.Net.Mail.SmtpClient

        Dim retval As New System.Net.Mail.SmtpClient()
        With retval
            .UseDefaultCredentials = False
            .Credentials = New System.Net.NetworkCredential(oEmp.MailboxUsr, oEmp.MailboxPwd) '("info@matiasmasso.es", "gT67e4DDxvr")
            .Port = 25 ' oEmp.MailBoxPort '; // You can use Port 25 if 587 is blocked (mine is!)
            .Host = oEmp.MailBoxSmtp ' "smtp.office365.com"
            .DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
            .EnableSsl = True
        End With
        Return retval
    End Function


    Private Sub SendSimpleMail(MailServerUrl As String, username As String, password As String, From As String, [to] As String, subject As String, body As String)
        Dim oSmtpClient As SmtpClient = New System.Net.Mail.SmtpClient(MailServerUrl)
        oSmtpClient.UseDefaultCredentials = False
        oSmtpClient.Credentials = New System.Net.NetworkCredential(username, password)

        Dim oMessage As New System.Net.Mail.MailMessage(From, [to])
        oMessage.Subject = subject
        oMessage.SubjectEncoding = System.Text.Encoding.UTF8
        oMessage.Body = body
        oMessage.BodyEncoding = System.Text.Encoding.UTF8
        oMessage.IsBodyHtml = True

        oSmtpClient.Send(oMessage)
    End Sub
End Class
