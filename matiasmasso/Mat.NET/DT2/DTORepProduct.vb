Public Class DTORepProduct
    Inherits DTOBaseGuid

    Property rep As DTORep
    Property product As Object
    Property area As Object
    Property distributionChannel As DTODistributionChannel
    Property cod As Cods
    Property fchFrom As Date
    Property fchTo As Date
    Property comStd As Decimal
    Property comRed As Decimal


    Public Enum Cods
        notSet
        included
        excluded
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oProduct As DTOProduct, Optional oRep As DTORep = Nothing, Optional oArea As DTOArea = Nothing, Optional oDistributionChannel As DTODistributionChannel = Nothing) As DTORepProduct
        Dim retval As New DTORepProduct()
        With retval
            .product = oProduct
            .area = oArea
            .distributionChannel = oDistributionChannel
            .fchFrom = Today
            If oRep IsNot Nothing Then
                .rep = oRep
                .comStd = oRep.comisionStandard
                .comRed = oRep.comisionReducida
            End If
            .cod = DTORepProduct.Cods.included
            .isNew = True
        End With
        Return retval
    End Function

    Public Sub RestoreGenerics()
        _product = DTOProduct.FromObject(_product)
        _area = DTOArea.FromObject(_area)
    End Sub

    Shared Function IsActive(oRepProduct As DTORepProduct) As Boolean
        Dim retval As Boolean = oRepProduct.fchFrom <= Today
        If oRepProduct.fchTo <> Nothing Then
            If oRepProduct.fchTo < Today Then retval = False
        End If
        Return retval
    End Function

    Shared Function Clon(src As DTORepProduct) As DTORepProduct
        Dim retval = DTORepProduct.Factory(src.product, src.rep, src.area)
        With retval
            .cod = src.cod
            .fchFrom = src.fchFrom
            .fchTo = src.fchTo
            .comStd = src.comStd
            .comRed = src.comRed
        End With
        Return retval
    End Function

    Public Shadows Function ToString() As String
        Dim sb As New System.Text.StringBuilder
        If _rep IsNot Nothing Then
            Dim sNom As String = IIf(_rep.nom = "", _rep.FullNom, _rep.nom)
            sb.Append("rep: " & sNom & " ")
        End If
        If _product IsNot Nothing Then
            sb.Append("product: " & _product.Nom & " ")
        End If
        If _area IsNot Nothing Then
            If TypeOf _area Is DTOZona Then
                sb.Append("area: ")
                Dim oCountry As DTOCountry = CType(_area, DTOZona).country
                If oCountry IsNot Nothing Then
                    sb.Append(oCountry.langNom.esp & "/")
                End If
                sb.Append(CType(_area, DTOZona).nom & " ")
            End If
        End If
        If _distributionChannel IsNot Nothing Then
            sb.Append("canal: " & _distributionChannel.langText.esp & " ")
        End If
        sb.Append("(" & _cod.ToString & ")")
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function Match(oRepProducts As List(Of DTORepProduct), oChannel As DTODistributionChannel, oZip As DTOZip, oSku As DTOProductSku, DtFch As Date) As List(Of DTORepProduct)
        Dim retval As New List(Of DTORepProduct)

        'filtra per data
        Dim Step1 As List(Of DTORepProduct) = oRepProducts.FindAll(Function(x) x.fchFrom <= DtFch And (x.fchTo = Nothing Or x.fchTo >= DtFch)).ToList
        'filtra per zona
        Dim Step2 As List(Of DTORepProduct) = Step1.FindAll(Function(x) (x.area.Guid.Equals(oZip.Guid) Or x.area.Guid.Equals(oZip.location.Guid) Or x.area.Guid.Equals(oZip.location.zona.Guid) Or x.area.Guid.Equals(oZip.location.zona.country.Guid))).ToList
        'filtra per producte
        Dim Step3 As List(Of DTORepProduct) = Step2.FindAll(Function(x) (x.product.Guid.Equals(oSku.Guid) Or x.product.Guid.Equals(oSku.category.Guid) Or x.product.Guid.Equals(oSku.category.Brand.Guid))).ToList

        'filtra per canal de distribució o surt si el destinatari no está assignat a cap canal
        Dim Step4 As New List(Of DTORepProduct)
        If oChannel IsNot Nothing Then
            Step4 = Step3.FindAll(Function(x) x.distributionChannel.Guid.Equals(oChannel.Guid)).ToList

            'identifica els exclosos
            Dim oRepsToRemove As New List(Of DTORep)
            For Each item As DTORepProduct In Step4.FindAll(Function(x) x.cod = DTORepProduct.Cods.excluded)
                oRepsToRemove.Add(item.rep)
            Next

            'llista els inclosos que el seu rep no ha estat exclos
            For Each item As DTORepProduct In Step4.FindAll(Function(x) x.cod = DTORepProduct.Cods.included)
                If Not oRepsToRemove.Exists(Function(x) x.Guid.Equals(item.rep.Guid)) Then
                    retval.Add(item)
                End If
            Next
        End If
        Return retval
    End Function


    Shared Function Excel(items As List(Of DTORepProduct)) As ExcelHelper.Sheet
        Dim ColProducts As Boolean = items.Exists(Function(x) x.product IsNot Nothing)
        Dim ColFchFrom As Boolean = items.Exists(Function(x) x.fchFrom <> Nothing)

        Dim retval As New ExcelHelper.Sheet
        With retval
            .AddColumn("zona")
            .AddColumn("representante")
            If ColProducts Then .AddColumn("producto")
            If ColFchFrom Then .AddColumn("desde")
            .AddColumn("movil")
        End With

        For Each item As DTORepProduct In items
            If DTORepProduct.IsActive(item) Then
                Dim oRow As ExcelHelper.Row = retval.AddRow
                With item
                    oRow.AddCell(DTOArea.NomOrDefault(.area))
                    oRow.AddCell(.rep.nickName)
                    If ColProducts Then oRow.AddCell(DTOProduct.GetNom(.product))
                    If ColFchFrom Then oRow.AddCell(.fchFrom)
                    oRow.AddCell(.rep.telefon.Replace(" ", ""))
                End With
            End If
        Next

        Return retval
    End Function

End Class
