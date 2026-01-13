Public Class CorrespondenciaController
    Inherits _BaseController

    <HttpPost>
    <Route("api/correspondencies")>
    Public Function Correspondencies(UserContact As DUI.UserContact) As List(Of DUI.Correspondencia)
        Dim retval As New List(Of DUI.Correspondencia)
        Dim oUser As DTOUser = BLLUser.Find(UserContact.User.Guid)
        Dim oContact As New DTOContact(UserContact.Contact.Guid)
        If oUser IsNot Nothing Then
            Dim oCorrespondencies As List(Of DTOCorrespondencia) = BLLCorrespondencies.All(oContact)
            For Each oCorrespondencia As DTOCorrespondencia In oCorrespondencies
                Dim item As New DUI.Correspondencia
                With item
                    .Fch = oCorrespondencia.Fch
                    .Subject = oCorrespondencia.Subject
                    .FileUrl = BLLDocFile.DownloadUrl(oCorrespondencia.DocFile, True)
                    .ThumbnailUrl = BLLDocFile.ThumbnailUrl(oCorrespondencia.DocFile, True)
                    .Features = BLLDocFile.MediaFeatures(oCorrespondencia.DocFile)
                End With
                retval.Add(item)
            Next
        End If
        Return retval
    End Function
End Class
