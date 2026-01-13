Public Class Frm_CustomerPmcs
    Private _Customer As DTOCustomer

    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New
        InitializeComponent()
        BLLCustomer.Load(oCustomer)
        _Customer = BLLCustomer.CcxOrMe(oCustomer)
    End Sub

    Private Sub Frm_CustomerPmcs_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_Contact21.Contact = _Customer
        Dim items As List(Of DTODeliveryItem) = BLLDeliveryItems.All(_Customer)
        Xl_CustomerPmcs1.Load(items)
    End Sub
End Class