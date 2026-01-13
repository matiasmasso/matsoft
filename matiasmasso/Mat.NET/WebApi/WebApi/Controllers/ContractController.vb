Public Class ContractController
    Inherits _BaseController

    <HttpPost>
    <Route("api/contracts/categories")>
    Public Function Categories(user As DTOGuidNom) As List(Of DUI.Guidnom)
        Dim retval As New List(Of DUI.Guidnom)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        If oUser IsNot Nothing Then
            Dim oCategories As List(Of DTOContractCodi) = BLLContractCodis.All()
            For Each oCategory As DTOContractCodi In oCategories
                Dim item As New DUI.Guidnom
                With item
                    .Guid = oCategory.Guid
                    .Nom = oCategory.Nom
                End With
                retval.Add(item)
            Next
        End If
        Return retval
    End Function

    <HttpPost>
    <Route("api/contracts")>
    Public Function Contracts(data As DUI.UserContractcategory) As List(Of DUI.Contract)
        Dim retval As New List(Of DUI.Contract)
        Dim oUser As DTOUser = BLLUser.Find(data.user.Guid)
        If oUser IsNot Nothing Then
            Dim oCategory As New DTOContractCodi(data.category.Guid)
            Dim oContracts As List(Of DTOContract) = BLLContracts.All(oCategory)
            For Each oContract As DTOContract In oContracts
                Dim item As New DUI.Contract
                With item
                    .contact = New DUI.Contact
                    .Contact.Guid = oContract.Contact.Guid
                    .contact.Nom = BLLContract.ContactNom(oContract)
                    .Nom = oContract.Nom
                    .FchFrom = oContract.fchFrom
                    .FchTo = oContract.fchTo
                    .url = BLLDocFile.DownloadUrl(oContract.DocFile, True)
                End With
                retval.Add(item)
            Next
        End If
        Return retval
    End Function


End Class
