Public Class DTOMailMessage
    Public Property [to] As IEnumerable(Of String)

    Public Property cc As IEnumerable(Of String)
    Public Property bcc As IEnumerable(Of String)
    Public Property subject As String
    Public Property body As String
    Public Property bodyUrl As String
    Public Property bodyFormat As MessageBodyFormats
    Public Property attachments As List(Of Attachment)


    Public Enum wellknownRecipients
        Admin
        Info
        Cuentas
    End Enum

    Public Enum MessageBodyFormats
        NotSet
        ASCII
        Html
    End Enum

    Public Sub New()
        MyBase.New()
        _Cc = New List(Of String)
        _Bcc = New List(Of String)
        _BodyFormat = MessageBodyFormats.Html
        _Attachments = New List(Of Attachment)
    End Sub

    Shared Function Factory(Optional recipient As String = "", Optional ByVal Subject As String = "", Optional ByVal Body As String = "") As DTOMailMessage
        Dim retval As New DTOMailMessage
        With retval
            If recipient > "" Then
                .To = {recipient}.ToList
            End If
            .Subject = Subject
            .Body = Body
        End With
        Return retval
    End Function

    Shared Function Factory(sRecipients As List(Of String), Optional ByVal Subject As String = "", Optional ByVal Body As String = "") As DTOMailMessage
        Dim retval As New DTOMailMessage
        With retval
            .To = sRecipients
            .Subject = Subject
            .Body = Body
        End With
        Return retval
    End Function

    Public Sub AddAttachment(friendlyName As String, oByteArray As Byte())
        Dim oAttachment = Attachment.Factory(friendlyName, oByteArray)
        _Attachments.Add(oAttachment)
    End Sub

    Public Sub AddAttachment(path As String, Optional sFriendlyName As String = "")
        If sFriendlyName = "" Then sFriendlyName = System.IO.Path.GetFileName(path)
        Dim oAttachment = Attachment.Factory(sFriendlyName, path)
        _Attachments.Add(oAttachment)
    End Sub

    Shared Function TemplateBodyUrl(oTemplate As DTODefault.MailingTemplates, ParamArray UrlSegments() As String) As String
        Dim oSegments As New List(Of String)
        oSegments.Add("mail")
        oSegments.Add(oTemplate.ToString())
        For Each s In UrlSegments
            oSegments.Add(s)
        Next
        Dim retval As String = MmoUrl.Factory(True, oSegments.ToArray)
        Return retval
    End Function


    Shared Function wellknownAddress(oRecipient As DTOMailMessage.wellknownRecipients) As String
        Dim retval As String = ""
        Select Case oRecipient
            Case DTOMailMessage.wellknownRecipients.Admin
                retval = "matias@matiasmasso.es"
            Case DTOMailMessage.wellknownRecipients.Info
                retval = "info@matiasmasso.es"
            Case DTOMailMessage.wellknownRecipients.Cuentas
                retval = "cuentas@matiasmasso.es"
        End Select
        Return retval
    End Function

    Public Function ConcatenatedCommaTo() As String
        Dim retval = String.Join(",", _To.ToArray)
        Return retval
    End Function

    Public Function ConcatenatedSemicolonTo() As String
        Dim retval = String.Join(";", _To.ToArray)
        Return retval
    End Function

    Public Class Attachment
        Property Path As String
        Property Friendlyname As String
        Property ByteArray As Byte()

        Shared Function Factory(friendlyName As String, path As String) As Attachment
            Dim retval As New Attachment
            With retval
                .Friendlyname = friendlyName
                .Path = path
            End With
            Return retval
        End Function

        Shared Function Factory(friendlyName As String, oByteArray As Byte()) As Attachment
            Dim retval As New Attachment
            With retval
                .Friendlyname = friendlyName
                .ByteArray = oByteArray
            End With
            Return retval
        End Function

    End Class


End Class
