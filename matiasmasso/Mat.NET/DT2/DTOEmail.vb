Public Class DTOEmail
    Inherits DTOBaseGuid


    Property EmailAddress As String
    Property BadMail As BadMailErrs
    Property Obs As String
    Property Privat As Boolean
    Property Obsoleto As Boolean

    Public Enum BadMailErrs
        None
        FullMailBox
        ServerNotFound
        UserNotFound
        BlackList
        Altres
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function MailAddressCollection(oEmails As List(Of DTOEmail)) As System.Net.Mail.MailAddressCollection
        Dim retval As New System.Net.Mail.MailAddressCollection
        For Each oEmail As DTOEmail In oEmails
            retval.Add(oEmail.EmailAddress)
        Next
        Return retval
    End Function

    Shared Function Recipients(oEmails As List(Of DTOEmail)) As String
        Dim sb As New Text.StringBuilder
        For Each oEmail As DTOEmail In oEmails
            If oEmail.UnEquals(oEmails.First) Then
                sb.Append("; ")
            End If
            sb.Append(oEmail.EmailAddress)
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class
