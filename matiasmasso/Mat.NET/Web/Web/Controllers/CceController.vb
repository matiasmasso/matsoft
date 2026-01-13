Public Class CceController
    Inherits _MatController

    Async Function Index(cta As Guid, fch As Long) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        ViewBag.Fch = Date.FromOADate(fch)

        Dim oCta = Await FEB2.PgcCta.Find(cta, exs)
        Dim oClasses = Await FEB2.PgcClasses.All(exs)
        Dim oTree As List(Of DTOPgcClass) = DTOPgcClass.Tree(oClasses)
        oCta.PgcClass = oClasses.Find(Function(x) x.Equals(oCta.PgcClass))
        Dim items As List(Of DTOBalanceSaldo) = FEB2.Balance.CceSync(exs, GlobalVariables.Emp, oCta, ViewBag.fch)
        If items.Count = 1 Then
            Dim oContact As DTOContact = items(0).Contact
            retval = Redirect(FEB2.Ccd.Url(oCta, oContact, ViewBag.fch))
        Else
            retval = View("Cce", oCta)
        End If
        Return retval
    End Function
End Class
