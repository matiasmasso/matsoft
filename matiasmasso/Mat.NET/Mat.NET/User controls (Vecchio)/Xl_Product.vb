

Public Class Xl_Product
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
        Dim oFrm As New Frm_Products_Old(mProduct)
        AddHandler oFrm.AfterSelect, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As System.EventArgs)
        mProduct = sender
        MyBase.Text = mProduct.Text
        RaiseEvent AfterUpdate(sender, EventArgs.Empty)
    End Sub

End Class
