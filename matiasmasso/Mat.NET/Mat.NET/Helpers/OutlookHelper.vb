Imports Microsoft.Office.Interop

Public Class OutlookHelper

    Shared Sub NewMessage(sRecipients As List(Of String), Optional sCcs As List(Of String) = Nothing, Optional sBccs As List(Of String) = Nothing, Optional sSubject As String = "")
        Dim oOlApp As New Outlook.Application
        Dim oNewMail As Outlook.MailItem = oOlApp.CreateItem(Outlook.OlItemType.olMailItem)
        With oNewMail
            Try
                For Each sRecipient As String In sRecipients
                    Dim oRecipient As Outlook.Recipient = .Recipients.Add(sRecipient)
                    oRecipient.Type = Outlook.OlMailRecipientType.olTo
                Next

                If sCcs IsNot Nothing Then
                    For Each sRecipient As String In sCcs
                        Dim oRecipient As Outlook.Recipient = .Recipients.Add(sRecipient)
                        oRecipient.Type = Outlook.OlMailRecipientType.olBCC
                    Next
                End If

                If sBccs IsNot Nothing Then
                    For Each sRecipient As String In sBccs
                        Dim oRecipient As Outlook.Recipient = .Recipients.Add(sRecipient)
                        oRecipient.Type = Outlook.OlMailRecipientType.olBCC
                    Next
                End If

                .Subject = sSubject
                .Display()
            Catch ex As Exception
                UIHelper.WarnError(ex)
            End Try
        End With
    End Sub
End Class
