Public Class ContactMessageController
    Inherits _MatController
    Function Index() As ActionResult
        Dim exs As New List(Of Exception)
        Dim oModel As New DTOContactMessage
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing Then
            oModel.Email = oUser.EmailAddress
            oModel.Nom = oUser.NicknameOrElse
            oModel.Location = oUser.LocationNom
        End If
        ViewBag.Title = ContextHelper.Tradueix("Formulario de contacto", "Formulari de contacte", "Contact form")
        ContextHelper.NavViewModel.ResetCustomMenu()
        Return View("ContactMessage", oModel)
    End Function

    <HttpPost>
    Async Function Index(model As DTOContactMessage) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oDomain As DTOWebDomain = ContextHelper.Domain
        ContextHelper.NavViewModel.ResetCustomMenu()
        If Await FEB.ContactMessage.Send(exs, model, oDomain) Then
            If Await FEB.ContactMessage.Update(exs, model) Then
                ViewBag.Title = ContextHelper.Tradueix("Formulario de contacto", "Formulari de contacte", "Contact form")
                Return View("ContactMessageThanks", model)
            Else
                Return Await MyBase.ErrorResult(exs)
            End If
        Else
            Return Await MyBase.ErrorResult(exs)
        End If
    End Function
End Class
