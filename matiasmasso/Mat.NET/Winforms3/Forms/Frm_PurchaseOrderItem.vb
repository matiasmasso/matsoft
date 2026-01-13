Public Class Frm_PurchaseOrderItem

    Private _value As DTOPurchaseOrderItem

    Public Sub New(item As DTOPurchaseOrderItem)
        MyBase.New
        InitializeComponent()
        _value = item
    End Sub

    Private Sub Frm_PurchaseOrderItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        RadioButtonSuccess.Checked = _value.ErrCod = DTOPurchaseOrderItem.ErrCods.Success
        LoadCombo
        refresca()
    End Sub

    Private Sub LoadCombo()

    End Sub
    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles _
        RadioButtonSuccess.CheckedChanged,
         RadioButtonErr.CheckedChanged
        refresca()
    End Sub

    Private Sub refresca()
        ComboBoxErr.Visible = RadioButtonErr.Checked
        TextBoxErrDsc.Visible = RadioButtonErr.Checked
    End Sub

End Class