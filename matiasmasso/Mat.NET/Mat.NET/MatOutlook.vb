Imports Microsoft.Office.Interop

Module MatOutlook
    Private mApp As Outlook.Application
    Private mNameSpace As Outlook.NameSpace

    Function OutlookApp() As Outlook.Application
        If mApp Is Nothing Then
            Try
                mApp = New Outlook.Application        ' Application object.
            Catch ex As System.Exception
                MsgBox("No s'ha pogut arrancar Outlook!", MsgBoxStyle.Exclamation, "MAT.NET")
            End Try
        End If
        Return mApp
    End Function

    Function OutlookNameSpace() As Outlook.NameSpace
        If mNameSpace Is Nothing Then
            mNameSpace = OutlookApp.GetNamespace("MAPI")
        End If
        Return mNameSpace
    End Function

    Public Function NewMessage(oMailMessage As MailMessage, Optional ByRef exs As list(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean
        Dim oOlApp As Outlook.Application = OutlookApp()
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)

        With oNewMail
            Try
                '.Recipients.Add("M+O <info@matiasmasso.es>")

                If oMailMessage.To > "" Then .Recipients.Add(oMailMessage.To)
                .CC = oMailMessage.Cc
                .BCC = oMailMessage.Bcc
                .Subject = oMailMessage.Subject
                Select Case oMailMessage.BodyFormat
                    Case MailMessage.MessageBodyFormats.ASCII
                        .Body = oMailMessage.Body
                    Case MailMessage.MessageBodyFormats.Html
                        .HTMLBody = oMailMessage.Body
                End Select
                If oMailMessage.Attachments IsNot Nothing Then
                    For Each sFileName As String In oMailMessage.Attachments
                        .Attachments.Add(sFileName)
                    Next
                End If
                .Display()
                retval = True
            Catch ex As Exception
                If exs IsNot Nothing Then
                    exs.Add(ex)
                End If
            End Try

        End With
        Return retval
    End Function

    Public Function NewMessage(ByVal sTo As String, Optional ByVal sCc As String = "", Optional ByVal sBcc As String = "", Optional ByVal sSubject As String = "", Optional ByVal sBody As String = "", Optional ByVal sHtmlBody As String = "", Optional ByVal oAttachments As ArrayList = Nothing, Optional ByRef exs As list(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean
        Dim oOlApp As Outlook.Application = OutlookApp()
        'oOlApp = CreateObject("Outlook.Application")
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)

        With oNewMail
            Try
                '.Recipients.Add("M+O <info@matiasmasso.es>")
                If sTo > "" Then .Recipients.Add(sTo)
                .CC = sCc
                .BCC = sBcc
                .Subject = sSubject
                If sHtmlBody > "" Then
                    If sHtmlBody.StartsWith("http://") Then
                        .HTMLBody = BLL.FileSystemHelper.DownloadHtml(sHtmlBody)
                    Else
                        .HTMLBody = sHtmlBody
                    End If
                Else
                    .Body = sBody
                End If
                If oAttachments IsNot Nothing Then
                    For Each sFileName As String In oAttachments
                        .Attachments.Add(sFileName)
                    Next
                End If
                .Display()
                retval = True
            Catch ex As Exception
                If exs IsNot Nothing Then
                    exs.Add(ex)
                End If
            End Try
        End With
        Return retval
    End Function

    Public Function NewMessage(ByVal sTo As String, Optional ByVal oCc As System.Net.Mail.MailAddressCollection = Nothing, Optional ByVal sBcc As String = "", Optional ByVal sSubject As String = "", Optional ByVal sBody As String = "", Optional ByVal sHtmlBody As String = "", Optional ByVal oAttachments As ArrayList = Nothing, Optional exs As list(Of Exception) = Nothing) As Boolean
        Dim sb As New System.Text.StringBuilder
        If oCc IsNot Nothing Then
            For Each item In oCc
                If sb.Length > 0 Then sb.Append(";")
                sb.Append(item)
            Next
        End If
        Dim sCc As String = sb.ToString
        Dim retval As Boolean = NewMessage(sTo, sCc, sBcc, sSubject, sBody, sHtmlBody, oAttachments, exs)
        Return retval
    End Function

    Public Sub RemittanceAdvice(oCca As Cca)
        Dim oRemittanceAdvice As DTORemittanceAdvice = BLL_Proveidor.RemittanceAdvice(oCca)
        Dim oProveidor As DTOProveidor = oRemittanceAdvice.Proveidor
        BLL.BLLContact.Load(oProveidor)
        Dim oSubscripcio As New DTOSubscription(DTOSubscription.Ids.Comptabilitat)
        Dim oSubscriptors As List(Of DTOSubscriptor) = BLL.BLLSubscriptors.All(oSubscripcio, oProveidor)
        Dim sTo As String = BLL.BLLSubscriptors.RecipientsString(oSubscriptors)
        Dim olang As DTOLang = oProveidor.Lang
        Dim sSubject As String = olang.Tradueix("Aviso de transferencia", "Avis de transferència", "Bank transfer notification")
        Dim url As String = BLL.Defaults.FromSegments(True, "mail", "remittanceAdvice", oCca.Guid.ToString)
        Dim sHtmlBody As String = BLL.FileSystemHelper.DownloadHtml(url)

        Dim exs As New List(Of exception)
        If Not MatOutlook.NewMessage(sTo, "", "matias@matiasmasso.es", sSubject, , sHtmlBody, , exs) Then
            UIHelper.WarnError(exs, "error al redactar missatge")
        End If

    End Sub
End Module
