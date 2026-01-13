Public Class Frm_Ccc
    Private _Iban As DTOIban
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oIban As DTOIban)
        MyBase.New
        InitializeComponent()
        _Iban = oIban
        If _Iban Is Nothing Then
            _Iban = New DTOIban
        Else
            Xl_LookupBankBranch1.BankBranch = _Iban.BankBranch
            TextBox1.Text = _Iban.Digits
        End If
        _AllowEvents = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        _Iban.BankBranch = Xl_LookupBankBranch1.BankBranch
        _Iban.Digits = TextBox1.Text
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_Iban))
        Me.Close()
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        Xl_LookupBankBranch1.AfterUpdate,
         TextBox1.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

End Class