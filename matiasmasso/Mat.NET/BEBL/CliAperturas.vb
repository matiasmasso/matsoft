Public Class CliApertura
    Shared Function Find(oGuid As Guid) As DTOCliApertura
        Dim retval As DTOCliApertura = CliAperturaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oCliApertura As DTOCliApertura) As Boolean
        Dim retval As Boolean = CliAperturaLoader.Load(oCliApertura)
        Return retval
    End Function

    Shared Function Update(oCliApertura As DTOCliApertura, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CliAperturaLoader.Update(oCliApertura, exs)
        Return retval
    End Function

    Shared Function UpdateStatus(exs As List(Of Exception), oCliApertura As DTOCliApertura, oCodTancament As DTOCliApertura.CodsTancament, sRepObs As String) As Boolean
        Return CliAperturaLoader.UpdateStatus(exs, oCliApertura, oCodTancament, sRepObs)
    End Function

    Shared Function Delete(oCliApertura As DTOCliApertura, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CliAperturaLoader.Delete(oCliApertura, exs)
        Return retval
    End Function

    Shared Async Function Send(oEmp As DTOEmp, value As DTOCliApertura, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oMailMessage = MailMessage(value, exs)
        Dim retval = Await MailMessageHelper.Send(oEmp, oMailMessage, exs)
        Return retval
    End Function

    Shared Function MailMessage(value As DTOCliApertura, exs As List(Of Exception)) As DTOMailMessage
        Dim retval = DTOMailMessage.Factory(value.Email)
        With retval
            .Cc = MailCc(value)
            .Subject = "Solicitud apertura distribuidor"
            .BodyUrl = MailUrl(value, True)
        End With
        Return retval
    End Function

    Shared Function MailUrl(oCliApertura As DTOCliApertura, Optional AbsoluteUrl As Boolean = True) As String
        Return DTOWebDomain.Default(AbsoluteUrl).Url("mail", "CliApertura", oCliApertura.Guid.ToString())
    End Function

    Shared Function MailCc(oCliApertura As DTOCliApertura) As List(Of String)
        Dim retval As New List(Of String)
        Dim oChannel As DTODistributionChannel = Nothing
        If oCliApertura.ContactClass IsNot Nothing Then
            oChannel = oCliApertura.ContactClass.DistributionChannel
            If oChannel IsNot Nothing Then
                If oCliApertura.Zona IsNot Nothing Then
                    Dim repEmails As List(Of DTOEmail) = BEBL.Reps.Emails(oCliApertura.Zona, oChannel)
                    For Each repEmail As DTOEmail In repEmails
                        retval.Add(repEmail.EmailAddress)
                    Next
                End If
            End If
        End If

        retval.Add("jr@matiasmasso.es")
        retval.Add("matias@matiasmasso.es")
        retval.Add("info@matiasmasso.es")
        Return retval
    End Function
End Class

Public Class CliAperturas

    Shared Function All(oUser As DTOUser) As DTOCliApertura.Collection
        Return CliAperturasLoader.All(oUser)
    End Function
End Class
