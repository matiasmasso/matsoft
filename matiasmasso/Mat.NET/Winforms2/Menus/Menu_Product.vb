Public Class Menu_Product
    Inherits Menu_Base

    Private _Products As List(Of DTOProduct)
    Private _Product As DTOProduct


    Public Sub New(ByVal oProducts As List(Of DTOProduct))
        MyBase.New()
        _Products = oProducts
        If _Products IsNot Nothing Then
            If _Products.Count > 0 Then
                _Product = _Products.First
            End If
        End If
    End Sub

    Public Sub New(ByVal oProduct As DTOProduct)
        MyBase.New()
        _Product = oProduct
        _Products = New List(Of DTOProduct)
        If _Product IsNot Nothing Then
            _Products.Add(_Product)
        End If
    End Sub

    Public Shadows Function Range() As ToolStripMenuItem()
        Dim oMenu As Menu_Base = Nothing
        Dim retval As ToolStripMenuItem() = Nothing
        If TypeOf _Product Is DTOProductBrand Then
            oMenu = New Menu_ProductBrand(_Product)
        ElseIf TypeOf _Product Is DTOProductCategory Then
            oMenu = New Menu_ProductCategory(_Product)
        ElseIf TypeOf _Product Is DTOProductSku Then
            oMenu = New Menu_ProductSku(_Product)
        ElseIf _Product.SourceCod = DTOProduct.SourceCods.Brand Then
            _Product = New DTOProductBrand(_Product.Guid)
            oMenu = New Menu_ProductBrand(_Product)
        ElseIf _Product.SourceCod = DTOProduct.SourceCods.Category Then
            _Product = New DTOProductCategory(_Product.Guid)
            oMenu = New Menu_ProductCategory(_Product)
        ElseIf _Product.SourceCod = DTOProduct.SourceCods.Sku Then
            _Product = New DTOProductSku(_Product.Guid)
            oMenu = New Menu_ProductSku(_Product)
        End If
        If oMenu IsNot Nothing Then
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            retval = oMenu.Range
        End If
        Return retval
    End Function


End Class

