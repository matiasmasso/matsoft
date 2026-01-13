Public Class LlibreDiari

    Shared Function Url(DtFch As Date, Optional AbsoluteUrl As Boolean = False)
        Return UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.LlibreDiari, DtFch.ToOADate)
    End Function

    Shared Function ExcelUrl(oExercici As DTOExercici, Optional AbsoluteUrl As Boolean = False)
        Return UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.LlibreDiari, oExercici.Guid.ToString())
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOCca))
        Return Await Api.Fetch(Of List(Of DTOCca))(exs, "Ccas/Headers", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Function HeadersSync(exs As List(Of Exception), oExercici As DTOExercici) As List(Of DTOCca)
        Return Api.FetchSync(Of List(Of DTOCca))(exs, "Ccas/Headers", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function Excel(exs As List(Of Exception), oExercici As DTOExercici, oLang As DTOLang) As Task(Of MatHelper.Excel.Sheet)
        Dim retval = Await Api.Fetch(Of MatHelper.Excel.Sheet)(exs, "Ccas/LlibreDiari/Excel", oExercici.Emp.Id, oExercici.Year, oLang.Tag)
        Return retval
    End Function

End Class
