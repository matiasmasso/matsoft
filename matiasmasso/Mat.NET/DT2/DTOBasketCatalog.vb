Public Class DTOBasketCatalog
    Property brands As List(Of Brand)

    Public Sub New()
        _brands = New List(Of Brand)
    End Sub

    Public Class Brand
        Property Guid As Guid
        Property nom As String
        Property categories As List(Of Category)

        Public Sub New(oguid As Guid, sNom As String)
            _guid = oguid
            _nom = sNom
            _categories = New List(Of Category)
        End Sub
    End Class

    Public Class Category
        Property Guid As Guid
        Property nom As String
        Property skus As List(Of Sku)

        Public Sub New(oguid As Guid, sNom As String)
            _guid = oguid
            _nom = sNom
            _skus = New List(Of Sku)
        End Sub
    End Class

    Public Class Sku
        Property Guid As Guid
        Property nomCurt As String
        Property nomLlarg As String
        Property price As Decimal
        Property dto As Decimal
        Property moq As Integer
        Property stock As Integer
        Public Sub New(oguid As Guid, sNomCurt As String, sNomLlarg As String)
            _guid = oguid
            _nomCurt = sNomCurt
            _nomLlarg = sNomLlarg
        End Sub
    End Class
End Class
