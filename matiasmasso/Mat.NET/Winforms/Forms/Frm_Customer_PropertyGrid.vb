Public Class Frm_Customer_PropertyGrid
    Private _value As DTOCustomer

    Public Sub New(value As DTOCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _value = value
    End Sub

    Private Sub Frm_Customer_PropertyGrid_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_PropertyGrid_Customer1.Load(_value)
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        _value = Xl_PropertyGrid_Customer1.Value
        Stop
    End Sub
End Class