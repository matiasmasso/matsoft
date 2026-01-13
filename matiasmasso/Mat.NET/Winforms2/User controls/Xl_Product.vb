

Public Class Xl_Product
    Inherits Xl_LookupTextboxButton

    Private _Product As DTOProduct

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Product() As DTOProduct
        Get
            Return _Product
        End Get
        Set(ByVal value As DTOProduct)
            _Product = value
            If _Product Is Nothing Then
                MyBase.Text = ""
            Else
                Dim exs As New List(Of Exception)
                If FEB.Product.Load(_Product, exs) Then
                    MyBase.Text = _Product.FullNom()
                End If
            End If
        End Set
    End Property


    Public Sub Clear()
        Me.Product = Nothing
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_ProductSkus(DTOProduct.SelectionModes.SelectAny, _Product)
        AddHandler oFrm.onItemSelected, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        Me.Product = e.Argument
        RaiseEvent AfterUpdate(Me, e)
    End Sub

End Class
