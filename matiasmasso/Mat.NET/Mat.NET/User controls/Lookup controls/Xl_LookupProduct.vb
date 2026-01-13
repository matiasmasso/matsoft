Public Class Xl_LookupProduct
    Inherits Xl_LookupTextboxButton

    Private _Product As DTOProduct
    Private _SelMode As Frm_Products.SelModes = Frm_Products.SelModes.SelectProduct

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Product() As DTOProduct
        Get
            Return _Product
        End Get
        Set(ByVal value As DTOProduct)
            _Product = value
            If _Product Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = BLL.BLLProduct.FullNom(_Product)
            End If
        End Set
    End Property

    Public Shadows Sub Load(oProduct As DTOProduct, Optional oSelMode As Frm_Products.SelModes = Frm_Products.SelModes.SelectProduct)
        _Product = oProduct
        _SelMode = oSelMode
        If _Product Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = BLL.BLLProduct.FullNom(_Product)
        End If
    End Sub

    Public Sub Clear()
        Me.Product = Nothing
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Products(_Product, _SelMode)
        AddHandler oFrm.onItemSelected, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Product = e.Argument
        MyBase.Text = BLL.BLLProduct.FullNom(_Product)
        RaiseEvent AfterUpdate(Me, e)
    End Sub


End Class
