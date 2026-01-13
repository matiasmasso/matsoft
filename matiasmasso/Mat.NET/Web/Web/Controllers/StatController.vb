Public Class StatController
    Inherits _MatController


    Function Index() As ActionResult
        Return View()
    End Function

    <HttpPost> _
    Function Months(year As Integer) As PartialViewResult
        'Dim oModel As DTOStat = StatLoader.Months(year, GetSession.Lang)
        'Return PartialView("_Table", oModel)
    End Function

    <HttpPost> _
    Function Days(year As Integer, month As Integer) As PartialViewResult
        'Dim oModel As DTOStat = StatLoader.Days(year, month, GetSession.Lang)
        'Return PartialView("_Table", oModel)
    End Function



End Class