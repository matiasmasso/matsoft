Public Class Menu_Product_Old

    Private _Product As Product

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oProduct As Product)
        MyBase.New()
        _Product = oProduct
    End Sub


    Public Function Range() As ToolStripMenuItem()
        Dim retval As ToolStripMenuItem() = Nothing
        Select Case _Product.ValueType
            Case Product.ValueTypes.Art
                Dim oArt As Art = CType(_Product.Value, Art)
                Dim oMenu_Art As New Menu_Art(oArt)
                AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
                retval = oMenu_Art.Range
            Case Product.ValueTypes.Stp
                Dim oStp As Stp = CType(_Product.Value, Stp)
                Dim oMenu_Stp As New Menu_ProductCategory(oStp)
                AddHandler oMenu_Stp.AfterUpdate, AddressOf RefreshRequest
                retval = oMenu_Stp.Range
            Case Product.ValueTypes.Tpa
                Dim oBrand As New DTOProductBrand(CType(_Product.Value, Tpa).Guid)
                Dim oMenu_Brand As New Menu_ProductBrand(oBrand)
                AddHandler oMenu_Brand.AfterUpdate, AddressOf RefreshRequest
                retval = oMenu_Brand.Range
        End Select
        Return retval
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================



    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
