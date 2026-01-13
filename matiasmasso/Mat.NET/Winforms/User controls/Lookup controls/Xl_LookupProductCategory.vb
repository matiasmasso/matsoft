Public Class Xl_LookupProductCategory
    Inherits Xl_LookupProduct

    Public Shadows Sub Load(oCategory As DTOProductCategory, Optional CustomLookup As Boolean = False)
        MyBase.Load(oCategory, DTOProduct.SelectionModes.SelectCategory, CustomLookup)
    End Sub

    Public ReadOnly Property Category As DTOProductCategory
        Get
            Return MyBase.Product
        End Get
    End Property
End Class

