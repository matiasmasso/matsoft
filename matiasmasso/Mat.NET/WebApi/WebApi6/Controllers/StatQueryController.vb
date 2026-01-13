Public Class StatQueryController
    Inherits _BaseController

    <HttpPost>
    <Route("api/stats")>
    Public Function stats(oDUI As DUI.StatQuery) As DUI.StatQuery
        Dim oDTO As New DTOStatQuery
        With oDTO
            .user = BLLUser.Find(oDUI.user)
            .reportMode = oDUI.reportMode
            .keyCod = oDUI.keyCod
            .valueCod = oDUI.valueCod
            .filterYear = oDUI.filterYear
            .filterMonth = oDUI.filterMonth
            .filterDay = oDUI.filterDay
            If oDUI.filterClient <> Nothing Then
                .filterClient = New DTOCustomer(oDUI.filterClient)
            End If
            If oDUI.filterRep <> Nothing Then
                .filterRep = New DTORep(oDUI.filterRep)
            End If
            If oDUI.filterManufacturer <> Nothing Then
                .filterManufacturer = New DTOManufacturer(oDUI.filterManufacturer)
            End If
            If oDUI.filterProduct <> Nothing Then
                .filterProduct = New DTOProduct(oDUI.filterProduct)
            End If
        End With
        BLLStatQuery.Load(oDTO)

        oDUI.items = New List(Of DUI.StatQueryItem)
        For Each item As DTOStatQueryItem In oDTO.items
            Dim tmp As New DUI.StatQueryItem
            tmp.keyCod = item.keyCod
            tmp.keyNom = item.keyNom
            tmp.value = item.value
            oDUI.items.Add(tmp)
        Next

        Return oDUI
    End Function


    <HttpPost>
    <Route("api/stats/products")>
    Public Function products(oDUI As DUI.StatQuery) As List(Of DUI.GuidNomEur)
        Dim oDTO As New DTOStatQuery
        With oDTO
            .user = BLLUser.Find(oDUI.user)
            .reportMode = oDUI.reportMode
            .keyCod = oDUI.keyCod
            .valueCod = oDUI.valueCod
            .filterYear = oDUI.filterYear
            .filterMonth = oDUI.filterMonth
            .filterDay = oDUI.filterDay
            If oDUI.filterClient <> Nothing Then
                .filterClient = New DTOCustomer(oDUI.filterClient)
            End If
            If oDUI.filterRep <> Nothing Then
                .filterRep = New DTORep(oDUI.filterRep)
            End If
            If oDUI.filterManufacturer <> Nothing Then
                .filterManufacturer = New DTOManufacturer(oDUI.filterManufacturer)
            End If
            If oDUI.filterProduct <> Nothing Then
                .filterProduct = BLLProduct.Find(oDUI.filterProduct)
            End If
        End With
        Dim oProducts As List(Of DTOGuidNomAmt) = BLLStatQuery.Products(oDTO)

        Dim retval As New List(Of DUI.GuidNomEur)
        For Each item As DTOGuidNomAmt In oProducts
            If Not item.Nom.Contains("(por clasificar)") Then
                Dim dui As New DUI.GuidNomEur
                dui.Guid = item.Guid
                dui.Nom = item.Nom
                If item.Amt IsNot Nothing Then
                    dui.Eur = item.Amt.Eur
                End If
                retval.Add(dui)
            End If
        Next

        Return retval
    End Function

    <HttpPost>
    <Route("api/stats/areas")>
    Public Function areas(oDUI As DUI.StatQuery) As List(Of DUI.GuidNomEur)
        Dim oDTO As New DTOStatQuery
        With oDTO
            .user = BLLUser.Find(oDUI.user)
            .reportMode = oDUI.reportMode
            .keyCod = oDUI.keyCod
            .valueCod = oDUI.valueCod
            .filterYear = oDUI.filterYear
            .filterMonth = oDUI.filterMonth
            .filterDay = oDUI.filterDay
            If oDUI.filterClient <> Nothing Then
                .filterClient = New DTOCustomer(oDUI.filterClient)
            End If
            If oDUI.filterRep <> Nothing Then
                .filterRep = New DTORep(oDUI.filterRep)
            End If
            If oDUI.filterManufacturer <> Nothing Then
                .filterManufacturer = New DTOManufacturer(oDUI.filterManufacturer)
            End If
            If oDUI.filterProduct <> Nothing Then
                .filterProduct = New DTOProduct(oDUI.filterProduct)
            End If
        End With

        Dim oAreas As List(Of DTOGuidNomAmt) = BLLStatQuery.Areas(oDTO)

        Dim retval As New List(Of DUI.GuidNomEur)
        For Each item As DTOGuidNomAmt In oAreas
            Dim dui As New DUI.GuidNomEur
            dui.Guid = item.Guid
            dui.Nom = item.Nom
            retval.Add(dui)
        Next

        Return retval
    End Function

    <HttpPost>
    <Route("api/stats/reps")>
    Public Function reps(oDUI As DUI.StatQuery) As List(Of DUI.GuidNomEur)
        Dim oDTO As New DTOStatQuery
        With oDTO
            .user = BLLUser.Find(oDUI.user)
            .reportMode = oDUI.reportMode
            .keyCod = oDUI.keyCod
            .valueCod = oDUI.valueCod
            .filterYear = oDUI.filterYear
            .filterMonth = oDUI.filterMonth
            .filterDay = oDUI.filterDay
            If oDUI.filterClient <> Nothing Then
                .filterClient = New DTOCustomer(oDUI.filterClient)
            End If
            If oDUI.filterRep <> Nothing Then
                .filterRep = New DTORep(oDUI.filterRep)
            End If
            If oDUI.filterManufacturer <> Nothing Then
                .filterManufacturer = New DTOManufacturer(oDUI.filterManufacturer)
            End If
            If oDUI.filterProduct <> Nothing Then
                .filterProduct = BLLProduct.Find(oDUI.filterProduct)
            End If
        End With
        Dim items As List(Of DTOGuidNomAmt) = BLLStatQuery.Reps(oDTO)

        Dim retval As New List(Of DUI.GuidNomEur)
        For Each item As DTOGuidNomAmt In items
            Dim dui As New DUI.GuidNomEur
            dui.Guid = item.Guid
            dui.Nom = item.Nom
            If item.Amt IsNot Nothing Then
                dui.Eur = item.Amt.Eur
            End If
            retval.Add(dui)
        Next

        Return retval
    End Function


End Class
