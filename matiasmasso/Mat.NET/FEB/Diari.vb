Public Class Diari


    Shared Async Function Load(oDiari As DTODiari, exs As List(Of Exception)) As Task(Of DTODiari)
        Return Await Api.Execute(Of DTODiari, DTODiari)(oDiari, exs, "Diari")
    End Function

    Shared Async Function LoadBrands(oDiari As DTODiari, exs As List(Of Exception)) As Task(Of DTODiari)
        Dim retval = Await Api.Execute(Of DTODiari, DTODiari)(oDiari, exs, "Diari/LoadBrands")
        Return retval
    End Function

    Shared Function Years(oDiari As DTODiari, exs As List(Of Exception)) As List(Of DtoDiariItem)
        Return Api.ExecuteSync(Of DTODiari, List(Of DtoDiariItem))(oDiari, exs, "Diari/Years")
    End Function

    Shared Function Months(oDiari As DTODiari, exs As List(Of Exception)) As List(Of DtoDiariItem)
        Return Api.ExecuteSync(Of DTODiari, List(Of DtoDiariItem))(oDiari, exs, "Diari/Months")
    End Function

    Shared Function Days(oDiari As DTODiari, exs As List(Of Exception)) As List(Of DtoDiariItem)
        Return Api.ExecuteSync(Of DTODiari, List(Of DtoDiariItem))(oDiari, exs, "Diari/Days")
    End Function

    Shared Function Orders(oDiari As DTODiari, exs As List(Of Exception)) As List(Of DtoDiariItem)
        Return Api.ExecuteSync(Of DTODiari, List(Of DtoDiariItem))(oDiari, exs, "Diari/Orders")
    End Function

    Shared Function Url(item As DtoDiariItem, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        Dim iYear As Integer = item.Parent.Year
        Dim iMonth As Integer
        Dim iDay As Integer
        Dim oContactGuid As Guid = Nothing
        If item.Parent.Owner IsNot Nothing Then oContactGuid = item.Parent.Owner.Guid

        Select Case item.Level
            Case DtoDiariItem.Levels.Yea
                iYear = item.Source
            Case DtoDiariItem.Levels.Mes
                iMonth = item.Source
            Case DtoDiariItem.Levels.Dia
                iMonth = item.Parent.Month
                iDay = CDate(item.Source).Day
        End Select
        retval = UrlHelper.Factory(AbsoluteUrl, "diari", oContactGuid.ToString, iYear, iMonth, iDay, item.Level)
        Return retval
    End Function
End Class
