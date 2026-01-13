Public Class ImpagatsController
    Inherits _BaseController

    <HttpPost>
    <Route("api/impagats/summary")>
    Public Function summary(user As DTOGuidNom) As List(Of DUI.GuidNomEur)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim oImpagats As List(Of DTOImpagat) = BLL.BLLImpagats.All(oUser)

        Dim Query = oImpagats.GroupBy(Function(g) New With {Key g.Csb.Contact.Guid, Key g.Csb.Contact.FullNom}).
            Select(Function(group) New With {.contactGuid = group.Key.Guid, .contactNom = group.Key.FullNom, .eur = group.Sum(Function(y) y.Nominal.Eur)})

        Dim retval As New List(Of DUI.GuidNomEur)
        For Each item In Query
            Dim dui As New DUI.GuidNomEur
            With dui
                .Guid = item.contactGuid
                .Nom = item.contactNom
                .Eur = item.eur
            End With
            retval.Add(dui)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/contact/impagats")>
    Public Function impagats(contact As DTOGuidNom) As List(Of DUI.Impagat)
        Dim retval As New List(Of DUI.Impagat)
        Dim oContact As New DTOContact(contact.Guid)
        Dim oImpagats As List(Of DTOImpagat) = BLL.BLLImpagats.All(oContact)
        For Each oImpagat As DTOImpagat In oImpagats
            Dim item As New DUI.Impagat
            With item
                '.Guid = oRep.Guid.ToString
                '.Nom = BLLRep.NicknameOrNom(oRep)
            End With
            retval.Add(item)
        Next
        Return retval
    End Function
End Class
