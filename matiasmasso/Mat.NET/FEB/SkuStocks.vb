Public Class SkuStocks
    Inherits _FeblBase

    Shared Async Function ForWeb(oUser As DTOUser, exs As List(Of Exception)) As Task(Of DTOCatalog)
        Dim retval = Await Api.Fetch(Of DTOCatalog)(exs, "SkuStocks/ForWeb2", oUser.Guid.ToString)
        Return retval
    End Function

    Shared Async Function Excel(exs As List(Of Exception), oEmp As DTOEmp, oParams As Dictionary(Of String, String)) As Task(Of MatHelper.Excel.Sheet)
        Dim retval As MatHelper.Excel.Sheet = Nothing
        Dim oGuid As New Guid(oParams("user"))
        Dim oUser = Await User.Find(oGuid, exs)
        If oUser IsNot Nothing Then
            Dim oCatalog As DTOCatalog = Await ForWeb(oUser, exs)
            retval = oCatalog.Excel(exs)
        End If
        Return retval
    End Function
End Class
