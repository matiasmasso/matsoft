Public Class RepsController
    Inherits _MatController

    '
    ' GET: /Reps

    Function QuartersProgress() As ActionResult
        Dim Model As List(Of DTOPeriodProgress) = BLL.BLLPeriodProgress.RepQuarters(GlobalVariables.Emp)
        Return LoginOrView(, Model)
    End Function




End Class