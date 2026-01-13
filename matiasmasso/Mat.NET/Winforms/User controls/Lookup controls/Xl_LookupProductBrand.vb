Public Class Xl_LookupProductBrand
    Inherits Xl_LookupProduct

    Public Shadows Sub Load(oBrand As DTOProductBrand, Optional CustomLookup As Boolean = False)
        MyBase.Load(oBrand, DTOProduct.SelectionModes.SelectBrand, CustomLookup)
    End Sub

    Public ReadOnly Property Brand As DTOProductBrand
        Get
            Return MyBase.Product
        End Get
    End Property
End Class
