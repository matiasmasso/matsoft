Public Class Frm_WortenOrder
    Private _Order As DTO.Integracions.Worten.OrderClass
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTO.Integracions.Worten.OrderClass)
        MyBase.New()
        Me.InitializeComponent()
        _Order = value
    End Sub

    Private Sub Frm_Order_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Order
            TextBox_Order_Id.Text = .order_id
            TextBox_created_date.Text = .created_date
            TextBox_fullname.Text = .customer.fullname()
            Xl_WortenOrderLines1.Load(.order_lines)
        End With
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBox_Order_Id.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Order
            '.Nom = TextBox1.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        ' If Await FEB.Order.Update(exs, _Order) Then
        ' RaiseEvent AfterUpdate(Me, New MatEventArgs(_Order))
        ' Me.Close()
        ' Else
        ' UIHelper.ToggleProggressBar(PanelButtons, False)
        ' UIHelper.WarnError(exs, "error al desar")
        ' End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

End Class


