Public Class CcdController
    Inherits _MatController

    Async Function Index(cta As Guid, contact As Guid, fch As Long) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim DtFch As Date = Date.FromOADate(fch)
        Dim oExercici As DTOExercici = DTOExercici.FromYear(Website.GlobalVariables.Emp, DtFch.Year)
        Dim oCta = Await FEB.PgcCta.Find(cta, exs)
        Dim oContact As DTOContact = Nothing
        If Not contact.Equals(Guid.Empty) Then
            oContact = Await FEB.Contact.Find(contact, exs)
        End If
        Dim Model = DTOCcd.Factory(oExercici, oCta, oContact, DtFch)
        Model.Ccbs = Await FEB.Ccbs.All(exs, Model)
        Return View("Ccd", Model)
    End Function
End Class
