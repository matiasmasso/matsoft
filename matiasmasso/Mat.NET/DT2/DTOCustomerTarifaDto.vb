Public Class DTOCustomerTarifaDto
    Inherits DTOBaseGuid

    Property CustomerOrChannel As DTOBaseGuid
    Property CustomerOrChannelNom As String
    Property Fch As Date
    Property Product As DTOProduct
    Property Dto As Decimal
    Property Src As Srcs
    Property Obs As String

    Public Enum Srcs
        NotSet
        Canal
        Client
    End Enum

    Public Sub New()
        MyBase.New
    End Sub


    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oCustomerOrChannel As DTOBaseGuid, Optional Nom As String = "")
        Dim retval As New DTOCustomerTarifaDto
        With retval
            .CustomerOrChannel = oCustomerOrChannel
            If TypeOf oCustomerOrChannel Is DTOContact Then
                .Src = Srcs.Client
            ElseIf TypeOf oCustomerOrChannel Is DTODistributionChannel Then
                .Src = Srcs.Canal
            End If
            .CustomerOrChannelNom = Nom
            .Fch = Now
        End With
        Return retval
    End Function

    Shared Function FullNom(oCustomerDto As DTOCustomerTarifaDto, Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.current.lang
        Dim sAllBrands As String = oLang.tradueix("todas las marcas", "totes les marques", "all brands")
        Dim sConcept As String = IIf(oCustomerDto Is Nothing, sAllBrands, oCustomerDto.Product.Nom)
        Dim retval As String = String.Format("{0:dd/MM/yy} {1}", oCustomerDto.Fch, sConcept)
        Return retval
    End Function

    Shared Function ProductDto(ActiveItems As List(Of DTOCustomerTarifaDto), oProduct As DTOProduct) As Decimal
        Dim retval As Decimal
        Dim oSku As DTOProductSku = Nothing
        Dim oCategory As DTOProductCategory = Nothing
        Dim oBrand As DTOProductBrand = Nothing

        Dim oDto As DTOCustomerTarifaDto = Nothing
        If TypeOf oProduct Is DTOProductSku Then
            oSku = oProduct
            oCategory = oSku.Category
            oBrand = oCategory.Brand
            oDto = ActiveItems.Find(Function(x) oSku.Equals(x.Product))
            If oDto Is Nothing Then
                oDto = ActiveItems.Find(Function(x) oCategory.Equals(x.Product))
                If oDto Is Nothing Then
                    oDto = ActiveItems.Find(Function(x) oBrand.Equals(x.Product))
                End If
            End If
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            oCategory = oProduct
            oBrand = oCategory.Brand
            oDto = ActiveItems.Find(Function(x) oCategory.Equals(x.Product))
            If oDto Is Nothing Then
                oDto = ActiveItems.Find(Function(x) oBrand.Equals(x.Product))
            End If
        ElseIf TypeOf oProduct Is DTOProductBrand Then
            oBrand = oProduct
            oDto = ActiveItems.Find(Function(x) oBrand.Equals(x.Product))
        End If

        If oDto Is Nothing Then
            oDto = ActiveItems.Find(Function(x) x.Product Is Nothing)
        End If
        If oDto IsNot Nothing Then
            retval = oDto.Dto
        End If
        Return retval
    End Function
End Class
