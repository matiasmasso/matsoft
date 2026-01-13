Public Class Frm_Projecte
    Private _Projecte As DTOProjecte
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOProjecte)
        MyBase.New()
        Me.InitializeComponent()
        _Projecte = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Projecte.Load(exs, _Projecte) Then
            With _Projecte
                TextBoxNom.Text = .Nom
                DateTimePickerFchFrom.Value = .FchFrom
                If .FchTo = Nothing Then
                    DateTimePickerFchTo.Visible = False
                Else
                    DateTimePickerFchTo.Visible = True
                    DateTimePickerFchTo.Value = .FchTo
                End If
                TextBoxDsc.Text = .Dsc
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With

            Dim items = Await FEB2.Projecte.Items(exs, _Projecte)
            If exs.Count = 0 Then
                Xl_ProjecteItems1.Load(items)
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         DateTimePickerFchFrom.ValueChanged,
          DateTimePickerFchTo.ValueChanged,
           CheckBoxTancament.CheckedChanged,
            TextBoxDsc.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Projecte
            .Nom = TextBoxNom.Text
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxTancament.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
            .Dsc = TextBoxDsc.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Projecte.Update(exs, _Projecte) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Projecte))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.Projecte.Delete(exs, _Projecte) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Projecte))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub CheckBoxTancament_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTancament.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxTancament.Checked
        End If
    End Sub
End Class


