Public Class CliCreditLog
    Shared Function Find(oGuid As Guid) As DTOCliCreditLog
        Dim retval As DTOCliCreditLog = CliCreditLogLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oCliCreditLog As DTOCliCreditLog) As Boolean
        Dim retval As Boolean = CliCreditLogLoader.Load(oCliCreditLog)
        Return retval
    End Function

    Shared Function Update(value As DTOCliCreditLog, exs As List(Of Exception)) As Boolean
        Dim retval = CliCreditLogLoader.Update(value, exs)
        Return retval
    End Function

    Shared Function Delete(value As DTOCliCreditLog, exs As List(Of Exception)) As Boolean
        Dim retval = CliCreditLogLoader.Delete(value, exs)
        Return retval
    End Function

    Shared Function CurrentLog(oCcx As DTOCustomer) As DTOCliCreditLog
        Dim retval As DTOCliCreditLog = Nothing
        Dim oLogs As List(Of DTOCliCreditLog) = BEBL.CliCreditLogs.All(oCcx)
        If oLogs.Count > 0 Then
            retval = oLogs(0)
        End If
        Return retval
    End Function

    Shared Function CurrentCreditLimit(oCcx As DTOCustomer) As DTOAmt
        Dim retval = DTOAmt.Empty
        Dim oCurrentLog As DTOCliCreditLog = CurrentLog(oCcx)
        If oCurrentLog IsNot Nothing Then
            retval = oCurrentLog.Amt
        End If
        Return retval
    End Function
End Class

Public Class CliCreditLogs

    Shared Function All(oCcx As DTOCustomer) As List(Of DTOCliCreditLog)
        Dim retval As List(Of DTOCliCreditLog) = CliCreditLogsLoader.All(oCcx)
        Return retval
    End Function

    Shared Function CreditLastAlbs(oEmp As DTOEmp) As List(Of DTOCreditLastAlb)
        Dim retval As List(Of DTOCreditLastAlb) = CliCreditLogsLoader.CreditLastAlbs(oEmp)
        Return retval
    End Function

    Shared Async Function CaducaCredits(oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim sb As New System.Text.StringBuilder
        Dim iCount As Integer
        Try
            Dim items As List(Of DTOCliCreditLog) = CliCreditLogsLoader.PendentsDeCaducar(oUser)
            For Each item As DTOCliCreditLog In items
                If CliCreditLogLoader.Update(item, exs) Then
                    sb.AppendLine(item.Customer.FullNom & " - " & item.Obs)
                    iCount += 1
                End If
            Next

            If exs.Count = 0 Then
                If iCount > 0 Then
                    Dim oSsc = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.CreditsCaducatsList)
                    Dim oSubscriptors As List(Of DTOSubscriptor) = SubscriptorsLoader.All(oSsc)

                    If oSubscriptors.Count > 0 Then
                        Dim sRecipients = oSubscriptors.ToHashSet.Select(Function(x) x.EmailAddress).ToList()
                        Dim oMailMessage = DTOMailMessage.Factory(sRecipients, "Relació de credits caducats per inactivitat", sb.ToString())
                        Await MailMessageHelper.Send(oUser.Emp, oMailMessage, exs)
                    End If

                End If

                retval = True
            Else
                If iCount = 0 Then
                    exs.Add(New Exception("no s'han pogut fer caducar els credits"))
                Else
                    exs.Add(New Exception(String.Format("{0} credits caducats, alguns amb errors", iCount)))
                End If
            End If

        Catch ex As Exception
            exs.Add(New Exception("no s'han pogut fer caducar els credits"))
            exs.Add(ex)
        End Try

        Return retval
    End Function

End Class
