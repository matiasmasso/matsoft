Public Class Frm_Mem
    Private _Mem As DTOMem
    Private _AllowEvents As Boolean
    Private _Locked As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oMem As DTOMem)
        MyBase.New()
        Me.InitializeComponent()
        _Mem = oMem
    End Sub

    Private Sub Frm_Mem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Mem.Load(_Mem, exs) Then
            With _Mem
                DateTimePicker1.Value = .Fch
                TextBoxContact.Text = .Contact.Nom
                TextBox1.Text = .text
                Xl_DocfilesListview1.Load(.docfiles)
            End With

            If _Mem.IsNew Then
                Me.Text = "Nou Memo"
                ButtonDel.Enabled = False
                _Mem.UsrLog = DTOUsrLog2.Factory(Current.Session.User)
                Select Case Current.Session.User.Rol.id
                    Case DTORol.Ids.comercial, DTORol.Ids.rep
                        If _Mem.Cod = DTOMem.Cods.Despaitx Then
                            _Mem.Cod = DTOMem.Cods.Rep
                        End If
                    Case Else
                        _Mem.Cod = DTOMem.Cods.Despaitx
                End Select
                Xl_UsrLog1.Visible = False
            Else
                Me.Text = "Memo"
                PictureBoxLocked.Visible = _Locked
                ButtonDel.Enabled = Not _Locked
                ButtonCancel.Focus()
            End If

            Xl_UsrLog1.Load(_Mem.UsrLog)
            ComboBoxCod.SelectedIndex = _Mem.Cod

            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Mem
            .fch = DateTimePicker1.Value
            .Text = TextBox1.Text
            .cod = ComboBoxCod.SelectedIndex
            .docfiles = Xl_DocfilesListview1.docfiles
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB.Mem.Update(exs, _Mem) Then
            RaiseEvent AfterUpdate(_Mem, New MatEventArgs(_Mem))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     TextBox1.TextChanged,
      ComboBoxCod.SelectedIndexChanged,
        DateTimePicker1.ValueChanged,
         Xl_DocfilesListview1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest memo?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Mem.Delete(_Mem, exs) Then
                RaiseEvent AfterUpdate(_Mem, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

End Class
