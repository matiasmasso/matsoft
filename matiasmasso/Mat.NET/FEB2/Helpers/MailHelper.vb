Imports System.Net.Mail
Module MailHelper
    Public Function MailAddressCollection(oUsers As IEnumerable(Of DTOUser)) As System.Net.Mail.MailAddressCollection
        Dim retVal As New System.Net.Mail.MailAddressCollection()
        For Each oUser As DTOUser In oUsers
            retVal.Add(oUser.EmailAddress)
        Next
        Return retVal
    End Function

    Public Function MailAdmin(ByVal StSubject As String, Optional ByVal stBody As String = "", Optional ByRef exs As List(Of Exception) = Nothing,
        Optional SendCompletedCallback As System.Net.Mail.SendCompletedEventHandler = Nothing) As Boolean

        Dim sFrom As String = Emp.Current.MsgFrom ' GetDefault("WEBMASTERMSGFROM")
        Dim sUsr As String = Emp.Current.MailboxUsr ' GetDefault("WEBMASTERMAILUSR")
        Dim sPwd As String = Emp.Current.MailboxPwd ' GetDefault("WEBMASTERMAILPWD")
        Dim sTo As String = "matias@matiasmasso.es"
        Dim oMailMessage = DTOMailMessage.Factory({sTo}.ToList, StSubject, stBody)

        If exs Is Nothing Then exs = New List(Of Exception)
        Dim retVal As Boolean = SendMail(sFrom, sUsr, sPwd, sTo, , , StSubject, stBody, DTOEnums.OutputFormat.ASCII, , exs)
        Return retVal
    End Function


    Public Function SendMail(ByVal sFrom As String, ByVal sUsr As String, ByVal sPwd As String, Optional ByVal sTo As String = "", Optional ByVal oCc As System.Net.Mail.MailAddressCollection = Nothing, Optional ByVal oBcc As System.Net.Mail.MailAddressCollection = Nothing, Optional ByVal sSubject As String = "", Optional ByVal sBody As String = "", Optional ByVal oBodyFormat As DTOEnums.OutputFormat = DTOEnums.OutputFormat.ASCII, Optional ByVal oAttachments As ArrayList = Nothing, Optional ByRef exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean
        If exs Is Nothing Then exs = New List(Of Exception)

        Dim oEmp As DTOEmp = Emp.Current

        Dim oMsg As New System.Net.Mail.MailMessage
        oMsg.From = New System.Net.Mail.MailAddress(sFrom, "M+O MATIAS MASSO, S.A.")
        oMsg.Subject = sSubject

        If sTo = "" Then
            If oCc IsNot Nothing Then
                sTo = oCc.First.Address
                oCc.RemoveAt(0)
            End If
        End If

        oMsg.To.Add(New System.Net.Mail.MailAddress(sTo, sTo))

        If oCc IsNot Nothing Then
            For Each oAdr As System.Net.Mail.MailAddress In oCc
                oMsg.CC.Add(oAdr)
            Next
        End If

        If oBcc IsNot Nothing Then
            For Each oAdr As System.Net.Mail.MailAddress In oBcc
                oMsg.Bcc.Add(oAdr)
            Next
        End If

        If oAttachments IsNot Nothing Then
            For Each s As String In oAttachments
                oMsg.Attachments.Add(New System.Net.Mail.Attachment(s))
            Next
        End If

        Select Case oBodyFormat
            Case DTOEnums.OutputFormat.ASCII
                oMsg.Body = sBody
                oMsg.BodyEncoding = System.Text.Encoding.UTF8
                oMsg.IsBodyHtml = False
            Case DTOEnums.OutputFormat.HTML
                oMsg.Body = sBody
                oMsg.IsBodyHtml = True
            Case DTOEnums.OutputFormat.URL
                oMsg.Body = WebHelper.ReadHtmlPage(sBody, exs)
                If exs.Count > 0 Then
                    'If oUser.Equals(DTOUser.Wellknown(DTOUser.Wellknowns.matias)) Then (-------------peta)
                    'Dim data_object As New DataObject
                    'data_object.SetData(DataFormats.Text, True, sBody)
                    'Clipboard.SetDataObject(data_object, True)
                    'exs.Add(New Exception("la adreça ha estat copiarda al portapapers"))
                    'End If
                End If
                oMsg.IsBodyHtml = True
        End Select



        If exs.Count = 0 Then
            Dim client As New System.Net.Mail.SmtpClient()
            client.UseDefaultCredentials = False
            client.Credentials = New System.Net.NetworkCredential(sUsr, sPwd) '("info@matiasmasso.es", "gT67e4DDxvr")
            client.Port = 25 ' oEmp.MailBoxPort '; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = "smtp.office365.com" ' oEmp.MailBoxSmtp ' "smtp.office365.com"
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
            client.EnableSsl = True

            Try
                client.Send(oMsg)
                retval = True
            Catch ex As Exception
                exs.Add(ex)
            End Try

        End If

        Return retval
    End Function

    Public Function SendMail(oEmp As DTOEmp, ByVal sTo As String, sCc As String, sBcc As String, ByVal sSubject As String, ByVal sBody As String, ByVal oBodyFormat As DTOEnums.OutputFormat, Optional ByVal oAttachments As IEnumerable(Of Attachment) = Nothing) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Dim exs As New List(Of Exception)
        If SendMail(exs, oEmp.MailboxUsr, oEmp.MailboxPwd, sTo, sSubject, sBody, oBodyFormat, oAttachments) Then
            retval.Succeed("Correu enviat correctament a {0}", sTo)
        Else
            retval.Fail(exs, "Error al enviar correu a {0}", sTo)
        End If
        Return retval
    End Function


    Public Function SendMail(exs As List(Of Exception), ByVal sUsr As String, ByVal sPwd As String, ByVal sTo As String, ByVal sSubject As String, ByVal sBody As String, ByVal oBodyFormat As DTOEnums.OutputFormat, ByVal oAttachments As IEnumerable(Of Attachment)) As Boolean
        Dim retval As Boolean
        If exs Is Nothing Then exs = New List(Of Exception)

        Dim oEmp As DTOEmp = Emp.Current

        Dim oMsg As New System.Net.Mail.MailMessage
        oMsg.To.Add(New System.Net.Mail.MailAddress(sTo, sTo))
        oMsg.From = New System.Net.Mail.MailAddress(sUsr, "M+O MATIAS MASSO, S.A.")
        oMsg.Subject = sSubject

        If oAttachments IsNot Nothing Then
            For Each item In oAttachments
                oMsg.Attachments.Add(item)
            Next
        End If

        Select Case oBodyFormat
            Case DTOEnums.OutputFormat.ASCII
                oMsg.Body = sBody
                oMsg.BodyEncoding = System.Text.Encoding.UTF8
                oMsg.IsBodyHtml = False
            Case DTOEnums.OutputFormat.HTML
                oMsg.Body = sBody
                oMsg.IsBodyHtml = True
            Case DTOEnums.OutputFormat.URL
                oMsg.Body = WebHelper.ReadHtmlPage(sBody, exs)
                If exs.Count > 0 Then
                    'If oUser.Equals(DTOUser.Wellknown(DTOUser.Wellknowns.matias)) Then (-------------peta)
                    'Dim data_object As New DataObject
                    'data_object.SetData(DataFormats.Text, True, sBody)
                    'Clipboard.SetDataObject(data_object, True)
                    'exs.Add(New Exception("la adreça ha estat copiarda al portapapers"))
                    'End If
                End If
                oMsg.IsBodyHtml = True
        End Select



        If exs.Count = 0 Then
            Dim client As New System.Net.Mail.SmtpClient()
            client.UseDefaultCredentials = False
            client.Credentials = New System.Net.NetworkCredential(sUsr, sPwd) '("info@matiasmasso.es", "gT67e4DDxvr")
            client.Port = 25 ' oEmp.MailBoxPort '; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = oEmp.MailBoxSmtp ' "smtp.office365.com"
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
            client.EnableSsl = True

            Try
                client.Send(oMsg)
                retval = True
            Catch ex As Exception
                exs.Add(ex)
            End Try

        End If

        Return retval
    End Function



    Public Async Function SendMailAsync(exs As List(Of Exception), oEmp As DTOEmp, ByVal sTo As String, sCc As String, sBcc As String, ByVal sSubject As String, ByVal sBody As String, ByVal oBodyFormat As DTOEnums.OutputFormat, Optional ByVal oAttachments As IEnumerable(Of Attachment) = Nothing) As Task(Of Boolean)
        Dim retval = Await SendMailAsync(exs, oEmp.MailboxUsr, oEmp.MailboxPwd, sTo, sSubject, sBody, oBodyFormat, oAttachments)
        Return retval
    End Function

    Public Async Function SendMailAsync(exs As List(Of Exception), ByVal sUsr As String, ByVal sPwd As String, ByVal sTo As String, ByVal sSubject As String, ByVal sBody As String, ByVal oBodyFormat As DTOEnums.OutputFormat, ByVal oAttachments As IEnumerable(Of Attachment)) As Task(Of Boolean)
        Dim retval As Boolean
        If exs Is Nothing Then exs = New List(Of Exception)

        Dim oEmp As DTOEmp = Emp.Current

        Dim oMsg As New System.Net.Mail.MailMessage
        oMsg.To.Add(New System.Net.Mail.MailAddress(sTo, sTo))
        oMsg.From = New System.Net.Mail.MailAddress(sUsr, "M+O MATIAS MASSO, S.A.")
        oMsg.Subject = sSubject

        If oAttachments IsNot Nothing Then
            For Each item In oAttachments
                oMsg.Attachments.Add(item)
            Next
        End If

        Select Case oBodyFormat
            Case DTOEnums.OutputFormat.ASCII
                oMsg.Body = sBody
                oMsg.BodyEncoding = System.Text.Encoding.UTF8
                oMsg.IsBodyHtml = False
            Case DTOEnums.OutputFormat.HTML
                oMsg.Body = sBody
                oMsg.IsBodyHtml = True
            Case DTOEnums.OutputFormat.URL
                oMsg.Body = WebHelper.ReadHtmlPage(sBody, exs)
                If exs.Count > 0 Then
                    'If oUser.Equals(DTOUser.Wellknown(DTOUser.Wellknowns.matias)) Then (-------------peta)
                    'Dim data_object As New DataObject
                    'data_object.SetData(DataFormats.Text, True, sBody)
                    'Clipboard.SetDataObject(data_object, True)
                    'exs.Add(New Exception("la adreça ha estat copiarda al portapapers"))
                    'End If
                End If
                oMsg.IsBodyHtml = True
        End Select



        If exs.Count = 0 Then
            Dim client As New System.Net.Mail.SmtpClient()
            client.UseDefaultCredentials = False
            client.Credentials = New System.Net.NetworkCredential(sUsr, sPwd) '("info@matiasmasso.es", "gT67e4DDxvr")
            client.Port = 25 ' oEmp.MailBoxPort '; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = oEmp.MailBoxSmtp ' "smtp.office365.com"
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
            client.EnableSsl = True

            Try
                Await client.SendMailAsync(oMsg)
                retval = True
            Catch ex As Exception
                exs.Add(ex)
            End Try

        End If

        Return retval
    End Function


End Module
