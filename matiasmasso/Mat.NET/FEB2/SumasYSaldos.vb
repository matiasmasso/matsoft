Public Class SumasYSaldos
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, DtFch As Date) As Task(Of DTOSumasYSaldos)
        Return Await Api.Fetch(Of DTOSumasYSaldos)(exs, "sumasysaldos", oEmp.Id, FormatFch(DtFch))
    End Function

    Shared Async Function All(exs As List(Of Exception), ByVal oExercici As DTOExercici,
                        Optional HideEmptySaldo As Boolean = False,
                        Optional oRange As DTO.Defaults.ContactRange = DTO.Defaults.ContactRange.AllContacts,
                        Optional oContact As DTOContact = Nothing) As Task(Of List(Of DTOPgcSaldo))

        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "sumasysaldos",
                                                        oExercici.Emp.Id,
                                                        oExercici.Year,
                                                        If(HideEmptySaldo, 11, 0),
                                                        oRange,
                                                        OpcionalGuid(oContact))
    End Function

    Shared Function AllSync(exs As List(Of Exception), ByVal oExercici As DTOExercici,
                        Optional HideEmptySaldo As Boolean = False,
                        Optional oRange As DTO.Defaults.ContactRange = DTO.Defaults.ContactRange.AllContacts,
                        Optional oContact As DTOContact = Nothing) As List(Of DTOPgcSaldo)

        Return Api.FetchSync(Of List(Of DTOPgcSaldo))(exs, "sumasysaldos",
                                                        oExercici.Emp.Id,
                                                        oExercici.Year,
                                                        If(HideEmptySaldo, 11, 0),
                                                        oRange,
                                                        OpcionalGuid(oContact))
    End Function


    Shared Async Function Summary(exs As List(Of Exception), oEmp As DTOEmp, DtFch As Date) As Task(Of DTOSumasYSaldos) ' TO DEPRECATE
        Return Await Api.Fetch(Of DTOSumasYSaldos)(exs, "sumasysaldos/SummaryFromFch", oEmp.Id, FormatFch(DtFch))
    End Function


    Shared Async Function Summary(exs As List(Of Exception), oExercici As DTOExercici, DtFch As Date) As Task(Of List(Of DTOPgcSaldo))
        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "sumasysaldos/SummaryFromYear", oExercici.Emp.Id, oExercici.Year, FormatFch(DtFch))
    End Function

    Shared Async Function Summary(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOPgcSaldo))
        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "sumasysaldos/SummaryFromYear", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Function SummarySync(exs As List(Of Exception), oExercici As DTOExercici) As List(Of DTOPgcSaldo)
        Return Api.FetchSync(Of List(Of DTOPgcSaldo))(exs, "sumasysaldos/SummaryFromYear", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function SubComptes(exs As List(Of Exception), oExercici As DTOExercici, oCta As DTOPgcCta) As Task(Of List(Of DTOPgcSaldo))
        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "sumasysaldos/SubComptes", oExercici.Emp.Id, oExercici.Year, oCta.Guid.ToString())
    End Function

    Shared Function SubComptesSync(exs As List(Of Exception), oExercici As DTOExercici, oCta As DTOPgcCta) As List(Of DTOPgcSaldo)
        Return Api.FetchSync(Of List(Of DTOPgcSaldo))(exs, "sumasysaldos/SubComptes", oExercici.Emp.Id, oExercici.Year, oCta.Guid.ToString())
    End Function

    Shared Function YearsSync(exs As List(Of Exception), oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional oCta As DTOPgcCta = Nothing) As List(Of Integer)
        Return Api.FetchSync(Of List(Of Integer))(exs, "sumasysaldos/Years", oEmp.Id, OpcionalGuid(oContact), OpcionalGuid(oCta))
    End Function

    Shared Function UrlSubComptes(oExercici As DTOExercici, oCta As DTOPgcCta, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = UrlHelper.Factory(AbsoluteUrl, "SumasYSaldos/SubComptes", oCta.Guid.ToString, oExercici.Year.ToString())
        Return retval
    End Function

    Shared Function Url(Optional oExercici As DTOExercici = Nothing, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oExercici Is Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "SumasYSaldos")
        Else
            retval = UrlHelper.Factory(AbsoluteUrl, "SumasYSaldos", "Summary", System.Guid.Empty.ToString, oExercici.Year.ToString())
        End If
        Return retval
    End Function

End Class
