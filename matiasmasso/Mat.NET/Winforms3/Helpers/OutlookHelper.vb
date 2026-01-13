Imports Microsoft.Office.Interop


Public Class OutlookHelper
    Private Shared _App As Outlook.Application
    Private Shared _NameSpace As Outlook.NameSpace


    Shared Function OutlookApp(exs As List(Of Exception)) As Outlook.Application
        If _App Is Nothing Then
            Try
                _App = New Outlook.Application     'Retrieving the COM class factory for component with CLSID {0006F03A-0000-0000-C000-000000000046} failed due to the following error: 80080005 Server execution failed (Exception from HRESULT: 0x80080005 (CO_E_SERVER_EXEC_FAILURE)).
            Catch ex As System.Exception
                exs.Add(ex)
            End Try
        End If
        Return _App
    End Function

    Shared Function OutlookNameSpace(exs As List(Of Exception)) As Outlook.NameSpace
        If _NameSpace Is Nothing Then
            Dim oOutlookApp = OutlookApp(exs)
            If exs.Count = 0 Then
                _NameSpace = oOutlookApp.GetNamespace("MAPI")
            End If
        End If
        Return _NameSpace
    End Function


    Shared Async Function Send(oMailMessage As DTOMailMessage, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oOutlookMailItem = Await MailItem(oMailMessage, exs)
        AddHandler oOutlookMailItem.CustomAction, AddressOf Do_AfterSent
        If exs.Count = 0 Then
            oOutlookMailItem.Display()
        End If
        Return exs.Count = 0
    End Function

    Private Shared Sub Do_AfterSent()
        'desar data avis client(proveidor
    End Sub

    Shared Async Function MailItem(oMailMessage As DTOMailMessage, exs As List(Of Exception)) As Task(Of Outlook.MailItem)
        Dim retval As Outlook.MailItem = Nothing
        Dim oOlApp = OutlookApp(exs)
        If exs.Count = 0 Then
            retval = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
            With retval
                Try
                    If oMailMessage.To IsNot Nothing Then
                        For Each sTo In oMailMessage.To
                            Dim oRecipient = .Recipients.Add(sTo)
                            oRecipient.Type = Outlook.OlMailRecipientType.olTo
                        Next
                    End If

                    For Each s In oMailMessage.Cc
                        Dim oCc = .Recipients.Add(s)
                        oCc.Type = Outlook.OlMailRecipientType.olCC
                    Next

                    For Each s In oMailMessage.Bcc
                        Dim oBcc = .Recipients.Add(s)
                        oBcc.Type = Outlook.OlMailRecipientType.olBCC
                    Next

                    .Subject = oMailMessage.Subject

                    If oMailMessage.BodyUrl > "" Then
                        oMailMessage.Body = Await DownloadBody(oMailMessage.BodyUrl, exs)
                        oMailMessage.BodyFormat = DTOMailMessage.MessageBodyFormats.Html
                    End If

                    Select Case oMailMessage.BodyFormat
                        Case DTOMailMessage.MessageBodyFormats.ASCII
                            .Body = oMailMessage.Body
                        Case DTOMailMessage.MessageBodyFormats.Html
                            .HTMLBody = oMailMessage.Body
                            '.BodyFormat = Outlook.OlBodyFormat.olFormatHTML
                    End Select

                    If oMailMessage.Attachments IsNot Nothing Then
                        For Each oAttachment In oMailMessage.Attachments
                            If String.IsNullOrEmpty(oAttachment.Path) Then
                                oAttachment.Path = System.IO.Path.GetTempPath() + oAttachment.Friendlyname
                                Dim fs As New System.IO.FileStream(oAttachment.Path, System.IO.FileMode.Create)
                                Dim ms As New System.IO.MemoryStream(oAttachment.ByteArray)
                                ms.CopyTo(fs)
                                fs.Close()
                            End If

                            .Attachments.Add(oAttachment.Path, Outlook.OlAttachmentType.olByValue,, oAttachment.Friendlyname)
                        Next
                    End If

                Catch ex As Exception
                    If exs IsNot Nothing Then
                        exs.Add(ex)
                    End If
                End Try

            End With

        End If
        Return retval
    End Function

    Shared Async Function DownloadBody(url As String, exs As List(Of Exception)) As Task(Of String)
        Dim retval As String = ""
        Try
            Using client As New System.Net.WebClient()
                client.Encoding = Text.Encoding.UTF8
                retval = Await client.DownloadStringTaskAsync(url)
            End Using

        Catch ex As Exception
            UIHelper.CopyToClipboard(url)
            exs.Add(New Exception(String.Format("Error al descarregar el cos del missatge de {0}", url)))
            exs.Add(ex)
        End Try
        Return retval
    End Function
End Class
