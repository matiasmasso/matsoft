

Public Class Xl_Lookup_Product_Old
    Inherits Xl_LookupTextboxButton

    Private mProduct As Product

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Product() As Product
        Get
            Return mProduct
        End Get
        Set(ByVal value As Product)
            mProduct = value
            If mProduct Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = mProduct.Text
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Product = Nothing
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Arts(Frm_Arts.SelModes.SelectProduct)
        AddHandler oFrm.AfterSelect, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        mProduct = e.Argument
        MyBase.Text = mProduct.Text
        RaiseEvent AfterUpdate(sender, EventArgs.Empty)
    End Sub

End Class
