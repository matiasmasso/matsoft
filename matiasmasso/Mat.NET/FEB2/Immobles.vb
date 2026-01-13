Public Class Immoble

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOImmoble)
        Return Await Api.Fetch(Of DTOImmoble)(exs, "Immoble", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oImmoble As DTOImmoble, exs As List(Of Exception)) As Boolean
        If Not oImmoble.IsLoaded And Not oImmoble.IsNew Then
            Dim pImmoble = Api.FetchSync(Of DTOImmoble)(exs, "Immoble", oImmoble.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOImmoble)(pImmoble, oImmoble, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oImmoble As DTOImmoble, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOImmoble)(oImmoble, exs, "Immoble")
        oImmoble.IsNew = False
    End Function


    Shared Async Function Delete(oImmoble As DTOImmoble, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOImmoble)(oImmoble, exs, "Immoble")
    End Function
End Class

Public Class Immobles

    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOImmoble))
        Return Await Api.Fetch(Of List(Of DTOImmoble))(exs, "Immobles", oEmp.Id)
    End Function

    Shared Function Excel(oImmobles As List(Of DTOImmoble), oLang As DTOLang) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet("Immobles")
        For Each item In oImmobles
            Dim oRow = retval.AddRow
            oRow.AddCell(item.Nom)
            oRow.AddCell(item.Cadastre)
            oRow.AddCell(DTOAddress.FullText(item.Address, oLang))
        Next
        Return retval
    End Function
End Class

