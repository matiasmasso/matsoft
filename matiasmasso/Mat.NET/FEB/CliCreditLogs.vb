Public Class CliCreditLog

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCliCreditLog)
        Return Await Api.Fetch(Of DTOCliCreditLog)(exs, "CliCreditLog", oGuid.ToString())
    End Function


    Shared Function Load(ByRef oCliCreditLog As DTOCliCreditLog, exs As List(Of Exception)) As Boolean
        If Not oCliCreditLog.IsLoaded And Not oCliCreditLog.IsNew Then
            Dim pCliCreditLog = Api.FetchSync(Of DTOCliCreditLog)(exs, "CliCreditLog", oCliCreditLog.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCliCreditLog)(pCliCreditLog, oCliCreditLog, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOCliCreditLog, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCliCreditLog)(value, exs, "CliCreditLog")
    End Function

    Shared Async Function Delete(value As DTOCliCreditLog, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCliCreditLog)(value, exs, "CliCreditLog")
    End Function

    Shared Async Function CurrentLog(oCcx As DTOCustomer, exs As List(Of Exception)) As Task(Of DTOCliCreditLog)
        Return Await Api.Fetch(Of DTOCliCreditLog)(exs, "CliCreditLog/CurrentLog", oCcx.Guid.ToString())
    End Function

End Class

Public Class CliCreditLogs
    Shared Async Function CaducaCredits(exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(exs, "CliCreditLogs/caducaCredits")
    End Function

    Shared Async Function All(oCcx As DTOCustomer, exs As List(Of Exception)) As Task(Of List(Of DTOCliCreditLog))
        Return Await Api.Fetch(Of List(Of DTOCliCreditLog))(exs, "CliCreditLogs", oCcx.Guid.ToString())
    End Function

    Shared Async Function CreditLastAlbs(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOCreditLastAlb))
        Return Await Api.Fetch(Of List(Of DTOCreditLastAlb))(exs, "CliCreditLogs/CreditLastAlbs", oEmp.Id)
    End Function


End Class
