Public Class Xl_LookupProductCategory
    Inherits Xl_LookupProduct

    Public Shadows Sub Load(oCategory As DTOProductCategory, Optional CustomLookup As Boolean = False)
        Dim oProducts As New List(Of DTOProduct)
        If oCategory IsNot Nothing Then oProducts.Add(oCategory)
        MyBase.Load(oProducts, DTOProduct.SelectionModes.SelectCategory, CustomLookup)
    End Sub

    Public ReadOnly Property Category As DTOProductCategory
        Get
            Return MyBase.Product
        End Get
    End Property
End Class

