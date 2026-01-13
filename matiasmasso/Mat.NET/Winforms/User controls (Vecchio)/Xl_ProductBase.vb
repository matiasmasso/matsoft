Public Class Xl_ProductBase
    Inherits Xl_LookupTextboxButton

    Private _Product As DTOProduct0

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Property Product() As DTOProduct0
        Get
            Return _Product
        End Get
        Set(ByVal value As DTOProduct0)
            _Product = value
            If _Product Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = BLL_Product.FullNom(_Product)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Product = Nothing
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_ProductsBase(_Product)
        AddHandler oFrm.AfterSelect, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        _Product = sender
        MyBase.Text = BLL_Product.FullNom(_Product)
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Product))
    End Sub
End Class
