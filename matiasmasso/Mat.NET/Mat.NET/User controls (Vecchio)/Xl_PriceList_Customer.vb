Public Class Xl_PriceList_Customer
    Inherits Xl_LookupTextboxButton
    Private _PriceList_Customer AS DTOPricelistCustomer

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property PriceList_Customer AS DTOPricelistCustomer
        Get
            Return _PriceList_Customer
        End Get
        Set(ByVal value AS DTOPricelistCustomer)
            _PriceList_Customer = value
            If _PriceList_Customer Is Nothing Then
                MyBase.Text = ""
            Else
                If _PriceList_Customer.Guid.Equals(Guid.Empty) Then
                    MyBase.Text = ""
                Else
                    MyBase.Text = _PriceList_Customer.Concepte
                End If
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.PriceList_Customer = Nothing
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        'Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oFrm As New Frm_PriceLists_Customers(bll.dEFAULTS.SelectionModes.Selection)
        AddHandler oFrm.OnItemSelected, AddressOf onProductSelected
        oFrm.Show()
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PriceList_Customer = e.Argument
        MyBase.Text = _PriceList_Customer.Concepte
        RaiseEvent AfterUpdate(sender, EventArgs.Empty)
    End Sub

End Class
