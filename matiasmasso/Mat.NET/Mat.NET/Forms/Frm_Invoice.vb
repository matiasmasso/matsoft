Public Class Frm_Invoice

    Private _Invoice As DTOInvoice
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOInvoice)
        MyBase.New()
        Me.InitializeComponent()
        _Invoice = value
        BLL.BLLInvoice.Load(_Invoice)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Invoice
            TextBoxFra.Text = .Num
            DateTimePicker1.Value = .Fch
            Xl_Contact21.Contact = .Customer
            Xl_InvoiceDeliveryItems1.Load(_Invoice.Deliveries)
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxFra.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Invoice
            '.Num = TextBoxFra.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLInvoice.Update(_Invoice, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Invoice))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLInvoice.Delete(_Invoice, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Invoice))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


