Public Class Frm_Mem
    Private _Mem As DTOMem
    Private _AllowEvents As Boolean
    Private _Locked As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oMem As DTOMem)
        MyBase.New()
        Me.InitializeComponent()
        _Mem = oMem
        BLL.BLLMem.Load(_Mem)

        With _Mem
            DateTimePicker1.Value = .Fch
            Xl_Contact1.Contact = .Contact
            TextBox1.Text = .Text
        End With

        If _Mem.IsNew Then
            Me.Text = "Nou Memo"
            ButtonDel.Enabled = False
            _Mem.Usr = BLL.BLLSession.Current.User
            Select Case _Mem.Usr.Rol.Id
                Case Rol.Ids.Comercial, Rol.Ids.Rep
                    If _Mem.Cod = DTOMem.Cods.Despaitx Then
                        _Mem.Cod = DTOMem.Cods.Rep
                    End If
                Case Else
                    '_Mem.Cod = DTOMem.Cods.Despaitx
            End Select
        Else
            Me.Text = "Memo"
            PictureBoxLocked.Visible = _Locked
            ButtonDel.Enabled = Not _Locked
            ButtonCancel.Focus()
        End If

        Xl_User1.Load(_Mem.Usr)
        ComboBoxCod.SelectedIndex = _Mem.Cod

        _AllowEvents = True
    End Sub



    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Mem
            .Fch = DateTimePicker1.Value
            .Contact = Xl_Contact1.Contact
            .Text = TextBox1.Text
            .Cod = ComboBoxCod.SelectedIndex
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLMem.Update(_Mem, exs) Then
            RaiseEvent AfterUpdate(_Mem, New MatEventArgs(_Mem))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Contact1.AfterUpdate, _
     TextBox1.TextChanged, _
      ComboBoxCod.SelectedIndexChanged, _
        DateTimePicker1.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest memo?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLMem.Delete(_Mem, exs) Then
                RaiseEvent AfterUpdate(_Mem, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


End Class
