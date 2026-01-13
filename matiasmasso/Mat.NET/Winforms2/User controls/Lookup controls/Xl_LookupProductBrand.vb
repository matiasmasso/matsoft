Public Class Xl_LookupProductBrand
    Inherits Xl_LookupProduct

    Public Shadows Sub Load(oBrand As DTOProductBrand, Optional CustomLookup As Boolean = False)
        Dim oProducts As New List(Of DTOProduct)
        If oBrand IsNot Nothing Then oProducts.Add(oBrand)
        MyBase.Load(oProducts, DTOProduct.SelectionModes.Selectbrand, CustomLookup)
    End Sub

    Public ReadOnly Property Brand As DTOProductBrand
        Get
            Return MyBase.Product
        End Get
    End Property
End Class
