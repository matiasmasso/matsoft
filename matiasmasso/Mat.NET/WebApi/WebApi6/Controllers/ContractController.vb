Public Class ContractController
    Inherits _BaseController

    <HttpPost>
    <Route("api/contracts2")>
    Public Function Categories2(user As DTOGuidNom) As List(Of DTOCompactContractCodi)
        Dim retval As New List(Of DTOCompactContractCodi)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        If oUser IsNot Nothing Then
            retval = Categories2(oUser)
        End If
        Return retval
    End Function

    <HttpGet>
    <Route("api/contracts2/{user}")>
    Public Function Categories2Get(user As Guid) As List(Of DTOCompactContractCodi)
        Dim oUser As DTOUser = BLLUser.Find(user)
        Dim retval As New List(Of DTOCompactContractCodi)
        If oUser IsNot Nothing Then
            retval = Categories2(oUser)
        End If
        Return retval
    End Function

    Private Function Categories2(oUser As DTOUser) As List(Of DTOCompactContractCodi)
        Dim retval As New List(Of DTOCompactContractCodi)
        If oUser IsNot Nothing Then
            Dim oCodi As New DTOCompactContractCodi
            Dim oContracts As List(Of DTOContract) = BLLContracts.All(oUser)
            oContracts = oContracts.OrderByDescending(Function(y) y.fchFrom).OrderBy(Function(x) x.Codi.Guid).OrderBy(Function(x) x.Codi.Nom).ToList
            For Each oContract As DTOContract In oContracts
                If Not oContract.Codi.Guid.Equals(oCodi.guid) Then
                    oCodi = New DTOCompactContractCodi
                    With oCodi
                        .guid = oContract.Codi.Guid
                        .nom = oContract.Codi.Nom
                        .items = New List(Of DTOCompactContractCodi.Contract)
                    End With
                    retval.Add(oCodi)
                End If

                Dim item As New DTOCompactContractCodi.Contract
                With item
                    .guid = oContract.Guid
                    .nom = oContract.Nom
                    .contact = New DTOCompactGuidNom
                    .contact.Guid = oContract.Contact.Guid
                    .contact.Nom = oContract.Contact.FullNom
                    .fchFrom = oContract.fchFrom
                    If oContract.fchTo <> Nothing Then
                        .fchTo = oContract.fchTo
                    End If
                    If oContract.DocFile IsNot Nothing Then
                        .downloadUrl = BLLDocFile.DownloadUrl(oContract.DocFile, True)
                        .thumbnailUrl = BLLDocFile.ThumbnailUrl(oContract.DocFile, True)
                    End If
                End With
                oCodi.items.Add(item)
            Next
        End If
        Return retval

    End Function

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
            Dim oContracts As List(Of DTOContract) = BLLContracts.All(oUser, oCategory)
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
